MERGE 
INTO TESTCAR_SCHEDULE UPDATE_TBL 
  USING ( 
    SELECT
      ID
      , 1 NEW_SYMBOL 
    FROM
      TESTCAR_SCHEDULE 
    WHERE
      SYMBOL = 3
  ) NEW_TBL 
    ON (UPDATE_TBL.ID = NEW_TBL.ID) WHEN MATCHED THEN UPDATE 
SET
  UPDATE_TBL.SYMBOL = NEW_TBL.NEW_SYMBOL
