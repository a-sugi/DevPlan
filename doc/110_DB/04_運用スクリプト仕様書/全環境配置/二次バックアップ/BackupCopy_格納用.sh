######################################################################
#   set parameter 
######################################################################
#log directory
LOG_DIR=/ope/log
LOGFILE=$LOG_DIR/BackupCopy.log
#mount point
MOUNT_POINT=/ope/bkcopy
#save date
BKSHSAVED=3
PATH=/u01/app/oracle/product/12.1.0.2/dbhome_1/bin:/u01/app/12.1.0.2/grid/bin:/usr/local/bin:/bin:/usr/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/oracle/bin ;export PATH

######################################################################
#   directory mount 
######################################################################
mount -t cifs -o user=dbbkup,password=XXXXXX //172.20.4.39/dbbkup/GKHDB001 $MOUNT_POINT
#mount -t cifs -o user=administrator,password=XXXXXX //192.168.101.41/backupTest $MOUNT_POINT
export RETURN_CODE=$?
if [ ${RETURN_CODE} -eq 1 ] ; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" Directory Mount Error. " >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
  exit 1
else
  echo `date '+%Y/%m/%d %H:%M:%S'`" Directory Mount Success. " >> $LOGFILE
fi


#####################################################################
   rman backup copy 
#####################################################################
tar -czvf $MOUNT_POINT/rmanbackup_odadb1_`date +%Y%m%d_%H%M%S`.tar.gz /u01/app/oracle/fast_recovery_area/rcoodadb1/odadb1/ODADB1 --exclude /u01/app/oracle/fast_recovery_area/rcoodadb1/odadb1/ODADB1/archivelog
export RETURN_CODE=$?
if [ ${RETURN_CODE} -eq 1 ] ; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" ODADB1 File Copy Error. " >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
  exit 1
else
    echo `date '+%Y/%m/%d %H:%M:%S'`" ODADB1 File Copy Success. " >> $LOGFILE
    find $MOUNT_POINT/rmanbackup_* -mtime +0 -exec rm -f {} \;
    mkdir /u01/app/oracle/fast_recovery_area/rcoodadb1/odadb1/ODADB1/archivelog
    chown oracle:oinstall /u01/app/oracle/fast_recovery_area/rcoodadb1/odadb1/ODADB1/archivelog
    chmod 750 /u01/app/oracle/fast_recovery_area/rcoodadb1/odadb1/ODADB1/archivelog
fi

tar -czvf $MOUNT_POINT/rmanbackup_odadb2_`date +%Y%m%d_%H%M%S`.tar.gz /u01/app/oracle/fast_recovery_area/rcoodadb2/ODADB2/ODADB2 --exclude /u01/app/oracle/fast_recovery_area/rcoodadb2/ODADB2/ODADB2/archivelog
export RETURN_CODE=$?
if [ ${RETURN_CODE} -eq 1 ] ; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" ODADB2 File Copy Error. " >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
  exit 1
else
    echo `date '+%Y/%m/%d %H:%M:%S'`" ODADB2 File Copy Success. " >> $LOGFILE
    find $MOUNT_POINT/rmanbackup_* -mtime +0 -exec rm -f {} \;
    mkdir /u01/app/oracle/fast_recovery_area/rcoodadb2/ODADB2/ODADB2/archivelog
    chown oracle:oinstall /u01/app/oracle/fast_recovery_area/rcoodadb2/ODADB2/ODADB2/archivelog
    chmod 750 /u01/app/oracle/fast_recovery_area/rcoodadb2/ODADB2/ODADB2/archivelog
fi

cp -f /ope/log/ORACLE_BACKUP.log $MOUNT_POINT/ORACLE_BACKUP.log

######################################################################
#   ocr backup copy 
######################################################################
echo `date '+%Y%m%d_%H%M%S'`"ocr backup copy start" >> $LOGFILE

\cp -f /u01/app/12.1.0.2/grid/cdata/ora-app001/backup_`date '+%Y%m%d'`_*.ocr $MOUNT_POINT
export RTNCODE=$?

