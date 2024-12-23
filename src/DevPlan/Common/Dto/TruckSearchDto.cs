using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 定時間日の設定検索条件保持クラス
    /// </summary>
    public class FixedTimeDaySettingSearchModel
    {
        /// <summary>
        /// トラックID
        /// </summary>
        public long トラック_ID { get; set; }

        /// <summary>
        /// 検索対象年月日終了
        /// </summary>
        public DateTime END_DATE { get; set; }

        /// <summary>
        /// 検索対象年月日開始
        /// </summary>
        public DateTime START_DATE { get; set; }
    }

    /// <summary>
    /// 項目検索条件クラス
    /// </summary>
    public class TruckScheduleItemSearchModel
    {
        /// <summary>
        /// 項目ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 定期便フラグ
        /// </summary>
        public bool IsRegular { get; set; }

        /// <summary>
        /// 予約可能開始日より前を検索
        /// </summary>
        public DateTime? BEFORE_DATE { get; set; }

        /// <summary>
        /// 予約可能開始日より後を検索
        /// </summary>
        public DateTime? AFTER_DATE { get; set; }

        /// <summary>
        /// 空車期間START
        /// </summary>
        public DateTime? EMPTY_START_DATE { get; set; }

        /// <summary>
        /// 空車期間END
        /// </summary>
        public DateTime? EMPTY_END_DATE { get; set; }

        /// <summary>
        /// 修正者ID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>
        /// ソート順検索最小値
        /// </summary>
        public double? MinSortNo { get; set; }

        /// <summary>
        /// ソート順検索最大値
        /// </summary>
        public double? MaxSortNo { get; set; }

        /// <summary>
        /// 定期便時間帯ID
        /// </summary>
        public int? REGULAR_TIME_ID { get; set; }
    }

    /// <summary>
    /// トラックスケジュール情報検索クラス
    /// </summary>
    public class TruckScheduleSearchModel
    {
        /// <summary>
        /// 検索対象予約ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 検索対象予約開始日
        /// </summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// 検索対象予約終了日
        /// </summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// 定期便フラグ
        /// </summary>
        public bool IsRegular { get; set; }

        /// <summary>
        /// トラックID
        /// </summary>
        public long? TruckId { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>
        /// 本予約取得フラグ
        /// </summary>
        /// <remarks>本予約のみ取得する場合はTrueを指定</remarks>
        public bool IsGetKettei { get; set; }
    }

    /// <summary>
    /// メール原文検索条件
    /// </summary>
    public class RegularMailDetailSearchModel
    {
        /// <summary>
        /// 予約種別
        /// </summary>
        public string RegularType { get; set; }

        /// <summary>
        /// トラックID
        /// </summary>
        public long TrackId { get; set; }
    }
}
