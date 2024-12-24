#!/bin/bash
###############################################################################
#
#   Title       : ORACLE LOGROTATION SHELL
#   Shell Name  : ORACLE_LOGROTATION.sh
#   Shell Dir   : 
#   Create      : System Exe
#   Create date : 2017/06
#   Update date : 2019/04
#                 2024/02
#
###############################################################################

######################################################################
#   set parameter 
######################################################################
ORACLE_BASE=/u01/app/oracle ; export ORACLE_BASE
ORACLE_HOME=/u01/app/oracle/product/19.3.0/dbhome_1 ; export ORACLE_HOME
CRS_HOME=/u01/app/grid ; export CRS_HOME
NLS_LANG=American_America.AL32UTF8 ; export NLS_LANG
PATH=$PATH:$HOME/bin:$ORACLE_HOME/bin ; export PATH

#2024/02 update
SRVNAME=p-vbt000-sts0$1
LOGFDIR=/ope/log
LOGFILE=$LOGFDIR/ORACLE_LOGROTATION.log
OLDFILE=$LOGFDIR/ORACLE_LOGROTATION_`date '+%Y%m%d_%H%M%S'`.log

#2024/02 delete
DB_AUDIT_DIR=$ORACLE_BASE/admin/odadb1/adump
#DB_AUDIT_DIR2=$ORACLE_BASE/admin/ODADB2/adump
LIS_XML_DIR=$CRS_HOME/diag/tnslsnr/$SRVNAME/listener/alert/
SCANLIS_XML_DIR1=$CRS_HOME/diag/tnslsnr/$SRVNAME/listener_scan1/alert/
#SCANLIS_XML_DIR2=$CRS_HOME/diag/tnslsnr/$SRVNAME/listener_scan2/alert/
ASMLIS_XML_DIR1=$CRS_HOME/diag/tnslsnr/$SRVNAME/asmnet1lsnr_asm/alert/
#ASMLIS_XML_DIR2=$CRS_HOME/diag/tnslsnr/$SRVNAME/asmnet2lsnr_asm/alert/
MGMTLIS_XML_DIR=/u01/app/grid/diag/tnslsnr/$SRVNAME/mgmtlsnr/alert/

#2019/04 add
#2024/02 delete
DB_TRACE_DIR=$ORACLE_BASE/diag/rdbms/odadb1/odadb1$1/trace
#DB_TRACE_DIR2=$ORACLE_BASE/diag/rdbms/odadb2/ODADB2$1/trace
CRS_TRACE_DIR=$CRS_HOME/diag/crs/$SRVNAME/crs/trace
ASM_TRACE_DIR=$CRS_HOME/diag/asm/+asm/+ASM$1/trace
#DBS_DIR=$ORACLE_BASE/product/12.1.0.2/dbhome_1/dbs
DBS_DIR=$ORACLE_BASE/product/19.3.0/dbhome_1/dbs
##

#2020/06 add
#2024/02 delete
#if [ ${1} -eq 1 ] ; then
  CLI_GRID_DIR=$CRS_HOME/diag/clients/user_grid/host_1556709885_82/trace/
#elif [ ${1} -eq 2 ] ; then
#  CLI_GRID_DIR=$CRS_HOME/diag/clients/user_grid/host_297772789_82/trace/
#fi
ODBCLIS_XML_DIR=$CRS_HOME/diag/tnslsnr/$SRVNAME/dg4odbc_lsnr/alert/
##

BKSAVED=51
BKSHSAVED=51

#2019/04 add
CORESAVED=51
##

######################################################################
#   log directory check 
######################################################################
if [ ! -e "$LOGFDIR" ] ; then
   logger -s "Oracle Log Rotation: [ERROR] Log Directory Not Found."
   exit 1
fi

if [ -e "$LOGFILE"  ] ; then
   cat $LOGFILE > $OLDFILE
   chown oracle:oinstall $LOGFILE
   chmod 664 $LOGFILE
fi
cp /dev/null $LOGFILE

######################################################################
#   start 
######################################################################
echo "-------------------------------------------------------------------"                      >> $LOGFILE
echo `date '+%Y%m%d_%H%M%S'`" Start.  "                                                         >> $LOGFILE

######################################################################
#   directory check 
######################################################################
if [ ! -e "$LIS_XML_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener ADR Home Directory Not Found. "               >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] listener ADR Home Directory Not Found. "
   exit 1
fi

