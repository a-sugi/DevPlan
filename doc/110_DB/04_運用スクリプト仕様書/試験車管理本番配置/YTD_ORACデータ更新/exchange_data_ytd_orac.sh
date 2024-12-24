#!/bin/bash

####################################################################################################
#
#  Name        : exchange_data_ytd_orac.sh
#  Description : YTD_ORAC DATA EXCHANGE SCRIPT
#---------------------------------------------------------------------------------------------------
#  Substitution variables
#  [YTD_ORAC_SYSTEM_PASSWORD] : PDB[YTD_ORAC] USER[SYSTEM] PASSWORD
#  [ORAC_SYSTEM_PASSWORD]     : PDB[ORAC] USER[SYSTEM] PASSWORD
#---------------------------------------------------------------------------------------------------
#  History     : 2018/01/24  System-EXE  NEW [devplan_exchange_data_ytd_orac.sh]
#                2018/12/11  System-EXE  CHANGED PROCESSING that exchange devplan schema
#                2019/12/05  System-EXE  RENAME [exchange_data_ytd_orac.sh]
#                                        CHANGED PROCESSING that export g948495 -> FULL
#                                        CHANGED PROCESSING that import g948495 schema
#                2019/12/24  System-EXE  ADDED PROCESSING that update g948495.試験計画_伝言
#                2020/05/28  System-EXE  ADDED PROCESSING that update devplan.parking_location
#                                                                     devplan.parking_area
#                2020/06/30  System-EXE  ADDED ARGUMENT1 [EXEC_TYPE]
#                2020/08/04  System-EXE  ADDED PROCESSING that update g948495.関連BR_NO
#                2020/08/17  System-EXE  ADDED ARGUMENT1 [EXEC_TYPE]=3
#                                        ADDED PROCESSING that ROTATE DMP
#                                        ADDED VARIABLE [DMPDIR]
#                2020/08/25  System-EXE  ADDED TAR TARGET FILE [${DMPDIR}/export_orac_full.log] 
#                2024/02/13  System-EXE  CHANGED DMPDIR that update /u02/app/oracle/oradata/datodadb1 ->/u01/app/oracle/oradata/datodadb
#                                        CHANGED SERVERNAME that update ODA Oracle12c -> SGI Oracle19c
#
####################################################################################################

SCRIPT_DIR=$(cd $(dirname "$0") && pwd)

# Oracle environment variable setting
. /home/oracle/.bash_profile

# variable setting
LOGDIR=/ope/log
LOGFILE=exchange_data_sh_ytd_orac.log
LOGPATH=${LOGDIR}/${LOGFILE}
DMPDIR=/u01/app/oracle/oradata/datodadb/tabbak
#MODE=DRYRUN
MODE=ORIGINAL

echo `date '+%Y/%m/%d %T'`" ***** Start *****"                                         >> ${LOGPATH}
echo "SCRIPT_DIR:${SCRIPT_DIR}"                                                        >> ${LOGPATH}
#---------------------------------------------------------------------------------------------------
# arguments check
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Arguments Check Start *****"                         >> ${LOGPATH}

