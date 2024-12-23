merge 
into �g���b�N_�\��� UPDATE_TBL 
  using ( 
    SELECT
      TIME_ID
      , ROW_NUMBER() OVER (ORDER BY TIME_ID) ROW_NO 
    FROM
      TRUCK_REGULAR_TIME_MST 
    WHERE
      IS_RESERVATION = 1 
      AND REGULAR_ID = 1
  ) NEW_TBL 
    on ( 
      TO_NUMBER(TO_CHAR(UPDATE_TBL.�\��J�n����, 'HH24')) = NEW_TBL.TIME_ID 
      AND UPDATE_TBL.FLAG_����� = 1
    ) when matched then update 
set
  UPDATE_TBL.PARALLEL_INDEX_GROUP = NEW_TBL.ROW_NO
