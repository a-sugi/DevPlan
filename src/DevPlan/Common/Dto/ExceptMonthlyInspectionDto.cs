﻿using System;

namespace DevPlan.UICommon.Dto
{
    #region 担当検索条件クラス
    /// <summary>
    ///担当検索条件クラス
    /// </summary>
    [Serializable]
    public class ExceptMonthlyInspectionUpdateModels
    {
        #region プロパティ
        /// <summary>
        /// SECTION_GROUP_ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// FLAG_月例点検
        /// </summary>
        public int? FLAG_月例点検 { get; set; }
        #endregion
    }
    #endregion

    #region 担当項目クラス
    /// <summary>
    /// 担当項目クラス
    /// </summary>
    [Serializable]
    public class ExceptMonthlyInspectionOutModels
    {
        #region プロパティ
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
        #endregion
    }
    #endregion
}