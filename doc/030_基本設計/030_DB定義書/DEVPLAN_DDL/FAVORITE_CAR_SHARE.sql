--------------------------------------------------------
--  ファイルを作成しました - 木曜日-1月-11-2018   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table FAVORITE_CAR_SHARE
--------------------------------------------------------

  CREATE TABLE "DEVPLAN"."FAVORITE_CAR_SHARE" 
   (	"ID" NUMBER(10,0), 
	"TITLE" VARCHAR2(100 BYTE), 
	"PERSONEL_ID" VARCHAR2(20 BYTE), 
	"CAR_GROUP" VARCHAR2(10 BYTE), 
	"GENERAL_CODE" VARCHAR2(20 BYTE), 
	"MANAGEMENT_NO" VARCHAR2(10 BYTE), 
	"PARKING_NO" VARCHAR2(50 BYTE), 
	"PLACE" VARCHAR2(20 BYTE), 
	"CAR_TYPE" VARCHAR2(10 BYTE), 
	"DESTINATION" VARCHAR2(50 BYTE), 
	"ETC_ARI_FLG" CHAR(1 BYTE), 
	"ETC_NASHI_FLG" CHAR(1 BYTE), 
	"TRANSMISSION" VARCHAR2(50 BYTE), 
	"STATUS_OPEN_FLG" CHAR(1 BYTE), 
	"STATUS_CLOSE_FLG" CHAR(1 BYTE), 
	"INPUT_DATETIME" DATE, 
	"INPUT_PERSONEL_ID" VARCHAR2(20 BYTE), 
	"CHANGE_DATETIME" DATE, 
	"CHANGE_PERSONEL_ID" VARCHAR2(20 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA" ;
--------------------------------------------------------
--  DDL for Index PK_FAVORITE_CAR_SHARE
--------------------------------------------------------

  CREATE UNIQUE INDEX "DEVPLAN"."PK_FAVORITE_CAR_SHARE" ON "DEVPLAN"."FAVORITE_CAR_SHARE" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA" ;
--------------------------------------------------------
--  DDL for Index IDX_FAVORITE_CAR_SHARE_1
--------------------------------------------------------

  CREATE INDEX "DEVPLAN"."IDX_FAVORITE_CAR_SHARE_1" ON "DEVPLAN"."FAVORITE_CAR_SHARE" ("PERSONEL_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA" ;
--------------------------------------------------------
--  Constraints for Table FAVORITE_CAR_SHARE
--------------------------------------------------------

  ALTER TABLE "DEVPLAN"."FAVORITE_CAR_SHARE" ADD CONSTRAINT "PK_FAVORITE_CAR_SHARE" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA"  ENABLE;
  ALTER TABLE "DEVPLAN"."FAVORITE_CAR_SHARE" MODIFY ("PERSONEL_ID" NOT NULL ENABLE);
  ALTER TABLE "DEVPLAN"."FAVORITE_CAR_SHARE" MODIFY ("TITLE" NOT NULL ENABLE);
  ALTER TABLE "DEVPLAN"."FAVORITE_CAR_SHARE" MODIFY ("ID" NOT NULL ENABLE);
