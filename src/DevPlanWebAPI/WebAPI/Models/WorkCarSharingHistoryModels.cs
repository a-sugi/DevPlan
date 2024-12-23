using System;
using System.ComponentModel.DataAnnotations;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 作業履歴検索条件クラス
    /// <summary>
    /// 作業履歴検索条件クラス
    /// </summary>
    [Serializable]
    public class WorkCarSharingHistorySearchModel
    {
        #region プロパティ
        /// <summary>カテゴリーID</summary>
        [Required]
        [Range(Const.ScheduleHistoryIdMin, Const.ScheduleHistoryIdMax)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }
        #endregion
    }

    /// <summary>
    /// 作業履歴(試験車)検索条件クラス
    /// </summary>
    [Serializable]
    public class CarSharingWorkHistorySearchModel
    {
        #region プロパティ
        /// <summary>所在地</summary>
        [Display(Name = "ESTABLISH")]
        public string ESTABLISH { get; set; }

        /// <summary>要点検フラグ</summary>
        [Display(Name = "要点検フラグ")]
        public bool NECESSARY_INSPECTION_FLAG { get; set; } = false;
        #endregion
    }
    #endregion

    #region 作業履歴クラス
    /// <summary>
    /// 作業履歴クラス
    /// </summary>
    [Serializable]
    public class WorkCarSharingHistoryModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(Const.ScheduleHistoryIdMin, Const.ScheduleHistoryIdMax)]
        [Display(Name = "課題フォローリストID")]
        public decimal ID { get; set; }

        /// <summary>カテゴリーID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public decimal? CATEGORY_ID { get; set; }

        /// <summary>カテゴリー</summary>
        [Display(Name = "カテゴリー")]
        public string CATEGORY { get; set; }

        /// <summary>作業履歴</summary>
        [StringLength(2000)]
        [Display(Name = "作業履歴")]
        public string CURRENT_SITUATION { get; set; }

        /// <summary>作業履歴</summary>
        [StringLength(100)]
        [Display(Name = "試験内容")]
        public string 試験内容 { get; set; }

        /// <summary>作業履歴</summary>
        [StringLength(50)]
        [Display(Name = "実走行距離")]
        public string 実走行距離 { get; set; }

        /// <summary>未来予定</summary>
        [StringLength(2000)]
        [Display(Name = "未来予定")]
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>入力者パーソナルID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "入力者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>入力者名</summary>
        public string INPUT_NAME { get; set; }

        /// <summary>入力者課コード</summary>
        public string INPUT_SECTION_CODE { get; set; }

        /// <summary>入力日時</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "入力日時")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>OPEN/CLOSE</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "OPEN/CLOSE")]
        public string OPEN_CLOSE { get; set; }

        /// <summary>日付</summary>
        [DataType(DataType.Date)]
        [Display(Name = "日付")]
        public DateTime? LISTED_DATE { get; set; }

        /// <summary>ソート順</summary>
        [Range(0, 99999)]
        [Display(Name = "ソート順")]
        public int? SORT_NO { get; set; }

        /// <summary>取込アイテム</summary>
        [Range(0, 99999)]
        [Display(Name = "取込アイテム")]
        public int? IMPORTANT_ITEM { get; set; }

        /// <summary>マーク</summary>
        [StringLength(30)]
        [Display(Name = "マーク")]
        public string MARK { get; set; }

        /// <summary>入力者ログインID</summary>
        [StringLength(Const.LoginIdLength)]
        [Display(Name = "入力者ログインID")]
        public string INPUT_LOGIN_ID { get; set; }

        /// <summary>種別ID</summary>
        [Range(0, 99999)]
        [Display(Name = "種別ID")]
        public int? 種別_ID { get; set; }

        /// <summary>種別</summary>
        [Display(Name = "種別")]
        public string 種別 { get; set; }

        /// <summary>スケジュールID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "スケジュールID")]
        public decimal? SCHEDULE_ID { get; set; }

        /// <summary>削除フラグ</summary>
        [Display(Name = "削除フラグ")]
        public bool DELETE_FLG { get; set; }

        /// <summary>データID</summary>
        
        [Display(Name = "データID")]
        public int? データID { get; set; }

        /// <summary>履歴NO</summary>
        [Display(Name = "履歴NO")]
        public int? 履歴NO { get; set; }

        /// <summary>管理票NO</summary>
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }

        /// <summary>車系</summary>
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>月例点検省略有無</summary>
        [Display(Name = "月例点検省略有無")]
        public string 月例点検省略有無 { get; set; }

        /// <summary>駐車場番号</summary>
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>登録ナンバー</summary>
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }

        /// <summary>管理責任部署ID</summary>
        [Display(Name = "管理責任部署ID")]
        public string 管理責任部署ID { get; set; }

        /// <summary>管理責任部署</summary>
        [Display(Name = "管理責任部署")]
        public string 管理責任部署 { get; set; }

        /// <summary>管理責任課ID</summary>
        [Display(Name = "管理責任課ID")]
        public string 管理責任課ID { get; set; }

        /// <summary>管理責任課</summary>
        [Display(Name = "管理責任課")]
        public string 管理責任課 { get; set; }

        /// <summary>工事区分NO</summary>
        [Display(Name = "工事区分NO")]
        public string 工事区分NO { get; set; }

        /// <summary>最新履歴_SEQNO</summary>
        [Display(Name = "最新履歴_SEQNO")]
        public int? 最新履歴_SEQNO { get; set; }

        /// <summary>最新履歴_承認状況</summary>
        [Display(Name = "最新履歴_承認状況")]
        public string 最新履歴_承認状況 { get; set; }

        /// <summary>最新履歴_日付</summary>
        [Display(Name = "最新履歴_日付")]
        public DateTime? 最新履歴_日付 { get; set; }

        /// <summary>最新履歴_試験内容</summary>
        [Display(Name = "最新履歴_試験内容")]
        public string 最新履歴_試験内容 { get; set; }

        /// <summary>最新履歴_実走行距離</summary>
        [Display(Name = "最新履歴_実走行距離")]
        public string 最新履歴_実走行距離 { get; set; }

        /// <summary>最新履歴_STEPNO</summary>
        [Display(Name = "最新履歴_STEPNO")]
        public int? 最新履歴_STEPNO { get; set; }
        #endregion
    }
    #endregion
}