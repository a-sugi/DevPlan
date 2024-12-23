using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 担当検索入力モデルクラス
    /// </summary>
    public class SectionGroupSearchInModel
    {
        /// <summary>
        /// 事業ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 職場ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 職場グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 事業コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 職場コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 職場グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
    }
    /// <summary>
    /// 担当検索出力モデルクラス
    /// </summary>
    public class SectionGroupSearchOutModel
    {
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
        /// <summary>
        /// 職場グループ名
        /// </summary>
        public string SECTION_GROUP_NAME { get; set; }
        /// <summary>
        /// 職場グループコード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 職場グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// 並び順（職場グループ）
        /// </summary>
        public short SECTION_GROUP_SORT_NO { get; set; }

    }
}