using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region 検討会資料編集区分
    /// <summary>検討会資料編集区分</summary>
    public enum MeetingDocumentEditType : int
    {
        /// <summary>表示</summary>
        Show,

        /// <summary>追加</summary>
        Insert,

        /// <summary>更新</summary>
        Update,

        /// <summary>削除</summary>
        Delete,

        /// <summary>コピー</summary>
        Copy

    }
    #endregion

    #region 検討会資料検索条件クラス
    /// <summary>
    /// 検討会資料検索条件クラス
    /// </summary>
    public class MeetingDocumentSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public int?[] ID { get; set; }

        /// <summary>行数</summary>
        public int? ROW_COUNT { get; set; }
        #endregion

    }
    #endregion

    #region 検討会資料クラス
    /// <summary>
    /// 検討会資料クラス
    /// </summary>
    public class MeetingDocumentModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public int ID { get; set; }

        /// <summary>資料名</summary>
        public string NAME { get; set; }

        /// <summary>資料種別</summary>
        public short MEETING_FLAG { get; set; }

        /// <summary>資料種別名</summary>
        public string MEETING_NAME { get; set; }

        /// <summary>開催日</summary>
        public DateTime MONTH { get; set; }

        /// <summary>更新開始日</summary>
        public DateTime? EDIT_TERM_START { get; set; }

        /// <summary>更新終了日</summary>
        public DateTime? EDIT_TERM_END { get; set; }

        /// <summary>資料_ID</summary>
        public int? 資料_ID { get; set; }

        /// <summary>課グループID</summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>編集者パーソナルID</summary>
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion

    }
    #endregion

    #region 検討会資料詳細検索条件クラス
    /// <summary>
    /// 検討会資料詳細検索条件クラス
    /// </summary>
    public class MeetingDocumentDetailSearchModel
    {
        #region プロパティ
        /// <summary>資料_ID</summary>
        public int? 資料_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>ステータス</summary>
        public string OPEN_CLOSE { get; set; }

        /// <summary>担当課</summary>
        public string 試験部署_CODE { get; set; }

        /// <summary>状況</summary>
        public string[] 状況 { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }
        #endregion

    }
    #endregion

    #region 検討会資料詳細クラス
    /// <summary>
    /// 検討会資料詳細クラス
    /// </summary>
    public class MeetingDocumentDetailModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>資料_ID</summary>
        public int? 資料_ID { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>資料名</summary>
        public string NAME { get; set; }

        /// <summary>開催日</summary>
        public DateTime? MONTH { get; set; }

        /// <summary>OPEN/CLOSE</summary>
        public string OPEN_CLOSE { get; set; }

        /// <summary>メンテ済部署</summary>
        public string メンテ部署_CODE { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>項目番号(部記号)</summary>
        public string 部記号 { get; set; }

        /// <summary>項目番号(番号)</summary>
        public int? 部別項目番号 { get; set; }

        /// <summary>課</summary>
        public string 試験部署_CODE { get; set; }

        /// <summary>項目</summary>
        public string CATEGORY { get; set; }

        /// <summary>課題</summary>
        public string CURRENT_SITUATION { get; set; }

        /// <summary>確認方法</summary>
        public string 確認方法 { get; set; }

        /// <summary>状況</summary>
        public string 状況 { get; set; }

        /// <summary>対応状況</summary>
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>設計部署(部)</summary>
        public string 関連設計 { get; set; }

        /// <summary>影響する(しそうな)部品</summary>
        public string 影響部品 { get; set; }

        /// <summary>出図日程</summary>
        public string 出図日程 { get; set; }

        /// <summary>確認完了日</summary>
        public string 完了日程情報 { get; set; }

        /// <summary>コスト変動見通し(円)</summary>
        public string コスト変動 { get; set; }

        /// <summary>質量変動見通し(g)</summary>
        public string 質量変動 { get; set; }

        /// <summary>投資変動見通し(百万円)</summary>
        public string 投資変動 { get; set; }

        /// <summary>ソート順</summary>
        public float? SORT_NO { get; set; }

        /// <summary>課グループID</summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>編集者パーソナルID</summary>
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>編集者</summary>
        public string INPUT_NAME { get; set; }

        /// <summary>編集日時</summary>
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>カテゴリーID数</summary>
        public long CATEGORY_ID_COUNT { get; set; }

        /// <summary>履歴表示名</summary>
        public string HISTORY_NAME { get; set; }

        /// <summary>編集フラグ</summary>
        public bool EDIT_FLG { get; set; }

        /// <summary>削除フラグ</summary>
        public bool DELETE_FLG { get; set; }
        #endregion

    }
    #endregion

}
