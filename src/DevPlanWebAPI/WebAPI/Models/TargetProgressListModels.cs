using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 目標進度リスト検索クラス(INPUT)
    /// <summary>
    /// 目標進度リスト検索クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListSearchInModel
    {
        #region プロパティ
        /// <summary>チェックリスト名ID</summary>
        [Range(0, 99999)]
        [Display(Name = "チェックリスト名ID")]
        public int? CHECKLIST_NAME_ID { get; set; }

        /// <summary>開発符号</summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>部ID</summary>
        [DataType(DataType.Text)]
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string DEPARTMENT_ID { get; set; }

        /// <summary>課ID</summary>
        [DataType(DataType.Text)]
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string SECTION_ID { get; set; }

        /// <summary>課CD</summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20)]
        [Display(Name = "課CD")]
        public string SECTION_CD { get; set; }


        /// <summary>表示モード(1:性能別 2:部別 3:課別 省略時は 1)</summary>
        [DataType(DataType.Text)]
        [Display(Name = "表示モード")]
        [Range(typeof(string), "1", "3")]
        [StringLength(1)]
        public string DISPLAY_MODE { get; set; } = "1";

        /// <summary>関連課表示フラグ(0:全て 1:含まない 2:含む省略時は 0)</summary>
        [DataType(DataType.Text)]
        [Display(Name = "関連課表示フラグ")]
        [Range(typeof(string), "0", "2")]
        [StringLength(1)]
        public string RELATION_DISPLAY_FLAG { get; set; } = "0";
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (this.DISPLAY_MODE)
            {
                //性能別
                case "1":
                    //チェックリスト名IDが入力されているかどうか
                    if (this.CHECKLIST_NAME_ID == null)
                    {
                        yield return new ValidationResult("チェックリスト名IDが未入力です。", new[] { "CHECKLIST_NAME_ID" });

                    }
                    break;

                //部別
                case "2":
                    //部IDが入力されているかどうか
                    if (string.IsNullOrWhiteSpace(this.DEPARTMENT_ID) == false)
                    {
                        yield return new ValidationResult("部IDが未入力です。", new[] { "DEPARTMENT_ID" });

                    }
                    break;

                //課別
                case "3":
                    //課IDが入力されているかどうか
                    if (string.IsNullOrWhiteSpace(this.SECTION_ID) == false)
                    {
                        yield return new ValidationResult("課IDが未入力です。", new[] { "SECTION_ID" });

                    }
                    break;
            }

        }
        #endregion
    }
    #endregion

    #region 目標進度リスト検索クラス(OUTPUT)
    /// <summary>
    /// 目標進度リスト検索クラス(OUTPUT)
    /// </summary>
    public class TargetProgressListSearchOutModel
    {
        #region プロパティ
        /// <summary>課コード</summary>
        public string SECTION_CODE { get; set; }

        /// <summary>性能名</summary>
        public string SPEC_NAME { get; set; }

        /// <summary>大項目</summary>
        public string LARGE_CLASSIFICATION { get; set; }

        /// <summary>中項目</summary>
        public string MIDDLE_CLASSIFICATION { get; set; }

        /// <summary>小項目</summary>
        public string SMALL_CLASSIFICATION { get; set; }

        /// <summary>目標値</summary>
        public string TARGET_VALUE { get; set; }

        /// <summary>達成値</summary>
        public string ACHIEVED_VALUE { get; set; }

        /// <summary>確認時期名</summary>
        public string[] CONFIRM_SEASON_NAME { get; set; }

        /// <summary>時期順</summary>
        public int[] SEASON_SEQUENCE { get; set; }

        /// <summary>確認結果</summary>
        public string[] CONFIRM_RESULTS { get; set; }

        /// <summary>関連課コード</summary>
        public string RELATIONAL_DIVISION_CODE { get; set; }

        /// <summary>編集日</summary>
        public DateTime? EDITED_DATE { get; set; }

        /// <summary>編集者氏名</summary>
        public string EDITOR_NAME { get; set; }

        /// <summary>チェックリスト名ID</summary>
        public long CHECKLIST_NAME_ID { get; set; }

        /// <summary>課ID</summary>
        public string SECTION_ID { get; set; }

        /// <summary>性能名ID</summary>
        public int SPEC_NAME_ID { get; set; }

        /// <summary>確認時期ID</summary>
        public long[] CONFIRM_SEASON_ID { get; set; }

        /// <summary>チェックリストID</summary>
        public long[] CHECKLIST_ID { get; set; }

        /// <summary>チェックリスト項目名ID</summary>
        public long CHECKLIST_ITEMNAME_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>項目名ID</summary>
        public long ITEMNAME_ID { get; set; }

        /// <summary>並び順(課)</summary>
        public int SECTION_SORT_NO { get; set; }

        /// <summary>並び順(項目名)</summary>
        public decimal LIST_SORT_NO { get; set; }

        /// <summary>表示フラグ(項目名)</summary>
        public int ITEM_FLAG_DISP { get; set; }

        /// <summary>表示フラグ(確認時期)</summary>
        public int[] SEASON_FLAG_DISP { get; set; }

        /// <summary>部ID</summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>部コード</summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>編集者ログインID</summary>
        public string EDITOR_LOGIN_ID { get; set; }

        /// <summary>開発中フラグ</summary>
        public int UNDER_DEVELOPMENT { get; set; }

        /// <summary>カラム番号</summary>
        public int COLUMN_NUMBER { get; set; }

        /// <summary>カラムタイトル</summary>
        public string COLUMN_TITLE { get; set; }
        #endregion
    }
    #endregion

    #region 目標進度_確認時期取得用クラス
    /// <summary>
    /// 目標進度_確認時期取得用クラス
    /// </summary>
    public class TargetProgressListConfirmSeasonModel
    {
        /// <summary>チェックリスト項目名ID</summary>
        public long CHECKLIST_ITEMNAME_ID { get; set; }

        /// <summary>チェックリスト名ID</summary>
        public long CHECKLIST_NAME_ID { get; set; }

        /// <summary>確認時期ID</summary>
        public long CONFIRM_SEASON_ID { get; set; }

        /// <summary>チェックリストID</summary>
        public long CHECKLIST_ID { get; set; }

        /// <summary>確認時期名</summary>
        public string CONFIRM_SEASON_NAME { get; set; }

        /// <summary>確認結果</summary>
        public string CONFIRM_RESULTS { get; set; }

        /// <summary>時期順</summary>
        public int SEASON_SEQUENCE { get; set; }

        /// <summary>表示フラグ(確認時期)</summary>
        public int SEASON_FLAG_DISP { get; set; }
    }
    #endregion

    #region 目標進度リスト登録クラス(INPUT)
    /// <summary>
    /// 目標進度リスト登録クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListRegistInModel
    {
        /// <summary>開発符号</summary>
        [Required]
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]

        public string GENERAL_CODE { get; set; }

        /// <summary>性能名_ID</summary>
        [Range(0, 99999)]
        [Display(Name = "性能名_ID")]
        public int SPEC_NAME_ID { get; set; }

        /// <summary>編集者ID</summary>
        [Required]
        [DataType(DataType.Text)]
        [StringLength(10)]
        [Display(Name = "編集者ID")]
        public string EDITOR_ID { get; set; }

    }
    #endregion

    #region 目標進度リスト更新クラス(INPUT)
    /// <summary>
    /// 目標進度リスト更新クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListUpdateInModel
    {
        /// <summary>チェックリスト名ID</summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "チェックリスト名ID")]
        public long CHECKLIST_NAME_ID { get; set; }

        /// <summary>チェックリスト項目名ID</summary>
        [Required]
        [Range(0, 9999999999)]
        [Display(Name = "チェックリスト項目名ID")]
        public long CHECKLIST_ITEMNAME_ID { get; set; }

        /// <summary>大項目</summary>
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "大項目")]
        public string LARGE_CLASSIFICATION { get; set; }

        /// <summary>中項目</summary>
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "中項目")]
        public string MIDDLE_CLASSIFICATION { get; set; }

        /// <summary>小項目</summary>
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "小項目")]
        public string SMALL_CLASSIFICATION { get; set; }

        /// <summary>並び順</summary>
        [Required]
        [Range(long.MinValue, long.MaxValue)]
        [Display(Name = "並び順")]
        public long SORT_NO { get; set; }

        /// <summary>目標値</summary>
        [DataType(DataType.Text)]
        [StringLength(200)]
        [Display(Name = "目標値")]
        public string TARGET_VALUE { get; set; }

        /// <summary>達成値</summary>
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "達成値")]
        public string ACHIEVED_VALUE { get; set; }

        /// <summary>表示フラグ(0:非表示 1:表示)</summary>
        [Required]
        [Range(0, 1)]
        [Display(Name = "表示フラグ")]
        public int DISPLAY_FLAG { get; set; }

        /// <summary>関連課コード</summary>
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Display(Name = "関連課コード")]
        public string RELATIONAL_DIVISION_CODE { get; set; }

        /// <summary>編集日</summary>
        [DataType(DataType.DateTime)]
        public DateTime? EDITED_DATE { get; set; }

        /// <summary>編集者ID</summary>
        [StringLength(10)]
        [Display(Name = "編集者ID")]
        public string EDITOR_ID { get; set; }

        /// <summary>編集者ログインID</summary>
        [StringLength(10)]
        [Display(Name = "編集者ログインID")]
        public string EDITOR_LOGIN_ID { get; set; }

        /// <summary>チェックリストID</summary>
        public long[] CHECKLIST_ID { get; set; }

        /// <summary>確認時期ID</summary>
        public long[] CONFIRM_SEASON_ID { get; set; }

        /// <summary>確認時期名</summary>
        public string[] CONFIRM_SEASON_NAME { get; set; }

        /// <summary>時期順</summary>
        public int[] SEASON_SEQUENCE { get; set; }

        /// <summary>確認結果</summary>
        public string[] CONFIRM_RESULTS { get; set; }
    }
    #endregion

    #region 目標進度リスト削除クラス(INPUT)
    /// <summary>
    /// 目標進度リスト削除クラス(INPUT)
    /// </summary>
    [Serializable]
    public class TargetProgressListDeleteInModel
    {
        /// <summary>チェックリスト名ID</summary>
        [Required]
        [Range(long.MinValue, long.MaxValue)]
        [Display(Name = "チェックリスト名ID")]
        public long CHECKLIST_NAME_ID { get; set; }
    }

    #endregion
}