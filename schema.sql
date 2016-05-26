
drop  SEQUENCE SQ_PERSON;
drop SEQUENCE SQ_DEPT;

drop table tb_person;
drop table tb_dept;

CREATE SEQUENCE SQ_PERSON;
CREATE SEQUENCE SQ_DEPT;

CREATE TABLE TB_PERSON 
(   OBJECT_ID INT NOT NULL, 
    PERSON_NAME VARCHAR(50),
    USER_ID     VARCHAR(50),
    DEPT_CODE   VARCHAR(50),
    EMAIL       VARCHAR(100),
    TELEPHONE   VARCHAR(50),
    MOBILE      VARCHAR(50),
    POS_NAME    VARCHAR(50),
    STATUS      VARCHAR(10) );
    
ALTER TABLE TB_PERSON 
ADD CONSTRAINT TB_PERSON PRIMARY KEY 
(
  OBJECT_ID 
);    

CREATE TABLE TB_DEPT
(   OBJECT_ID INT NOT NULL, 
    DEPT_CODE   VARCHAR(50),
    DEPT_NAME   VARCHAR(50),
    UPPER_DEPT_CODE   VARCHAR(50),
    MANAGER_ID  VARCHAR(50),
    IFRESULT    VARCHAR(50) );
    
ALTER TABLE TB_DEPT
ADD CONSTRAINT TB_DEPT PRIMARY KEY 
(
  OBJECT_ID 
);    

-- TABLE 생성이후 계정정보 GET...
--insert into tb_person  ( select sq_person.nextval, a.* from enovia.hr26_gw a );
--insert into tb_dept ( select sq_dept.nextval, a.* from enovia.hr25_gw a );

--select * from tb_person;
--select * from tb_dept;


CREATE SEQUENCE SQ_OBJECT_CLASSIFICATION;

CREATE TABLE TB_OBJECT_CLASSIFICATION
(   OBJECT_ID INT NOT NULL,
    CREATE_DATE     VARCHAR(50), 
    CREATOR_ID      VARCHAR(50),
    UPDATE_DATE     VARCHAR(50), 
    UPDATOR_ID      VARCHAR(50),
    CLASSIFICATION_CODE VARCHAR(200),
    CATE_NAME       VARCHAR(200),
    CATE_ORDER      VARCHAR(200),
    CATE_COMMENT    VARCHAR(200),
    CATE_STATE      VARCHAR(200),
    DESCR           VARCHAR(200)
);

ALTER TABLE TB_OBJECT_CLASSIFICATION 
ADD CONSTRAINT TB_OBJECT_CLASSIFICATION PRIMARY KEY 
(
  OBJECT_ID 
);


CREATE SEQUENCE SQ_OBJECT;


