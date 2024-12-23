merge 
into �g���b�N_��� UPDATE_TBL 
  using ( 
select
  ID
  , ROW_NUMBER() over (order by FLAG_����� desc,"NO") NEW_NUMBER 
from
  �g���b�N_���
  ) NEW_TBL 
    on (UPDATE_TBL.ID = NEW_TBL.ID) when matched then update 
set
  UPDATE_TBL.SORT_NO = NEW_TBL.NEW_NUMBER;


UPDATE �g���b�N_��� 
SET
  SERIAL_NUMBER = SORT_NO 
WHERE
  FLAG_����� = 1;

commit;
