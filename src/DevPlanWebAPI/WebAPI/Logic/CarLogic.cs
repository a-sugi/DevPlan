using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 車両ロジッククラス
    /// </summary>
    /// <remarks>車両検索</remarks>
    public class CarLogic : BaseLogic
    {
        #region カーシェアデータの取得
        /// <summary>
        /// カーシェアデータの取得
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CarModel> Get(CarSearchModel val)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            switch (val.CAR_CLASS)
            {
                //カーシェア車
                case 1:
                    if (val.RESERVATION != 0)
                    {
                        var sqlCar = new StringBuilder();
                        sqlCar = SetCarShare(val);
                        sql.AppendLine(sqlCar.ToString());
                    }
                    break;

                //外製車(SJSB-G管理No付、参考)
                case 2:
                    if (val.RESERVATION != 0)
                    {
                        var sqlOut1 = new StringBuilder();
                        sqlOut1 = SetOutCar1(val);
                        sql.AppendLine(sqlOut1.ToString());
                    }
                    break;

                //外製車(SJSB-G管理No付、参考)
                case 3:
                    if (val.RESERVATION != 1)
                    {
                        var sqlOut2 = new StringBuilder();
                        sqlOut2 = SetOutCar2(val);
                        sql.AppendLine(sqlOut2.ToString());
                    }
                    break;

                //その他外製車(参考)
                case 4:
                    if (val.RESERVATION != 1)
                    {
                        var sqlOther = new StringBuilder();
                        sqlOther = SetOtherOutCar(val);
                        sql.AppendLine(sqlOther.ToString());
                    }
                    break;

                //専用車(参考)
                case 5:
                    if (val.RESERVATION != 1)
                    {
                        var sqlExclusive = new StringBuilder();
                        sqlExclusive = SetExclusiveCar(val);
                        sql.AppendLine(sqlExclusive.ToString());
                    }
                    break;

                //美深
                case 6:
                    if (val.RESERVATION != 1)
                    {
                        var sqlBifuka = new StringBuilder();
                        sqlBifuka = SetBifuka(val);
                        sql.AppendLine(sqlBifuka.ToString());
                    }
                    break;

                //全保有車両
                case 7:
                    var sqlAll = new StringBuilder();
                    sqlAll = this.SetAllCar();
                    sql.AppendLine(sqlAll.ToString());

                    break;
            }

            //対象の検索がない場合は終了
            if (sql.Length == 0)
            {
                return new List<CarModel>();
            }

            sql.AppendLine("ORDER BY 車系, 開発符号, メーカー名, 外製車名");

            if (val.EMPTY_DATE_FROM != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":EMPTY_DATE_FROM",
                    Type = OracleDbType.Date,
                    Object = val.EMPTY_DATE_FROM,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.EMPTY_DATE_TO != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":EMPTY_DATE_TO",
                    Type = OracleDbType.Date,
                    Object = val.EMPTY_DATE_TO,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.車系 != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":車系",
                    Type = OracleDbType.Varchar2,
                    Object = val.車系,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.駐車場番号 != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":駐車場番号",
                    Type = OracleDbType.Varchar2,
                    Object = val.駐車場番号,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.所在地 != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":所在地",
                    Type = OracleDbType.Varchar2,
                    Object = val.所在地,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.FLAG_ETC付 != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":FLAG_ETC付",
                    Type = OracleDbType.Int16,
                    Object = val.FLAG_ETC付,
                    Direct = ParameterDirection.Input
                });
            }
            if (val.トランスミッション != null)
            {
                prms.Add(new BindModel
                {
                    Name = ":トランスミッション",
                    Type = OracleDbType.Varchar2,
                    Object = val.トランスミッション,
                    Direct = ParameterDirection.Input
                });
            }

            IEnumerable <CarModel> list = db.ReadModelList<CarModel>(sql.ToString(), prms);

            foreach (CarModel data in list)
            {
                //予約現況を設定
                this.SetYoyakuGenkyou(data);

                //車検期限,車検期限まで残りを設定
                this.SetVehicleInspection(data);
            }

            return list;
        }
        #endregion

        #region 予約現況設定
        /// <summary>
        /// 予約現況設定
        /// </summary>
        /// <param name="data"></param>
        private void SetYoyakuGenkyou(CarModel data)
        {
            var sql = new StringBuilder();
            var prms = new List<BindModel>();

            if (data.予約現況 == "返却済み")
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("    A.\"END_DATE\"");
                sql.AppendLine("    ,C.\"NAME\"");
                sql.AppendLine("    ,B.\"TEL\"");
                sql.AppendLine("    ,E.\"SECTION_CODE\"");
                sql.AppendLine("FROM");
                sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
                sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
                sql.AppendLine("    INNER JOIN \"PERSONEL_LIST\" C ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" D ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_DATA\" E ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
                sql.AppendLine("WHERE");
                sql.AppendLine("    A.\"START_DATE\" <= sysdate");
                sql.AppendLine("    AND A.\"END_DATE\" > sysdate");
                sql.AppendLine("    AND A.\"CATEGORY_ID\" = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Varchar2,
                    Object = data.ID,
                    Direct = ParameterDirection.Input
                });

                List<YoyakuGenkyouModel> list = db.ReadModelList<YoyakuGenkyouModel>(sql.ToString(), prms);
                if (0 < list.Count)
                {
                    data.予約現況 = "返却済み。\n予約は、" + DateTimeUtil.ConvertToString(list[0].END_DATE, "yyyy/MM/dd H") + "時まで。\n";
                    data.予約現況 += "(予約者：" + list[0].SECTION_CODE + " " + list[0].NAME + " TEL:" + list[0].TEL + ")";
                }
            }
            else if (data.予約現況 == "貸出待ち")
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("    A.\"START_DATE\"");
                sql.AppendLine("    ,A.\"END_DATE\"");
                sql.AppendLine("    ,C.\"NAME\"");
                sql.AppendLine("    ,B.\"TEL\"");
                sql.AppendLine("    ,E.\"SECTION_CODE\"");
                sql.AppendLine("FROM");
                sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
                sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
                sql.AppendLine("    INNER JOIN \"PERSONEL_LIST\" C ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" D ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_DATA\" E ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
                sql.AppendLine("WHERE");
                sql.AppendLine("    A.\"START_DATE\" <= sysdate");
                sql.AppendLine("    AND A.\"END_DATE\" > sysdate");
                sql.AppendLine("    AND A.\"CATEGORY_ID\" = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Varchar2,
                    Object = data.ID,
                    Direct = ParameterDirection.Input
                });

                List<YoyakuGenkyouModel> list = db.ReadModelList<YoyakuGenkyouModel>(sql.ToString(), prms);
                if (0 < list.Count)
                {
                    data.予約現況 = "貸出待ち。\n予約期間は、" + DateTimeUtil.ConvertToString(list[0].START_DATE, "yyyy/MM/dd H");
                    data.予約現況 += "時 ～ " + DateTimeUtil.ConvertToString(list[0].END_DATE, "yyyy/MM/dd H") + "時。\n";
                    data.予約現況 += "(予約者：" + list[0].SECTION_CODE + " " + list[0].NAME + " TEL:" + list[0].TEL + ")";
                }
            }
            else if (data.予約現況 == "貸出中")
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("    A.\"END_DATE\"");
                sql.AppendLine("    ,C.\"NAME\"");
                sql.AppendLine("    ,B.\"TEL\"");
                sql.AppendLine("    ,B.\"行先\"");
                sql.AppendLine("    ,E.\"SECTION_CODE\"");
                sql.AppendLine("FROM");
                sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
                sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
                sql.AppendLine("    INNER JOIN \"PERSONEL_LIST\" C ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" D ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
                sql.AppendLine("    INNER JOIN \"SECTION_DATA\" E ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
                sql.AppendLine("WHERE");
                sql.AppendLine("    A.\"START_DATE\" <= sysdate");
                sql.AppendLine("    AND A.\"END_DATE\" > sysdate");
                sql.AppendLine("    AND A.\"CATEGORY_ID\" = :ID");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Varchar2,
                    Object = data.ID,
                    Direct = ParameterDirection.Input
                });

                List<YoyakuGenkyouModel> list = db.ReadModelList<YoyakuGenkyouModel>(sql.ToString(), prms);
                if (0 < list.Count)
                {
                    if (data.FLAG_空時間貸出可 != null && data.FLAG_空時間貸出可 == 1)
                    {
                        data.予約現況 = "貸出中( ～ " + DateTimeUtil.ConvertToString(list[0].END_DATE, "yyyy/MM/dd H") + "時@" + list[0].行先 + ")\n";
                        data.予約現況 += "(空き時間貸出可 " + list[0].SECTION_CODE + " " + list[0].NAME + " TEL:" + list[0].TEL + ")";
                    }
                    else
                    {
                        data.予約現況 = "貸出中( ～ " + DateTimeUtil.ConvertToString(list[0].END_DATE, "yyyy/MM/dd H") + "時@" + list[0].行先 + ")";
                    }
                }
            }
            else if (data.予約現況 == "空き")
            {
                sql.AppendLine("SELECT");
                sql.AppendLine("    A.\"START_DATE\"");
                sql.AppendLine("FROM");
                sql.AppendLine("    \"CARSHARING_SCHEDULE\" A");
                sql.AppendLine("WHERE");
                sql.AppendLine("    sysdate < A.\"START_DATE\"");
                sql.AppendLine("    AND A.\"CATEGORY_ID\" = :ID");
                sql.AppendLine("ORDER BY START_DATE");

                prms.Add(new BindModel
                {
                    Name = ":ID",
                    Type = OracleDbType.Varchar2,
                    Object = data.ID,
                    Direct = ParameterDirection.Input
                });

                List<YoyakuGenkyouModel> list = db.ReadModelList<YoyakuGenkyouModel>(sql.ToString(), prms);
                if (list.Count == 0)
                {
                    data.予約現況 = "空き。次の予約は無し。";
                }
                else if (0 < list.Count)
                {
                    data.予約現況 = "空き。次の予約は、" + DateTimeUtil.ConvertToString(list[0].START_DATE, "yyyy/MM/dd H") + "時 ～。";
                }

            }
            //前回予約者取得
            var sql_pre = new StringBuilder();
            var prms_pre = new List<BindModel>();

            sql_pre.AppendLine("SELECT");
            sql_pre.AppendLine("    TMP.*");
            sql_pre.AppendLine("FROM");
            sql_pre.AppendLine("(");
            sql_pre.AppendLine("    SELECT");
            sql_pre.AppendLine("        A.\"END_DATE\"");
            sql_pre.AppendLine("        ,C.\"NAME\"");
            sql_pre.AppendLine("        ,B.\"TEL\"");
            sql_pre.AppendLine("        ,E.\"SECTION_CODE\"");
            sql_pre.AppendLine("        ,ROW_NUMBER() OVER (PARTITION BY A.\"CATEGORY_ID\" ORDER BY A.\"END_DATE\" DESC) ROW_NUM");
            sql_pre.AppendLine("    FROM");
            sql_pre.AppendLine("        \"CARSHARING_SCHEDULE\" A");
            sql_pre.AppendLine("        INNER JOIN \"試験計画_外製車日程_目的行先\" B ON A.\"ID\" = B.\"SCHEDULE_ID\"");
            sql_pre.AppendLine("        INNER JOIN \"PERSONEL_LIST\" C ON B.\"予約者_ID\" = C.\"PERSONEL_ID\"");
            sql_pre.AppendLine("        INNER JOIN \"SECTION_GROUP_DATA\" D ON C.\"SECTION_GROUP_ID\" = D.\"SECTION_GROUP_ID\"");
            sql_pre.AppendLine("        INNER JOIN \"SECTION_DATA\" E ON D.\"SECTION_ID\" = E.\"SECTION_ID\"");
            sql_pre.AppendLine("    WHERE");
            sql_pre.AppendLine("        A.\"END_DATE\" < sysdate");
            sql_pre.AppendLine("        AND A.\"CATEGORY_ID\" = :ID");
            sql_pre.AppendLine("    ORDER BY");
            sql_pre.AppendLine("        A.\"END_DATE\" DESC");
            sql_pre.AppendLine(") TMP");
            sql_pre.AppendLine("WHERE");
            sql_pre.AppendLine("    TMP.ROW_NUM = 1");

            prms_pre.Add(new BindModel
            {
                Name = ":ID",
                Type = OracleDbType.Varchar2,
                Object = data.ID,
                Direct = ParameterDirection.Input
            });

            List<YoyakuGenkyouModel> list_pre = db.ReadModelList<YoyakuGenkyouModel>(sql_pre.ToString(), prms_pre);
            if (0 < list_pre.Count)
            {
                data.予約現況 += "\n(前回予約者：" + list_pre[0].SECTION_CODE + " " + list_pre[0].NAME + " TEL:" + list_pre[0].TEL + ")";
            }


        }
        #endregion

        #region 車検期限設定
        /// <summary>
        /// 車検期限設定
        /// </summary>
        /// <param name="data"></param>
        private void SetVehicleInspection(CarModel data)
        {
            if (data.車検区分1 == null || data.車検登録日 == null)
            {
                //対象外は処理終了
                return;
            }

            //満経過年数を計算
            int yearCount = this.GetElapsedYears((DateTime)data.車検登録日, DateTime.Now);
            if(yearCount == -1)
            {
                //車検登録日が未来（ありえない）
                return;
            }

            int year = 0;
            switch (data.車検区分1)
            {
                case "軽貨物":
                    int rest = yearCount % 2;

                    if (rest == 0)
                    {
                        //１年経過していないので２年後
                        year = yearCount + 2;
                    }
                    else if (rest == 1)
                    {
                        //１年経過しているので１年後
                        year = yearCount + 1;
                    }

                    break;

                case "小型貨物":

                    if (yearCount < 2)
                    {
                        //２年未満なので２年後
                        year = 2;
                    }
                    else
                    {
                        //２年経過後は毎年
                        year = yearCount + 1;
                    }

                    break;

                default:
                    if((data.車検区分1).Contains("乗用") == true)
                    {
                        if (yearCount < 3)
                        {
                            //３年未満なので３年後
                            year = 3;
                        }
                        else
                        {
                            //
                            year = yearCount - ((yearCount - 3) % 2) + 2;
                        }
                    }

                    break;
            }

            if(0 < year)
            {
                //車検期限設定
                DateTime date = (((DateTime)data.車検登録日).AddYears(year)).AddDays(-1);
                data.車検期限 = new DateTime(date.Year, date.Month, date.Day);

                //車検期限まで残り
                TimeSpan span = (DateTime)data.車検期限 - (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
                if (span.Days < 30)
                {
                    data.車検期限まで残り = span.Days + "日";
                }
                else
                {
                    data.車検期限まで残り = "約" + Convert.ToInt32(span.Days / 30) + "ヶ月";
                }
            }
        }

        /// <summary>
        /// 基準日baseDayからdayまでの経過月数を求める(DateTimeの時間部分に付いては考慮しない)
        /// </summary>
        /// <param name="baseDay"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public int GetElapsedMonths(DateTime baseDay, DateTime day)
        {
            if (day < baseDay)
            {
                // 日付が基準日より前の場合は例外とする
                return -1;
            }

            // 経過月数を求める(満月数を考慮しない単純計算)
            var elapsedMonths = (day.Year - baseDay.Year) * 12 + (day.Month - baseDay.Month);

            if (baseDay.Day <= day.Day)
            {                
                // baseDayの日部分がdayの日部分以上の場合は、その月を満了しているとみなす
                return elapsedMonths;
            }
            else if (day.Day == DateTime.DaysInMonth(day.Year, day.Month) && day.Day <= baseDay.Day)
            {
                // baseDayの日部分がdayの表す月の末日以降の場合は、その月を満了しているとみなす
                return elapsedMonths;
            }
            else
            {
                // それ以外の場合は、その月を満了していないとみなす
                return elapsedMonths - 1;
            }
        }

        /// <summary>
        /// 基準日baseDayからdayまでの経過年数を求める
        /// </summary>
        /// <param name="baseDay"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public int GetElapsedYears(DateTime baseDay, DateTime day)
        {
            // 経過月数÷12(端数切り捨て)したものを経過年数とする
            return GetElapsedMonths(baseDay, day) / 12;
        }
        #endregion

        #region カーシェア車検索
        /// <summary>
        /// カーシェア車検索
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetCarShare(CarSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT ");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"CARSHARING_SCHEDULE_ITEM\".\"GENERAL_CODE\" AS \"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,(CAST(");
            sql.AppendLine("        (SELECT COUNT(*) FROM");
            sql.AppendLine("            \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("            INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("        WHERE");
            sql.AppendLine("            \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("            AND \"試験計画_外製車日程_目的行先\".\"FLAG_空時間貸出可\" = 1");
            sql.AppendLine("            AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= SYSDATE");
            sql.AppendLine("            AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > SYSDATE");
            sql.AppendLine("        ) AS NUMBER(1)");
            sql.AppendLine("    )) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,NULL AS \"車検期限\"");
            sql.AppendLine("    ,NULL AS \"車検期限まで残り\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,\"CARSHARING_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("    ,\"CARSHARING_SCHEDULE_ITEM\".\"FLAG_CLASS\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) = 0");
            sql.AppendLine("            THEN '空き'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND \"試験計画_外製車日程_貸返備考\".\"FLAG_返却済\" = 1");
            sql.AppendLine("                    AND \"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" = 1");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '予約有り'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND \"試験計画_外製車日程_貸返備考\".\"FLAG_返却済\" = 1");
            sql.AppendLine("                    AND (\"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" IS NULL OR \"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" <> 1)");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '返却済み'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND (\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" IS NULL OR \"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" <> 1)");
            sql.AppendLine("                    AND \"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" = 1");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '予約有り'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND (\"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" IS NULL OR \"試験計画_外製車日程_目的行先\".\"FLAG_実使用\" <> 1)");
            sql.AppendLine("                    AND (\"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" IS NULL OR \"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\" <> 1)");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '貸出待ち'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND \"試験計画_外製車日程_目的行先\".\"FLAG_空時間貸出可\" = 1");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '貸出中'");
            sql.AppendLine("            WHEN");
            sql.AppendLine("                (SELECT COUNT(*) FROM");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"試験計画_外製車日程_貸返備考\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_貸返備考\".\"SCHEDULE_ID\"");
            sql.AppendLine("                    INNER JOIN \"PERSONEL_LIST\" ON \"試験計画_外製車日程_目的行先\".\"予約者_ID\" = \"PERSONEL_LIST\".\"PERSONEL_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_GROUP_DATA\" ON \"PERSONEL_LIST\".\"SECTION_GROUP_ID\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("                    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("                WHERE");
            sql.AppendLine("                    \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("                    AND (\"試験計画_外製車日程_目的行先\".\"FLAG_空時間貸出可\" IS NULL OR \"試験計画_外製車日程_目的行先\".\"FLAG_空時間貸出可\" <> 1)");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"START_DATE\" <= sysdate");
            sql.AppendLine("                    AND \"CARSHARING_SCHEDULE\".\"END_DATE\" > sysdate");
            sql.AppendLine("                ) > 0");
            sql.AppendLine("            THEN '貸出中'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 予約現況");
            sql.AppendLine("    ,\"車系情報\".\"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_車両リスト\" ON \"CARSHARING_SCHEDULE_ITEM\".\"CATEGORY_ID\" = \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_最終予約日\" ON \"CARSHARING_SCHEDULE_ITEM\".\"CATEGORY_ID\" = \"試験計画_外製車日程_最終予約日\".\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"VIEW_試験車基本情報\" ON \"試験計画_外製車日程_車両リスト\".\"管理票番号\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"車系情報\" ON \"VIEW_試験車基本情報\".\"車系\" = \"車系情報\".\"車系\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" = \'スバル\'");
            sql.AppendLine("    AND \"試験車履歴情報\".\"登録ナンバー\" IS NOT NULL");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");
            sql.AppendLine("    AND \"CARSHARING_SCHEDULE_ITEM\".\"CLOSED_DATE\" IS NULL");
            sql.AppendLine("    AND (\"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" IS NULL OR \"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" > TRUNC(SYSDATE) - 1)");
            sql.AppendLine("    AND \"SECTION_GROUP_DATA\".\"SECTION_ID\" = '367'");

            if (val.EMPTY_DATE_FROM != null && val.EMPTY_DATE_TO != null)
            {
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            \"CARSHARING_SCHEDULE\"");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
                sql.AppendLine("                            AND");
                sql.AppendLine("                                (");
                sql.AppendLine("                                    (\"CARSHARING_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_FROM AND :EMPTY_DATE_FROM < \"CARSHARING_SCHEDULE\".\"END_DATE\")");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (\"CARSHARING_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_TO AND :EMPTY_DATE_TO < \"CARSHARING_SCHEDULE\".\"END_DATE\")");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:EMPTY_DATE_FROM < \"CARSHARING_SCHEDULE\".\"START_DATE\" AND \"CARSHARING_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_TO)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:EMPTY_DATE_FROM < \"CARSHARING_SCHEDULE\".\"END_DATE\" AND \"CARSHARING_SCHEDULE\".\"END_DATE\" < :EMPTY_DATE_TO)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (\"CARSHARING_SCHEDULE\".\"START_DATE\" = :EMPTY_DATE_FROM AND \"CARSHARING_SCHEDULE\".\"END_DATE\" = :EMPTY_DATE_TO)");
                sql.AppendLine("                                )");
                sql.AppendLine("                    )");
            }
            if (val.車系 != null)
            {
                sql.AppendLine("    AND \"CARSHARING_SCHEDULE_ITEM\".\"GENERAL_CODE\" = :車系");
            }
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region 外製車検索1
        /// <summary>
        /// 外製車検索1
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetOutCar1(CarSearchModel val)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT ");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"OUTERCAR_SCHEDULE_ITEM\".\"GENERAL_CODE\" AS \"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,\"OUTERCAR_SCHEDULE_ITEM\".\"ID\"");
            sql.AppendLine("    ,\"OUTERCAR_SCHEDULE_ITEM\".\"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            //sql.AppendLine("FROM");
            //sql.AppendLine("    \"OUTERCAR_SCHEDULE_ITEM\"");
            //sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_車両リスト\" ON \"OUTERCAR_SCHEDULE_ITEM\".\"CATEGORY_ID\" = \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\"");
            //sql.AppendLine("    INNER JOIN \"VIEW_試験車基本情報\" ON \"試験計画_外製車日程_車両リスト\".\"管理票番号\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            //sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            //sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            //sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            //sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"VIEW_試験車基本情報\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_車両リスト\" ON \"VIEW_試験車基本情報\".\"管理票NO\" = \"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("    LEFT JOIN \"OUTERCAR_SCHEDULE_ITEM\" ON \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\" = \"OUTERCAR_SCHEDULE_ITEM\".\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_最終予約日\" ON \"OUTERCAR_SCHEDULE_ITEM\".\"CATEGORY_ID\" = \"試験計画_外製車日程_最終予約日\".\"CATEGORY_ID\"");
            //sql.AppendLine("    LEFT JOIN \"車系情報\" ON \"VIEW_試験車基本情報\".\"車系\" = \"車系情報\".\"車系\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'スバル\'");
            //Append Start 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'ｽﾊﾞﾙ\'");
            //Append End 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"登録ナンバー\" IS NOT NULL");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");
            sql.AppendLine("    AND \"OUTERCAR_SCHEDULE_ITEM\".\"CLOSED_DATE\" IS NULL");
            sql.AppendLine("    AND (\"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" IS NULL OR \"試験計画_外製車日程_最終予約日\".\"最終予約可能日\" > TRUNC(SYSDATE) - 1)");
            sql.AppendLine(string.Format("    AND \"SECTION_GROUP_DATA\".\"SECTION_ID\" IN (\'{0}\')", string.Join("\',\'", Const.SoukatsuSectionIds)));
            sql.AppendLine(string.Format("    AND \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\" NOT IN (\'{0}\')", string.Join("\',\'", Const.SoukatsuNotSectionGroupIds)));
            //sql.AppendLine("    AND \"OUTERCAR_SCHEDULE_ITEM\".\"CLOSED_DATE\" IS NULL");

            if (val.EMPTY_DATE_FROM != null && val.EMPTY_DATE_TO != null)
            {
                sql.AppendLine("    AND NOT EXISTS");
                sql.AppendLine("                    (");
                sql.AppendLine("                        SELECT");
                sql.AppendLine("                            *");
                sql.AppendLine("                        FROM");
                sql.AppendLine("                            \"OUTERCAR_SCHEDULE\"");
                sql.AppendLine("                        WHERE 0 = 0");
                sql.AppendLine("                            AND \"OUTERCAR_SCHEDULE_ITEM\".\"ID\" = \"OUTERCAR_SCHEDULE\".\"CATEGORY_ID\"");
                sql.AppendLine("                            AND");
                sql.AppendLine("                                (");
                sql.AppendLine("                                    (\"OUTERCAR_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_FROM AND :EMPTY_DATE_FROM < \"OUTERCAR_SCHEDULE\".\"END_DATE\")");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (\"OUTERCAR_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_TO AND :EMPTY_DATE_TO < \"OUTERCAR_SCHEDULE\".\"END_DATE\")");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:EMPTY_DATE_FROM < \"OUTERCAR_SCHEDULE\".\"START_DATE\" AND \"OUTERCAR_SCHEDULE\".\"START_DATE\" < :EMPTY_DATE_TO)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (:EMPTY_DATE_FROM < \"OUTERCAR_SCHEDULE\".\"END_DATE\" AND \"OUTERCAR_SCHEDULE\".\"END_DATE\" < :EMPTY_DATE_TO)");
                sql.AppendLine("                                    OR");
                sql.AppendLine("                                    (\"OUTERCAR_SCHEDULE\".\"START_DATE\" = :EMPTY_DATE_FROM AND \"OUTERCAR_SCHEDULE\".\"END_DATE\" = :EMPTY_DATE_TO)");

                sql.AppendLine("                                )");
                sql.AppendLine("                    )");
            }
            if (val.車系 != null)
            {
                sql.AppendLine("    AND \"OUTERCAR_SCHEDULE_ITEM\".\"GENERAL_CODE\" = :車系");
            }
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region 外製車検索2
        /// <summary>
        /// 外製車検索2
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetOutCar2(CarSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"管理票NO\" AS \"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,NULL AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"VIEW_試験車基本情報\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT  JOIN \"DEPARTMENT_DATA\" ON \"DEPARTMENT_DATA\".\"DEPARTMENT_ID\" = \"SECTION_DATA\".\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'スバル\'");
            //Append Start 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'ｽﾊﾞﾙ\'");
            //Append End 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"登録ナンバー\" IS NULL");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region その他外製車(参考)
        /// <summary>
        /// その他外製車(参考)
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetOtherOutCar(CarSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"DEPARTMENT_DATA\".\"DEPARTMENT_CODE\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"管理票NO\" AS \"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,NULL AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"VIEW_試験車基本情報\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT  JOIN \"DEPARTMENT_DATA\" ON \"DEPARTMENT_DATA\".\"DEPARTMENT_ID\" = \"SECTION_DATA\".\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"研実管理廃却申請受理日\" IS NULL");
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'スバル\'");
            //Append Start 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'ｽﾊﾞﾙ\'");
            //Append End 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND (");
            sql.AppendLine(string.Format("        \"SECTION_DATA\".\"SECTION_ID\" NOT IN (\'{0}\') OR \"SECTION_GROUP_DATA\".SECTION_GROUP_ID IN ('{1}')", string.Join("\',\'", Const.SoukatsuSectionIds), string.Join("\',\'", Const.SoukatsuNotSectionGroupIds)));
            sql.AppendLine(string.Format("        OR ((\"SECTION_DATA\".\"SECTION_ID\" IN (\'{0}\') AND \"SECTION_GROUP_DATA\".SECTION_GROUP_ID NOT IN ('{1}')) AND \"試験車履歴情報\".\"登録ナンバー\" IS NULL)", string.Join("\',\'", Const.SoukatsuSectionIds), string.Join("\',\'", Const.SoukatsuNotSectionGroupIds)));
            sql.AppendLine("        )");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region 専用車検索
        /// <summary>
        /// 専用車検索
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetExclusiveCar(CarSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"GENERAL_CODE\".\"CAR_GROUP\" AS \"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"管理票NO\" AS \"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,NULL AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"VIEW_試験車基本情報\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" ON \"試験車履歴情報\".\"開発符号\" = \"GENERAL_CODE\".\"GENERAL_CODE\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" = \'スバル\'");
            //Append Start 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"メーカー名\" <> \'ｽﾊﾞﾙ\'");
            //Append End 2023/10/21 メーカー名の絞り込み追加
            sql.AppendLine("    AND \"試験車履歴情報\".\"登録ナンバー\" IS NOT NULL");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");
            sql.AppendLine("    AND \"試験車履歴情報\".\"名称備考\" LIKE '%' || \'占有車\' || '%'");
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region 美深検索
        /// <summary>
        /// 美深検索
        /// </summary>
        /// <param name="val"></param>
        private StringBuilder SetBifuka(CarSearchModel val)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT ");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine("    ,\"CARSHARING_SCHEDULE_ITEM\".\"GENERAL_CODE\" AS \"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"所在地\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"試験計画_外製車日程_車両リスト\".\"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,NULL AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"CARSHARING_SCHEDULE_ITEM\"");
            sql.AppendLine("    LEFT JOIN \"CARSHARING_SCHEDULE\" ON \"CARSHARING_SCHEDULE_ITEM\".\"ID\" = \"CARSHARING_SCHEDULE\".\"CATEGORY_ID\"");
            sql.AppendLine("    INNER JOIN \"試験計画_外製車日程_車両リスト\" ON \"CARSHARING_SCHEDULE_ITEM\".\"CATEGORY_ID\" = \"試験計画_外製車日程_車両リスト\".\"CATEGORY_ID\"");
            sql.AppendLine("    LEFT JOIN \"試験計画_外製車日程_目的行先\" ON \"CARSHARING_SCHEDULE\".\"ID\" = \"試験計画_外製車日程_目的行先\".\"SCHEDULE_ID\"");
            sql.AppendLine("    INNER JOIN \"VIEW_試験車基本情報\" ON \"試験計画_外製車日程_車両リスト\".\"管理票番号\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    (\"試験計画_外製車日程_目的行先\".\"行先\" LIKE '%' || \'美深\' || '%'");
            sql.AppendLine("    OR \"試験計画_外製車日程_目的行先\".\"目的\" LIKE '%' || \'美深\' || '%'");
            sql.AppendLine("    OR \"CARSHARING_SCHEDULE\".\"DESCRIPTION\" LIKE '%' || \'美深\' || '%')");
            sql.AppendLine("    AND \"CARSHARING_SCHEDULE_ITEM\".\"CLOSED_DATE\" IS NULL");
            if (val.駐車場番号 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"駐車場番号\" LIKE '%' || :駐車場番号 || '%'");
            }
            if (val.所在地 != null)
            {
                sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"所在地\" = :所在地");
            }
            if (val.FLAG_ETC付 == 1)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" = 1");
            }
            else if (val.FLAG_ETC付 == 0)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"FLAG_ETC付\" != 1");
            }
            if (val.トランスミッション != null)
            {
                sql.AppendLine("    AND \"試験車履歴情報\".\"トランスミッション\" = :トランスミッション");
            }

            return sql;
        }
        #endregion

        #region 全保有車検索
        /// <summary>
        /// 全保有車検索
        /// </summary>
        private StringBuilder SetAllCar()
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT ");
            sql.AppendLine("     \"SCHEDULE_TO_XEYE\".\"物品コード\" AS \"今どこ\"");
            sql.AppendLine(" ,( ");
            sql.AppendLine("    CASE \"SECTION_DATA\".\"SECTION_ID\" ");
            sql.AppendLine("      WHEN '367' THEN 'カーシェア' ");
            sql.AppendLine("      ELSE ( ");
            sql.AppendLine("        CASE ");
            sql.AppendLine("          WHEN \"試験車履歴情報\".\"名称備考\" LIKE '%占有車%' ");
            sql.AppendLine("            THEN '専用車' ");
            sql.AppendLine("          ELSE '' ");
            sql.AppendLine("          END");
            sql.AppendLine("      ) END");
            sql.AppendLine("  ) AS 分類");
            sql.AppendLine("    ,\"GENERAL_CODE\".\"CAR_GROUP\" AS \"車系\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"開発符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"メーカー名\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"外製車名\"");
            sql.AppendLine("    ,\"SECTION_DATA\".\"SECTION_CODE\"");
            sql.AppendLine("    ,\"SECTION_GROUP_DATA\".\"SECTION_GROUP_CODE\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_空時間貸出可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"登録ナンバー\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"駐車場番号\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE LOWER(DEPARTMENT_DATA.ESTABLISHMENT)");
            sql.AppendLine("            WHEN 'g' THEN '群馬'");
            sql.AppendLine("            WHEN 't' THEN '東京'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 所在地");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_要予約許可\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ETC付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"FLAG_ナビ付\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"仕向地\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"排気量\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G型式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"駆動方式\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"トランスミッション\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"車型\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"グレード\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"型式符号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体色\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試作時期\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リース満了日\"");
            sql.AppendLine("    ,\"固定資産情報\".\"処分予定年月\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"管理票NO\" AS \"管理票番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車体番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"E_G番号\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"固定資産NO\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"リースNO\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命ナンバー\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"研命期間\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検登録日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"車検期限\"");
            sql.AppendLine("    ,(");
            sql.AppendLine("        CASE");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) = 0 THEN (試験車履歴情報.車検期限 - TRUNC(SYSDATE)) || '日'");
            sql.AppendLine("            WHEN TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) > 0 THEN '約' || TRUNC(MONTHS_BETWEEN(TRUNC(車検期限),TRUNC(SYSDATE))) || 'ヶ月'");
            sql.AppendLine("        END");
            sql.AppendLine("    ) AS 車検期限まで残り");
            sql.AppendLine("    ,\"試験車履歴情報\".\"廃艦日\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"号車\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"名称備考\"");
            sql.AppendLine("    ,\"試験車履歴情報\".\"試験目的\"");
            sql.AppendLine("    ,\"VIEW_試験車基本情報\".\"メモ\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(1)) AS \"FLAG_実使用\"");
            sql.AppendLine("    ,CAST(NULL AS NUMBER(10)) AS \"ID\"");
            sql.AppendLine("    ,NULL AS \"FLAG_CLASS\"");
            sql.AppendLine("    ,NULL AS \"予約現況\"");
            sql.AppendLine("    ,NULL AS \"車検区分1\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"VIEW_試験車基本情報\"");
            sql.AppendLine("    INNER JOIN \"試験車履歴情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"固定資産情報\" ON \"VIEW_試験車基本情報\".\"データID\" = \"固定資産情報\".\"データID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_GROUP_DATA\" ON \"試験車履歴情報\".\"管理責任部署\" = \"SECTION_GROUP_DATA\".\"SECTION_GROUP_ID\"");
            sql.AppendLine("    INNER JOIN \"SECTION_DATA\" ON \"SECTION_GROUP_DATA\".\"SECTION_ID\" = \"SECTION_DATA\".\"SECTION_ID\"");
            sql.AppendLine("    INNER JOIN \"DEPARTMENT_DATA\" ON \"SECTION_DATA\".\"DEPARTMENT_ID\" = \"DEPARTMENT_DATA\".\"DEPARTMENT_ID\"");
            sql.AppendLine("    LEFT JOIN \"GENERAL_CODE\" ON \"試験車履歴情報\".\"開発符号\" = \"GENERAL_CODE\".\"GENERAL_CODE\"");
            sql.AppendLine("    LEFT JOIN \"SCHEDULE_TO_XEYE\" ON \"SCHEDULE_TO_XEYE\".\"物品名2\" = \"VIEW_試験車基本情報\".\"管理票NO\"");
            sql.AppendLine("WHERE");
            sql.AppendLine("    \"VIEW_試験車基本情報\".\"廃却決済承認年月\" IS NULL");
            sql.AppendLine("    AND \"VIEW_試験車基本情報\".\"車両搬出日\" IS NULL");
            sql.AppendLine("    AND (\"試験車履歴情報\".\"履歴NO\" = (SELECT MAX(\"試験車履歴情報\".\"履歴NO\") FROM \"試験車履歴情報\" WHERE (\"VIEW_試験車基本情報\".\"データID\" = \"試験車履歴情報\".\"データID\") GROUP BY \"試験車履歴情報\".\"データID\"))");

            return sql;
        }
        #endregion
    }
}