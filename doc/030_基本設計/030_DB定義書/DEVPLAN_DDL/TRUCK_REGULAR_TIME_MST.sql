--------------------------------------------------------
--  ファイルを作成しました - 金曜日-6月-21-2019   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table TRUCK_REGULAR_TIME_MST
--------------------------------------------------------

  CREATE TABLE "DEVPLAN"."TRUCK_REGULAR_TIME_MST" 
   (	"ID" NUMBER, 
	"REGULAR_ID" NUMBER, 
	"TIME_ID" NUMBER, 
	"DEPARTURE_TIME" VARCHAR2(20 BYTE), 
	"IS_RESERVATION" NUMBER(1,0) DEFAULT 1
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA" ;
REM INSERTING into DEVPLAN.TRUCK_REGULAR_TIME_MST
SET DEFINE OFF;
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (8,2,10,'10:50',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (6,1,18,'16:30',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (7,2,8,'9:10',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (9,2,12,'12:30',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (10,2,14,'14:30',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (11,2,16,'15:50',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (12,2,18,'17:10',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (13,1,6,'定期便1',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (14,2,6,'定期便2',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (15,1,20,'-',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (16,2,20,'-',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (17,1,22,'-',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (18,2,22,'-',0);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (1,1,8,'8:30',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (2,1,10,'10:10',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (3,1,12,'11:50',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (4,1,14,'13:50',1);
Insert into DEVPLAN.TRUCK_REGULAR_TIME_MST (ID,REGULAR_ID,TIME_ID,DEPARTURE_TIME,IS_RESERVATION) values (5,1,16,'15:10',1);
--------------------------------------------------------
--  DDL for Index TRUCK_REGULAR_TIME_MST_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "DEVPLAN"."TRUCK_REGULAR_TIME_MST_PK" ON "DEVPLAN"."TRUCK_REGULAR_TIME_MST" ("ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA" ;
--------------------------------------------------------
--  Constraints for Table TRUCK_REGULAR_TIME_MST
--------------------------------------------------------

  ALTER TABLE "DEVPLAN"."TRUCK_REGULAR_TIME_MST" MODIFY ("IS_RESERVATION" NOT NULL ENABLE);
  ALTER TABLE "DEVPLAN"."TRUCK_REGULAR_TIME_MST" MODIFY ("REGULAR_ID" NOT NULL ENABLE);
  ALTER TABLE "DEVPLAN"."TRUCK_REGULAR_TIME_MST" ADD CONSTRAINT "TRUCK_REGULAR_TIME_MST_PK" PRIMARY KEY ("ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "USERDATA"  ENABLE;
  ALTER TABLE "DEVPLAN"."TRUCK_REGULAR_TIME_MST" MODIFY ("ID" NOT NULL ENABLE);
