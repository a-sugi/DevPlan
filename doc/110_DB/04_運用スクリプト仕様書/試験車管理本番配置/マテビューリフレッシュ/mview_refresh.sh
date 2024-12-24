# Oracle environment variable setting
. /home/oracle/.bash_profile

#  Substitution variables
#  [ORAC_SYSTEM_PASSWORD]     : PDB[ORAC] USER[SYSTEM] PASSWORD


# variable setting
LOGDIR=/ope/log
ERR_LOGPATH=${LOGDIR}/mview_refresh_error.log

# check arg
if [ $# -ne 2 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" Argument Error"                                                 >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" Argument is 1:PDB Name 2:Logfiles Save Days"                    >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ******************** FAILED ******************** "              >> $ERR_LOGPATH
  exit 1
fi

# logfile setting
LOGFILE=mview_refresh_$1_`date +"%Y%m%d"`.log
LOGPATH=${LOGDIR}/${LOGFILE}

# exec refresh
echo `date '+%Y/%m/%d %H:%M:%S'`" ***** MVIEW Refresh Start ***** "                                 >> $LOGPATH
DB_CH=`$ORACLE_HOME/bin/sqlplus -s system/ora-app001@$1 <<EOF
show con_name;
EOF
`
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/ora-app001@$1 <<EOF
whenever sqlerror exit sql.sqlcode
exec dbms_mview.refresh('DEVPLAN.RHAC1860');
EOF
`

SQLCODE=$?
echo $DB_CH                                                                                         >> $LOGPATH
if [ $SQLCODE -eq 0 ]; then
  echo $RTN_MS                                                                                      >> $LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ***** MVIEW Refresh Success ***** "                             >> $LOGPATH
  # delete old lofile
  echo "++++++++++++++++++++ DELETE  LOG LIST ++++++++++++++++++++"                                 >> $LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" Remove Old Log File Start. [ KEEP `expr $2 + 1` DAYS ]"         >> $LOGPATH
  find $LOGDIR -mtime +$2 -name "mview_refresh_$1*.log"                                             >> $LOGPATH
  find $LOGDIR -mtime +$2 -name "mview_refresh_$1*.log" | xargs --no-run-if-empty rm
  echo `date '+%Y/%m/%d %H:%M:%S'`" Remove Old Log File End. "                                      >> $LOGPATH
  echo "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"                                 >> $LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ***** MVIEW Refresh End ***** "                                 >> $LOGPATH
else
  echo `date '+%Y/%m/%d %H:%M:%S'`" MVIEW Refresh Error : $SQL_CODE "                               >> $LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" MVIEW Refresh Error : $SQL_CODE "                               >> $ERR_LOGPATH
  echo $RTN_MS                                                                                      >> $LOGPATH
  echo $RTN_MS                                                                                      >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ******************** FAILED ******************** "              >> $LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ******************** FAILED ******************** "              >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ***** MVIEW Refresh End ***** "                                 >> $LOGPATH
  exit 1
fi

exit

