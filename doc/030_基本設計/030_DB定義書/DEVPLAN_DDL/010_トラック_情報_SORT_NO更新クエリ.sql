merge 
into トラック_情報 UPDATE_TBL 
  using ( 
select
  ID
  , ROW_NUMBER() over (order by FLAG_定期便 desc,"NO") NEW_NUMBER 
from
  トラック_情報
  ) NEW_TBL 
    on (UPDATE_TBL.ID = NEW_TBL.ID) when matched then update 
set
  UPDATE_TBL.SORT_NO = NEW_TBL.NEW_NUMBER;


UPDATE トラック_情報 
SET
  SERIAL_NUMBER = SORT_NO 
WHERE
  FLAG_定期便 = 1;

commit;
