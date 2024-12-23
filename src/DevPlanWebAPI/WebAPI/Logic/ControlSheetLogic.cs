using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 管理票検索業務ロジッククラス
    /// </summary>
    public class ControlSheetLogic : TestCarBaseLogic
    {
        #region メンバ変数
        private const string Syamei = "車名";
        private const string KaihatuHugou = "開発符号";
        private const string JyuryouBusyo = "受領部署";
        private const string KanriSekininBusyo = "管理責任部署";

        private readonly Dictionary<string, string> WhereMap = new Dictionary<string, string>
        {
            { "かつ", "AND" },
            { "または", "OR" }

        };

        private enum RangeType : int
        {
            //のみ
            Only = 0,

            //範囲
            Range = 1,

            //大なり
            Greater = 2,

            //小なり
            Less = 3

        }
        #endregion

        #region 試験車取得
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TestCarCommonModel> GetTestCar(ControlSheetModel cond)
        {
            var parm = new List<BindModel>
            {
                new BindModel { Name = ":管理票発行有無", Object = "済", Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

            };

            //ユーザー検索項目
            var searchItem = this.GetUserSearchItem();

            //SQL
            //Update Start 2021/07/08 矢作
            //var sql = base.GetBaseTestCarSql(parm);
            var sql = base.GetBaseTestCarSql(parm, !cond.ControlSheetSearch.改修前車両検索対象);
            //Update End 2021/07/08 矢作

            var i = 0;

            Action<object, OracleDbType> addParm = (value, type) => parm.Add(new BindModel { Name = string.Format(":{0}", i), Object = value, Type = type, Direct = ParameterDirection.Input });

            Action<string, string, string> set = (tableName, columnName, value) => base.SetStringWhere(sql, parm, tableName, columnName, value);

            Func <string, bool> convertFlg = s => string.IsNullOrWhiteSpace(s) == true ? false : Convert.ToBoolean(s);

            //ユーザー指定の条件があるかどうか
            var searchConditionList = cond.ControlSheetSearchConditionList == null ? null : cond.ControlSheetSearchConditionList.Where(x => x.行番号 >= 0).ToArray();
            if (searchConditionList != null && searchConditionList.Any() == true)
            {
                sql.AppendLine("    AND");
                sql.AppendLine("        (");

                foreach (var c in searchConditionList)
                {
                    //左括弧
                    if (string.IsNullOrWhiteSpace(c.BEGIN_KAKKO) == false)
                    {
                        sql.AppendFormat("            {0}", c.BEGIN_KAKKO).AppendLine();

                    }

                    sql.Append("            ");

                    //添字
                    if (string.IsNullOrWhiteSpace(c.CONJUNCTION) == false)
                    {
                        sql.AppendFormat("{0} ", WhereMap[c.CONJUNCTION]);

                    }

                    //条件あるかどうか
                    var item = searchItem.FirstOrDefault(x => x.Index == c.ELEM);
                    if (item == null || string.IsNullOrWhiteSpace(c.TEXT) == true)
                    {
                        sql = new StringBuilder(sql.ToString().Trim());

                    }
                    else
                    {
                        var target = string.Format("{0}.\"{1}\"", item.TableName, item.ColumnName);

                        //Append Start 2023/03/28 杉浦 「EVデバイス」⇒「電動デバイス」に変更
                        if (item.ColumnName.Equals("電動デバイス"))
                        {
                            target = string.Format("{0}.\"{1}\"", item.TableName, "EVデバイス");
                        }
                        //Append End 2023/03/28 杉浦 「EVデバイス」⇒「電動デバイス」に変更

                        //Append Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                        if (item.ColumnName.Contains("_old"))
                        {
                            target = string.Format("{0}.\"{1}\"", item.TableName, item.ColumnName.Replace("_old", ""));
                        }
                        if (item.ColumnName.Equals("登録年月日"))
                        {
                            target = string.Format("{0}.\"{1}\"", item.TableName, "車検登録日");
                        }
                        if (item.ColumnName.Equals("廃艦年月日"))
                        {
                            target = string.Format("{0}.\"{1}\"", item.TableName, "廃艦日");
                        }
                        //Append End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)

                        var where = new StringBuilder();

                        //抽出対象
                        where.AppendFormat("{0}", target);

                        //文字列の検索かどうか
                        if (item.SearchDataType == SearchDataType.String)
                        {
                            //変換関数セット
                            base.SetConvert(ref where);

                        }
                        where.Append(" ");

                        //NULLが対象かどうか
                        if (convertFlg(c.NULLFLAG) == true)
                        {
                            where.Append(convertFlg(c.NOTFLAG) == true ? "IS NOT NULL" : "IS NULL");

                        }
                        else
                        {
                            var value = c.STR ?? "";

                            //検索種別ごとの分岐
                            switch (item.SearchDataType)
                            {
                                //文字列
                                case SearchDataType.String:
                                    //完全一致させるかどうか
                                    if (value.Contains(Asterisk) == false)
                                    {
                                        where.Append("= ");

                                    }
                                    else
                                    {
                                        value = value.Replace(Asterisk, Like);

                                        where.Append("LIKE ");

                                    }

                                    where.AppendFormat(ConvertFunc, string.Format(":{0}", ++i));

                                    //パラメータ追加
                                    addParm(value, OracleDbType.Varchar2);
                                    break;

                                //数値
                                case SearchDataType.Number:
                                    //数値範囲条件ごとの分岐
                                    switch ((RangeType)c.NUMMODE)
                                    {
                                        //のみ
                                        case RangeType.Only:
                                            where.AppendFormat("= :{0}", ++i);

                                            //パラメータ追加
                                            addParm(c.FROMNUM, OracleDbType.Int32);
                                            break;

                                        //範囲
                                        case RangeType.Range:
                                            var fromNum = c.FROMNUM <= c.TONUM ? c.FROMNUM : c.TONUM;
                                            var toNum = c.TONUM >= c.FROMNUM ? c.TONUM : c.FROMNUM;

                                            //パラメータ追加
                                            where.AppendFormat("BETWEEN :{0} ", ++i);
                                            addParm(fromNum, OracleDbType.Int32);

                                            //パラメータ追加
                                            where.AppendFormat("AND :{0}", ++i);
                                            addParm(toNum, OracleDbType.Int32);
                                            break;

                                        //大なり
                                        case RangeType.Greater:
                                            where.AppendFormat(">= :{0}", ++i);

                                            //パラメータ追加
                                            addParm(c.FROMNUM, OracleDbType.Int32);
                                            break;

                                        //小なり
                                        case RangeType.Less:
                                            where.AppendFormat("<= :{0}", ++i);

                                            //パラメータ追加
                                            addParm(c.FROMNUM, OracleDbType.Int32);
                                            break;

                                    }
                                    break;

                                //日付
                                case SearchDataType.Date:
                                    var fromDate = DateTimeUtil.GetDate(c.FROMDATE);
                                    var toDate = DateTimeUtil.GetDate(c.TODATE);

                                    //日付範囲条件ごとの分岐
                                    switch ((RangeType)c.DATEMODE)
                                    {
                                        //のみ
                                        case RangeType.Only:
                                            //日付種別ごとの分岐
                                            switch (item.DateType)
                                            {
                                                //年月
                                                case DateType.Month:
                                                    //パラメータ追加
                                                    where.AppendFormat(">= :{0} AND {1} ", ++i, target);
                                                    addParm(fromDate, OracleDbType.Date);

                                                    //パラメータ追加
                                                    where.AppendFormat("< (:{0} + 1)", ++i);
                                                    addParm(DateTimeUtil.GetLastDate(fromDate), OracleDbType.Date);
                                                    break;

                                                //その他
                                                default:
                                                    //パラメータ追加
                                                    where.AppendFormat(">= :{0} AND {1} < (:{0} + 1)", ++i, target);
                                                    addParm(fromDate, OracleDbType.Date);
                                                    break;

                                            }
                                            break;

                                        //範囲
                                        case RangeType.Range:
                                            fromDate = DateTimeUtil.GetDate(c.FROMDATE <= c.TODATE ? c.FROMDATE : c.TODATE);
                                            toDate = DateTimeUtil.GetDate(c.TODATE >= c.FROMDATE ? c.TODATE : c.FROMDATE);

                                            //パラメータ追加
                                            where.AppendFormat(">= :{0} AND {1} ", ++i, target);
                                            addParm(fromDate, OracleDbType.Date);

                                            //パラメータ追加
                                            where.AppendFormat("< (:{0} + 1)", ++i);
                                            addParm(item.DateType == DateType.Month ? DateTimeUtil.GetLastDate(toDate) : toDate, OracleDbType.Date);
                                            break;

                                        //大なり
                                        case RangeType.Greater:
                                            where.AppendFormat(">= :{0}", ++i);

                                            //パラメータ追加
                                            addParm(fromDate, OracleDbType.Date);
                                            break;

                                        //小なり
                                        case RangeType.Less:
                                            where.AppendFormat("< (:{0} + 1)", ++i);

                                            //パラメータ追加
                                            addParm(item.DateType == DateType.Month ? DateTimeUtil.GetLastDate(fromDate) : fromDate, OracleDbType.Date);
                                            break;

                                    }
                                    break;

                                //マスタ
                                case SearchDataType.Master:
                                    //マスタの値を選択しているかどうか
                                    if (c.INDEX_NO == -1)
                                    {
                                        //部分一致させるかどうか
                                        if (value.Contains(Asterisk) == true)
                                        {
                                            value = value.Replace(Asterisk, Like);

                                            where.AppendFormat("LIKE :{0}", ++i);

                                        }
                                        else
                                        {
                                            where.AppendFormat("= :{0}", ++i);

                                        }

                                    }
                                    else
                                    {
                                        //列名ごとの分岐
                                        switch (item.ColumnName)
                                        {
                                            //車名
                                            case Syamei:
                                                where.Clear();
                                                where.AppendFormat("KR.\"{0}\" IN (SELECT A.\"{0}\" FROM \"{1}\" A WHERE A.\"{2}\" ", KaihatuHugou, item.MasterTableName, item.MasterNameColumnName);
                                                break;

                                            //開発符号
                                            case KaihatuHugou:
                                                where.AppendFormat("IN (SELECT A.\"{0}\" FROM \"{1}\" A INNER JOIN (SELECT A.\"データID\", MAX(A.\"履歴NO\") AS \"履歴NO\" FROM \"試験車履歴情報\" A WHERE 0 = 0 GROUP BY A.\"データID\") B ON A.\"データID\" = B.\"データID\" AND A.\"履歴NO\" = B.\"履歴NO\" WHERE A.\"{0}\" ", item.MasterCodeColumnName, item.MasterTableName, item.MasterNameColumnName);
                                                break;

                                            default:
                                                where.AppendFormat("IN (SELECT A.\"{0}\" FROM \"{1}\" A WHERE A.\"{2}\" ", item.MasterCodeColumnName, item.MasterTableName, item.MasterNameColumnName);
                                                break;
                                        }

                                        //受領部署か管理責任部署かどうか
                                        if (item.ColumnName == JyuryouBusyo || item.ColumnName == KanriSekininBusyo)
                                        {
                                            //常に部分一致
                                            value = value.Replace(Asterisk, Like);

                                            where.AppendFormat("LIKE '%' || :{0} || '%'", ++i);

                                        }
                                        else
                                        {
                                            where.AppendFormat("= :{0}", ++i);

                                        }

                                        where.Append(")");

                                    }

                                    //パラメータ追加
                                    addParm(value, OracleDbType.Varchar2);
                                    break;

                            }

                            //条件否定かどうか
                            if (convertFlg(c.NOTFLAG) == true)
                            {
                                where.Insert(0, "NOT(");
                                where.Append(")");

                            }

                        }

                        sql.Append(where.ToString());

                    }

                    sql.AppendLine();

                    //右括弧
                    if (string.IsNullOrWhiteSpace(c.END_KAKKO) == false)
                    {
                        sql.AppendFormat("            {0}", c.END_KAKKO).AppendLine();

                    }

                }

                sql.AppendLine("        )");

                //ユーザー検索条件を登録
                this.MergeUserSearch(searchConditionList);

            }
            else
            {
                //条件
                set("KK", "管理票NO", cond.ControlSheetSearch?.管理票NO);
                set("KR", "固定資産NO", cond.ControlSheetSearch?.固定資産NO);
                set("KR", "車体番号", cond.ControlSheetSearch?.車体番号);
                set("KR", "号車", cond.ControlSheetSearch?.号車);
                set("KR", "登録ナンバー", cond.ControlSheetSearch?.登録ナンバー);
                set("KK", "駐車場番号", cond.ControlSheetSearch?.駐車場番号);

            }

            //条件
            set("DD", "ESTABLISHMENT", cond.ControlSheetSearch?.ESTABLISHMENT);

            //ソート順
            sql.AppendLine("ORDER BY");
            //Update Start 2021/07/08 矢作
            //sql.AppendLine("    KK.\"管理票NO\"");
            sql.AppendLine("    KK.\"管理票NO\", KR.\"履歴NO\"");
            //Update End 2021/07/08 矢作

            //取得
            return db.ReadModelList<TestCarCommonModel>(sql.ToString(), parm);

        }

        /// <summary>
        /// ユーザー検索条件を登録
        /// </summary>
        /// <param name="list">ユーザー検索条件情報</param>
        /// <returns></returns>
        private bool MergeUserSearch(IEnumerable<ControlSheetSearchConditionModel> list)
        {
            //対象が無ければ終了
            var target = list.Where(x => x.行番号 >= 0).Select(x => new { x.ユーザーID, x.条件名 }).Distinct().Where(x => string.IsNullOrWhiteSpace(x.ユーザーID) == false && string.IsNullOrWhiteSpace(x.条件名) == false).ToArray();
            if (target.Any() == false)
            {
                return true;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"USER_SEARCH_CONDITION\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             :PERSONEL_ID AS \"PERSONEL_ID\"");
            sql.AppendLine("            ,:NAME AS \"NAME\"");
            sql.AppendLine("            ,1 AS \"SEARCH_COUNT\"");
            sql.AppendLine("            ,SYSTIMESTAMP AS \"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("            ,SYSTIMESTAMP AS \"INPUT_DATETIME\"");
            sql.AppendLine("            ,:PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("            ,SYSTIMESTAMP AS \"UPDATE_DATETIME\"");
            sql.AppendLine("            ,:PERSONEL_ID AS \"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            DUAL");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"PERSONEL_ID\" = B.\"PERSONEL_ID\"");
            sql.AppendLine("        AND A.\"NAME\" = B.\"NAME\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"SEARCH_COUNT\" = (A.\"SEARCH_COUNT\" + 1)");
            sql.AppendLine("        ,\"LAST_SEARCH_DATETIME\" = B.\"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"PERSONEL_ID\"");
            sql.AppendLine("        ,\"NAME\"");
            sql.AppendLine("        ,\"SEARCH_COUNT\"");
            sql.AppendLine("        ,\"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("        ,\"INPUT_DATETIME\"");
            sql.AppendLine("        ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,\"UPDATE_DATETIME\"");
            sql.AppendLine("        ,\"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"NAME\"");
            sql.AppendLine("        ,B.\"SEARCH_COUNT\"");
            sql.AppendLine("        ,B.\"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("        ,B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"UPDATE_DATETIME\"");
            sql.AppendLine("        ,B.\"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var cond in target)
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":PERSONEL_ID", Object = cond.ユーザーID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME", Object = cond.条件名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                };

                //ユーザー検索条件の登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        #endregion

        #region ユーザー検索条件情報取得
        /// <summary>
        /// ユーザー検索条件情報取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ControlSheetSearchConditionModel> GetUserSearchCondition(ControlSheetSearchConditionSearchModel cond)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ユーザーID\"");
            sql.AppendLine("    ,A.\"条件名\"");
            sql.AppendLine("    ,A.\"行番号\"");
            sql.AppendLine("    ,TRIM(A.\"BEGIN_KAKKO\") AS \"BEGIN_KAKKO\"");
            sql.AppendLine("    ,TRIM(A.\"CONJUNCTION\") AS \"CONJUNCTION\"");
            sql.AppendLine("    ,A.\"ELEM\"");
            sql.AppendLine("    ,A.\"TEXT\"");
            sql.AppendLine("    ,A.\"STR\"");
            sql.AppendLine("    ,A.\"FROMNUM\"");
            sql.AppendLine("    ,A.\"NUMMODE\"");
            sql.AppendLine("    ,A.\"TONUM\"");
            sql.AppendLine("    ,A.\"FROMDATE\"");
            sql.AppendLine("    ,A.\"DATEMODE\"");
            sql.AppendLine("    ,A.\"TODATE\"");
            sql.AppendLine("    ,A.\"INDEX_NO\"");
            sql.AppendLine("    ,A.\"NOTFLAG\"");
            sql.AppendLine("    ,A.\"NULLFLAG\"");
            sql.AppendLine("    ,TRIM(A.\"END_KAKKO\") AS \"END_KAKKO\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"ユーザー検索条件情報\" A");
            sql.AppendLine("    LEFT JOIN \"USER_SEARCH_CONDITION\" B");
            sql.AppendLine("    ON A.\"ユーザーID\" = B.\"PERSONEL_ID\"");
            sql.AppendLine("    AND A.\"条件名\" = B.\"NAME\"");
            sql.AppendLine("WHERE 0 = 0");

            //ユーザーID
            if (string.IsNullOrWhiteSpace(cond.ユーザーID) == false)
            {
                sql.AppendLine("    AND A.\"ユーザーID\" = :ユーザーID");

                prms.Add(new BindModel { Name = ":ユーザーID", Object = cond.ユーザーID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

            }

            //条件名
            if (string.IsNullOrWhiteSpace(cond.条件名) == false)
            {
                sql.AppendLine("    AND A.\"条件名\" = :条件名");

                prms.Add(new BindModel { Name = ":条件名", Object = cond.条件名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input });

            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.\"条件名\"");

            //取得
            return db.ReadModelList<ControlSheetSearchConditionModel>(sql.ToString(), prms);

        }
        #endregion

        #region ユーザー検索項目取得
        /// <summary>
        /// ユーザー検索項目取得
        /// </summary>
        /// <returns></returns>
        public List<UserSearchItemModel> GetUserSearchItem()
        {
            return new List<UserSearchItemModel>
            {
                new UserSearchItemModel { Index = 0, TableName = "KK", ColumnName = "管理票NO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 1, TableName = "KR", ColumnName = "発行年月日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 2, TableName = "KH", ColumnName = "車名", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "開発符号情報", MasterCodeColumnName = "車名", MasterNameColumnName = "車名" },
                new UserSearchItemModel { Index = 3, TableName = "KK", ColumnName = "車系", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "車系情報", MasterCodeColumnName = "車系", MasterNameColumnName = "車系" },
                new UserSearchItemModel { Index = 4, TableName = "KK", ColumnName = "車型", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "車型情報", MasterCodeColumnName = "車型", MasterNameColumnName = "車型" },
                new UserSearchItemModel { Index = 5, TableName = "KK", ColumnName = "型式符号", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 6, TableName = "KR", ColumnName = "開発符号", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "試験車履歴情報", MasterCodeColumnName = "開発符号", MasterNameColumnName = "開発符号" },
                new UserSearchItemModel { Index = 7, TableName = "KR", ColumnName = "試作時期", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "試作時期情報", MasterCodeColumnName = "試作時期", MasterNameColumnName = "試作時期" },
                new UserSearchItemModel { Index = 8, TableName = "KR", ColumnName = "号車", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 9, TableName = "KR", ColumnName = "仕向地", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "仕向地情報", MasterCodeColumnName = "仕向地", MasterNameColumnName = "仕向地" },
                new UserSearchItemModel { Index = 10, TableName = "KR", ColumnName = "メーカー名", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "メーカー名情報", MasterCodeColumnName = "メーカー名", MasterNameColumnName = "メーカー名" },
                new UserSearchItemModel { Index = 11, TableName = "KR", ColumnName = "外製車名", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 12, TableName = "KR", ColumnName = "名称備考", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 13, TableName = "KR", ColumnName = "車体番号", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 14, TableName = "KR", ColumnName = "E_G番号", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 15, TableName = "KR", ColumnName = "E_G型式", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "E_G型式情報", MasterCodeColumnName = "E_G型式", MasterNameColumnName = "E_G型式" },
                new UserSearchItemModel { Index = 16, TableName = "KR", ColumnName = "排気量", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "排気量情報", MasterCodeColumnName = "排気量", MasterNameColumnName = "排気量" },
                new UserSearchItemModel { Index = 17, TableName = "KR", ColumnName = "トランスミッション", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "トランスミッション情報", MasterCodeColumnName = "トランスミッション", MasterNameColumnName = "トランスミッション" },
                new UserSearchItemModel { Index = 18, TableName = "KR", ColumnName = "駆動方式", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "駆動方式情報", MasterCodeColumnName = "駆動方式", MasterNameColumnName = "駆動方式" },
                new UserSearchItemModel { Index = 19, TableName = "KR", ColumnName = "グレード", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 20, TableName = "KR", ColumnName = "車体色", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "車体色情報", MasterCodeColumnName = "車体色", MasterNameColumnName = "車体色" },
                new UserSearchItemModel { Index = 21, TableName = "KK", ColumnName = "駐車場番号", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "駐車場情報", MasterCodeColumnName = "駐車場番号", MasterNameColumnName = "駐車場番号" },
                new UserSearchItemModel { Index = 22, TableName = "KR", ColumnName = "試験目的", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 23, TableName = "KR", ColumnName = "受領日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 24, TableName = "KR", ColumnName = "受領部署", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "VIEW_BUSYO_LIST", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "BUSYO_LIST" },
                new UserSearchItemModel { Index = 25, TableName = "KR", ColumnName = "受領者", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "PERSONEL_LIST", MasterCodeColumnName = "PERSONEL_ID", MasterNameColumnName = "NAME" },
                new UserSearchItemModel { Index = 26, TableName = "KR", ColumnName = "受領時走行距離", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 27, TableName = "KK", ColumnName = "正式取得日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 28, TableName = "KR", ColumnName = "完成日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 29, TableName = "KR", ColumnName = "管理責任部署", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "VIEW_BUSYO_LIST", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "BUSYO_LIST" },
                new UserSearchItemModel { Index = 30, TableName = "KR", ColumnName = "研命ナンバー", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 31, TableName = "KR", ColumnName = "研命期間", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 31, TableName = "KR", ColumnName = "研命期間_old", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 32, TableName = "KR", ColumnName = "固定資産NO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 33, TableName = "KR", ColumnName = "登録ナンバー", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 34, TableName = "KR", ColumnName = "車検登録日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 35, TableName = "KR", ColumnName = "廃艦日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 36, TableName = "KR", ColumnName = "保険NO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 37, TableName = "KR", ColumnName = "保険加入日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 38, TableName = "KR", ColumnName = "保険解約日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 34, TableName = "KR", ColumnName = "登録年月日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 35, TableName = "KR", ColumnName = "廃艦年月日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 36, TableName = "KR", ColumnName = "保険NO_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 37, TableName = "KR", ColumnName = "保険加入日_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 38, TableName = "KR", ColumnName = "保険解約日_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 39, TableName = "KK", ColumnName = "リースNO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 40, TableName = "KK", ColumnName = "リース満了日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 41, TableName = "KR", ColumnName = "移管依頼NO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 41, TableName = "KR", ColumnName = "移管依頼NO_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 42, TableName = "KR", ColumnName = "試験着手日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 43, TableName = "KR", ColumnName = "試験着手証明文書", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 44, TableName = "KR", ColumnName = "工事区分NO", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 45, TableName = "KK", ColumnName = "研実管理廃却申請受理日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 46, TableName = "KK", ColumnName = "廃却見積日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 47, TableName = "KK", ColumnName = "廃却決済承認年月", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 48, TableName = "KK", ColumnName = "車両搬出日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 49, TableName = "KK", ColumnName = "廃却見積額", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 50, TableName = "KK", ColumnName = "貸与先", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 51, TableName = "KK", ColumnName = "貸与返却予定期限", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 52, TableName = "KK", ColumnName = "貸与返却日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 53, TableName = "KR", ColumnName = "自動車税", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 54, TableName = "KR", ColumnName = "保険料", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 55, TableName = "KK", ColumnName = "棚卸実施日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 49, TableName = "KK", ColumnName = "廃却見積額_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 50, TableName = "KK", ColumnName = "貸与先_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 51, TableName = "KK", ColumnName = "貸与返却予定期限_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 52, TableName = "KK", ColumnName = "貸与返却日_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 53, TableName = "KR", ColumnName = "自動車税_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 54, TableName = "KR", ColumnName = "保険料_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 55, TableName = "KK", ColumnName = "棚卸実施日_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 56, TableName = "KK", ColumnName = "メモ", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 57, TableName = "KS", ColumnName = "勘定科目", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 58, TableName = "KS", ColumnName = "子資産", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 59, TableName = "KS", ColumnName = "所得年月", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 60, TableName = "KS", ColumnName = "設置場所", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 61, TableName = "KS", ColumnName = "耐用年数", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 62, TableName = "KS", ColumnName = "取得価額", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 63, TableName = "KS", ColumnName = "減価償却累計額", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 64, TableName = "KS", ColumnName = "期末簿価", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 65, TableName = "KS", ColumnName = "資産タイプ", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 66, TableName = "KS", ColumnName = "固定資産税", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 67, TableName = "KS", ColumnName = "原価部門", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 68, TableName = "KS", ColumnName = "管理部署", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "SECTION_GROUP_DATA", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "SECTION_GROUP_CODE" },
                //new UserSearchItemModel { Index = 69, TableName = "KS", ColumnName = "資産計上部署", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "SECTION_GROUP_DATA", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "SECTION_GROUP_CODE" },
                //new UserSearchItemModel { Index = 70, TableName = "KS", ColumnName = "事業所コード", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 57, TableName = "KS", ColumnName = "勘定科目_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 58, TableName = "KS", ColumnName = "子資産_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 59, TableName = "KS", ColumnName = "所得年月_old", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 60, TableName = "KS", ColumnName = "設置場所_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 61, TableName = "KS", ColumnName = "耐用年数_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 62, TableName = "KS", ColumnName = "取得価額_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 63, TableName = "KS", ColumnName = "減価償却累計額_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 64, TableName = "KS", ColumnName = "期末簿価_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 65, TableName = "KS", ColumnName = "資産タイプ_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 66, TableName = "KS", ColumnName = "固定資産税_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 67, TableName = "KS", ColumnName = "原価部門_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 68, TableName = "KS", ColumnName = "管理部署_old", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "SECTION_GROUP_DATA", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "SECTION_GROUP_CODE" },
                new UserSearchItemModel { Index = 69, TableName = "KS", ColumnName = "資産計上部署_old", SearchDataType = SearchDataType.Master, DateType = DateType.None, MasterTableName = "SECTION_GROUP_DATA", MasterCodeColumnName = "SECTION_GROUP_ID", MasterNameColumnName = "SECTION_GROUP_CODE" },
                new UserSearchItemModel { Index = 70, TableName = "KS", ColumnName = "事業所コード_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/05/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 71, TableName = "KS", ColumnName = "処分コード", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 72, TableName = "KS", ColumnName = "処分予定年月", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update Start 2023/5/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //new UserSearchItemModel { Index = 73, TableName = "KS", ColumnName = "処分数", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 74, TableName = "KS", ColumnName = "処分区分", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 75, TableName = "KS", ColumnName = "除却年度", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 76, TableName = "KS", ColumnName = "除却明細名称", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 77, TableName = "KI", ColumnName = "開本内移管履歴NO", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 78, TableName = "KI", ColumnName = "開本内移管日", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //new UserSearchItemModel { Index = 79, TableName = "KI", ColumnName = "内容", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 73, TableName = "KS", ColumnName = "処分数_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 74, TableName = "KS", ColumnName = "処分区分_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 75, TableName = "KS", ColumnName = "除却年度_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 76, TableName = "KS", ColumnName = "除却明細名称_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 77, TableName = "KI", ColumnName = "開本内移管履歴NO_old", SearchDataType = SearchDataType.Number, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 78, TableName = "KI", ColumnName = "開本内移管日_old", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 79, TableName = "KI", ColumnName = "内容_old", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/5/11 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                //Update Start 2023/03/28 杉浦 「EVデバイス」⇒「電動デバイス」に変更
                //new UserSearchItemModel { Index = 80, TableName = "KR", ColumnName = "EVデバイス", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 80, TableName = "KR", ColumnName = "電動デバイス", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Update End 2023/03/28 杉浦 「EVデバイス」⇒「電動デバイス」に変更
                new UserSearchItemModel { Index = 81, TableName = "KR", ColumnName = "車検期限", SearchDataType = SearchDataType.Date, DateType = DateType.Date, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 82, TableName = "KR", ColumnName = "初年度登録年月", SearchDataType = SearchDataType.Date, DateType = DateType.Month, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                
                //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
                new UserSearchItemModel { Index = 83, TableName = "KR", ColumnName = "自動車ﾘｻｲｸﾙ法", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                new UserSearchItemModel { Index = 84, TableName = "KR", ColumnName = "A_C冷媒種類", SearchDataType = SearchDataType.String, DateType = DateType.None, MasterTableName = "", MasterCodeColumnName = "", MasterNameColumnName = "" },
                //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
            };

        }
        #endregion

        #region ユーザー検索マスター取得
        /// <summary>
        /// ユーザー検索マスター取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<CommonMasterModel> GetControlSheetSearchMaster(ControlSheetSearchMasterModel cond)
        {
            //開発符号の取得かどうか
            if (cond.CodeColumn == KaihatuHugou)
            {
                return base.GetLogic<GeneralCodeInfoLogic>().GetMaster();

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT DISTINCT");
            sql.AppendFormat("     A.\"{0}\" AS \"CODE\"", cond.CodeColumn).AppendLine();
            sql.AppendFormat("    ,A.\"{0}\" AS \"NAME\"", cond.NameColumn).AppendLine();
            sql.AppendLine("FROM");
            sql.AppendFormat("    \"{0}\" A", cond.Table).AppendLine();
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendFormat("    AND A.\"{0}\" IS NOT NULL", cond.CodeColumn).AppendLine();
            sql.AppendLine("ORDER BY");
            sql.AppendLine("     \"NAME\"");
            sql.AppendLine("    ,\"CODE\"");

            //取得
            return db.ReadModelList<CommonMasterModel>(sql.ToString(), null);

        }
        #endregion

        #region ユーザー検索条件情報登録
        /// <summary>
        /// ユーザー検索条件情報登録
        /// </summary>
        /// <param name="list">ユーザー検索条件情報</param>
        /// <returns></returns>
        public bool MergeUserSearchCondition(List<ControlSheetSearchConditionModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //ユーザー検索条件情報を登録
                results.Add(this.MergeUserSearch(list));

                //ユーザー検索条件情報を削除
                results.Add(this.DeleteUserSearchInfo(list));

                //ユーザー検索条件情報を登録
                results.Add(this.MergeUserSearchInfo(list));

            }

            //削除が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();

            }
            else
            {
                //ロールバック
                db.Rollback();

            }

            return flg;

        }

        /// <summary>
        /// ユーザー検索条件を登録
        /// </summary>
        /// <param name="list">ユーザー検索条件情報</param>
        /// <returns></returns>
        private bool MergeUserSearch(List<ControlSheetSearchConditionModel> list)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"USER_SEARCH_CONDITION\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             :PERSONEL_ID AS \"PERSONEL_ID\"");
            sql.AppendLine("            ,:NAME AS \"NAME\"");
            sql.AppendLine("            ,0 AS \"SEARCH_COUNT\"");
            sql.AppendLine("            ,NULL AS \"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("            ,SYSTIMESTAMP AS \"INPUT_DATETIME\"");
            sql.AppendLine("            ,:PERSONEL_ID AS \"INPUT_PERSONEL_ID\"");
            sql.AppendLine("            ,SYSTIMESTAMP AS \"UPDATE_DATETIME\"");
            sql.AppendLine("            ,:PERSONEL_ID AS \"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            DUAL");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"PERSONEL_ID\" = B.\"PERSONEL_ID\"");
            sql.AppendLine("        AND A.\"NAME\" = B.\"NAME\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         \"UPDATE_DATETIME\" = B.\"UPDATE_DATETIME\"");
            sql.AppendLine("        ,\"UPDATE_PERSONEL_ID\" = B.\"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"PERSONEL_ID\"");
            sql.AppendLine("        ,\"NAME\"");
            sql.AppendLine("        ,\"SEARCH_COUNT\"");
            sql.AppendLine("        ,\"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("        ,\"INPUT_DATETIME\"");
            sql.AppendLine("        ,\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,\"UPDATE_DATETIME\"");
            sql.AppendLine("        ,\"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"NAME\"");
            sql.AppendLine("        ,B.\"SEARCH_COUNT\"");
            sql.AppendLine("        ,B.\"LAST_SEARCH_DATETIME\"");
            sql.AppendLine("        ,B.\"INPUT_DATETIME\"");
            sql.AppendLine("        ,B.\"INPUT_PERSONEL_ID\"");
            sql.AppendLine("        ,B.\"UPDATE_DATETIME\"");
            sql.AppendLine("        ,B.\"UPDATE_PERSONEL_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var cond in list.Where(x => x.行番号 >= 0).Select(x => new { x.ユーザーID, x.条件名 }).Distinct())
            {
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":PERSONEL_ID", Object = cond.ユーザーID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NAME", Object = cond.条件名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                };

                //ユーザー検索条件の登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// ユーザー検索条件情報を登録
        /// </summary>
        /// <param name="list">ユーザー検索条件情報</param>
        /// <returns></returns>
        private bool MergeUserSearchInfo(List<ControlSheetSearchConditionModel> list)
        {
            var results = new List<bool>();

            foreach (var cond in list)
            {
                //ユーザー検索条件情報の登録
                results.Add(base.Merge("ユーザー検索条件情報",
                new []
                {
                    new BindModel { Name = ":BEGIN_KAKKO", Object = cond.BEGIN_KAKKO, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":CONJUNCTION", Object = cond.CONJUNCTION, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":ELEM", Object = cond.ELEM, Type = OracleDbType.Int16, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TEXT", Object = cond.TEXT, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":STR", Object = cond.STR, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FROMNUM", Object = cond.FROMNUM, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NUMMODE", Object = cond.NUMMODE, Type = OracleDbType.Int16, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TONUM", Object = cond.TONUM, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":FROMDATE", Object = cond.FROMDATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":DATEMODE", Object = cond.DATEMODE, Type = OracleDbType.Int16, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":TODATE", Object = cond.TODATE, Type = OracleDbType.Date, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":INDEX_NO", Object = cond.INDEX_NO, Type = OracleDbType.Int32, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NOTFLAG", Object = cond.NOTFLAG, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":NULLFLAG", Object = cond.NULLFLAG, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":END_KAKKO", Object = cond.END_KAKKO, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input }

                },
                new []
                {
                    new BindModel { Name = ":ユーザーID", Object = cond.ユーザーID, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":条件名", Object = cond.条件名, Type = OracleDbType.Varchar2, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":行番号", Object = cond.行番号, Type = OracleDbType.Int16, Direct = ParameterDirection.Input }

                }));

            }

            return results.All(x => x == true);

        }
        #endregion

        #region ユーザー検索条件情報削除
        /// <summary>
        /// ユーザー検索条件情報削除
        /// </summary>
        /// <param name="list">ユーザー検索条件情報</param>
        /// <returns></returns>
        public bool DeleteUserSearchCondition(List<ControlSheetSearchConditionModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //削除対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //ユーザー検索条件を削除
                results.Add(this.DeleteUserSearch(list));

                //ユーザー検索条件情報を削除
                results.Add(this.DeleteUserSearchInfo(list));

            }

            //削除が成功したかどうか
            var flg = results.All(x => x == true);
            if (flg == true)
            {
                //コミット
                db.Commit();

            }
            else
            {
                //ロールバック
                db.Rollback();

            }

            return flg;

        }

        /// <summary>
        /// ユーザー検索条件を削除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool DeleteUserSearch(List<ControlSheetSearchConditionModel> list)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"USER_SEARCH_CONDITION\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND (\"PERSONEL_ID\",\"NAME\") IN ((NULL,NULL)");

            var target = list.Select(x => new { x.ユーザーID, x.条件名 }).Distinct();

            //ユーザーIDと条件名で削除
            for (var i = 0; i < target.Count(); i++)
            {
                var id = string.Format(":PERSONEL_ID{0}", i);
                var name = string.Format(":NAME{0}", i);

                sql.AppendFormat(",({0},{1})", id, name);

                var key = target.ElementAt(i);

                prms.Add(new BindModel { Name = id, Type = OracleDbType.Varchar2, Object = key.ユーザーID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = key.条件名, Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }

        /// <summary>
        /// ユーザー検索条件情報を削除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool DeleteUserSearchInfo(List<ControlSheetSearchConditionModel> list)
        {
            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"ユーザー検索条件情報\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND ((ユーザーID = NULL AND 条件名 = NULL)");

            var target = list.Select(x => new { x.ユーザーID, x.条件名 }).Distinct();

            //ユーザーIDと条件名で削除
            for (var i = 0; i < target.Count(); i++)
            {
                var id = string.Format(":ユーザーID{0}", i);
                var name = string.Format(":条件名{0}", i);

                sql.AppendFormat(" OR (ユーザーID = {0} AND 条件名 = {1})", id, name);

                var key = target.ElementAt(i);

                prms.Add(new BindModel { Name = id, Type = OracleDbType.Varchar2, Object = key.ユーザーID, Direct = ParameterDirection.Input });
                prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = key.条件名, Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion
    }
}