if [ ! -e "$SCANLIS_XML_DIR1" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener_scan1 ADR Home Directory Not Found. "         >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] listener_scan1 ADR Home Directory Not Found. "
   exit 1
fi

#2024/02 delete
#if [ ! -e "$SCANLIS_XML_DIR2" ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener_scan2 ADR Home Directory Not Found. "         >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] listener_scan2 ADR Home Directory Not Found. "
#   exit 1
#fi

if [ ! -e "$ASMLIS_XML_DIR1" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] asmnet1lsnr_asm ADR Home Directory Not Found. "        >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] asmnet1lsnr_asm ADR Home Directory Not Found. "
   exit 1
fi

#2024/02 delete
#if [ ! -e "$ASMLIS_XML_DIR2" ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] asmnet2lsnr_asm ADR Home Directory Not Found. "        >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] asmnet2lsnr_asm ADR Home Directory Not Found. "
#   exit 1
#fi

if [ ! -e "$MGMTLIS_XML_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] mgmtlsnr ADR Home Directory Not Found. "               >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] mgmtlsnr ADR Home Directory Not Found. "
   exit 1
fi

if [ ! -e "$DB_AUDIT_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB1 Audit File Directory Not Found. "               >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] ODADB1 Audit File Directory Not Found. "
   exit 1
fi

#2024/02 delete
#if [ ! -e "$DB_AUDIT_DIR2" ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB2 Audit File Directory Not Found. "               >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] ODADB2 Audit File Directory Not Found. "
#   exit 1
#fi

#2019/04 add
if [ ! -e "$DB_TRACE_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB1 Trace File Directory Not Found. "               >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] ODADB1 Trace File Directory Not Found. "
   exit 1
fi

#2024/02 delete
#if [ ! -e "$DB_TRACE_DIR2" ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB2 Trace File Directory Not Found. "               >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] ODADB2 Trace File Directory Not Found. "
#   exit 1
#fi

if [ ! -e "$CRS_TRACE_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] CRS Trace File Directory Not Found. "                  >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] CRS Trace File Directory Not Found. "
   exit 1
fi

if [ ! -e "$ASM_TRACE_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ASM Trace File Directory Not Found. "                  >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] ASM Trace File Directory Not Found. "
   exit 1
fi

if [ ! -e "$DBS_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] DBS File Directory Not Found. "                        >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] DBS File Directory Not Found. "
   exit 1
fi
##

#2020/06 add
if [ ! -e "$CLI_GRID_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] CLIENT(GRID) Trace File Directory Not Found. "         >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] CLIENT(GRID) Trace File Directory Not Found. "
   exit 1
fi

if [ ! -e "$ODBCLIS_XML_DIR" ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] dg4odbc_scan1 ADR Home Directory Not Found. "          >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] dg4odbc_scan1 ADR Home Directory Not Found. "
   exit 1
fi
##

######################################################################
#   log rotation
######################################################################
echo `date '+%Y%m%d_%H%M%S'`" Oracle log rotation Start.  "                                     >> $LOGFILE

## adrci (listener log rotate)
cd $LIS_XML_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

export RTNCODE=$?
if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] listener Log Purge Success. "                        >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener Log Purge Failure. "                          >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] listener Log Purge Failure. "
   exit 1
fi

cd $SCANLIS_XML_DIR1
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

export RTNCODE=$?
if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] listener_scan1 Log Purge Success. "                  >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener_scan1 Log Purge Failure. "                    >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] listener_scan1 Log Purge Failure. "
   exit 1
fi

#2024/02 delete
#cd $SCANLIS_XML_DIR2
#$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

#export RTNCODE=$?
#if [ ${RTNCODE} -eq 0 ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] listener_scan2 Log Purge Success. "                  >> $LOGFILE
#else
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] listener_scan2 Log Purge Failure. "                    >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] listener_scan2 Log Purge Failure. "
#   exit 1
#fi

cd $ASMLIS_XML_DIR1
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

export RTNCODE=$?
if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] asmnet1lsnr_asm Log Purge Success. "                 >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] asmnet1lsnr_asm Log Purge Failure. "                   >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] asmnet1lsnr_asm Log Purge Failure. "
   exit 1
fi

#2024/02 delete
#cd $ASMLIS_XML_DIR2
#$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

#export RTNCODE=$?
#if [ ${RTNCODE} -eq 0 ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] asmnet2lsnr_asm Log Purge Success. "                 >> $LOGFILE
#else
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] asmnet2lsnr_asm Log Purge Failure. "                   >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] asmnet2lsnr_asm Log Purge Failure. "
#   exit 1
#fi

cd $MGMTLIS_XML_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE

export RTNCODE=$?
if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] mgmtlsnr Log Purge Success. "                        >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] mgmtlsnr Log Purge Failure. "                          >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] mgmtlsnr Log Purge Failure. "
   exit 1
fi

## audit file rotate
cd $DB_AUDIT_DIR
   find ./ -name "*.aud" -mtime +0 | xargs --no-run-if-empty tar zcvf odadb1_audit_`date '+%Y%m%d'`.tar.gz
   find ./ -name "*.aud" -mtime +0 | xargs --no-run-if-empty rm
   ls -ltr odadb1_audit_`date '+%Y%m%d'`.tar.gz                                                 >> $LOGFILE

