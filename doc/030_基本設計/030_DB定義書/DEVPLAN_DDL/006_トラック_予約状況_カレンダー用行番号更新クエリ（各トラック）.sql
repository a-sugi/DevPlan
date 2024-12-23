--トラック_予約状況テーブルカレンダー行番号設定クエリ
merge 
into トラック_予約状況 UPDATE_TBL 
  using ( 
    select
      ID
      , ROW_NUMBER() over ( 
        PARTITION BY
          トラック_ID
          , TO_CHAR(トラック_予約状況.予約開始時間, 'YYYYMMDD') 
        order by
          トラック_ID
          , 予約開始時間
      ) NEW_NUMBER 
    from
      トラック_予約状況 
    where
      トラック_予約状況.FLAG_定期便 = 0 
      or トラック_予約状況.FLAG_定期便 is null
  ) NEW_TBL 
    on (UPDATE_TBL.ID = NEW_TBL.ID) when matched then update 
set
  UPDATE_TBL.PARALLEL_INDEX_GROUP = NEW_TBL.NEW_NUMBER
