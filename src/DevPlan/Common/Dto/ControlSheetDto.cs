using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
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
        public string ユーザーID { get; set; }

        /// <summary>
        /// 条件名
        /// </summary>
        public string 条件名 { get; set; }
        #endregion

    }
    #endregion

    #region 管理票検索クラス
    /// <summary>
    /// 管理票検索クラス
    /// </summary>
    public class ControlSheetModel
    {
        #region プロパティ
        /// <summary>管理票検索条件</summary>
        public ControlSheetSearchModel ControlSheetSearch { get; set; }

        /// <summary>ユーザー検索条件</summary>
        public IEnumerable<ControlSheetSearchConditionModel> ControlSheetSearchConditionList { get; set; }
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
        public string 管理票NO { get; set; }

        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string 固定資産NO { get; set; }

        /// <summary>
        /// 車体番号
        /// </summary>
        public string 車体番号 { get; set; }

        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }

        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }

        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }

        //Append Start 2021/07/06 矢作
        /// <summary>
        /// 改修前車両検索対象
        /// </summary>
        public bool 改修前車両検索対象 { get; set; }
        //Append Start 2021/07/06 矢作
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
        public string ユーザーID { get; set; }

        /// <summary>
        /// 条件名
        /// </summary>
        public string 条件名 { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public short 行番号 { get; set; }

        /// <summary>
        /// 左括弧
        /// </summary>
        public string BEGIN_KAKKO { get; set; }

        /// <summary>
        /// 添字
        /// </summary>
        public string CONJUNCTION { get; set; }

        /// <summary>
        /// インデックス
        /// </summary>
        public short? ELEM { get; set; }

        /// <summary>
        /// 表示条件
        /// </summary>
        public string TEXT { get; set; }

        /// <summary>
        /// 文字列の条件
        /// </summary>
        public string STR { get; set; }

        /// <summary>
        /// 数値FROM
        /// </summary>
        public int? FROMNUM { get; set; }

        /// <summary>
        /// 数値範囲条件
        /// </summary>
        public short? NUMMODE { get; set; }

        /// <summary>
        /// 数値TO
        /// </summary>
        public int? TONUM { get; set; }

        /// <summary>
        /// 日付FROM
        /// </summary>
        public DateTime? FROMDATE { get; set; }

        /// <summary>
        /// 日付範囲条件
        /// </summary>
        public short? DATEMODE { get; set; }

        /// <summary>
        /// 日付TO
        /// </summary>
        public DateTime? TODATE { get; set; }

        /// <summary>
        /// マスターインデックス
        /// </summary>
        public int? INDEX_NO { get; set; }

        /// <summary>
        /// 否定可否
        /// </summary>
        public string NOTFLAG { get; set; }

        /// <summary>
        /// NULL可否
        /// </summary>
        public string NULLFLAG { get; set; }

        /// <summary>
        /// 右括弧
        /// </summary>
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
        public int Index { get; set; }

        /// <summary>テーブル名</summary>
        public string TableName { get; set; }

        /// <summary>列名</summary>
        public string ColumnName { get; set; }

        /// <summary>検索種別</summary>
        public SearchDataType SearchDataType { get; set; }

        /// <summary>日付種別</summary>
        public DateType DateType { get; set; }

        /// <summary>マスターテーブル名</summary>
        public string MasterTableName { get; set; }

        /// <summary>マスターコード列名</summary>
        public string MasterCodeColumnName { get; set; }

        /// <summary>マスター表示名</summary>
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
        public string Table { get; set; }

        /// <summary>コード列名</summary>
        public string CodeColumn { get; set; }

        /// <summary>表示列名</summary>
        public string NameColumn { get; set; }
        #endregion
    }
    #endregion

}
