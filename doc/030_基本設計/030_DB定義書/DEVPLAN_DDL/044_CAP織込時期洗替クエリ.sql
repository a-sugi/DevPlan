/*変更前確認*/
SELECT * FROM 試験計画_CAP_織込時期;


/* CAP_フォロー状況データ削除 */
DELETE FROM 試験計画_CAP_織込時期;

/* CAP_フォロー状況データ作成 */
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (1,' ',1,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (2,'【K3開発CAP】',2,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (3,'開発完了確認車',3,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (4,'PUG工試',4,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (5,'量産確認車',5,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (6,'認証車',6,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (7,'工試車',7,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (8,'F/T-1',8,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (9,'F/T-2',9,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (10,'生試車',10,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (11,'PP-A',11,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (12,'PP-B',12,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (13,'量先車',13,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (14,'PreSOP',14,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (15,'ﾗﾝﾁｪﾝ',15,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (17,'ｸﾛｰｽﾞ',17,null);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (18,'要調整',18,null);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (19,'  ',19,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (20,'【図面CAP】',20,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (21,'第1回集計',21,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (22,'第2回集計',22,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (23,'第3回集計',23,1);
insert into DEVPLAN."試験計画_CAP_織込時期"(ID,"織込時期",SORT_NO,FLAG_DISP) values (24,'第4回集計',24,1);

/*変更後確認*/
SELECT * FROM 試験計画_CAP_織込時期;



--ROLLBACK
--COMMIT