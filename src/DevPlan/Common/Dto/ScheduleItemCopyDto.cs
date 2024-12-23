//
// 業務計画表システム
// KKA00110 項目コピー・移動（項目コピー）API
// 作成者 : 岸　義将
// 作成日 : 2017/02/10

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// KKA00110 項目コピー・移動（項目コピー）モデルクラス（入力）
    /// </summary>
    public class ScheduleItemCopyInModel
    {
        /// <summary>
        /// スケジュールID(必須)
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// コピー先開発符号(必須)
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// コピー先所属(必須)
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID(パーソナルID)(必須)
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID(必須) 対象テーブルを決定する
        /// </summary>
        public int TABLE_NUMBER { get; set; }
    }
}