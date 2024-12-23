
SELECT * FROM 試験車履歴情報 ;

ALTER TABLE 試験車履歴情報 ADD ( "自動車ﾘｻｲｸﾙ法" VARCHAR2(10) ) ;
ALTER TABLE 試験車履歴情報 ADD ( "A_C冷媒種類" VARCHAR2(50) ) ;

SELECT * FROM 試験車履歴情報 ;