using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 検討会資料検索条件クラス
    /// <summary>
    /// 検討会資料検索条件クラス
    /// </summary>
    public class MeetingDocumentSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [MaxLength(1000)]
        [Display(Name = "ID")]
        public int?[] ID { get; set; }

        /// <summary>行数</summary>
        [Range(1, 99999)]
        [Display(Name = "行数")]
        public int? ROW_COUNT { get; set; }
        #endregion

    }
    #endregion

    #region 検討会資料クラス
    /// <summary>
    /// 検討会資料クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int ID { get; set; }

        /// <summary>資料種別</summary>
        [Required]
        [Range(0, 9)]
        [Display(Name = "資料種別")]
        public short MEETING_FLAG { get; set; }

        /// <summary>資料種別名</summary>
        public string MEETING_NAME { get; set; }

        /// <summary>開催日</summary>
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "開催日")]
        public DateTime MONTH { get; set; }

        /// <summary>資料名</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "資料名")]
        public string NAME { get; set; }

        /// <summary>更新開始日</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "更新開始日")]
        public DateTime? EDIT_TERM_START { get; set; }

        /// <summary>更新終了日</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "更新終了日")]
        public DateTime? EDIT_TERM_END { get; set; }

        /// <summary>資料_ID</summary>
        [Range(0, 99999)]
        [Display(Name = "資料_ID")]
        public int? 資料_ID { get; set; }

        /// <summary>課グループID</summary>
        [StringLength(Const.SectionGroupIdLength)]
        [Display(Name = "課グループID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>編集者パーソナルID</summary>
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "編集者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //資料_IDを指定時で課グループIDか編集者パーソナルIDが未指定はエラー
            if (this.資料_ID != null)
            {
                //課グループIDが未指定はエラー
                if (string.IsNullOrWhiteSpace(this.SECTION_GROUP_ID) == true)
                {
                    yield return new ValidationResult("課グループIDを入力してください。", new[] { "SECTION_GROUP_ID" });

                }

                //編集者パーソナルIDが未指定はエラー
                if (string.IsNullOrWhiteSpace(this.INPUT_PERSONEL_ID) == true)
                {
                    yield return new ValidationResult("編集者パーソナルID。", new[] { "INPUT_PERSONEL_ID" });

                }

            }

        }
        #endregion

    }
    #endregion

    #region 検討会資料詳細検索条件クラス
    /// <summary>
    /// 検討会資料履歴検索条件クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentDetailSearchModel
    {
        #region プロパティ
        /// <summary>資料_ID</summary>
        [Range(0, 99999)]
        [Display(Name = "資料_ID")]
        public int? 資料_ID { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>ステータス</summary>
        [StringLength(10)]
        [Display(Name = "ステータス")]
        public string OPEN_CLOSE { get; set; }

        /// <summary>担当課</summary>
        [StringLength(200)]
        [Display(Name = "担当課")]
        public string 試験部署_CODE { get; set; }

        /// <summary>状況</summary>
        [Display(Name = "状況")]
        public string[] 状況 { get; set; }

        /// <summary>カテゴリーID</summary>
        [Range(0, 9999999999)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //カテゴリーIDと資料_IDが未指定はエラー
            if (this.CATEGORY_ID == null && this.資料_ID == null)
            {
                yield return new ValidationResult("カテゴリーIDか資料_IDはどちらかを入力してください。", new[] { "CATEGORY_ID", "資料_ID" });

            }

        }
        #endregion
    }
    #endregion

    #region 検討会資料詳細クラス
    /// <summary>
    /// 検討会資料履歴クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentDetailModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>資料_ID</summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "資料_ID")]
        public int? 資料_ID { get; set; }

        /// <summary>カテゴリーID</summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }

        /// <summary>資料名</summary>
        public string NAME { get; set; }

        /// <summary>開催日</summary>
        public DateTime? MONTH { get; set; }

        /// <summary>OPEN/CLOSE</summary>
        [StringLength(10)]
        [Display(Name = "OPEN/CLOSE")]
        public string OPEN_CLOSE { get; set; }

        /// <summary>メンテ済部署</summary>
        [StringLength(100)]
        [Display(Name = "メンテ済部署")]
        public string メンテ部署_CODE { get; set; }

        /// <summary>開発符号</summary>
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>項目番号(部記号)</summary>
        [StringLength(10)]
        [Display(Name = "項目番号(部記号)")]
        public string 部記号 { get; set; }

        /// <summary>項目番号(番号)</summary>
        [Range(-99999, 99999)]
        [Display(Name = "項目番号(番号)")]
        public int? 部別項目番号 { get; set; }

        /// <summary>課</summary>
        [StringLength(200)]
        [Display(Name = "課")]
        public string 試験部署_CODE { get; set; }

        /// <summary>項目</summary>
        [StringLength(200)]
        [Display(Name = "項目")]
        public string CATEGORY { get; set; }

        /// <summary>課題</summary>
        [StringLength(2000)]
        [Display(Name = "課題")]
        public string CURRENT_SITUATION { get; set; }

        /// <summary>確認方法</summary>
        [StringLength(30)]
        [Display(Name = "確認方法")]
        public string 確認方法 { get; set; }

        /// <summary>状況</summary>
        [StringLength(10)]
        [Display(Name = "状況")]
        public string 状況 { get; set; }

        /// <summary>対応状況</summary>
        [StringLength(2000)]
        [Display(Name = "対応状況")]
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>設計部署(部)</summary>
        [StringLength(100)]
        [Display(Name = "設計部署(部)")]
        public string 関連設計 { get; set; }

        /// <summary>影響する(しそうな)部品</summary>
        [StringLength(500)]
        [Display(Name = "影響する(しそうな)部品")]
        public string 影響部品 { get; set; }

        /// <summary>出図日程</summary>
        [StringLength(300)]
        [Display(Name = "出図日程")]
        public string 出図日程 { get; set; }

        /// <summary>確認完了日</summary>
        [StringLength(100)]
        [Display(Name = "確認完了日")]
        public string 完了日程情報 { get; set; }

        /// <summary>コスト変動見通し(円)</summary>
        [StringLength(150)]
        [Display(Name = "コスト変動見通し(円)")]
        public string コスト変動 { get; set; }

        /// <summary>質量変動見通し(g)</summary>
        [StringLength(150)]
        [Display(Name = "質量変動見通し(g)")]
        public string 質量変動 { get; set; }

        /// <summary>投資変動見通し(百万円)</summary>
        [StringLength(150)]
        [Display(Name = "投資変動見通し(百万円)")]
        public string 投資変動 { get; set; }

        /// <summary>ソート順</summary>
        [Required]
        [Range(0, 999999.9)]
        [Display(Name = "ソート順")]

        public float? SORT_NO { get; set; }

        /// <summary>課グループID</summary>
        [Required]
        [StringLength(Const.SectionGroupIdLength)]
        [Display(Name = "課グループID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>編集者パーソナルID</summary>
        [Required]
        [StringLength(Const.PersonelIdLength)]
        [Display(Name = "編集者パーソナルID")]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>編集者</summary>
        public string INPUT_NAME { get; set; }

        /// <summary>編集日時</summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "編集日時")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>カテゴリーID数</summary>
        public long CATEGORY_ID_COUNT { get; set; }

        /// <summary>履歴表示名</summary>
        public string HISTORY_NAME { get; set; }

        /// <summary>削除フラグ</summary>
        [Display(Name = "削除フラグ")]
        public bool DELETE_FLG { get; set; }
        #endregion

    }
    #endregion

}