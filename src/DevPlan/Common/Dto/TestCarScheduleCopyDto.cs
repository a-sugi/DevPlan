using System;

namespace DevPlan.UICommon.Dto
{
    #region 試験車スケジュール要望案コピークラス
    /// <summary>
    /// 試験車スケジュール要望案コピークラス
    /// </summary>
    public class TestCarScheduleCopyModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>コピー元ステータス</summary>
        public string SOURCE_STATUS { get; set; }

        /// <summary>コピー先ステータス</summary>
        public string TARGET_STATUS { get; set; }

        /// <summary>対象期間（開始）</summary>
        public DateTime TARGET_START_DATE { get; set; }

        /// <summary>対象期間(終了）</summary>
        public DateTime? TARGET_END_DATE { get; set; }

        #endregion
    }
    #endregion
}