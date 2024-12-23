using System;

namespace DevPlan.UICommon.Dto
{
    #region 試験車スケジュール一括本予約クラス
    /// <summary>
    /// 試験車スケジュール一括本予約クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleReserveModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>対象期間（開始）</summary>
        public DateTime TARGET_START_DATE { get; set; }

        /// <summary>対象期間（終了）</summary>
        public DateTime? TARGET_END_DATE { get; set; }

        /// <summary>入力者パーソナルID</summary>
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion
    }
    #endregion
}