#2024/02 delete
#cd $DB_AUDIT_DIR2
#   find ./ -name "*.aud" -mtime +0 | xargs --no-run-if-empty tar zcvf odadb2_audit_`date '+%Y%m%d'`.tar.gz
#   find ./ -name "*.aud" -mtime +0 | xargs --no-run-if-empty rm
#   ls -ltr odadb2_audit_`date '+%Y%m%d'`.tar.gz                                                 >> $LOGFILE

## delete gzip file +1years
   find $DB_AUDIT_DIR -mtime +$BKSAVED -name "*.gz" | xargs --no-run-if-empty rm
   find $DB_AUDIT_DIR2 -mtime +$BKSAVED -name "*.gz" | xargs --no-run-if-empty rm

#2019/04 add
cd $DB_TRACE_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
export RTNCODE=$?

if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] ODADB1 Trace File Purge Success. "                   >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB1 Trace File Purge Failure. "                     >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] ODADB1 Trace File Purge Failure. "
   exit 1
fi

#2024/02 delete
#cd $DB_TRACE_DIR2
#$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
#export RTNCODE=$?

#if [ ${RTNCODE} -eq 0 ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] ODADB2 Trace File Purge Success. "                   >> $LOGFILE
#else
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ODADB2 Trace File Purge Failure. "                     >> $LOGFILE
#   echo "-------------------------------------------------------------------"                   >> $LOGFILE
#   logger -s "Oracle Log Rotation: [ERROR] ODADB2 Trace File Purge Failure. "
#   exit 1
#fi

cd $CRS_TRACE_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
export RTNCODE=$?

if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] CRS Trace File Purge Success. "                      >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] CRS Trace File Purge Failure. "                        >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] CRS Trace File Purge Failure. "
   exit 1
fi

cd $ASM_TRACE_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
export RTNCODE=$?

if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] ASM Trace File Purge Success. "                      >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ASM Trace File Purge Failure. "                        >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] ASM Trace File Purge Failure. "
   exit 1
fi

cd $DBS_DIR
find $DBS_DIR -mtime +$CORESAVED -name 'core_*'                                                 >> $LOGFILE
find $DBS_DIR -mtime +$CORESAVED -name 'core_*'   | xargs --no-run-if-empty rm -rf
export RTNCODE=$?
if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] Old CORE File Delete Success. "                      >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] Old CORE File Delete Failure. "                        >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] Old DBS File Delete Failure. "
   exit 1
fi
##

#2020/06 add
cd $CLI_GRID_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
export RTNCODE=$?

if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] CLIENT(GRID) Trace File Purge Success. "             >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] CLIENT(GRID) Trace File Purge Failure. "               >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] CLIENT(GRID) Trace File Purge Failure. "
   exit 1
fi

cd $ODBCLIS_XML_DIR
$ORACLE_HOME/bin/adrci exec="SHOW CONTROL;PURGE;SHOW CONTROL;"                                  >> $LOGFILE
export RTNCODE=$?

if [ ${RTNCODE} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" [SUCCESS] dg4odbc_lsnr log Purge Success. "                    >> $LOGFILE
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] dg4odbc_lsnr Purge Failure. "                          >> $LOGFILE
   echo "-------------------------------------------------------------------"                   >> $LOGFILE
   logger -s "Oracle Log Rotation: [ERROR] dg4odbc_lsnr Purge Failure. "
   exit 1
fi

##

echo `date '+%Y%m%d_%H%M%S'`" Oracle log rotation End.  "                                       >> $LOGFILE

######################################################################
#   remove old shell log files 
######################################################################
echo "+++++++++++++++++++++++++ DELETE LOG LIST +++++++++++++++++++++++++"                      >> $LOGFILE
echo `date '+%Y%m%d_%H%M%S'`" Remove Old Log File Start. [ KEEP `expr $BKSHSAVED + 1` DAYS ]"   >> $LOGFILE
find $LOGFDIR -mtime +$BKSHSAVED -name 'ORACLE_LOGROTATION*.log'                                >> $LOGFILE
find $LOGFDIR -mtime +$BKSHSAVED -name "ORACLE_LOGROTATION*.log" | xargs --no-run-if-empty rm
find $LOGFDIR -mtime +$BKSHSAVED -name 'ORACLE_BACKUP.log.bk*'                                  >> $LOGFILE
find $LOGFDIR -mtime +$BKSHSAVED -name "ORACLE_BACKUP.log.bk*" | xargs --no-run-if-empty rm

echo `date '+%Y%m%d_%H%M%S'`" Remove Old Log File End."                                         >> $LOGFILE
echo "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"                      >> $LOGFILE

######################################################################
#   end 
######################################################################

echo `date '+%Y%m%d_%H%M%S'`" End.  "                                                           >> $LOGFILE
echo "-------------------------------------------------------------------"                      >> $LOGFILE

exit 0


