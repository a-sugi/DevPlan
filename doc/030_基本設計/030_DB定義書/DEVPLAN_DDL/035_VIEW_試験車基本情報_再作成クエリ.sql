--------------------------------------------------------
--  DDL for View VIEW_±Ôî{îñ
--------------------------------------------------------

CREATE OR REPLACE FORCE EDITIONABLE VIEW "DEVPLAN"."VIEW_±Ôî{îñ" ("f[^ID", "Ç[NO", "Çx­sL³", "Ôn", "Ô^", "^®", "ÔêÔ", "Ýn", "[XNO", "[X¹ú", "¤ÀÇpp\¿óú", "pp©Ïú", "ppÏ³FN", "Ô¼Àoú", "pp©Ïz", "Ý^æ", "Ý^Ôp\èúÀ", "Ý^Ôpú", "", "³®æ¾ú", "IµÀ{ú", "á_ÈªL³") AS 
  SELECT A."f[^ID",
    A."Ç[NO",
    A."Çx­sL³",
    A."Ôn",
    A."Ô^",
    A."^®",
    A."ÔêÔ",
   (CASE
      WHEN ÔêÔ IS NULL THEN NULL
      WHEN B.LOCATION_NO = 1 OR C.LOCATION_NO = 1 THEN 'Qn'
      ELSE 'SKC'
    END )
AS
Ýn, A."[XNO", A."[X¹ú", A."¤ÀÇpp\¿óú", A."pp©Ïú", A."ppÏ³FN", A."Ô¼Àoú", A."pp©Ïz", A."Ý^æ", A."Ý^Ôp\èúÀ", A."Ý^Ôpú", A."", A."³®æ¾ú", A."IµÀ{ú", A."á_ÈªL³" 
  FROM "DEVPLAN"."±Ôî{îñ" A
    LEFT JOIN "DEVPLAN"."PARKING_SECTION" B ON A.ÔêÔ = B.NAME
    LEFT JOIN "DEVPLAN"."PARKING_AREA" C ON A.ÔêÔ = C.NAME;