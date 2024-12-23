UPDATE 日程項目表示設定情報 SET 非表示列名 = NULL;
UPDATE 日程項目表示設定情報 SET 表示列名 = REPLACE(表示列名, 'FLAG_', '');
UPDATE 日程項目表示設定情報 SET 表示列名 = REPLACE(表示列名, 'トランスミッション', 'T/M');
UPDATE 日程項目表示設定情報 SET 表示列名 = REPLACE(表示列名, ',号車,登録ナンバー', ',登録ナンバー,号車');