/*変更前確認*/
SELECT * FROM 試験計画_CAP_フォロー状況;


/* CAP_フォロー状況データ削除 */
DELETE FROM 試験計画_CAP_フォロー状況;

/* CAP_フォロー状況データ作成 */
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('【K3開発CAP】',1);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('調査中／調査待ち',2);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('現象再現しない',3);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善案検討中',4);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善する（設変）',5);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善する（ﾁｭｰﾆﾝｸﾞ）',6);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善する（玉成）',7);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('本開発車ではこのままとしたい',8);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('本開発車ではこのままとする（ｸﾛｰｽﾞ）',9);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('再評価（欠品･暫定品）',10);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善を確認（ｸﾛｰｽﾞ）',11);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('その他',12);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('  ',13);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('【図面CAP】',14);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('改善する／改善済み',15);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('K3実車で確認',16);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('検討中',17);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('本開発車ではこのままとしたい ',18);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('本開発車ではこのままとする（クローズ）',19);
insert into DEVPLAN."試験計画_CAP_フォロー状況"("フォロー状況",SORT_NO) values ('対象外',20);

/*変更後確認*/
SELECT * FROM 試験計画_CAP_フォロー状況;



--ROLLBACK
--COMMIT