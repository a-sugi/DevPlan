#!/bin/bash -l

sqlplus  -s  / as sysdba <<'EOF'
spool /ope/log/Update_Calendar.log replace
  alter session set nls_date_format='YYYY/MM/DD HH24:MI:SS'; 
  alter session set container=ORAC;
  
  select * from g948495.休日 
  where (holiday like (select substr(sysdate,1,4) from dual)|| '%' or holiday like (select substr(sysdate,1,4)+1 from dual)|| '%');

  delete from g948495.休日 
  where (holiday like (select substr(sysdate,1,4) from dual)|| '%' or holiday like (select substr(sysdate,1,4)+1 from dual)|| '%');

  select 
  substr(calendar_date,1,4)|| '/' 
  || substr(calendar_date,5,2)|| '/' 
  || substr(calendar_date,7,2)|| ' 00:00:00' 
  from devplan.rhac1860 
  where kadobi_kbn='0' 
  and (calendar_date like (select substr(sysdate,1,4) from dual)|| '%' or calendar_date like (select substr(sysdate,1,4)+1 from dual)|| '%');

  insert into g948495.休日 
  select 
  substr(calendar_date,1,4)|| '/' 
  || substr(calendar_date,5,2)|| '/' 
  || substr(calendar_date,7,2)|| ' 00:00:00' 
  from devplan.rhac1860 
  where kadobi_kbn='0' 
  and (calendar_date like (select substr(sysdate,1,4) from dual)|| '%' or calendar_date like (select substr(sysdate,1,4)+1 from dual)|| '%');
  commit; 

spool off
exit

EOF

exit
