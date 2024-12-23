CREATE 
OR REPLACE FORCE VIEW "DEVPLAN"."試験計画_DCHK_指摘リスト" ( 
    "ID"
    , "試験車_ID"
    , "部品"
    , "状況"
    , "処置課"
    , "処置対象"
    , "処置方法"
    , "処置調整"
    , "完了確認日"
    , "織込日程"
    , "担当課名"
    , "担当課_ID"
    , "担当者_ID"
    , "担当者_TEL"
    , "編集者日"
    , "編集者_ID"
    , "FLAG_CLOSE"
    , "FLAG_試作CLOSE"
    , "履歴作成日"
    , "FLAG_最新"
    , "親_ID"
    , "FLAG_上司承認"
    , "指摘NO"
    , "FLAG_処置不要"
    , "FLAG_調整済"
    , "FLAG_試作改修"
    , "開催日_ID"
    , "部品納入日"
    , "試作管理NO"
) AS 
SELECT
      "ID"
    , "試験車_ID"
    , "部品"
    , "状況"
    , "処置課"
    , "処置対象"
    , "処置方法"
    , "処置調整"
    , "完了確認日"
    , "織込日程"
    , "担当課名"
    , "担当課_ID"
    , "担当者_ID"
    , "担当者_TEL"
    , "編集者日"
    , "編集者_ID"
    , "FLAG_CLOSE"
    , "FLAG_試作CLOSE"
    , "履歴作成日"
    , "FLAG_最新"
    , "親_ID"
    , "FLAG_上司承認"
    , "指摘NO"
    , "FLAG_処置不要"
    , "FLAG_調整済"
    , "FLAG_試作改修"
    , "開催日_ID"
    , "部品納入日"
    , "試作管理NO" 
FROM
    "G948495"."試験計画_DCHK_指摘リスト"; 