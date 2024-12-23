using System;

namespace DevPlan.UICommon.Dto
{
    #region API 共通モデル
    /// <summary>
    /// 外製車日程項目検索入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemGetInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long? ID { get; set; }

        /// <summary>
        /// 空車期間FROM
        /// </summary>
        public DateTime? EMPTY_START_DATE { get; set; }
        /// <summary>
        /// 空車期間TO
        /// </summary>
        public DateTime? EMPTY_END_DATE { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string 所在地 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public int? FLAG_ETC付 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }

        /// <summary>
        /// Openフラグ
        /// </summary>
        public bool? OPEN_FLG { get; set; }

        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }

        /// <summary>
        /// 予約者ID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// 外製車日程項目検索出力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemGetOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 終了日
        /// </summary>
        public DateTime? CLOSED_DATE { get; set; }
        /// <summary>
        /// FLAG_要予約許可
        /// </summary>
        public int? FLAG_要予約許可 { get; set; }
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        public int? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public int? FLAG_ETC付 { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }

        /// <summary>GPS搭載</summary>
        public string XEYE_EXIST { get; set; }
    }
    /// <summary>
    /// 外製車日程項目登録入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemPostInModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        public string 管理票番号 { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// FLAG_要予約許可
        /// </summary>
        public int? FLAG_要予約許可 { get; set; }
    }
    /// <summary>
    /// 外製車日程項目更新入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemPutInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        public string 管理票番号 { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public double? SORT_NO { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 最終予約可能日
        /// </summary>
        public DateTime? 最終予約可能日 { get; set; }
        /// <summary>
        /// FLAG_要予約許可
        /// </summary>
        public int? FLAG_要予約許可 { get; set; }
    }
    /// <summary>
    /// 外製車日程項目削除入力モデルクラス
    /// </summary>
    public class OuterCarScheduleItemDeleteInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 外製車日程検索入力モデルクラス
    /// </summary>
    public class OuterCarScheduleGetInModel
    {
        /// <summary>
        /// 空車期間FROM
        /// </summary>
        public DateTime? EMPTY_START_DATE { get; set; }
        /// <summary>
        /// 空車期間TO
        /// </summary>
        public DateTime? EMPTY_END_DATE { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string 所在地 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public int? FLAG_ETC付 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? DATE_START { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? DATE_END { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
    }
    /// <summary>
    /// 外製車日程検索出力モデルクラス
    /// </summary>
    public class OuterCarScheduleGetOutModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// カテゴリー
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 予約種別
        /// </summary>
        public string 予約種別 { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// 登録日
        /// </summary>
        public DateTime? INPUT_DATETIME { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// TEL
        /// </summary>
        public string TEL { get; set; }
        /// <summary>
        /// FLAG_実使用
        /// </summary>
        public int? FLAG_実使用 { get; set; }
        /// <summary>
        /// 予約者_ID
        /// </summary>
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 予約者_SECTION_CODE
        /// </summary>
        public string 予約者_SECTION_CODE { get; set; }
        /// <summary>
        /// 予約者_NAME
        /// </summary>
        public string 予約者_NAME { get; set; }
        /// <summary>
        /// 管理票番号
        /// </summary>
        public string 管理票番号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// FLAG_空時間貸出可
        /// </summary>
        public int? FLAG_空時間貸出可 { get; set; }

        /// <summary>Openフラグ</summary>        
        public bool? OPEN_FLG { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        public short? FLAG_要予約許可 { get; set; }

    }
    /// <summary>
    /// 外製車日程登録入力モデルクラス
    /// </summary>
    public class OuterCarSchedulePostInModel
    {
        /// <summary>
        /// カテゴリーID
        /// </summary>
        public long CATEGORY_ID { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 予約種別
        /// </summary>
        public string 予約種別 { get; set; }
        /// <summary>
        /// 所属グループID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// 使用者TEL
        /// </summary>
        public string TEL { get; set; }
        /// <summary>
        /// 空時間貸出可フラグ
        /// </summary>
        public int FLAG_空時間貸出可 { get; set; }
        /// <summary>
        /// 予約者ID
        /// </summary>
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
    }
    /// <summary>
    /// 外製車日程更新入力モデルクラス
    /// </summary>
    public class OuterCarSchedulePutInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
        /// <summary>
        /// 期間（開始）
        /// </summary>
        public DateTime? START_DATE { get; set; }
        /// <summary>
        /// 期間（終了）
        /// </summary>
        public DateTime? END_DATE { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public int PARALLEL_INDEX_GROUP { get; set; }
        /// <summary>
        /// スケジュール区分
        /// </summary>
        public int SYMBOL { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 予約種別
        /// </summary>
        public string 予約種別 { get; set; }
        /// <summary>
        /// パーソナルID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        /// <summary>
        /// 目的
        /// </summary>
        public string 目的 { get; set; }
        /// <summary>
        /// 行先
        /// </summary>
        public string 行先 { get; set; }
        /// <summary>
        /// 使用者TEL
        /// </summary>
        public string TEL { get; set; }
        /// <summary>
        /// 空時間貸出可フラグ
        /// </summary>
        public int FLAG_空時間貸出可 { get; set; }
        /// <summary>
        /// 予約者ID
        /// </summary>
        public string 予約者_ID { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
    }
    /// <summary>
    /// 外製車日程削除入力モデルクラス
    /// </summary>
    public class OuterCarScheduleDeleteInModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long SCHEDULE_ID { get; set; }
    }
    #endregion
}
