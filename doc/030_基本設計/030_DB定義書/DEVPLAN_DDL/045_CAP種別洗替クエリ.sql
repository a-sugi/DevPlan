/*変更前確認*/
SELECT * FROM 試験計画_CAP_種別;


/* CAP_フォロー状況データ削除 */
DELETE FROM 試験計画_CAP_種別;

/* CAP_フォロー状況データ作成 */
insert into DEVPLAN."試験計画_CAP_種別"("種別",SORT_NO) values ('BY',1);
insert into DEVPLAN."試験計画_CAP_種別"("種別",SORT_NO) values ('PU',2);
insert into DEVPLAN."試験計画_CAP_種別"("種別",SORT_NO) values ('図面',3);

/*変更後確認*/
SELECT * FROM 試験計画_CAP_種別;



--ROLLBACK
--COMMIT