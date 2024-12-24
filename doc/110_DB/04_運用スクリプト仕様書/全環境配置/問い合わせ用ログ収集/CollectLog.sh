######################################################################
#   set parameter
######################################################################
PATH=/u01/app/oracle/product/19.3.0/dbhome_1/bin:/u01/app/19.3.0/grid/bin:/usr/local/bin:/bin:/usr/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/oracle/bin ;export PATH

#log directory
LOG_DIR=/ope/log
LOGFILE=$LOG_DIR/CollectLog.log

#use directory
USEDIR=/u01/app/oracle/oradata/datodadb1/CollectLogs
LOGDIR=${USEDIR}/logfiles

######################################################################
#   clear logfile
######################################################################
: > $LOGFILE
export RETURN_CODE=$?
if [ ${RETURN_CODE} -ne 0 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`"Clear logfile Error"                                                                  >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`"********** FAILED **********"                                                         >> $LOGFILE
  exit 1
else 
  echo `date '+%Y/%m/%d %H:%M:%S'`"Crear logfile Success"                                                                >> $LOGFILE
  echo 
fi

######################################################################
#   check arg count
######################################################################
if [ $# -ne 1 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`"No Argument"                                                                          >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`"Argument is instance_number(1or2)"                                                    >> $LOGFILE
  exit 1
fi

######################################################################
#   collect logfiles
######################################################################
echo `date '+%Y/%m/%d %H:%M:%S'`"Collect Logfiles Start."                                                                >> $LOGFILE
mkdir ${USEDIR}
export RETURN_CODE=$?

if [ ${RETURN_CODE} -ne 0 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`"Create UseDirectory Error"                                                            >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`"********** FAILED **********"                                                         >> $LOGFILE
  exit 1
else 
  echo `date '+%Y/%m/%d %H:%M:%S'`"Create UseDirectory Success"                                                          >> $LOGFILE
  echo 
fi

mkdir ${LOGDIR}
export RETURN_CODE=$?

if [ ${RETURN_CODE} -ne 0 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`"Create LogDirectory Error"                                                            >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`"********** FAILED **********"                                                         >> $LOGFILE
  exit 1
else 
  echo `date '+%Y/%m/%d %H:%M:%S'`"Create LogDirectory Success"                                                          >> $LOGFILE
  echo 
fi

cd ${LOGDIR}

#各ログファイルの圧縮
tar zcvf DBTraceODADB1$1.tar.gz /u01/app/oracle/diag/rdbms/odadb1/odadb1$1/trace/                                        >> $LOGFILE
#tar zcvf DBTraceODADB2$1.tar.gz /u01/app/oracle/diag/rdbms/odadb2/ODADB2$1/trace/                                        >> $LOGFILE
tar zcvf CrsTraceFGNT423$1.tar.gz /u01/app/grid/diag/crs/fgnt423$1/crs/trace/                                            >> $LOGFILE
tar zcvf AsmTraceFGNT423$1.tar.gz /u01/app/grid/diag/asm/+asm/+ASM$1/trace/                                              >> $LOGFILE
tar zcvf ScanLsnr1TraceFGNT423$1.tar.gz /u01/app/grid/diag/tnslsnr/fgnt423$1/listener_scan1/trace/                       >> $LOGFILE
#tar zcvf ScanLsnr2TraceFGNT423$1.tar.gz /u01/app/grid/diag/tnslsnr/fgnt423$1/listener_scan2/trace/                       >> $LOGFILE
tar zcvf LsnrTraceFGNT423$1.tar.gz /u01/app/grid/diag/tnslsnr/fgnt423$1/listener/trace/                                  >> $LOGFILE

#まとめて圧縮
cd ${USEDIR}
tar zcvf logfiles_fgnt423$1.tar.gz ${LOGDIR}/                                                                            >> $LOGFILE

#テスト用(サイズ見積もりのみ)
#tar zcvf - /u01/app/oracle/diag/rdbms/odadb1/odadb1$1/trace/ | wc -c                                                    >> $LOGFILE
#tar zcvf - /u01/app/oracle/diag/rdbms/odadb2/ODADB2$1/trace/ | wc -c                                                    >> $LOGFILE
#tar zcvf - /u01/app/grid/diag/crs/fgnt423$1/crs/trace/ | wc -c                                                          >> $LOGFILE
#tar zcvf - /u01/app/grid/diag/asm/+asm/+ASM$1/trace/ | wc -c                                                            >> $LOGFILE
#tar zcvf - /u01/app/grid/diag/tnslsnr/fgnt423$1/listener_scan1/trace/| wc -c                                            >> $LOGFILE
#tar zcvf - /u01/app/grid/diag/tnslsnr/fgnt423$1/listener_scan2/trace/ | wc -c                                           >> $LOGFILE
#tar zcvf - /u01/app/grid/diag/tnslsnr/fgnt423$1/listener/trace/ | wc -c                                                 >> $LOGFILE
#tar zcvf - ${USEDIR}/ | wc -c                                                                                           >> $LOGFILE

echo `date '+%Y/%m/%d %H:%M:%S'`"Collect Logfiles Success"                                                               >> $LOGFILE

######################################################################
#   wait to complete file copy
######################################################################
echo "以下の手順で、収集したファイルをローカル端末にコピーしてください"
echo "①teratermウィンドウの「ファイル(F)」から「SSH SCP...」を選択"
echo "②画面下側の「From」と「To」を以下のように入力してください。"
echo "<From>${USEDIR}/logfiles_fgnt423$1.tar.gz"
echo "<To>右側の「...」をクリックし、ダウンロードするディレクトリを選択"
echo "**************************************************"
echo "ファイルコピーが終了したら、「Enter」キーを押してください。"
echo "※ボタン押下により、収集したファイルをDBサーバから削除します。"
read Wait


######################################################################
#   delete directory
######################################################################
echo `date '+%Y/%m/%d %H:%M:%S'`"Remove Directory Start"                                                                >> $LOGFILE
cd /root
rm -rf ${USEDIR}
export RETURN_CODE=$?

if [ ${RETURN_CODE} -ne 0 ]; then
  echo `date '+%Y/%m/%d %H:%M:%S'`"Remove Directory Error"                                                              >> $LOGFILE
  echo `date '+%Y/%m/%d %H:%M:%S'`"********** FAILED **********"                                                        >> $LOGFILE
  exit 1
else 
  echo `date '+%Y/%m/%d %H:%M:%S'`"Remove Directory Success"                                                            >> $LOGFILE
  echo 
fi



