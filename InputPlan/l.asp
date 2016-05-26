<%@ Language=VBScript%>
<%Option Explicit%>
<%Response.Expires=0%>

<!-- #include virtual="/institute/include/dbcon.inc" -->
<!-- #include virtual="/institute/include/SSODecrypt.asp" -->

<%
	Dim cktrp
	cktrp = Request("cktrp")	'//S : G/W 제안 및 건의사항 요청 건수 클릭 시

	Dim user_id, dept_code, BSC_RIGHT, sabun, author, user_id1
	Dim szSql, Rs


	Dim strWSURL
	Dim strParam

	Dim strHKMCENC_ID
	Dim strCompanyCode
	Dim strEncCode
	Dim strReturn
	Dim User_ID_Start
	Dim User_ID_End
	Dim User_Name_Start
	Dim User_ID_Len


	strWSURL = "http://autoever.hyundai.net/Webservices/SSO/SitemapWS.asmx"
	strEncCode = Request("Encode")						' 그룹웨어에서 넘어 온 값
	strCompanyCode = Request("CompanyCode")					' 그룹웨에에서 넘어 온 값
	strHKMCENC_ID = "SINMUNGO"


	strParam = "<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><GetPlainText xmlns=""http://tempuri.org/"">"

	strParam = strParam & "<strEncID>" & strHKMCENC_ID & "</strEncID>"
	strParam = strParam & "<strCompanyCode>" & strCompanyCode & "</strCompanyCode>"
	strParam = strParam & "<strEncText>" & strEncCode & "</strEncText>"
	strParam = strParam & "</GetPlainText></soap:Body></soap:Envelope>"

	strReturn = CallWebService( strWSURL, strParam )

	User_ID_Start = instr(strReturn,"User_ID")

	'--User_Name_Start = instr(strReturn,"User_Name")


	strReturn = Mid(strReturn,User_ID_Start,100)

	User_ID_Start = instr(strReturn,"User_ID")
	User_ID_End = instr(strReturn,"___")

	User_ID_Len = User_ID_End - (User_ID_Start + 9)

	user_id = Mid(strReturn,User_ID_Start+9,User_ID_Len)



	Function CallWebService(url, param)

	    Dim result
	    Dim xmlhttp
    
	    Set xmlhttp = Server.CreateObject("MSXML2.ServerXMLHTTP")
    
	    xmlhttp.Open "POST", url, false
	    xmlhttp.setRequestHeader "Content-Type", "text/xml; charset=utf-8"
	    xmlhttp.setRequestHeader "SOAPAction", "http://tempuri.org/GetPlainText"
	    xmlhttp.send param 
	    result = xmlhttp.responseText
	    set xmlhttp = nothing    
           
	    CallWebService = result

	End Function 

	'user_id = Request("user_id")
		
	If user_id = "" Then
		Response.Write "<script>"
		Response.Write "alert('[SSO Error]사용자 정보가 없어 브라우저를 종료합니다!');"
		Response.Write "self.opener=self;"
		Response.Write "self.close();"
		Response.Write "</script>"
		Response.End
	Else
		If Left(user_id, 3) <> "Q03" Then
			user_id = Mid(strReturn,User_ID_Start+9,User_ID_Len)
		End If
				
		Call ConnectDB()

		Set Rs = Server.CreateObject("ADODB.RecordSet")
		
		
		szSql = "SELECT USER_NAME, POS_NAME, DEPT_CODE, DEPT_NAME FROM USERINFO WHERE USER_ID='" & user_id & "'"
		Rs.Open szSql, dbcon, 3, 1



		If Rs.Eof = True Then
			Rs.Close
			Set Rs = Nothing

			Call CloseDB()

			Response.Write "<script>"
			Response.Write "alert('[USER Error]사용자 정보가 없어 브라우저를 종료합니다!');"
			Response.Write "self.opener=self;"
			Response.Write "self.close();"
			Response.Write "</script>"
			Response.End
		Else
			Response.Cookies("seUSER_ID")	= user_id
			Response.Cookies("seUSER_NAME")	= Rs("USER_NAME")
			Response.Cookies("sePOS_NAME")	= Rs("POS_NAME")

			dept_code						= Rs("DEPT_CODE")
			Response.Cookies("seDEPT_CODE") = dept_code
			Response.Cookies("seDEPT_NAME") = Rs("DEPT_NAME")

			sabun							= Replace(user_id, "Q03", "")
			Response.Cookies("seSABUN")		= sabun

			Rs.Close




			'//정보시스템팀 이대현BJ, 이동한DR, 이태엽GJ, 김종립GJ, 최동신GJ, 이왕기CJ
