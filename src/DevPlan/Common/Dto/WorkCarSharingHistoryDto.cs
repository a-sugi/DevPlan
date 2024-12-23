using System;

using DevPlan.UICommon.Attributes;

namespace DevPlan.UICommon.Dto
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
        public long CATEGORY_ID { get; set; }
        #endregion
    }

    /// <summary>
    /// 作業履歴(試験車)検索条件クラス
    /// </summary>
    [Serializable]
    public class CarSharingWorkHistorySearchModel
    {
        #region プロパティ
        /// <summary>ESTABLISH</summary>
        public string ESTABLISH { get; set; }

        /// <summary>要点検フラグ</summary>
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
        [CellSettingAttribute(Visible = false)]
        public decimal ID { get; set; }

        /// <summary>カテゴリーID</summary>
        [CellSettingAttribute(Visible = false)]
        public decimal? CATEGORY_ID { get; set; }

        /// <summary>カテゴリー</summary>
        [CellSettingAttribute(Visible = false)]
        public string CATEGORY { get; set; }

        /// <summary>作業履歴</summary>
        [CellSettingAttribute(DisplayIndex = 3, WordWrap = true, ReadOnly = false, HeaderText = "作業履歴", Tag = "Byte(2000);ItemName(作業履歴)")]
        public string CURRENT_SITUATION { get; set; }

        /// <summary>作業履歴</summary>
        [CellSettingAttribute(Visible = false)]
        public string 試験内容 { get; set; }

        /// <summary>作業履歴</summary>
        [CellSettingAttribute(Visible = false)]
        public string 実走行距離 { get; set; }

        /// <summary>未来予定</summary>
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>入力者パーソナルID</summary>
        [CellSettingAttribute(Visible = false)]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>入力者名</summary>
        [CellSettingAttribute(DisplayIndex = 5, HeaderText = "編集者")]
        public string INPUT_NAME { get; set; }

        /// <summary>入力者課コード</summary>
        [CellSettingAttribute(DisplayIndex = 4, HeaderText = "編集課")]
        public string INPUT_SECTION_CODE { get; set; }

        /// <summary>入力日時</summary>
        [CellSettingAttribute(DisplayIndex = 6, HeaderText = "編集日時")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>OPEN/CLOSE</summary>
        [CellSettingAttribute(Visible = false)]
        public string OPEN_CLOSE { get; set; }

        /// <summary>日付</summary>
        [CellSettingAttribute(DisplayIndex = 2, ReadOnly = false, HeaderText = "日付", Tag = "Required;ItemName(日付)")]
        public DateTime? LISTED_DATE { get; set; }

        /// <summary>ソート順</summary>
        [CellSettingAttribute(Visible = false)]
        public int? SORT_NO { get; set; }

        /// <summary>取込アイテム</summary>
        public int? IMPORTANT_ITEM { get; set; }

        /// <summary>マーク</summary>
        public string MARK { get; set; }

        /// <summary>入力者ログインID</summary>
        [CellSettingAttribute(Visible = false)]
        public string INPUT_LOGIN_ID { get; set; }

        /// <summary>種別ID</summary>
        public int? 種別_ID { get; set; }

        /// <summary>種別</summary>
        public string 種別 { get; set; }

        /// <summary>スケジュールID</summary>
        [CellSettingAttribute(Visible = false)]
        public decimal? SCHEDULE_ID { get; set; }

        /// <summary>編集フラグ</summary>
        [CellSettingAttribute(Visible = false)]
        public bool EDIT_FLG { get; set; }

        /// <summary>削除フラグ</summary>
        [CellSettingAttribute(Visible = false)]
        public bool DELETE_FLG { get; set; }

        /// <summary>データID</summary>
        [CellSettingAttribute(Visible = false)]
        public int? データID { get; set; }

        /// <summary>履歴NO</summary>
        [CellSettingAttribute(Visible = false)]
        public int? 履歴NO { get; set; }

        /// <summary>管理票NO</summary>
        [CellSettingAttribute(Visible = false)]
        public string 管理票NO { get; set; }

        /// <summary>車系</summary>
        [CellSettingAttribute(Visible = false)]
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        [CellSettingAttribute(Visible = false)]
        public string GENERAL_CODE { get; set; }

        /// <summary>月例点検省略有無</summary>
        [CellSettingAttribute(Visible = false)]
        public string 月例点検省略有無 { get; set; }
        
        /// <summary>駐車場番号</summary>
        [CellSettingAttribute(Visible = false)]
        public string 駐車場番号 { get; set; }

        /// <summary>登録ナンバー</summary>
        [CellSettingAttribute(Visible = false)]
        public string 登録ナンバー { get; set; }

        /// <summary>管理責任部署ID</summary>
        [CellSettingAttribute(Visible = false)]
        public string 管理責任部署ID { get; set; }

        /// <summary>管理責任部署</summary>
        [CellSettingAttribute(Visible = false)]
        public string 管理責任部署 { get; set; }

        /// <summary>管理責任課ID</summary>
        [CellSettingAttribute(Visible = false)]
        public string 管理責任課ID { get; set; }

        /// <summary>管理責任課</summary>
        [CellSettingAttribute(Visible = false)]
        public string 管理責任課 { get; set; }

        /// <summary>工事区分NO</summary>
        [CellSettingAttribute(Visible = false)]
        public string 工事区分NO { get; set; }

        /// <summary>SEQNO</summary>
        [CellSettingAttribute(Visible = false)]
        public int? 最新履歴_SEQNO { get; set; }

        /// <summary>最新履歴_承認状況</summary>
        [CellSettingAttribute(Visible = false)]
        public string 最新履歴_承認状況 { get; set; }

        /// <summary>最新履歴_日付</summary>
        [CellSettingAttribute(Visible = false)]
        public DateTime? 最新履歴_日付 { get; set; }

        /// <summary>最新履歴_試験内容</summary>
        [CellSettingAttribute(Visible = false)]
        public string 最新履歴_試験内容 { get; set; }

        /// <summary>最新履歴_実走行距離</summary>
        [CellSettingAttribute(Visible = false)]
        public string 最新履歴_実走行距離 { get; set; }

        /// <summary>最新履歴_STEPNO</summary>
        [CellSettingAttribute(Visible = false)]
        public int? 最新履歴_STEPNO { get; set; }

        /// <summary>エラーメッセージ</summary>
        [CellSettingAttribute(HeaderText = "エラーメッセージ", WordWrap = true)]
        public string ERROR_MESSAGE { get; set; }

        /// <summary>前月走行距離無視登録</summary>
        [CellSettingAttribute(HeaderText = "前月走行距離無視登録")]
        public bool 前月走行距離無視登録 { get; set; }
        #endregion
    }
    #endregion
}
