/* 機能権限テーブル削除 */
--DROP TABLE DEVPLAN.FUNCTION_AUTH CASCADE CONSTRAINTS PURGE;

/* 機能マスタデータ削除 */
DELETE FROM DEVPLAN.FUNCTION_MST WHERE ID IN (16, 20, 21);

/* 機能マスタデータ作成 */
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('1','ログイン制御',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('11','試験車日程',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('12','カーシェア日程',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('13','外製車日程',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('14','カーシェア管理',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('15','車両検索',SYSDATE,'00001',NULL,NULL);
INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('16','トラック予約',SYSDATE,'00001',NULL,NULL);
INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('20','CAP・商品力',SYSDATE,'00001',NULL,NULL);
INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('21','設計チェック',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('81','お知らせ設定',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('82','ロール設定',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('83','機能権限設定',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('84','閲覧権限設定',SYSDATE,'00001',NULL,NULL);
--INSERT INTO DEVPLAN.FUNCTION_MST VALUES ('101','試験車管理',SYSDATE,'00001',NULL,NULL);
