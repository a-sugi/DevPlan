using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// クエリバインドモデルクラス
    /// </summary>
    public class BindModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// カラム型
        /// </summary>
        public OracleDbType Type { get; set; }
        /// <summary>
        /// カラムサイズ
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// オブジェクト
        /// </summary>
        public object Object { get; set; }
        /// <summary>
        /// パラメータタイプ
        /// </summary>
        public ParameterDirection Direct { get; set; }
    }
}