CREATE TABLE EDASHBOARD.TB_OBJECT
(
    OBJECT_ID   NUMBER NOT NULL,    -- KEY 값 SEQUENCE에서 가져온다
    CREATE_DATE VARCHAR2(50),       -- 생성일.. 유니크..
    CREATOR_ID  VARCHAR2(50),       -- 생성자.. 유니크..
    UPDATE_DATE VARCHAR2(50),       -- 최종수정일.. 
    UPDATOR_ID  VARCHAR2(50),       -- 최종수정자..
    MASTER_ID   NUMBER,             -- MASTER_ID.. 최초버전의 OBJECT_ID와 동일
    VERSION     NUMBER,             -- 값이 변경되면서 변경이력 관리를 위한 버전..
    OWNER       NUMBER,             -- 권한처리를 위한값.. 아직 어떻게 사용할지는 모르겠다..
    STATUS      VARCHAR2(50),       -- 상태.. 삭제.. 및 기타 등등을 위한 처리.. 같은 MASTER_ID기준 동일
    TITLE       VARCHAR2(200),      -- 관리명칭    
    CODE        VARCHAR2(50),       -- 관리코드
    INPUT_TYPE  VARCHAR2(50),       -- 입력유형 -> P : 계획, S : 실적
    UNIT        VARCHAR2(50),       -- 단위 : ...... NOT DEFINED ...
    DIVISION    VARCHAR2(50),       -- 사업부 : -> R : 철차, T : 중기, P : 플랜트, C : 공통
    YEAR        VARCHAR2(5),        -- 기준년도
    MONTH       VARCHAR2(2),        -- 기준월
    VAL_YEAR_01 VARCHAR2(10),       -- 년간 목표 or 실적
    VAL_HALF_01 VARCHAR2(10),       -- 반기
    VAL_HALF_02 VARCHAR2(10),       -- 반기
    VAL_QTR_01  VARCHAR2(10),       -- 분기
    VAL_QTR_02  VARCHAR2(10),       -- 분기
    VAL_QTR_03  VARCHAR2(10),       -- 분기
    VAL_QTR_04  VARCHAR2(10),       -- 분기
    VAL_MONTH_01 VARCHAR2(10),       -- 월간
    VAL_MONTH_02 VARCHAR2(10),       -- 월간
    VAL_MONTH_03 VARCHAR2(10),       -- 월간
    VAL_MONTH_04 VARCHAR2(10),       -- 월간
    VAL_MONTH_05 VARCHAR2(10),       -- 월간
    VAL_MONTH_06 VARCHAR2(10),       -- 월간
    VAL_MONTH_07 VARCHAR2(10),       -- 월간
    VAL_MONTH_08 VARCHAR2(10),       -- 월간
    VAL_MONTH_09 VARCHAR2(10),       -- 월간
    VAL_MONTH_10 VARCHAR2(10),       -- 월간
    VAL_MONTH_11 VARCHAR2(10),       -- 월간
    VAL_MONTH_12 VARCHAR2(10),       -- 월간
    VAL_WEEK_01 VARCHAR2(10),       -- 주간
    VAL_WEEK_02 VARCHAR2(10),       -- 주간
    VAL_WEEK_03 VARCHAR2(10),       -- 주간
    VAL_WEEK_04 VARCHAR2(10),       -- 주간
    VAL_WEEK_05 VARCHAR2(10),       -- 주간
    VAL_DAY_01 VARCHAR2(10),       -- 일간
    VAL_DAY_02 VARCHAR2(10),       -- 일간
    VAL_DAY_03 VARCHAR2(10),       -- 일간
    VAL_DAY_04 VARCHAR2(10),       -- 일간
    VAL_DAY_05 VARCHAR2(10),       -- 일간
    VAL_DAY_06 VARCHAR2(10),       -- 일간
    VAL_DAY_07 VARCHAR2(10),       -- 일간
    VAL_DAY_08 VARCHAR2(10),       -- 일간
    VAL_DAY_09 VARCHAR2(10),       -- 일간
    VAL_DAY_10 VARCHAR2(10),       -- 일간
    VAL_DAY_11 VARCHAR2(10),       -- 일간
    VAL_DAY_12 VARCHAR2(10),       -- 일간
    VAL_DAY_13 VARCHAR2(10),       -- 일간
    VAL_DAY_14 VARCHAR2(10),       -- 일간
    VAL_DAY_15 VARCHAR2(10),       -- 일간
    VAL_DAY_16 VARCHAR2(10),       -- 일간
    VAL_DAY_17 VARCHAR2(10),       -- 일간
    VAL_DAY_18 VARCHAR2(10),       -- 일간
    VAL_DAY_19 VARCHAR2(10),       -- 일간
    VAL_DAY_20 VARCHAR2(10),       -- 일간
    VAL_DAY_21 VARCHAR2(10),       -- 일간
    VAL_DAY_22 VARCHAR2(10),       -- 일간
    VAL_DAY_23 VARCHAR2(10),       -- 일간
    VAL_DAY_24 VARCHAR2(10),       -- 일간
    VAL_DAY_25 VARCHAR2(10),       -- 일간
    VAL_DAY_26 VARCHAR2(10),       -- 일간
    VAL_DAY_27 VARCHAR2(10),       -- 일간
    VAL_DAY_28 VARCHAR2(10),       -- 일간
    VAL_DAY_29 VARCHAR2(10),       -- 일간
    VAL_DAY_30 VARCHAR2(10),       -- 일간
    VAL_DAY_31 VARCHAR2(10),       -- 일간
    ATTR1       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR2       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR3       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...    
    ATTR4       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR5       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR6       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR7       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR8       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    ATTR9       VARCHAR2(50),       -- 추가 속성1 -- NOT DEFINED ...
    CATE_PCODE	NUMBER,				-- 산위 분류 코드 ( TB_OBJECT_CLASSIFICATION 의 OBJECT_ID )
    DESCR       VARCHAR2(200)
)