if [ $# -ne 1 ] ; then
  echo `date '+%Y/%m/%d %T'`" no argument"                                             >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" Argument1 is EXEC_TYPE"                                  >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      1:EXPORT and IMPORT or "                            >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      2:ONLY IMPORT or "                                  >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      3:DAILY PROCESSING"                                 >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" ***** Arguments Check Failed *****"                      >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" ***** END *****"                                         >> ${LOGPATH}
  exit 1
else
  echo `date '+%Y/%m/%d %T'`" ***** Arguments Check Success *****"                     >> ${LOGPATH}
fi

if [ ${1} -eq 1 ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** EXPORT and IMPORT START *****"                     >> ${LOGPATH}
elif [ ${1} -eq 2 ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** ONLY IMPORT START *****"                           >> ${LOGPATH}
elif [ ${1} -eq 3 ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** DAILY PROCESSING START *****"                      >> ${LOGPATH}
else
  echo `date '+%Y/%m/%d %T'`" Invalid Argument1[${1}]"                                 >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" Argument1 is EXEC_TYPE"                                  >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      1:EXPORT and IMPORT or "                            >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      2:ONLY IMPORT or "                                  >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`"      3:DAILY PROCESSING"                                 >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" ***** Argument1 ERROR*****"                              >> ${LOGPATH}
  echo `date '+%Y/%m/%d %T'`" ***** END *****"                                         >> ${LOGPATH}
  exit 1
fi

#---------------------------------------------------------------------------------------------------
# export data (SGI Oracle19c ORAC FULL)
#---------------------------------------------------------------------------------------------------
if [ ${1} = '1' -o ${1} = '3' ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Export FULL Start *****"                           >> ${LOGPATH}

  if [ ${MODE} = "ORIGINAL" ] ; then
    #Original
    echo `date '+%Y/%m/%d %T'`" ***** Export FULL [Original] *****"                    >> ${LOGPATH}
    expdp system/[ORAC_SYSTEM_PASSWORD]@ORAC directory=TABBAK_DIR dumpfile=ORAC_FULL.dmp logfile=export_orac_full.log full=y cluster=no reuse_dumpfiles=yes
    RTN_CD=$?

    if [ ${RTN_CD} -eq 0 ] ; then
      echo `date '+%Y/%m/%d %T'`" ***** Export FULL Success *****"                     >> ${LOGPATH}

      #ROTATE DMP
      if [ ${1} = '3' ] ; then
        echo `date '+%Y/%m/%d %T'`" ***** DMP Rotation Start *****"                    >> ${LOGPATH}
        echo `date '+%Y/%m/%d %T'`" ***** DMP LIST [BEFORE] *****"                     >> ${LOGPATH}
        ls -lSh --full-time  ${DUMPDIR}                                                >> ${LOGPATH}
        rm -f ${DMPDIR}/ORAC_FULL_DMP_7.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_6.tar.gz ${DMPDIR}/ORAC_FULL_DMP_7.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_5.tar.gz ${DMPDIR}/ORAC_FULL_DMP_6.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_4.tar.gz ${DMPDIR}/ORAC_FULL_DMP_5.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_3.tar.gz ${DMPDIR}/ORAC_FULL_DMP_4.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_2.tar.gz ${DMPDIR}/ORAC_FULL_DMP_3.tar.gz
        mv ${DMPDIR}/ORAC_FULL_DMP_1.tar.gz ${DMPDIR}/ORAC_FULL_DMP_2.tar.gz
        tar zcvf ${DMPDIR}/ORAC_FULL_DMP_1.tar.gz ${DMPDIR}/ORAC_FULL.dmp ${DMPDIR}/export_orac_full.log
        echo `date '+%Y/%m/%d %T'`" ***** DMP LIST [AFTER] *****"                      >> ${LOGPATH}
        ls -lSh --full-time  ${DUMPDIR}                                                >> ${LOGPATH}
        echo `date '+%Y/%m/%d %T'`" ***** DMP Rotation End *****"                      >> ${LOGPATH}
      fi
      ##
      
    else
      echo `date '+%Y/%m/%d %T'`" ***** Export FULL Failed *****"                      >> ${LOGPATH}
    #  exit 1
    fi

  else
    #Dryrun
    echo `date '+%Y/%m/%d %T'`" ***** Export FULL [Dryrun] *****"                      >> ${LOGPATH}
    expdp system/[ORAC_SYSTEM_PASSWORD]@ORAC directory=TABBAK_DIR logfile=dryrun_export_orac_full.log full=y estimate_only=y
    RTN_CD=$?

    if [ ${RTN_CD} -eq 0 ] ; then
      echo `date '+%Y/%m/%d %T'`" ***** Export FULL Success *****"                     >> ${LOGPATH}

      #ROTATE DMP
      if [ ${1} = '3' ] ; then
        echo `date '+%Y/%m/%d %T'`" ***** DMP Rotation Start *****"                    >> ${LOGPATH}
        echo `date '+%Y/%m/%d %T'`" ***** DMP LIST [DRYRUN] *****"                     >> ${LOGPATH}
        ls -lSh --full-time  ${DUMPDIR}                                                >> ${LOGPATH}
        echo `date '+%Y/%m/%d %T'`" ***** DMP Rotation End *****"                      >> ${LOGPATH}
      fi
      ##

    else
      echo `date '+%Y/%m/%d %T'`" ***** Export FULL Failed *****"                      >> ${LOGPATH}
    #  exit 1
    fi
  fi
fi

#---------------------------------------------------------------------------------------------------
# import data (SGI Oracle19c ORAC G948495 -> SGI Oracle19c YTD_ORAC G948495)
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Import G948495 Start *****"                          >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  #Original
  impdp system/[YTD_ORAC_SYSTEM_PASSWORD]@YTD_ORAC directory=TABBAK_YTD_ORAC dumpfile=ORAC_FULL.dmp logfile=import_ytd_orac_g948495.log schemas=G948495 cluster=no table_exists_action=truncate content=data_only
  echo `date '+%Y/%m/%d %T'`" ***** Import G948495 [Original] *****"                   >> ${LOGPATH}
else
  #Dryrun
  impdp system/[YTD_ORAC_SYSTEM_PASSWORD]@YTD_ORAC directory=TABBAK_YTD_ORAC dumpfile=ORAC_FULL.dmp logfile=dryrun_import_ytd_orac_g948495.log schemas=G948495 sqlfile=dryrun_import_ytd_orac_g948495.sql
  echo `date '+%Y/%m/%d %T'`" ***** Import G948495 [Dryrun] *****"                     >> ${LOGPATH}
fi
RTN_CD=$?

if [ ${RTN_CD} -eq 0 ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Import G948495 Success *****"                      >> ${LOGPATH}
else
  echo `date '+%Y/%m/%d %T'`" ***** Import G948495 Failed *****"                       >> ${LOGPATH}
#  exit 1
fi

sleep 2m

#---------------------------------------------------------------------------------------------------
# import data (SGI Oracle19c ORAC DEVPLAN -> SGI Oracle19c YTD_ORAC DEVPLAN)
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Import DEVPLAN Start *****"                          >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  #Original
  impdp system/[YTD_ORAC_SYSTEM_PASSWORD]@YTD_ORAC directory=TABBAK_YTD_ORAC dumpfile=ORAC_FULL.dmp logfile=import_ytd_orac_devplan.log schemas=DEVPLAN cluster=no table_exists_action=truncate content=data_only
  echo `date '+%Y/%m/%d %T'`" ***** Import DEVPLAN [Original] *****"                   >> ${LOGPATH}
else
 #Dryrun
  impdp system/[YTD_ORAC_SYSTEM_PASSWORD]@YTD_ORAC directory=TABBAK_YTD_ORAC dumpfile=ORAC_FULL.dmp logfile=dryrun_import_ytd_orac_devplan.log schemas=DEVPLAN sqlfile=dryrun_import_ytd_orac_devplan.sql
  echo `date '+%Y/%m/%d %T'`" ***** Import DEVPLAN [Dryrun] *****"                     >> ${LOGPATH}
fi
RTN_CD=$?

if [ ${RTN_CD} -eq 0 ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Import DEVPLAN Success *****"                      >> ${LOGPATH}
else
  echo `date '+%Y/%m/%d %T'`" ***** Import DEVPLAN Failed *****"                       >> ${LOGPATH}
#  exit 1
fi

#---------------------------------------------------------------------------------------------------
# insert into devplan.information
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Insert devplan.information Start *****"              >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Insert devplan.information [Original] *****"       >> ${LOGPATH}
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/[YTD_ORAC_SYSTEM_PASSWORD]@ytd_orac <<EOF
whenever sqlerror exit sql.sqlcode
insert into devplan.information values
((select max(id)+1 from devplan.information), 'ytd_orac', 'http://hoge', SYSDATE-1, SYSDATE+365, SYSDATE, '00000', SYSDATE, '00000');
EOF
`
SQLCODE=$?   
                                                                                       >> ${LOGPATH}
  if [ $SQLCODE -eq 0 ]; then
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Insert devplan.information Success ***** "       >> ${LOGPATH}
  else
    echo `date '+%Y/%m/%d %T'`" Insert Error : $SQL_CODE "                             >> ${LOGPATH}
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Insert devplan.information Failed ***** "        >> ${LOGPATH}
  fi
else
  echo `date '+%Y/%m/%d %T'`" ***** Insert devplan.information [DRYRUN] *****"         >> ${LOGPATH}
fi

#---------------------------------------------------------------------------------------------------
# update devplan.parking_location
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_location Start *****"         >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_location [Original] *****"  >> ${LOGPATH}
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/[YTD_ORAC_SYSTEM_PASSWORD]@ytd_orac <<EOF
whenever sqlerror exit sql.sqlcode
DECLARE
CURSOR park_loc IS SELECT LOCATION_NO, MAP_PDF, NAME FROM DEVPLAN.PARKING_LOCATION;
loc_rec park_loc%ROWTYPE;
back_slash VARCHAR(2) := '\';
file_path1 VARCHAR(50) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\g';
file_path2 VARCHAR(50) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\skc';
file_path3 VARCHAR(50) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\t';
BEGIN
FOR loc_rec IN park_loc LOOP
UPDATE DEVPLAN.PARKING_LOCATION
SET MAP_PDF=back_slash || file_path1 || SUBSTR(loc_rec.MAP_PDF,INSTR(loc_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=loc_rec.LOCATION_NO
AND LOCATION_NO='1';
UPDATE DEVPLAN.PARKING_LOCATION
SET MAP_PDF=back_slash || file_path2 || SUBSTR(loc_rec.MAP_PDF,INSTR(loc_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=loc_rec.LOCATION_NO
AND LOCATION_NO='2';
UPDATE DEVPLAN.PARKING_LOCATION
SET MAP_PDF=back_slash || file_path3 || SUBSTR(loc_rec.MAP_PDF,INSTR(loc_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=loc_rec.LOCATION_NO
AND LOCATION_NO='3';
END LOOP;
END;
/
EOF
`
SQLCODE=$?   
                                                                                       >> ${LOGPATH}
  if [ $SQLCODE -eq 0 ]; then
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_location Success ***** "  >> ${LOGPATH}
  else
    echo `date '+%Y/%m/%d %T'`" Update Error : $SQL_CODE "                             >> ${LOGPATH}
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_location Failed ***** "   >> ${LOGPATH}
  fi
else
  echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_location [DRYRUN] *****"    >> ${LOGPATH}
fi

#---------------------------------------------------------------------------------------------------
# update devplan.parking_area
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_area Start *****"             >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_area [Original] *****"      >> ${LOGPATH}
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/[YTD_ORAC_SYSTEM_PASSWORD]@ytd_orac <<EOF
whenever sqlerror exit sql.sqlcode
DECLARE
CURSOR park_area IS SELECT LOCATION_NO, AREA_NO, MAP_PDF, NAME FROM DEVPLAN.PARKING_AREA;
area_rec park_area%ROWTYPE;
back_slash VARCHAR(2) := '\';
file_path1 VARCHAR(80) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\g\エリア';
file_path2 VARCHAR(80) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\skc\エリア';
file_path3 VARCHAR(80) := '\\fgnt5069\gj1tdstest\DevPlan\parking_map\t\エリア';
BEGIN
FOR area_rec IN park_area LOOP
UPDATE DEVPLAN.PARKING_AREA 
SET MAP_PDF=back_slash || file_path1 || SUBSTR(area_rec.MAP_PDF,INSTR(area_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=area_rec.LOCATION_NO
AND AREA_NO=area_rec.AREA_NO
AND LOCATION_NO='1';
UPDATE DEVPLAN.PARKING_AREA 
SET MAP_PDF=back_slash || file_path2 || SUBSTR(area_rec.MAP_PDF,INSTR(area_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=area_rec.LOCATION_NO
AND AREA_NO=area_rec.AREA_NO
AND LOCATION_NO='2';
UPDATE DEVPLAN.PARKING_AREA 
SET MAP_PDF=back_slash || file_path3 || SUBSTR(area_rec.MAP_PDF,INSTR(area_rec.MAP_PDF,'\', -1))
WHERE LOCATION_NO=area_rec.LOCATION_NO
AND AREA_NO=area_rec.AREA_NO
AND LOCATION_NO='3';
END LOOP;
END;
/
EOF
`
SQLCODE=$?   
                                                                                       >> ${LOGPATH}
  if [ $SQLCODE -eq 0 ]; then
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_area Success ***** "      >> ${LOGPATH}
  else
    echo `date '+%Y/%m/%d %T'`" Update Error : $SQL_CODE "                             >> ${LOGPATH}
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_area Failed ***** "       >> ${LOGPATH}
  fi
else
  echo `date '+%Y/%m/%d %T'`" ***** Update devplan.parking_area [DRYRUN] *****"        >> ${LOGPATH}
fi

#---------------------------------------------------------------------------------------------------
# update g948495.試験計画_伝言
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Update g948495.試験計画_伝言 Start *****"            >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Update devplan.information [Original] *****"       >> ${LOGPATH}
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/[YTD_ORAC_SYSTEM_PASSWORD]@ytd_orac <<EOF
whenever sqlerror exit sql.sqlcode
update g948495.試験計画_伝言
set message = 'YTD_ORAC' || CHR(13) || CHR(10) || CHR(13) || CHR(10) || message where id=1;
EOF
`
SQLCODE=$?   
                                                                                       >> ${LOGPATH}
  if [ $SQLCODE -eq 0 ]; then
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update g948495.試験計画_伝言 Success ***** "     >> ${LOGPATH}
  else
    echo `date '+%Y/%m/%d %T'`" Update Error : $SQL_CODE "                             >> ${LOGPATH}
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update g948495.試験計画_伝言 Failed ***** "      >> ${LOGPATH}
  fi
else
  echo `date '+%Y/%m/%d %T'`" ***** Update g948495.試験計画_伝言 [DRYRUN] *****"       >> ${LOGPATH}
fi

#---------------------------------------------------------------------------------------------------
# update g948495.関連BR_NO
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** Update g948495.関連BR_NO Start *****"                >> ${LOGPATH}

if [ ${MODE} = "ORIGINAL" ] ; then
  echo `date '+%Y/%m/%d %T'`" ***** Update g948495.関連BR_NO [Original] *****"         >> ${LOGPATH}
RTN_MS=`$ORACLE_HOME/bin/sqlplus -s system/[YTD_ORAC_SYSTEM_PASSWORD]@ytd_orac <<EOF
whenever sqlerror exit sql.sqlcode
update g948495.関連BR_NO 
set BR_NO='ytd_orac'
where システム名='設備管理マネージャ';
EOF
`
SQLCODE=$?   
                                                                                       >> ${LOGPATH}
  if [ $SQLCODE -eq 0 ]; then
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update g948495.関連BR_NO Success ***** "         >> ${LOGPATH}
  else
    echo `date '+%Y/%m/%d %T'`" Update Error : $SQL_CODE "                             >> ${LOGPATH}
    echo $RTN_MS                                                                       >> ${LOGPATH}
    echo `date '+%Y/%m/%d %T'`" ***** Update g948495.関連BR_NO Failed ***** "          >> ${LOGPATH}
  fi
else
  echo `date '+%Y/%m/%d %T'`" ***** Update g948495.関連BR_NO [DRYRUN] *****"           >> ${LOGPATH}
fi

#---------------------------------------------------------------------------------------------------
# end
#---------------------------------------------------------------------------------------------------
echo `date '+%Y/%m/%d %T'`" ***** END *****"                                           >> ${LOGPATH}
exit


