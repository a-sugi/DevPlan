#!/bin/bash
###############################################################################
#
#   Title       : Oracle Backup shell [DB] (from root shell)
#   Shell Name  : ORACLE_BK_ORACLE.sh
#   Shell Dir   : /usr/local/tool/orabkup
#   Create      : System Exe
#   Create date : 2015/08
#   Update date : 2024/02
#
###############################################################################

######################################################################
#   set parameter 
######################################################################
#2024/02 update
ORACLE_BASE=/u01/app/oracle ; export ORACLE_BASE
ORACLE_HOME=/u01/app/oracle/product/19.3.0/dbhome_1 ; export ORACLE_HOME
GRID_HOME=/u01/app/19.3.0/grid ; export GRID_HOME
ORACLE_SID=odadb1$1 ; export ORACLE_SID
NLS_LANG=American_America.JA16EUC;export NLS_LANG
NLS_DATE_FORMAT='YYYY/MM/DD HH24:MI:SS'; export NLS_DATE_FORMAT
PATH=/u01/app/oracle/product/19.3.0/dbhome_1/bin:/u01/app/19.3.0/grid/bin:/usr/local/bin:/bin:/usr/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/oracle/bin ;export PATH

######################################################################
#   set logdir
######################################################################
BACKUP_LOG="/ope/log"

######################################################################
#   RMAN backup
######################################################################
echo "-------------------------------------------------------------------"
echo `date '+%Y%m%d_%H%M%S'`" ORACLE Backup Start. "

#2024/02 update
rman target / nocatalog << EOF >> $BACKUP_LOG/ORACLE_BACKUP.log
run {
CONFIGURE CONTROLFILE AUTOBACKUP ON;
CONFIGURE RETENTION POLICY TO REDUNDANCY 2;
#CONFIGURE CHANNEL DEVICE TYPE DISK FORMAT '/u01/app/oracle/oradata/datodadb/dbbkup/%U';
backup database plus archivelog delete all input;
REPORT OBSOLETE;
DELETE noprompt OBSOLETE;
delete noprompt archivelog until time 'sysdate-5';
}
list backupset;
exit
EOF

export RMAN_RETURN=$?

if [ ${RMAN_RETURN} -eq 0 ] ; then
   echo `date '+%Y%m%d_%H%M%S'`" ODADB1 Backup Success. "
else
   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ORACLE backup Failed. " 
   echo "-------------------------------------------------------------------"
  exit 1
fi

#2024/02 delete
####ODADB2—
#ORACLE_SID=ODADB2$1 ; export ORACLE_SID

#echo "-------------------------------------------------------------------"
#echo `date '+%Y%m%d_%H%M%S'`" ORACLE Backup Start. "

#rman target / nocatalog << EOF >> $BACKUP_LOG/ORACLE_BACKUP.log
#run {
#CONFIGURE CONTROLFILE AUTOBACKUP ON;
#CONFIGURE RETENTION POLICY TO REDUNDANCY 2;
#backup database plus archivelog delete all input;
#REPORT OBSOLETE;
#DELETE noprompt OBSOLETE;
#delete noprompt archivelog until time 'sysdate-5';
#}
#list backupset;
#exit
#EOF

#if [ ${RMAN_RETURN} -eq 0 ] ; then
#   echo `date '+%Y%m%d_%H%M%S'`" ODADB2 Backup Success. "
#else
#   echo `date '+%Y%m%d_%H%M%S'`" [ERROR] ORACLE backup Failed. " 
#   echo "-------------------------------------------------------------------"
#  exit 1
#fi

######################################################################
#   End message
######################################################################
echo "-------------------------------------------------------------------"

exit 0

