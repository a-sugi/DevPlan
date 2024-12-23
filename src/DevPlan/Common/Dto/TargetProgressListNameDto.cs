using System;

namespace DevPlan.UICommon.Dto
{
    #region 目標進度リスト名検索クラス(INPUT)
    /// <summary>
    /// 目標進度リスト名検索クラス(INPUT)
    /// </summary>
    public class TargetProgressListNameSearchInModel
    {
        /// <summary>部ID</summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>パーソナルID</summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>処理種別</summary>
        public string PROCESS_CATEGORY { get; set; } = "1";

        /// <summary>部署選択</summary>
        public string DIVISION_CATEGORY { get; set; } = "1";

        /// <summary>目標進度リスト名ID</summary>
        public int? CHECKLIST_NAME_ID { get; set; }

    }
    #endregion

    #region 目標進度リスト名検索クラス(OUTPUT)
    /// <summary>
    /// 目標進度リスト名検索クラス(OUTPUT)
    /// </summary>
    public class TargetProgressListNameSearchOutModel
    {
        /// <summary>チェックリスト名ID</summary>
        public int CHECKLIST_NAME_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>部ID</summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>課ID</summary>
        public string SECTION_ID { get; set; }

        /// <summary>チェックリスト名</summary>
        public string CHECKLIST_NAME { get; set; }
    }
    #endregion

    #region 目標進度リスト名更新クラス(INPUT)
    /// <summary>
    /// 目標進度リスト名更新クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListNameUpdateInModel
    {
        #region プロパティ
        /// <summary>目標進度リスト名ID</summary>
        public int? CHECKLIST_NAME_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>性能名_ID</summary>
        public int? 性能名_ID { get; set; }

        /// <summary>新目標進度リスト名(仕様)</summary>
        public string NEW_LISTNAME { get; set; }
        #endregion
    }
    #endregion
}