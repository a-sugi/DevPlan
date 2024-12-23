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
    /// 目標進度リストマスターロジッククラス
    /// </summary>
    public class TargetProgressListMasterLogic : BaseLogic
    {
        #region 目標進度リストマスターの取得
        /// <summary>
        /// 目標進度_項目マスタの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<TargetProgressListMasterModel> GetTargetProgressListMaster(TargetProgressListMasterSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"ID\"");
            sql.AppendLine("    ,A.\"大項目\"");
            sql.AppendLine("    ,A.\"中項目\"");
            sql.AppendLine("    ,A.\"小項目\"");
            sql.AppendLine("    ,A.\"目標値\"");
            sql.AppendLine("    ,A.\"TS_NO\"");
            sql.AppendLine("    ,A.\"SORT_NO\"");
            sql.AppendLine("    ,A.\"性能名_ID\"");
            sql.AppendLine("    ,A.\"編集日時\"");
            sql.AppendLine("    ,A.\"編集者_ID\"");
            sql.AppendLine("    ,B.\"NAME\" AS \"編集者_NAME\"");
            sql.AppendLine("    ,A.\"関連課_CODE\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    \"目標進度_項目マスター\" A");
            sql.AppendLine("    LEFT JOIN \"PERSONEL_LIST\" B");
            sql.AppendLine("    ON A.\"編集者_ID\" = B.\"PERSONEL_ID\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.AppendLine("    AND A.\"性能名_ID\" = :性能名_ID");
            sql.AppendLine("ORDER BY");
            sql.AppendLine("    A.\"SORT_NO\"");

            //パラメータ
            var prms = new List<BindModel>
            {
                new BindModel { Name = ":性能名_ID", Type = OracleDbType.Int32, Object = cond.性能名_ID, Direct = ParameterDirection.Input }

            };

            return db.ReadModelList<TargetProgressListMasterModel>(sql.ToString(), prms);

        }
        #endregion

        #region 目標進度リストマスターの登録
        /// <summary>
        /// 目標進度リストマスターの登録
        /// </summary>
        /// <param name="list">目標進度リストマスター</param>
        /// <returns></returns>
        public bool MergeTargetProgressListMaster(List<TargetProgressListMasterModel> list)
        {
            var results = new List<bool>();

            //トランザクション開始
            db.Begin();

            //登録対象があるかどうか
            if (list != null && list.Any() == true)
            {
                //目標進度_項目マスターの登録
                results.Add(this.MergeMokuhyouSindoKoumokuMaster(list.Where(x => x.DELETE_FLG == false).ToList()));

                //目標進度_項目マスターの削除
                results.Add(this.DeleteMokuhyouSindoKoumokuMaster(list.Where(x => x.DELETE_FLG == true).Select(x => x.ID).ToList()));

            }

            //登録が成功したかどうか
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
        /// 目標進度_項目マスターの登録
        /// </summary>
        /// <param name="list">目標進度リストマスター</param>
        /// <returns></returns>
        private bool MergeMokuhyouSindoKoumokuMaster(List<TargetProgressListMasterModel> list)
        {
            //対象が無ければ終了
            if (list == null || list.Any() == false)
            {
                return true;

            }

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("MERGE INTO");
            sql.AppendLine("    \"目標進度_項目マスター\" A");
            sql.AppendLine("USING");
            sql.AppendLine("    (");
            sql.AppendLine("        SELECT");
            sql.AppendLine("             (NVL(MAX(\"ID\"),0) + 1) AS \"NEW_ID\"");
            sql.AppendLine("            ,:ID AS \"ID\"");
            sql.AppendLine("            ,:大項目 AS \"大項目\"");
            sql.AppendLine("            ,:中項目 AS \"中項目\"");
            sql.AppendLine("            ,:小項目 AS \"小項目\"");
            sql.AppendLine("            ,:目標値 AS \"目標値\"");
            sql.AppendLine("            ,:SORT_NO AS \"SORT_NO\"");
            sql.AppendLine("            ,:性能名_ID AS \"性能名_ID\"");
            sql.AppendLine("            ,:編集日時 AS \"編集日時\"");
            sql.AppendLine("            ,:編集者_ID AS \"編集者_ID\"");
            sql.AppendLine("        FROM");
            sql.AppendLine("            \"目標進度_項目マスター\"");
            sql.AppendLine("    ) B");
            sql.AppendLine("ON");
            sql.AppendLine("    (0 = 0");
            sql.AppendLine("        AND A.\"ID\" = B.\"ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("WHEN MATCHED THEN");
            sql.AppendLine("    UPDATE SET");
            sql.AppendLine("         A.\"大項目\" = B.\"大項目\"");
            sql.AppendLine("        ,A.\"中項目\" = B.\"中項目\"");
            sql.AppendLine("        ,A.\"小項目\" = B.\"小項目\"");
            sql.AppendLine("        ,A.\"目標値\" = B.\"目標値\"");
            sql.AppendLine("        ,A.\"SORT_NO\" = B.\"SORT_NO\"");
            sql.AppendLine("        ,A.\"性能名_ID\" = B.\"性能名_ID\"");
            sql.AppendLine("        ,A.\"編集日時\" = B.\"編集日時\"");
            sql.AppendLine("        ,A.\"編集者_ID\" = B.\"編集者_ID\"");
            sql.AppendLine("WHEN NOT MATCHED THEN");
            sql.AppendLine("    INSERT");
            sql.AppendLine("    (");
            sql.AppendLine("         \"ID\"");
            sql.AppendLine("        ,\"大項目\"");
            sql.AppendLine("        ,\"中項目\"");
            sql.AppendLine("        ,\"小項目\"");
            sql.AppendLine("        ,\"目標値\"");
            sql.AppendLine("        ,\"SORT_NO\"");
            sql.AppendLine("        ,\"性能名_ID\"");
            sql.AppendLine("        ,\"編集日時\"");
            sql.AppendLine("        ,\"編集者_ID\"");
            sql.AppendLine("    )");
            sql.AppendLine("    VALUES");
            sql.AppendLine("    (");
            sql.AppendLine("         B.\"NEW_ID\"");
            sql.AppendLine("        ,B.\"大項目\"");
            sql.AppendLine("        ,B.\"中項目\"");
            sql.AppendLine("        ,B.\"小項目\"");
            sql.AppendLine("        ,B.\"目標値\"");
            sql.AppendLine("        ,B.\"SORT_NO\"");
            sql.AppendLine("        ,B.\"性能名_ID\"");
            sql.AppendLine("        ,B.\"編集日時\"");
            sql.AppendLine("        ,B.\"編集者_ID\"");
            sql.AppendLine("    )");

            var results = new List<bool>();

            foreach (var master in list)
            {
                //パラメータ
                var prms = new List<BindModel>
                {
                    new BindModel { Name = ":ID", Type = OracleDbType.Int64, Object = master.ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":大項目", Type = OracleDbType.Varchar2, Object = master.大項目, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":中項目", Type = OracleDbType.Varchar2, Object = master.中項目, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":小項目", Type = OracleDbType.Varchar2, Object = master.小項目, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":目標値", Type = OracleDbType.Varchar2, Object = master.目標値, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":SORT_NO", Type = OracleDbType.Decimal, Object = master.SORT_NO, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":性能名_ID", Type = OracleDbType.Int32, Object = master.性能名_ID, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集日時", Type = OracleDbType.Date, Object = master.編集日時, Direct = ParameterDirection.Input },
                    new BindModel { Name = ":編集者_ID", Type = OracleDbType.Varchar2, Object = master.編集者_ID, Direct = ParameterDirection.Input }
                    
                };

                //登録
                results.Add(db.UpdateData(sql.ToString(), prms));

            }

            return results.All(x => x == true);

        }

        /// <summary>
        /// 目標進度_項目マスターの削除
        /// </summary>
        /// <param name="idList">IDリスト</param>
        /// <returns></returns>
        private bool DeleteMokuhyouSindoKoumokuMaster(List<long> idList)
        {
            //対象が無ければ終了
            if (idList == null || idList.Any() == false)
            {
                return true;

            }

            var prms = new List<BindModel>();

            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM");
            sql.AppendLine("    \"目標進度_項目マスター\"");
            sql.AppendLine("WHERE 0 = 0");
            sql.Append("    AND \"ID\" IN (NULL");

            //IDで削除
            for (var i = 0; i < idList.Count(); i++)
            {
                var name = string.Format(":ID{0}", i);

                sql.AppendFormat(",{0}", name);

                prms.Add(new BindModel { Name = name, Type = OracleDbType.Decimal, Object = idList.ElementAt(i), Direct = ParameterDirection.Input });

            }

            sql.AppendLine(")");

            return db.DeleteData(sql.ToString(), prms);

        }
        #endregion

    }

}