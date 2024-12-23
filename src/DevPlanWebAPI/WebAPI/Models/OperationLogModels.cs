using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 操作ログモデルクラス
    /// </summary>
    public class OperationLogModel
    {
        /// <summary>
        /// ログ情報(カンマ区切り)
        /// </summary>
        public string INFO { get; set; }
    }
}