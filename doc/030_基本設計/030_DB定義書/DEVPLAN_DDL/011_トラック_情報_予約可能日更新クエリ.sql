update トラック_情報
set
  トラック_情報.予約可能開始日 = trunc(SYSDATE) 
where
  トラック_情報.flag_予約可 = 1;

update トラック_情報
set
  トラック_情報.予約可能開始日 = trunc(to_date('9998-12-31'))
where
  トラック_情報.flag_予約可 = 0;

commit;
