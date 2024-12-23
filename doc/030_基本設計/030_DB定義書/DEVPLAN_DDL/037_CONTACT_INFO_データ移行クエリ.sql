--�S�̘A����ݒ�
INSERT
INTO CONTACT_INFO(ID, GENERAL_CODE, PERSONEL_ID, TEL, STATUS, CATEGORY_ID, REMARKS, FUNCTION_ID)
VALUES(1,NULL,NULL,NULL,'�S',NULL, 'GJ1 �ԗ����Q�S��(8-22-2509)', NULL);

--�����ԓ���
INSERT 
INTO CONTACT_INFO(ID, GENERAL_CODE, PERSONEL_ID, TEL, STATUS, CATEGORY_ID, REMARKS, FUNCTION_ID) 
SELECT
  (SELECT MAX(ID) FROM CONTACT_INFO) + ROW_NUMBER() OVER (ORDER BY ID)
  , �����v��_�O���ԓ���_�Ǘ���.GENERAL_CODE
  , �����v��_�O���ԓ���_�Ǘ���.PERSONEL_ID
  , �����v��_�O���ԓ���_�Ǘ���.TEL
  , �����v��_�O���ԓ���_�Ǘ���.����
  , NULL
  , NULL
  , '11' AS A
FROM
  �����v��_�O���ԓ���_�Ǘ���
WHERE
  ���� IN ('��', '��') 
  AND GENERAL_CODE IS NOT NULL;

--�J�[�V�F�A����
INSERT 
INTO CONTACT_INFO(ID, GENERAL_CODE, PERSONEL_ID, TEL, STATUS, CATEGORY_ID, REMARKS, FUNCTION_ID) 
SELECT
  (SELECT MAX(ID) FROM CONTACT_INFO) + ROW_NUMBER() OVER (ORDER BY ID)
  , �����v��_�O���ԓ���_�Ǘ���.GENERAL_CODE
  , �����v��_�O���ԓ���_�Ǘ���.PERSONEL_ID
  , �����v��_�O���ԓ���_�Ǘ���.TEL
  , �����v��_�O���ԓ���_�Ǘ���.����
  , NULL
  , NULL
  , '12'  AS A
FROM
  �����v��_�O���ԓ���_�Ǘ��� 
  LEFT JOIN ( 
    SELECT
      CAR_GROUP 
    FROM
      GENERAL_CODE 
    GROUP BY
      CAR_GROUP
  ) CAR 
    ON �����v��_�O���ԓ���_�Ǘ���.GENERAL_CODE = CAR.CAR_GROUP 
WHERE
  CAR.CAR_GROUP IS NOT NULL 
  AND ���� IN ('��', '��') 
  AND GENERAL_CODE IS NOT NULL;

--�O���ԓ���
INSERT 
INTO CONTACT_INFO(ID, GENERAL_CODE, PERSONEL_ID, TEL, STATUS, CATEGORY_ID, REMARKS, FUNCTION_ID) 
SELECT
  (SELECT MAX(ID) FROM CONTACT_INFO) + ROW_NUMBER() OVER (ORDER BY ID)
  , �����v��_�O���ԓ���_�Ǘ���.GENERAL_CODE
  , �����v��_�O���ԓ���_�Ǘ���.PERSONEL_ID
  , �����v��_�O���ԓ���_�Ǘ���.TEL
  , �����v��_�O���ԓ���_�Ǘ���.����
  , NULL
  , NULL
  , '13'  AS A
FROM
  �����v��_�O���ԓ���_�Ǘ��� 
  LEFT JOIN ( 
    SELECT
      CAR_GROUP 
    FROM
      GENERAL_CODE 
    GROUP BY
      CAR_GROUP
  ) CAR 
    ON �����v��_�O���ԓ���_�Ǘ���.GENERAL_CODE = CAR.CAR_GROUP 
WHERE
  CAR.CAR_GROUP IS NOT NULL 
  AND ���� IN ('��', '��') 
  AND GENERAL_CODE IS NOT NULL;

COMMIT;
