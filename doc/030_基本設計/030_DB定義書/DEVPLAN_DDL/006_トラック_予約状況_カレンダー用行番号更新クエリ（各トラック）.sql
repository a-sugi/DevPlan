--�g���b�N_�\��󋵃e�[�u���J�����_�[�s�ԍ��ݒ�N�G��
merge 
into �g���b�N_�\��� UPDATE_TBL 
  using ( 
    select
      ID
      , ROW_NUMBER() over ( 
        PARTITION BY
          �g���b�N_ID
          , TO_CHAR(�g���b�N_�\���.�\��J�n����, 'YYYYMMDD') 
        order by
          �g���b�N_ID
          , �\��J�n����
      ) NEW_NUMBER 
    from
      �g���b�N_�\��� 
    where
      �g���b�N_�\���.FLAG_����� = 0 
      or �g���b�N_�\���.FLAG_����� is null
  ) NEW_TBL 
    on (UPDATE_TBL.ID = NEW_TBL.ID) when matched then update 
set
  UPDATE_TBL.PARALLEL_INDEX_GROUP = NEW_TBL.NEW_NUMBER
