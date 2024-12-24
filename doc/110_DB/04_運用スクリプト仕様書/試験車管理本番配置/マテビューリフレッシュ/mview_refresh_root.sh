# variable setting
PATH=/u01/app/oracle/product/19.3.0/dbhome_1/bin:/u01/app/19.3.0/grid/bin:/usr/local/bin:/bin:/usr/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/oracle/bin ;export PATH
LOGDIR=/ope/log
ERR_LOGPATH=${LOGDIR}/mview_refresh_error.log

# check arg
if [ $# -ne 2 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" Argument Error"                                                 >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" Argument is 1:PDB Name 2:Logfiles Save Days"                    >> $ERR_LOGPATH
  echo `date '+%Y/%m/%d %H:%M:%S'`" ******************** FAILED ******************** "              >> $ERR_LOGPATH
  exit 1
fi

# exec shell
su - oracle -c "/ope/tool/mview_refresh.sh $1 $2"
