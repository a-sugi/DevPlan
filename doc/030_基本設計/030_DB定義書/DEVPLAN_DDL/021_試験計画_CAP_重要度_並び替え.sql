UPDATE 試験計画_CAP_重要度 SET SORT_NO = SORT_NO + 12;

UPDATE 試験計画_CAP_重要度 SET SORT_NO = 1 WHERE 重要度 = 'S';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 2 WHERE 重要度 = 'A';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 3 WHERE 重要度 = 'B';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 4 WHERE 重要度 = 'C';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 5 WHERE 重要度 = 'E';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 6 WHERE 重要度 = 'G';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 7 WHERE 重要度 = 'N';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 8 WHERE 重要度 = '◎';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 9 WHERE 重要度 = '○';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 10 WHERE 重要度 = '△';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 11 WHERE 重要度 = '×';
UPDATE 試験計画_CAP_重要度 SET SORT_NO = 12 WHERE 重要度 = '-';

COMMIT;
