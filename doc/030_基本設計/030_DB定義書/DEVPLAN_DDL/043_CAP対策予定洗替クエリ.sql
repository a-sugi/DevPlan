/*変更前確認*/
SELECT * FROM 試験計画_CAP_対策予定;


/* CAP_対策予定データ削除 */
DELETE FROM 試験計画_CAP_対策予定;

/* CAP_対策予定データ作成 */
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('【K3開発CAP】',1);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('調査中／調査待ち',2);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('現象再現しない',3);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('改善案検討中',4);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('改善する（設変）',5);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('改善する（チューニング）',6);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('改善する（玉成）',7);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('製造／ 部品不良',8);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('本開発車ではこのままとしたい',9);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('  ',10);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('【図面CAP】',11);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('改善する/改善済み',12);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('検討中',13);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('本開発車ではこのままとしたい',14);
insert into DEVPLAN."試験計画_CAP_対策予定"("対策予定",SORT_NO) values ('対象外',15);

/*変更後確認*/
SELECT * FROM 試験計画_CAP_対策予定;



--ROLLBACK
--COMMIT