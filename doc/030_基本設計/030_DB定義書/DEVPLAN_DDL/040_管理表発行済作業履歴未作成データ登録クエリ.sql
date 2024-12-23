INSERT INTO 
 使用履歴情報(
  データID
  , 履歴NO
  , SEQNO
  , 承認要件コード
  , STEPNO
  , 承認状況
  , 承認者レベル
  , 管理部署承認
  , 処理日
  , 管理責任課名
  , 管理責任部署名
  , 使用課名
  , 使用部署名
  , 試験内容
  , 工事区分NO
  , 実走行距離
  , 編集日
  , 編集者
  , 移管先部署ID
  , 駐車場番号
 )
SELECT
  A.データID データID
  , B.履歴NO 履歴NO
  , '1' SEQNO
  , 'A' 承認要件コード
  , '0' STEPNO
  , '済' 承認状況
  , NULL 承認者レベル
  , NULL 管理部署承認
  , B.発行年月日 処理日
  , G.SECTION_CODE 管理責任課名
  , F.SECTION_GROUP_CODE 管理責任部署名
  , E.SECTION_CODE 使用課名
  , D.SECTION_GROUP_CODE 使用部署名
  , '受領' 試験内容
  , B.工事区分NO 工事区分NO
  , B.受領時走行距離 実走行距離
  , SYSDATE 編集日
  , '000001' 編集者
  , NULL 移管先部署ID
  , NULL 駐車場番号 
FROM
  ( 
    select
      main.データID 
    from
      試験車基本情報 main 
      LEFT JOIN 使用履歴情報 SUB 
        ON main.データID = SUB.データID 
    where
      SUB.データID is null
  ) A 
  inner JOIN ( 
    SELECT
      B2.* 
    FROM
      ( 
        SELECT
          データID
          , MAX(履歴NO) 履歴NO 
        FROM
          試験車履歴情報 
        GROUP BY
          データID
      ) B1 
      INNER JOIN 試験車履歴情報 B2 
        ON B1.データID = B2.データID 
        AND B1.履歴NO = B2.履歴NO
  ) B 
    ON A.データID = B.データID 
    and B.管理票発行有無 = '済' 
  LEFT JOIN SECTION_GROUP_DATA D 
    ON B.受領部署 = D.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA E 
    ON D.SECTION_ID = E.SECTION_ID 
  LEFT JOIN SECTION_GROUP_DATA F 
    ON B.管理責任部署 = F.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA G 
    ON F.SECTION_ID = G.SECTION_ID 
ORDER BY
  A.データID
  , B.履歴NO;
