
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

-- TABLE �������� �������� GET...
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
    OBJECT_ID   NUMBER NOT NULL,    -- KEY �� SEQUENCE���� �����´�
    CREATE_DATE VARCHAR2(50),       -- ������.. ����ũ..
    CREATOR_ID  VARCHAR2(50),       -- ������.. ����ũ..
    UPDATE_DATE VARCHAR2(50),       -- ����������.. 
    UPDATOR_ID  VARCHAR2(50),       -- ����������..
    MASTER_ID   NUMBER,             -- MASTER_ID.. ���ʹ����� OBJECT_ID�� ����
    VERSION     NUMBER,             -- ���� ����Ǹ鼭 �����̷� ������ ���� ����..
    OWNER       NUMBER,             -- ����ó���� ���Ѱ�.. ���� ��� ��������� �𸣰ڴ�..
    STATUS      VARCHAR2(50),       -- ����.. ����.. �� ��Ÿ ����� ���� ó��.. ���� MASTER_ID���� ����
    TITLE       VARCHAR2(200),      -- ������Ī    
    CODE        VARCHAR2(50),       -- �����ڵ�
    INPUT_TYPE  VARCHAR2(50),       -- �Է����� -> P : ��ȹ, S : ����
    UNIT        VARCHAR2(50),       -- ���� : ...... NOT DEFINED ...
    DIVISION    VARCHAR2(50),       -- ����� : -> R : ö��, T : �߱�, P : �÷�Ʈ, C : ����
    YEAR        VARCHAR2(5),        -- ���س⵵
    MONTH       VARCHAR2(2),        -- ���ؿ�
    VAL_YEAR_01 VARCHAR2(10),       -- �Ⱓ ��ǥ or ����
    VAL_HALF_01 VARCHAR2(10),       -- �ݱ�
    VAL_HALF_02 VARCHAR2(10),       -- �ݱ�
    VAL_QTR_01  VARCHAR2(10),       -- �б�
    VAL_QTR_02  VARCHAR2(10),       -- �б�
    VAL_QTR_03  VARCHAR2(10),       -- �б�
    VAL_QTR_04  VARCHAR2(10),       -- �б�
    VAL_MONTH_01 VARCHAR2(10),       -- ����
    VAL_MONTH_02 VARCHAR2(10),       -- ����
    VAL_MONTH_03 VARCHAR2(10),       -- ����
    VAL_MONTH_04 VARCHAR2(10),       -- ����
    VAL_MONTH_05 VARCHAR2(10),       -- ����
    VAL_MONTH_06 VARCHAR2(10),       -- ����
    VAL_MONTH_07 VARCHAR2(10),       -- ����
    VAL_MONTH_08 VARCHAR2(10),       -- ����
    VAL_MONTH_09 VARCHAR2(10),       -- ����
    VAL_MONTH_10 VARCHAR2(10),       -- ����
    VAL_MONTH_11 VARCHAR2(10),       -- ����
    VAL_MONTH_12 VARCHAR2(10),       -- ����
    VAL_WEEK_01 VARCHAR2(10),       -- �ְ�
    VAL_WEEK_02 VARCHAR2(10),       -- �ְ�
    VAL_WEEK_03 VARCHAR2(10),       -- �ְ�
    VAL_WEEK_04 VARCHAR2(10),       -- �ְ�
    VAL_WEEK_05 VARCHAR2(10),       -- �ְ�
    VAL_DAY_01 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_02 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_03 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_04 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_05 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_06 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_07 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_08 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_09 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_10 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_11 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_12 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_13 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_14 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_15 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_16 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_17 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_18 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_19 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_20 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_21 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_22 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_23 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_24 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_25 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_26 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_27 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_28 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_29 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_30 VARCHAR2(10),       -- �ϰ�
    VAL_DAY_31 VARCHAR2(10),       -- �ϰ�
    ATTR1       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR2       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR3       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...    
    ATTR4       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR5       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR6       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR7       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR8       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    ATTR9       VARCHAR2(50),       -- �߰� �Ӽ�1 -- NOT DEFINED ...
    CATE_PCODE	NUMBER,				-- ���� �з� �ڵ� ( TB_OBJECT_CLASSIFICATION �� OBJECT_ID )
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
    


-- ����� ������Ʈ�� ���ν���..
-- ����� ������Ʈ�� �Ű� ���� ���ؼ� ���ν����� ������ְ�..
-- ���߿� ��ġ�� ���ؼ�.. ó���� �ϵ��� ����..
    
    


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
 


-- ��Ȯ������ ������.. 
-- �ϴ��� �������� �ɾ��ش�..
-- ������ ����Ŭ 11g������ JOB�� �ְ� SCHEDULER JOB�� ���� �ִ°� ������..
-- �׳� �����ϰ� JOB���ٰ��� �ɾ��ֱ�� �Ѵ�.


DECLARE
    jobno number;
BEGIN
    DBMS_JOB.SUBMIT(jobno,
        what => 'EDASHBOARD.EDASHBOARD_UPDATEUSER;',
        next_date => TO_DATE('2016/03/29 00:00:00','YYYY/MM/DD HH24:MI:SS'),
        interval => 'TRUNC(SYSDATE+1)');
END;

/*
������ �𸣰�����.. ORANGE�� �۾���..
�۾��� ��������� ����/������ �ȵȴ� ������ �߻��Ͽ�����..
(�������� �ʴ´�..)
�׳� ������ ������ �ٽ� �ϴϱ� ���������� �ȴ�..
�ϴ��� ����� �� 65�� ���� ����� ���.. 
*/