echo "ls -l $MOUNT_POINT/*.ocr" >> $LOGFILE
ls -l $MOUNT_POINT/*.ocr >> $LOGFILE

if [ ${RTNCODE} -eq 1 ] ; then
        echo `date '+%Y%m%d_%H%M%S'`"ocr backup copy failed" >> $LOGFILE
        echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
else
        echo `date '+%Y%m%d_%H%M%S'`"ocr backup copy success" >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old ocr backup delete Start" >> $LOGFILE
        echo "---------------------Delete File List--------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name '*.ocr'  >> $LOGFILE
        echo "---------------------------------------------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name "*.ocr" | xargs --no-run-if-empty rm
        echo "ls -l $MOUNT_POINT/*.ocr" >> $LOGFILE
        ls -l $MOUNT_POINT/*.ocr >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old ocr backup delete End" >> $LOGFILE
fi

echo `date '+%Y%m%d_%H%M%S'`"ocr backup copy finished" >> $LOGFILE


######################################################################
#   asm backup copy 
######################################################################
echo `date '+%Y%m%d_%H%M%S'`"asm backup copy start" >> $LOGFILE

\cp -f /u02/app/oracle/oradata/datodadb1/asmbkup/asm_md_`date +%Y%m%d`* $MOUNT_POINT
export RTNCODE=$?

echo "ls -l $MOUNT_POINT/asm_md_*" >> $LOGFILE
ls -l $MOUNT_POINT/asm_md_* >> $LOGFILE

if [ ${RTNCODE} -eq 1 ] ; then
        echo `date '+%Y%m%d_%H%M%S'`"asm backup copy failed" >> $LOGFILE
        echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
else
        echo `date '+%Y%m%d_%H%M%S'`"asm backup copy success" >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old asm backup delete Start" >> $LOGFILE
        echo "---------------------Delete File List--------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name 'asm_md_*'  >> $LOGFILE
        echo "---------------------------------------------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name "asm_md_*" | xargs --no-run-if-empty rm
        echo "ls -l $MOUNT_POINT/asm_md_*" >> $LOGFILE
        ls -l $MOUNT_POINT/asm_md_* >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old asm backup delete End" >> $LOGFILE
fi

echo `date '+%Y%m%d_%H%M%S'`"asm backup copy finished" >> $LOGFILE


######################################################################
#   acfs backup copy 
######################################################################
echo `date '+%Y%m%d_%H%M%S'`"acfs backup copy start" >> $LOGFILE

\cp -f /u02/app/oracle/oradata/datodadb1/asmbkup/asm_volinfo_`date +%Y%m%d`* $MOUNT_POINT
export RTNCODE=$?

echo "ls -l $MOUNT_POINT/asm_volinfo_*" >> $LOGFILE
ls -l $MOUNT_POINT/asm_volinfo_* >> $LOGFILE

if [ ${RTNCODE} -eq 1 ] ; then
        echo `date '+%Y%m%d_%H%M%S'`"acfs backup copy failed" >> $LOGFILE
        echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
else
        echo `date '+%Y%m%d_%H%M%S'`"acfs backup copy success" >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old acfs backup delete Start" >> $LOGFILE
        echo "---------------------Delete File List--------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name 'asm_volinfo_*'  >> $LOGFILE
        echo "---------------------------------------------------------" >> $LOGFILE
        find $MOUNT_POINT -mtime +$BKSHSAVED -name "asm_volinfo_*" | xargs --no-run-if-empty rm
        echo "ls -l $MOUNT_POINT/asm_volinfo_*" >> $LOGFILE
        ls -l $MOUNT_POINT/asm_volinfo_* >> $LOGFILE
        echo `date '+%Y%m%d_%H%M%S'`"old acfs backup delete End" >> $LOGFILE
fi

echo `date '+%Y%m%d_%H%M%S'`"acfs backup copy finished" >> $LOGFILE


######################################################################
#   log file copy
######################################################################
\cp -f $LOGFILE $MOUNT_POINT/BackupCopy.log

######################################################################
#   directory unmount
######################################################################
umount $MOUNT_POINT
export RETURN_CODE=$?
if [ ${RETURN_CODE} -eq 1 ] ; then
  echo `date '+%Y/%m/%d %H:%M:%S'`" Directory Unmount Error. " >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** FAILED ******************" >> $LOGFILE
  exit 1
else
  echo `date '+%Y/%m/%d %H:%M:%S'`" Directory Unmount Success. " >> $LOGFILE
fi


echo `date '+%Y/%m/%d %H:%M:%S'`" ****************** SUCCESS ******************" >> $LOGFILE