'2013.04.10			
'			If user_id <> "Q03214212" And user_id <> "Q03242661" And user_id <> "Q03220886" And user_id <> "Q03244376" And user_id <> "Q03261056" And user_id <> "Q03257645" Then
'				szSql = "SELECT TOP 1 1 FROM RIMS_DEPT "
'				szSql = szSql & "WHERE DEPT_CODE=(SELECT DEPT_CODE FROM USERINFO WHERE USER_ID='" & user_id & "')"
'
''				szSql = "SELECT TOP 1 1 FROM RAMINQU WHERE PLACECD2='F' AND PEMPNO='" & Replace(user_id, "Q03", "") & "'"
'				Rs.Open szSql, dbcon, 3, 1
'				If Rs.Eof = True Then
'					Rs.Close
'					Set Rs = Nothing
'
'					Call CloseDB()
'
'					Response.Write "<script>"
'					Response.Write "alert('[사용권한없음]기술연구소 소속만 사용 가능합니다!');"
'					Response.Write "self.opener=self;"
'					Response.Write "self.close();"
'					Response.Write "</script>"
'					Response.End
'				End If
'				Rs.Close
'			End If


			
			'//----------------------------------------------------------------------------------------------------------------------
			'//---성과 평가 관리 권한 체크 시작
			'//권한(R:본인 소속 부서(하위포함) 조회, W:성과 평가 확정(팀장), W1:성과 평가 등록(팀원))
			BSC_RIGHT = ""

			szSql = "SELECT TOP 1 1 FROM RAMINQU WHERE PEMPNO='" & sabun & "' AND POSTCD BETWEEN 'BA' AND 'FZ'"
			Rs.Open szSql, dbcon, 3, 1
			If Rs.Eof = True Then
				BSC_RIGHT = ""
			End If
			Rs.Close




			
			'//본인 소속 부서(하위포함) 조회
			'//: 연구소장 및 담당중역(단, 팀장 겸직자는 제외 : 예)최종묵 부장(전장신호연구담당,전장품개발팀))
			szSql = "SELECT TOP 1 1 FROM BSC_RIGHT WHERE USER_ID='" & user_id & "'"
			Rs.Open szSql, dbcon, 3, 1

			If Rs.Eof = False Then
				BSC_RIGHT = "R"
			End If
			Rs.Close


			'//성과 평가 등록 권한 체크 : 팀장 유무 체크
'			szSql = "SELECT TOP 1 1 FROM BSC_DEPT_USER A, BSC_DEPT_SECTION B"
'			szSql = szSql & " WHERE A.DEPT_CODE=B.DEPT_CODE AND A.USER_ID='" & user_id & "'"
			
'			szSql = "SELECT A.USER_ID FROM BSC_DEPT_USER A, USERINFO B WHERE A.DEPT_CODE=B.DEPT_CODE AND B.USER_ID='" & user_id & "'"

			szSql = "SELECT A.USER_ID FROM BSC_DEPT_USER A, USERINFO B WHERE A.DEPT_CODE=B.DEPT_CODE AND B.USER_ID='" & user_id & "'"
			szSql = szSql & " UNION"
			szSql = szSql & " SELECT A.USER_ID FROM BSC_DEPT_USER A, DEPTINFO B WHERE A.DEPT_CODE=B.DEPT_CODE AND B.MANAGER_ID='" & user_id & "'"
			Rs.Open szSql, dbcon, 3, 1

			If Rs.Eof = False Then
				user_id1 = Rs(0)			'//팀장 사원번호

				If user_id = user_id1 Then
					BSC_RIGHT = "W"			'//팀장
				Else
					BSC_RIGHT = "W1"		'//팀원
				End If
			End If
			Rs.Close

				

					
			Response.Cookies("seBSC_RIGHT") = BSC_RIGHT
			

			'//관리자인 경우(이대현BJ, 이동한DR, 최성찬CJ, 최동환SW, 이태엽GJ, 김종립GJ, 이왕기CJ, 이민호SJ)
'2013.04.10			
'			If user_id = "Q03214212" Or user_id = "Q03242661" Or user_id = "Q03217301"  Or user_id = "Q03258125" Or user_id = "Q03220886" Or user_id = "Q03244376" Or user_id = "Q03257645"  Then
'				Response.Cookies("seBSC_IS_ADMIN") = "Y"
'			Else
				Response.Cookies("seBSC_IS_ADMIN") = "N"
'			End If
			'//---성과 평가 관리 권한 체크 끝
			'//----------------------------------------------------------------------------------------------------------------------


			'//---------------------------------------------------------------------------------------------------
			'//---회의 관리 시스템 권한 체크 시작
			author = ""

			szSql = "SELECT ISNULL(AUTHOR,'') AS AUTHOR FROM DIUSER WHERE USERID='" & sabun & "'"
			
			Rs.Open szSql, dbcon, 3, 1
			If Rs.Eof = False Then
				author = Rs("AUTHOR")
			End If

			Rs.Close
			Set Rs = Nothing

			Call CloseDB()
			
			Response.Cookies("seAUTHOR") = author
			'//---회의 관리 시스템 권한 체크 끝
			'//---------------------------------------------------------------------------------------------------


			'//---G/W 결재함에서 연구소제안건의요청 클릭 시(값 : S)
			Response.Cookies("secktrp") = cktrp


			Response.Redirect "index.asp"



		End If
	End If
%>