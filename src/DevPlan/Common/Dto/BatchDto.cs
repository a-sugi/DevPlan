using System;

namespace DevPlan.UICommon.Dto
{
    #region バッチ起動パラメータクラス
    /// <summary>
    /// バッチ起動パラメータクラス
    /// </summary>
    [Serializable]
    public class BatchModel
    {
        #region プロパティ
        /// <summary>
        /// 起動種別(true:バッチ、false:その他)
        /// </summary>
        public bool batch { get; set; }
        #endregion
    }
    #endregion
}
