--------------------------------------------------------
--  DDL for View VIEW_試験車基本情報
--------------------------------------------------------

CREATE OR REPLACE FORCE EDITIONABLE VIEW "DEVPLAN"."VIEW_試験車基本情報" ("データID", "管理票NO", "管理ラベル発行有無", "車系", "車型", "型式符号", "駐車場番号", "所在地", "リースNO", "リース満了日", "研実管理廃却申請受理日", "廃却見積日", "廃却決済承認年月", "車両搬出日", "廃却見積額", "貸与先", "貸与返却予定期限", "貸与返却日", "メモ", "正式取得日", "棚卸実施日", "月例点検省略有無") AS 
  SELECT A."データID",
    A."管理票NO",
    A."管理ラベル発行有無",
    A."車系",
    A."車型",
    A."型式符号",
    A."駐車場番号",
   (CASE
      WHEN 駐車場番号 IS NULL THEN NULL
      WHEN B.LOCATION_NO = 1 OR C.LOCATION_NO = 1 THEN '群馬'
      ELSE 'SKC'
    END )
AS
所在地, A."リースNO", A."リース満了日", A."研実管理廃却申請受理日", A."廃却見積日", A."廃却決済承認年月", A."車両搬出日", A."廃却見積額", A."貸与先", A."貸与返却予定期限", A."貸与返却日", A."メモ", A."正式取得日", A."棚卸実施日", A."月例点検省略有無" 
  FROM "DEVPLAN"."試験車基本情報" A
    LEFT JOIN "DEVPLAN"."PARKING_SECTION" B ON A.駐車場番号 = B.NAME
    LEFT JOIN "DEVPLAN"."PARKING_AREA" C ON A.駐車場番号 = C.NAME;