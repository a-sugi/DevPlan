using System;

namespace DevPlan.UICommon.Dto
{
    #region 目標進度リストマスタ検索条件クラス
    /// <summary>
    /// 目標進度リストマスタ検索条件クラス
    /// </summary>
    [Serializable]
    public class TargetProgressListMasterSearchModel
    {
        /// <summary>性能名_ID</summary>
        public int 性能名_ID { get; set; }

    }
    #endregion

    #region 目標進度リストマスタクラス
    /// <summary>
    /// 目標進度リストマスタクラス
    /// </summary>
    [Serializable]
    public class TargetProgressListMasterModel
    {
        /// <summary>NO</summary>
        public long NO { get; set; }

        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>大項目</summary>
        public string 大項目 { get; set; }

        /// <summary>中項目</summary>
        public string 中項目 { get; set; }

        /// <summary>小項目</summary>
        public string 小項目 { get; set; }

        /// <summary>目標値</summary>
        public string 目標値 { get; set; }

        /// <summary>性能名_ID</summary>
        public int? 性能名_ID { get; set; }

        /// <summary>SORT_NO</summary>
        public decimal? SORT_NO { get; set; }

        /// <summary>TS_NO</summary>
        public string TS_NO { get; set; }

        /// <summary>編集日時</summary>
        public DateTime? 編集日時 { get; set; }

        /// <summary>編集者_ID</summary>
        public string 編集者_ID { get; set; }

        /// <summary>編集者_NAME</summary>
        public string 編集者_NAME { get; set; }

        /// <summary>関連課_CODE</summary>
        public string 関連課_CODE { get; set; }

        /// <summary>編集フラグ</summary>
        public bool EDIT_FLG { get; set; }

        /// <summary>削除フラグ</summary>
        public bool DELETE_FLG { get; set; }

    }
    #endregion
}
