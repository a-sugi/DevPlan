using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// ユーザー検索業務ロジッククラス
    /// </summary>
    /// <remarks></remarks>
    public class UserLogic : BaseLogic
    {
        /// <summary>変換フォーマット</summary>
        private const string TranslateFormat = "UPPER(TO_SINGLE_BYTE(UTL_I18N.TRANSLITERATE({0},'hwkatakana_fwkatakana')))";

        /// <summary>変換文字列の取得</summary>
        private static Func<string, bool, string> GetTranslateString = (str, flg) => { return flg ? str : string.Format(TranslateFormat, str); };

        /// <summary>ライク句の取得</summary>
        private static Func<string, string, bool, string> GetLikePhrase = (col, str, flg) => { return string.Format("{0} LIKE '%' || {1} || '%' ", GetTranslateString(col, flg), GetTranslateString(str, flg)); };

        /// <summary>
        /// ユーザーデータの取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns>DataTable</returns>
        public List<UserSearchOutModel> GetData(UserSearchInModel val)
        {
            var userList = this.GetUserList(val);

            // 管理部署種別を利用しない場合
            if (!val.MANAGE_FLG) return userList;

            var busyoList = this.GetKanriBusyo(userList, val.STATUS_CODE);

            userList.ForEach(user =>
            {
                //管理部署マスタになければ一般ユーザー
                var syubetu = busyoList.Where(x => x.担当ID == user.SECTION_GROUP_ID).ToArray();
                if (syubetu.Any() == false)
                {
                    user.管理部署種別 = new[] { Const.ManagementGroupCode.Ippan };

                }
                else
                {
                    var list = new List<string>();

                    //研実権限があるかどうか
                    if (syubetu.Any(x => x.管理部署種別 == Const.ManagementGroupCode.Kenjitu) == true)
                    {
                        list.Add(Const.ManagementGroupCode.Kenjitu);

                    }

                    //管理権限があるかどうか
                    if (syubetu.Any(x => x.管理部署種別 == Const.ManagementGroupCode.Kanri) == true)
                    {
                        list.Add(Const.ManagementGroupCode.Kanri);

                    }

                    //管理権限が無ければ一般権限
                    if (list.Any() == false)
                    {
                        list.Add(Const.ManagementGroupCode.Ippan);

                    }

                    user.管理部署種別 = list.ToArray();

                }

            });

            return userList;

        }

        /// <summary>
        /// ユーザーデータの取得
        /// </summary>
        /// <param name="val">検索条件</param>
        /// <returns></returns>
        private List<UserSearchOutModel> GetUserList(UserSearchInModel val)
        {
            var isDist = val.DISTINCT_FLG;

            var prms = new List<BindModel>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("     PERSONEL_LIST.PERSONEL_ID");
            sql.AppendLine("    ,PERSONEL_LIST.NAME");
            sql.AppendLine("    ,PERSONEL_LIST.MAIL_ADDRESS");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_ID");
            sql.AppendLine("    ,PERSONEL_LIST.ACCESS_LEVEL");
            sql.AppendLine("    ,PERSONEL_LIST.OFFICIAL_POSITION");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_CODE");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SECTION_GROUP_NAME");
            sql.AppendLine("    ,SECTION_GROUP_DATA.請負関係課_ID SECTION_RELATIONAL_ID");
            sql.AppendLine("    ,SECTION_DATA.SECTION_ID");
            sql.AppendLine("    ,SECTION_DATA.SECTION_CODE");
            sql.AppendLine("    ,SECTION_DATA.SECTION_NAME");
            sql.AppendLine("    ,DEPARTMENT_DATA.DEPARTMENT_ID");
            sql.AppendLine("    ,DEPARTMENT_DATA.DEPARTMENT_CODE");
            sql.AppendLine("    ,DEPARTMENT_DATA.DEPARTMENT_NAME");
            sql.AppendLine("    ,DEPARTMENT_DATA.FLAG_KENJITSU");
            sql.AppendLine("    ,DEPARTMENT_DATA.ESTABLISHMENT");
            sql.AppendLine("    ,PERSONEL_LIST.STATUS_CODE");
            sql.AppendLine("FROM");
            sql.AppendLine("    DEPARTMENT_DATA");
            sql.AppendLine("    INNER JOIN SECTION_DATA ON DEPARTMENT_DATA.DEPARTMENT_ID = SECTION_DATA.DEPARTMENT_ID");
            sql.AppendLine("    INNER JOIN SECTION_GROUP_DATA ON SECTION_DATA.SECTION_ID = SECTION_GROUP_DATA.SECTION_ID");
            sql.AppendLine("    INNER JOIN PERSONEL_LIST ON SECTION_GROUP_DATA.SECTION_GROUP_ID = PERSONEL_LIST.SECTION_GROUP_ID");
            sql.AppendLine("WHERE");
            sql.AppendLine("    DEPARTMENT_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND SECTION_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND SECTION_GROUP_DATA.FLAG_EXIST = 1");
            sql.AppendLine("    AND PERSONEL_LIST.NAME IS NOT NULL");

            if (val.PERSONEL_ID != null)
            {
                sql.AppendLine("    AND PERSONEL_LIST.PERSONEL_ID IN (NULL");
                for (var i = 0; i < val.PERSONEL_ID.Count(); i++)
                {
                    var name = string.Format(":PERSONEL_ID{0}", i);
                    sql.AppendFormat(",{0}", name);
                    prms.Add(new BindModel
                    {
                        Name = name,
                        Type = OracleDbType.Varchar2,
                        Object = val.PERSONEL_ID.ElementAt(i),
                        Direct = ParameterDirection.Input
                    });
                }
                sql.AppendLine(")");
            }

            if (val.DEPARTMENT_ID != null)
            {
                sql.AppendLine("    AND DEPARTMENT_DATA.DEPARTMENT_ID IN (NULL");
                for (var i = 0; i < val.DEPARTMENT_ID.Count(); i++)
                {
                    var name = string.Format(":DEPARTMENT_ID{0}", i);
                    sql.AppendFormat(",{0}", name);
                    prms.Add(new BindModel
                    {
                        Name = name,
                        Type = OracleDbType.Varchar2,
                        Object = val.DEPARTMENT_ID.ElementAt(i),
                        Direct = ParameterDirection.Input
                    });
                }
                sql.AppendLine(")");
            }

            if (val.SECTION_ID != null || val.SECTION_RELATIONAL_ID != null)
            {
                sql.AppendLine("AND (");

                if (val.SECTION_ID != null)
                {
                    sql.AppendLine("    SECTION_DATA.SECTION_ID IN (NULL");
                    for (var i = 0; i < val.SECTION_ID.Count(); i++)
                    {
                        var name = string.Format(":SECTION_ID{0}", i);
                        sql.AppendFormat(",{0}", name);
                        prms.Add(new BindModel
                        {
                            Name = name,
                            Type = OracleDbType.Varchar2,
                            Object = val.SECTION_ID.ElementAt(i),
                            Direct = ParameterDirection.Input
                        });
                    }
                    sql.AppendLine(")");
                }

                if (val.SECTION_ID != null && val.SECTION_RELATIONAL_ID != null)
                {
                    sql.AppendLine("OR");
                }

                if (val.SECTION_RELATIONAL_ID != null)
                {
                    sql.AppendLine("    SECTION_GROUP_DATA.請負関係課_ID IN (NULL");
                    for (var i = 0; i < val.SECTION_RELATIONAL_ID.Count(); i++)
                    {
                        var name = string.Format(":SECTION_RELATIONAL_ID{0}", i);
                        sql.AppendFormat(",{0}", name);
                        prms.Add(new BindModel
                        {
                            Name = name,
                            Type = OracleDbType.Varchar2,
                            Object = val.SECTION_RELATIONAL_ID.ElementAt(i),
                            Direct = ParameterDirection.Input
                        });
                    }
                    sql.AppendLine(")");
                }

                sql.AppendLine(")");
            }

            if (val.SECTION_GROUP_ID != null)
            {
                sql.AppendLine("    AND SECTION_GROUP_DATA.SECTION_GROUP_ID IN (NULL");
                for (var i = 0; i < val.SECTION_GROUP_ID.Count(); i++)
                {
                    var name = string.Format(":SECTION_GROUP_ID{0}", i);
                    sql.AppendFormat(",{0}", name);
                    prms.Add(new BindModel
                    {
                        Name = name,
                        Type = OracleDbType.Varchar2,
                        Object = val.SECTION_GROUP_ID.ElementAt(i),
                        Direct = ParameterDirection.Input
                    });
                }
                sql.AppendLine(")");
            }

            if (val.PERSONEL_NAME != null)
            {
                sql.AppendLine(string.Format("AND ({0})", GetLikePhrase("PERSONEL_LIST.NAME", ":NAME", isDist)));

                prms.Add(new BindModel
                {
                    Name = ":NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.PERSONEL_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            if (val.DEPARTMENT_NAME != null)
            {
                sql.AppendLine(string.Format("AND ({0}", GetLikePhrase("DEPARTMENT_DATA.DEPARTMENT_NAME", ":DEPARTMENT_NAME", isDist)));
                sql.AppendLine(string.Format(" OR {0})", GetLikePhrase("DEPARTMENT_DATA.DEPARTMENT_CODE", ":DEPARTMENT_NAME", isDist)));

                prms.Add(new BindModel
                {
                    Name = ":DEPARTMENT_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.DEPARTMENT_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            if (val.SECTION_NAME != null)
            {
                sql.AppendLine(string.Format("AND ({0}", GetLikePhrase("SECTION_DATA.SECTION_NAME", ":SECTION_NAME", isDist)));
                sql.AppendLine(string.Format(" OR {0})", GetLikePhrase("SECTION_DATA.SECTION_CODE", ":SECTION_NAME", isDist)));

                prms.Add(new BindModel
                {
                    Name = ":SECTION_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            if (val.SECTION_GROUP_NAME != null)
            {
                sql.AppendLine(string.Format("AND ({0}", GetLikePhrase("SECTION_GROUP_DATA.SECTION_GROUP_NAME", ":SECTION_GROUP_NAME", isDist)));
                sql.AppendLine(string.Format(" OR {0})", GetLikePhrase("SECTION_GROUP_DATA.SECTION_GROUP_CODE", ":SECTION_GROUP_NAME", isDist)));

                prms.Add(new BindModel
                {
                    Name = ":SECTION_GROUP_NAME",
                    Type = OracleDbType.Varchar2,
                    Object = val.SECTION_GROUP_NAME,
                    Direct = ParameterDirection.Input
                });
            }

            // ユーザーステータスコード
            if (val.STATUS_CODE != null)
            {
                sql.AppendLine("AND PERSONEL_LIST.STATUS_CODE = :STATUS_CODE");
                prms.Add(new BindModel
                {
                    Name = ":STATUS_CODE",
                    Type = OracleDbType.Varchar2,
                    Object = val.STATUS_CODE,
                    Direct = ParameterDirection.Input
                });
            }

            sql.AppendLine("ORDER BY");
            sql.AppendLine("     DEPARTMENT_DATA.SORT_NO");
            sql.AppendLine("    ,SECTION_DATA.SORT_NO");
            sql.AppendLine("    ,SECTION_GROUP_DATA.SORT_NO");
            sql.AppendLine("    ,PERSONEL_LIST.NAME");

            return db.ReadModelList<UserSearchOutModel>(sql.ToString(), prms);

        }

        /// <summary>
        /// 管理部署マスタ取得
        /// </summary>
        /// <param name="list">ユーザー</param>
        /// <param name="statusCode">ユーザーステータスコード</param>
        /// <returns></returns>
        private List<ManagementDepartmentModel> GetKanriBusyo(List<UserSearchOutModel> list, string statusCode = "")
        {
            var target = new List<ManagementDepartmentModel>();

            if (list != null && list.Any() == true)
            {
                var sectionGroup = list.Select(x => x.SECTION_GROUP_ID).Distinct().ToArray();

                var count = (sectionGroup.Length / InMax) + (sectionGroup.Length % InMax == 0 ? 0 : 1);

                for (var i = 0; i < count; i++)
                {
                    var idList = sectionGroup.Skip(i * InMax).Take(InMax);

                    var prms = new List<BindModel>();

                    //SQL
                    var sql = new StringBuilder();
                    sql.AppendLine("SELECT");
                    sql.AppendLine("     A.\"担当ID\"");
                    sql.AppendLine("    ,A.\"管理部署種別\"");
                    sql.AppendLine("FROM");
                    sql.AppendLine("    \"管理部署マスタ\" A");
                    sql.AppendLine("WHERE 0 = 0");

                    sql.Append("    AND A.\"担当ID\" IN (NULL");

                    var j = 0;
                    foreach (var id in idList)
                    {
                        var name = string.Format(":担当ID{0}", j++);

                        sql.AppendFormat(",{0}", name);

                        prms.Add(new BindModel { Name = name, Type = OracleDbType.Varchar2, Object = id, Direct = ParameterDirection.Input });

                    }

                    sql.AppendLine(")");
                    sql.AppendLine("ORDER BY");
                    sql.AppendLine("     A.\"担当ID\"");
                    sql.AppendLine("    ,A.\"管理部署種別\"");

                    //取得
                    var results = db.ReadModelList<ManagementDepartmentModel>(sql.ToString(), prms);

                    //取得できたかどうか
                    if (results != null && results.Any() == true)
                    {
                        target.AddRange(results);

                    }

                }

            }

            return target;

        }
    }
}