INSERT INTO 
 �g�p�������(
  �f�[�^ID
  , ����NO
  , SEQNO
  , ���F�v���R�[�h
  , STEPNO
  , ���F��
  , ���F�҃��x��
  , �Ǘ��������F
  , ������
  , �Ǘ��ӔC�ۖ�
  , �Ǘ��ӔC������
  , �g�p�ۖ�
  , �g�p������
  , �������e
  , �H���敪NO
  , �����s����
  , �ҏW��
  , �ҏW��
  , �ڊǐ敔��ID
  , ���ԏ�ԍ�
 )
SELECT
  A.�f�[�^ID �f�[�^ID
  , B.����NO ����NO
  , '1' SEQNO
  , 'A' ���F�v���R�[�h
  , '0' STEPNO
  , '��' ���F��
  , NULL ���F�҃��x��
  , NULL �Ǘ��������F
  , B.���s�N���� ������
  , G.SECTION_CODE �Ǘ��ӔC�ۖ�
  , F.SECTION_GROUP_CODE �Ǘ��ӔC������
  , E.SECTION_CODE �g�p�ۖ�
  , D.SECTION_GROUP_CODE �g�p������
  , '���' �������e
  , B.�H���敪NO �H���敪NO
  , B.��̎����s���� �����s����
  , SYSDATE �ҏW��
  , '000001' �ҏW��
  , NULL �ڊǐ敔��ID
  , NULL ���ԏ�ԍ� 
FROM
  ( 
    select
      main.�f�[�^ID 
    from
      �����Ԋ�{��� main 
      LEFT JOIN �g�p������� SUB 
        ON main.�f�[�^ID = SUB.�f�[�^ID 
    where
      SUB.�f�[�^ID is null
  ) A 
  inner JOIN ( 
    SELECT
      B2.* 
    FROM
      ( 
        SELECT
          �f�[�^ID
          , MAX(����NO) ����NO 
        FROM
          �����ԗ������ 
        GROUP BY
          �f�[�^ID
      ) B1 
      INNER JOIN �����ԗ������ B2 
        ON B1.�f�[�^ID = B2.�f�[�^ID 
        AND B1.����NO = B2.����NO
  ) B 
    ON A.�f�[�^ID = B.�f�[�^ID 
    and B.�Ǘ��[���s�L�� = '��' 
  LEFT JOIN SECTION_GROUP_DATA D 
    ON B.��̕��� = D.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA E 
    ON D.SECTION_ID = E.SECTION_ID 
  LEFT JOIN SECTION_GROUP_DATA F 
    ON B.�Ǘ��ӔC���� = F.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA G 
    ON F.SECTION_ID = G.SECTION_ID 
ORDER BY
  A.�f�[�^ID
  , B.����NO;