ALTER TABLE TB_OBJECT
ADD CONSTRAINT TB_OBJECT PRIMARY KEY 
(
  OBJECT_ID 
);




CREATE TABLE TB_CODE5
(   OBJECT_ID INT NOT NULL, 
    CODE      VARCHAR2(20),
    NAME      VARCHAR2(200)
);

ALTER TABLE TB_CODE5
ADD CONSTRAINT TB_CODE5 PRIMARY KEY 
(
  OBJECT_ID 
);    
    


-- 사용자 업데이트용 프로시져..
-- 사용자 업데이트를 신경 끄기 위해서 프로시저로 만들어주고..
-- 나중에 배치를 통해서.. 처리를 하도록 하자..
    
    


 CREATE
    OR REPLACE PROCEDURE EDASHBOARD_UPDATEUSER IS BEGIN
INSERT

    

  INTO TB_PERSON
SELECT SQ_PERSON.NEXTVAL,
       USER_NAME PERSON_NAME,
       USER_ID,
       DEPT_CODE,
       EMAIL,
       TELEPHONE,
       MOBILE,
       POS_NAME,
       IFRESULT AS STATUS
  FROM ENOVIA.HR26_GW
 WHERE USER_ID NOT IN (SELECT USER_ID
          FROM TB_PERSON ) ;
 
UPDATE TB_PERSON A
   SET DEPT_CODE = (SELECT DEPT_CODE
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID ),
       EMAIL = (SELECT EMAIL
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID ),
       TELEPHONE = (SELECT TELEPHONE
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID ),
       MOBILE = (SELECT MOBILE
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID ),
       POS_NAME = (SELECT POS_NAME
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID ),
       STATUS = (SELECT STATUS
          FROM ENOVIA.HR26_GW B
         WHERE A.USER_ID= B.USER_ID );
 
INSERT
  INTO TB_DEPT
SELECT SQ_DEPT.NEXTVAL,
       DEPT_CODE,
       DEPT_NAME,
       UPPER_DEPT_CODE,
       MANAGER_ID,
       IFRESULT
  FROM ENOVIA.HR25_GW
 WHERE DEPT_CODE NOT IN (SELECT DEPT_CODE
          FROM TB_DEPT ) ;
 
UPDATE TB_DEPT A
   SET DEPT_CODE = NVL( (SELECT DEPT_CODE
                  FROM ENOVIA.HR25_GW B
                 WHERE A.DEPT_CODE = B.DEPT_CODE ), '' ),
       DEPT_NAME = NVL( (SELECT DEPT_NAME
                  FROM ENOVIA.HR25_GW B
                 WHERE A.DEPT_CODE = B.DEPT_CODE ), '' ),
       UPPER_DEPT_CODE = NVL( (SELECT UPPER_DEPT_CODE
                  FROM ENOVIA.HR25_GW B
                 WHERE A.DEPT_CODE = B.DEPT_CODE ), '' ),
       MANAGER_ID = NVL( (SELECT MANAGER_ID
                  FROM ENOVIA.HR25_GW B
                 WHERE A.DEPT_CODE = B.DEPT_CODE ), '' ),
       IFRESULT = NVL( (SELECT IFRESULT
                  FROM ENOVIA.HR25_GW B
                 WHERE A.DEPT_CODE = B.DEPT_CODE ), '' );
                 
commit;
 
END ;
 


-- 정확하지는 않지만.. 
-- 일단은 스케줄을 걸어준다..
-- 원래는 오라클 11g에서는 JOB이 있고 SCHEDULER JOB이 따로 있는것 같은데..
-- 그냥 무시하고 JOB에다가만 걸어주기로 한다.


DECLARE
    jobno number;
BEGIN
    DBMS_JOB.SUBMIT(jobno,
        what => 'EDASHBOARD.EDASHBOARD_UPDATEUSER;',
        next_date => TO_DATE('2016/03/29 00:00:00','YYYY/MM/DD HH24:MI:SS'),
        interval => 'TRUNC(SYSDATE+1)');
END;

/*
원인은 모르겠지만.. ORANGE로 작업중..
작업이 만들어지고 수정/삭제가 안된느 현상이 발생하였느데..
(보이지는 않는다..)
그냥 오렌지 종료후 다시 하니까 정상적으로 된다..
일단은 운영서바 상에 65번 으로 잡업을 등록.. 
*/

