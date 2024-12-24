#!/bin/bash
###############################################################################
#
#   Title       : Oracle Backup shell [ASM] (from root shell)
#   Shell Name  : ORACLE_BK_GRID.sh
#   Shell Dir   : /ope/tool/
#   Create      : System Exe
#   Create date : 2017/08
#   Update date : 2024/02
#
###############################################################################

######################################################################
#   set parameter
######################################################################
#2024/02 update
ORACLE_HOME=/u01/app/19.3.0/grid ; export ORACLE_HOME
ORACLE_SID=+ASM$1; export ORACLE_SID
NLS_LANG=American_America.AL32UTF8 ; export NLS_LANG
NLS_DATE_FORMAT='YYYY/MM/DD HH24:MI:SS'; export NLS_DATE_FORMAT
ASM_BKDIR=/u01/app/oracle/oradata/datodadb/asmbkup ; export ASM_BKDIR
PATH=/u01/app/19.3.0/grid/bin:/usr/local/bin:/bin:/usr/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/grid/bin

######################################################################
#   set logdir
######################################################################
BACKUP_LOG="/ope/log" 

######################################################################
#   ASM pfile Backup
######################################################################
echo "-------------------------------------------------------------------"
echo `date '+%Y%m%d_%H%M%S'`" ASM pfile Backup Start " 

echo $ORACLE_SID >> $BACKUP_LOG/ORACLE_BACKUP.log
which sqlplus >> $BACKUP_LOG/ORACLE_BACKUP.log

mv $ASM_BKDIR/initASM.ora $ASM_BKDIR/initASM.ora.`date +%Y%m%d%H%M%S`
sqlplus -L "/ as sysasm" @/ope/tool/backup_pfile.sql $ASM_BKDIR/initASM.ora >> $BACKUP_LOG/ORACLE_BACKUP.log

export SQLPLUS_RETURN=$?

if [ ${SQLPLUS_RETURN} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" ASM pfile Full Backup Success.   "
   find $ASM_BKDIR -mtime +3 -name 'initASM.ora.*' -exec rm -f '{}' ';'
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ASM pfile Backup Failed. "
  exit 1
fi

######################################################################
#   ASM metadata backup
######################################################################
asmcmd md_backup $ASM_BKDIR/asm_md_`date +%Y%m%d%H%M%S` >> $BACKUP_LOG/ORACLE_BACKUP.log

export ASMCMD_RETURN=$?
if [ ${ASMCMD_RETURN} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" ASM metadata Full Backup Success.   "
   find $ASM_BKDIR -mtime +3 -name 'asm_md_*' -exec rm -f '{}' ';'
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ASM metadata Backup Failed. "
  exit 1
fi

######################################################################
#   ACFS backup
######################################################################
asmcmd volinfo --all >> $ASM_BKDIR/asm_volinfo_`date +%Y%m%d%H%M%S`.log
export ASMCMD_RETURN=$?

if [ ${ASMCMD_RETURN} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" ACFS Backup Success.   "
   find $ASM_BKDIR -mtime +3 -name 'asm_volinfo_*' -exec rm -f '{}' ';'
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ACFS Backup Failed. "
  exit 1
fi

######################################################################
#   End message
######################################################################
echo "-------------------------------------------------------------------"

exit 0

