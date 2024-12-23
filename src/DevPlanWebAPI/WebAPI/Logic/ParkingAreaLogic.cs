using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

using Oracle.ManagedDataAccess.Client;

using DevPlanWebAPI.Base;
using DevPlanWebAPI.Common;
using DevPlanWebAPI.Models;

namespace DevPlanWebAPI.Logic
{
    /// <summary>
    /// 駐車場エリアロジッククラス
    /// </summary>
    public class ParkingAreaLogic : BaseLogic
    {
        #region 駐車場エリア検索
        /// <summary>
        /// 駐車場エリア検索
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        public List<ParkingModel> Get(ParkingSearchModel cond)
        {
            //SQL
            var sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("     A.\"LOCATION_NO\"");
            sql.AppendLine("    ,A.\"AREA_NO\"");
            sql.AppendLine("    ,A.\"NAME\"");
            sql.AppendLine("    ,A.\"MAP_PDF\"");
            sql.AppendLine("    ,A.\"SORT_NO\"");
            sql.AppendLine("FROM");
            sql.AppendLine("    PARKING_AREA A");
            sql.AppendLine("WHERE 0 = 0");

            //パラメータ
            var prms = new List<BindModel>();

            if (cond.LOCATION_NO != null)
            {
                sql.AppendLine("    AND A.\"LOCATION_NO\" = :LOCATION_NO ");
                prms.Add(new BindModel
                {
                    Name = ":LOCATION_NO",
                    Type = OracleDbType.Int64,
                    Object = cond.LOCATION_NO,
                    Direct = ParameterDirection.Input
                });
            }
            if (cond.AREA_NO != null)
            {
                sql.AppendLine("    AND A.\"AREA_NO\" = :AREA_NO ");
                prms.Add(new BindModel
                {
                    Name = ":AREA_NO",
                    Type = OracleDbType.Int64,
                    Object = cond.AREA_NO,
                    Direct = ParameterDirection.Input
                });
            }

            //並べ替え
            sql.Append("ORDER BY ");
            sql.Append("A.\"SORT_NO\"");

            //取得
            return db.ReadModelList<ParkingModel>(sql.ToString(), prms);
        }
        #endregion
    }
}