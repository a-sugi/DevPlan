CREATE TABLE "DEVPLAN"."ROLL_MST" 
   (
	"ID" NUMBER(10,0) NOT NULL ENABLE, 
	"ROLL_ID" NUMBER(10,0) NOT NULL ENABLE, 
	"ROLL_NAME" VARCHAR2(100), 
	"FUNCTION_ID" NUMBER(5,0) NOT NULL ENABLE, 
	"READ_FLG" CHAR(1) NOT NULL ENABLE, 
	"UPDATE_FLG" CHAR(1) NOT NULL ENABLE, 
	"EXPORT_FLG" CHAR(1) NOT NULL ENABLE, 
	"MANAGEMENT_FLG" CHAR(1) NOT NULL ENABLE, 
	"PRINTSCREEN_FLG" CHAR(1) NOT NULL ENABLE, 
	"CARSHARE_OFFICE_FLG" CHAR(1) NOT NULL ENABLE, 
	"ALL_GENERAL_CODE_FLG" CHAR(1) NOT NULL ENABLE, 
	"SKS_FLG" CHAR(1) NOT NULL ENABLE, 
	"JIBU_UPDATE_FLG" CHAR(1) NOT NULL ENABLE, 
	"JIBU_EXPORT_FLG" CHAR(1) NOT NULL ENABLE, 
	"JIBU_MANAGEMENT_FLG" CHAR(1) NOT NULL ENABLE, 
	"INPUT_DATETIME" DATE, 
	"INPUT_PERSONEL_ID" VARCHAR2(20), 
	"CHANGE_DATETIME" DATE, 
	"CHANGE_PERSONEL_ID" VARCHAR2(20), 
	 CONSTRAINT "PK_ROLL_MST" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA"  ENABLE)
  SEGMENT CREATION IMMEDIATE
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
  NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA";