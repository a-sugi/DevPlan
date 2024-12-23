using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region ユーザー検索条件検索クラス
    /// <summary>
    /// ユーザー検索条件検索クラス
    /// </summary>
    public class ControlSheetSearchConditionSearchModel
    {
        #region プロパティ
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string ユーザーID { get; set; }

        /// <summary>
        /// 条件名
        /// </summary>
        [StringLength(60)]
        [Display(Name = "条件名")]
        public string 条件名 { get; set; }
        #endregion

    }
    #endregion

    #region 管理票検索クラス
    /// <summary>
    /// 管理票検索クラス
    /// </summary>
    public class ControlSheetModel : IValidatableObject
    {
        #region プロパティ
        /// <summary>管理票検索条件</summary>
        public ControlSheetSearchModel ControlSheetSearch { get; set; }

        /// <summary>ユーザー検索条件</summary>
        public IEnumerable<ControlSheetSearchConditionModel> ControlSheetSearchConditionList { get; set; }
        #endregion

        #region 検証
        /// <summary>
        /// 必須チェック
        /// </summary>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ユーザー検索条件が指定されているかどうか
            if (this.ControlSheetSearchConditionList != null && this.ControlSheetSearchConditionList.Any() == true)
            {
                var beginList = new List<string>();
                var endList = new List<string>();

                foreach (var x in this.ControlSheetSearchConditionList)
                {
                    //括弧の開始
                    if (string.IsNullOrWhiteSpace(x.BEGIN_KAKKO) == false)
                    {
                        beginList.Add(x.BEGIN_KAKKO);

                    }

                    //括弧の終了
                    if (string.IsNullOrWhiteSpace(x.END_KAKKO) == false)
                    {
                        endList.Add(x.END_KAKKO);

                    }

                }

                //開始と終了の括弧の数が不一致の場合はエラー
                if (beginList.Sum(x => x.Length) != endList.Sum(x => x.Length))
                {
                    yield return new ValidationResult("括弧の対応が不完全です。。", new[] { "ControlSheetSearchConditionList" });

                }

            }

        }
        #endregion
    }
    #endregion

    #region 管理票検索条件クラス
    /// <summary>
    /// 管理票検索検索条件クラス
    /// </summary>
    public class ControlSheetSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }

        /// <summary>
        /// 固定資産NO
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }

        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }

        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>
        /// 登録ナンバー
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }

        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        [StringLength(10)]
        [Display(Name = "所属")]
        public string ESTABLISHMENT { get; set; }

        //Append Start 2021/07/06 矢作
        /// <summary>
        /// 改修前車両検索対象
        /// </summary>
        [Display(Name = "改修前車両検索対象")]
        public bool 改修前車両検索対象 { get; set; }
        //Append End 2021/07/06 矢作
        #endregion
    }
    #endregion

    #region ユーザー検索条件クラス
    /// <summary>
    /// ユーザー検索条件クラス
    /// </summary>
    public class ControlSheetSearchConditionModel
    {
        #region プロパティ
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string ユーザーID { get; set; }

        /// <summary>
        /// 条件名
        /// </summary>
        [StringLength(60)]
        [Display(Name = "条件名")]
        public string 条件名 { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        [Required]
        [Range(-1, 999)]
        [Display(Name = "行番号")]
        public short 行番号 { get; set; }

        /// <summary>
        /// 左括弧
        /// </summary>
        [StringLength(10)]
        [RegularExpression(@"^[(]{1,10}$")]
        [Display(Name = "左括弧")]
        public string BEGIN_KAKKO { get; set; }

        /// <summary>
        /// 添字
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "添字")]
        public string CONJUNCTION { get; set; }

        /// <summary>
        /// インデックス
        /// </summary>
        [Range(-1, 999)]
        [Display(Name = "インデックス")]
        public short? ELEM { get; set; }

        /// <summary>
        /// 表示条件
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "表示条件")]
        public string TEXT { get; set; }

        /// <summary>
        /// 文字列の条件
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "文字列の条件")]
        public string STR { get; set; }

        /// <summary>
        /// 数値FROM
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "数値FROM")]
        public int? FROMNUM { get; set; }

        /// <summary>
        /// 数値範囲条件
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "数値範囲条件")]
        public short? NUMMODE { get; set; }

        /// <summary>
        /// 数値TO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "数値TO")]
        public int? TONUM { get; set; }

        /// <summary>
        /// 日付FROM
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "日付FROM")]
        public DateTime? FROMDATE { get; set; }

        /// <summary>
        /// 日付範囲条件
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "日付範囲条件")]
        public short? DATEMODE { get; set; }

        /// <summary>
        /// 日付TO
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "日付TO")]
        public DateTime? TODATE { get; set; }

        /// <summary>
        /// マスターインデックス
        /// </summary>
        [Range(-1, 99999999)]
        [Display(Name = "マスターインデックス")]
        public int? INDEX_NO { get; set; }

        /// <summary>
        /// 否定可否
        /// </summary>
        [StringLength(10)]
        [RegularExpression(@"^(True|False|true|false)$")]
        [Display(Name = "否定可否")]
        public string NOTFLAG { get; set; }

        /// <summary>
        /// NULL可否
        /// </summary>
        [StringLength(10)]
        [RegularExpression(@"^(True|False|true|false)$")]
        [Display(Name = "NULL可否")]
        public string NULLFLAG { get; set; }

        /// <summary>
        /// 右括弧
        /// </summary>
        [StringLength(10)]
        [RegularExpression(@"^[)]{1,10}$")]
        [Display(Name = "右括弧")]
        public string END_KAKKO { get; set; }
        #endregion

    }
    #endregion

    #region ユーザー検索項目クラス
    /// <summary>検索種別</summary>
    public enum SearchDataType : int
    {
        /// <summary>文字列</summary>
        String = 1,

        /// <summary>日付</summary>
        Date = 2,

        /// <summary>数値</summary>
        Number = 3,

        /// <summary>マスター</summary>
        Master = 4

    }

    /// <summary>日付種別</summary>
    public enum DateType : int
    {
        /// <summary>なし</summary>
        None = 0,

        /// <summary>日付</summary>
        Date = 1,

        /// <summary>年月</summary>
        Month = 2

    }

    /// <summary>
    /// ユーザー検索項目クラス
    /// </summary>
    public class UserSearchItemModel
    {
        #region プロパティ
        /// <summary>インデックス</summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int Index { get; set; }

        /// <summary>テーブル名</summary>
        [Required]
        [StringLength(30)]
        public string TableName { get; set; }

        /// <summary>列名</summary>
        [Required]
        [StringLength(30)]
        public string ColumnName { get; set; }

        /// <summary>検索種別</summary>
        [Required]
        public SearchDataType SearchDataType { get; set; }

        /// <summary>日付種別</summary>
        [Required]
        public DateType DateType { get; set; }

        /// <summary>マスターテーブル名</summary>
        [StringLength(30)]
        public string MasterTableName { get; set; }

        /// <summary>マスターコード列名</summary>
        [StringLength(30)]
        public string MasterCodeColumnName { get; set; }

        /// <summary>マスター表示名</summary>
        [StringLength(30)]
        public string MasterNameColumnName { get; set; }
        #endregion
    }
    #endregion

    #region ユーザー検索マスタークラス
    /// <summary>
    /// ユーザー検索マスタークラス
    /// </summary>
    public class ControlSheetSearchMasterModel
    {
        #region プロパティ
        /// <summary>テーブル名</summary>
        [Required]
        [StringLength(30)]
        public string Table { get; set; }

        /// <summary>コード列名</summary>
        [Required]
        [StringLength(30)]
        public string CodeColumn { get; set; }

        /// <summary>表示列名</summary>
        [Required]
        [StringLength(30)]
        public string NameColumn { get; set; }
        #endregion
    }
    #endregion
}