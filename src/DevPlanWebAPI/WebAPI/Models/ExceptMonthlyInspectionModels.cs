using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 月例点検省略部署更新モデルクラス
    /// </summary>
    public class ExceptMonthlyInspectionUpdateModels
    {
        /// <summary>
        /// SECTION_GROUP_ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// FLAG_月例点検
        /// </summary>
        public int? FLAG_月例点検 { get; set; }
    }
    /// <summary>
    /// 月例点検省略部署出力モデルクラス
    /// </summary>
    public class ExceptMonthlyInspectionOutModels
    {
        /// <summary>
        /// FLAG_月例点検
        /// </summary>
        public int FLAG_月例点検 { get; set; }
        /// <summary>
        /// 企業名
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 事業名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 事業コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 事業ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 並び順（事業）
        /// </summary>
        public long DEPARTMENT_SORT_NO { get; set; }
        /// <summary>
        /// 職場名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 職場コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 並び順（職場）
        /// </summary>
        public short SECTION_SORT_NO { get; set; }

    }
}