#!/bin/bash
###############################################################################
#
#   Title       : Oracle Backup shell (for root cron)
#   Shell Name  : ORACLE_BK_FULL.sh
#   Shell Dir   : /ope/tool
#   Create      : System Exe
#   Create date : 2017/08
#   Update date : 2024/02
#
###############################################################################

######################################################################
#   check arg count
######################################################################

if [ $# -ne 1 ]; then
  echo "no argument"
  echo "argument is instance_number(1or2)"
  exit 1
fi

######################################################################
#   set logdir
######################################################################
BACKUP_LOG="/ope/log"

######################################################################
#   log rotate
######################################################################
cat $BACKUP_LOG/ORACLE_BACKUP.log > $BACKUP_LOG/ORACLE_BACKUP.log.bk_`date +%Y%m%d_%H%M%S`
echo "-------------------------------------------------------------------"  | tee $BACKUP_LOG/ORACLE_BACKUP.log
chown oracle:oinstall $BACKUP_LOG/ORACLE_BACKUP.log
chmod 664 $BACKUP_LOG/ORACLE_BACKUP.log

######################################################################
#   start message
######################################################################
echo `date '+%Y%m%d_%H%M%S'`" BACKUP Start.            "   | tee -a $BACKUP_LOG/ORACLE_BACKUP.log

######################################################################
#   ocr backup
######################################################################
#2024/02 update
echo `date '+%Y%m%d_%H%M%S'`" OCR BACKUP Start.            "   | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
/u01/app/19.3.0/grid/bin/ocrconfig -manualbackup  >> $BACKUP_LOG/ORACLE_BACKUP.log
export RETURN_CODE=$?
if [ ${RETURN_CODE} -eq 0 ] ; then
  echo `date '+%Y%m%d_%H%M%S'`" ocr backup Success. " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
else
  echo `date '+%Y%m%d_%H%M%S'`" [ERROR]ocr backup Failed.  " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
  exit 1
fi

######################################################################
#   oracle user shell
######################################################################
echo `date '+%Y%m%d_%H%M%S'`" oracle user shell Start. "   | tee -a $BACKUP_LOG/ORACLE_BACKUP.log

su - oracle -c "/ope/tool/ORACLE_BK_ORACLE.sh $1"    >> $BACKUP_LOG/ORACLE_BACKUP.log

export RETURN_CODE=$?

if [ ${RETURN_CODE} -eq 0 ] ; then
  echo `date '+%Y%m%d_%H%M%S'`" oracle user shell Success. " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
else
  echo `date '+%Y%m%d_%H%M%S'`"[ERROR]oracle user shell Failed.  " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
  exit 1
fi

######################################################################
#   grid user shell
######################################################################
echo `date '+%Y%m%d_%H%M%S'`" grid user shell Start. "   | tee -a $BACKUP_LOG/ORACLE_BACKUP.log

su - grid -c "/ope/tool/ORACLE_BK_GRID.sh $1"  >> $BACKUP_LOG/ORACLE_BACKUP.log

export RETURN_CODE=$?

if [ ${RETURN_CODE} -eq 0 ] ; then
  echo `date '+%Y%m%d_%H%M%S'`" grid user shell Success. " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
else
  echo `date '+%Y%m%d_%H%M%S'`"[ERROR]grid user shell Failed.  " | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
  exit 1
fi

######################################################################
#   end message
######################################################################

echo `date '+%Y%m%d_%H%M%S'`" End. "  | tee -a $BACKUP_LOG/ORACLE_BACKUP.log
echo "-------------------------------------------------------------------"  | tee -a $BACKUP_LOG/ORACLE_BACKUP.log

exit 0