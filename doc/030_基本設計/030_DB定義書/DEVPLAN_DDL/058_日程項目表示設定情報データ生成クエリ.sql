
SELECT * FROM 日程項目表示設定情報;

INSERT INTO 日程項目表示設定情報
SELECT DISTINCT * FROM
(
SELECT
  GENERAL_CODE "開発符号",
  '管理票番号,開発符号,試作時期,メーカー名,外製車名,号車,登録ナンバー,駐車場番号,FLAG_ナビ付,FLAG_ETC付,仕向地,排気量,E_G型式,駆動方式,トランスミッション,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考' "表示列名",
  'CATEGORY_ID' "非表示列名",
  NULL "固定列数"
FROM
  GENERAL_CODE
UNION ALL
SELECT
  A.GENERAL_CODE "開発符号",
  '管理票番号,開発符号,試作時期,メーカー名,外製車名,号車,登録ナンバー,駐車場番号,FLAG_ナビ付,FLAG_ETC付,仕向地,排気量,E_G型式,駆動方式,トランスミッション,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考' "表示列名",
  'CATEGORY_ID' "非表示列名",
  NULL "固定列数"
FROM
  (SELECT DISTINCT "GENERAL_CODE" FROM CARSHARING_SCHEDULE_ITEM) A
);

SELECT * FROM 日程項目表示設定情報;