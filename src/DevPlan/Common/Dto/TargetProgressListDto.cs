using System;

namespace DevPlan.UICommon.Dto
{
    #region 目標進度リスト検索クラス(INPUT)
    /// <summary>
    /// 目標進度リスト検索クラス(INPUT)
    /// </summary>
    public class TargetProgressListSearchInModel
    {
        #region プロパティ
        /// <summary>チェックリスト名ID</summary>
        public long CHECKLIST_NAME_ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>部ID</summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>課ID</summary>
        public string SECTION_ID { get; set; }

        /// <summary>課CD</summary>
        public string SECTION_CD { get; set; }

        /// <summary>表示モード(1:性能別 2:部別 3:課別 省略時は 1)</summary>
        public string DISPLAY_MODE { get; set; } = "1";

        /// <summary>関連課表示フラグ(0:表示しない 1:表示する 省略時は 0)</summary>
        public string RELATION_DISPLAY_FLAG { get; set; } = "0";
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

    #region 目標進度リスト登録クラス(INPUT)
    /// <summary>
    /// 目標進度リスト登録クラス(INPUT)
    /// </summary>
    public class TargetProgressListRegistInModel
    {
        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>性能名_ID</summary>
        public int SPEC_NAME_ID { get; set; }

        /// <summary>編集者ID</summary>
        public string EDITOR_ID { get; set; }

    }
    #endregion

    #region 目標進度リスト更新クラス(INPUT)
    /// <summary>
    /// 目標進度リスト更新クラス(INPUT)
    /// </summary>
    public class TargetProgressListUpdateInModel
    {
        /// <summary>チェックリスト名ID</summary>
        public long CHECKLIST_NAME_ID { get; set; }

        /// <summary>チェックリスト項目名ID</summary>
        public long CHECKLIST_ITEMNAME_ID { get; set; }

        /// <summary>大項目</summary>
        public string LARGE_CLASSIFICATION { get; set; }

        /// <summary>中項目</summary>
        public string MIDDLE_CLASSIFICATION { get; set; }

        /// <summary>小項目</summary>
        public string SMALL_CLASSIFICATION { get; set; }

        /// <summary>並び順</summary>
        public long SORT_NO { get; set; }

        /// <summary>目標値</summary>
        public string TARGET_VALUE { get; set; }

        /// <summary>達成値</summary>
        public string ACHIEVED_VALUE { get; set; }

        /// <summary>表示フラグ(0:非表示 1:表示)</summary>
        public int DISPLAY_FLAG { get; set; }

        /// <summary>関連課コード</summary>
        public string RELATIONAL_DIVISION_CODE { get; set; }

        /// <summary>編集者ID</summary>
        public string EDITOR_ID { get; set; }

        /// <summary>編集日</summary>
        public DateTime? EDITED_DATE { get; set; }

        /// <summary>編集者ログインID</summary>
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
    public class TargetProgressListDeleteInModel
    {
        /// <summary>チェックリスト名ID</summary>
        public long CHECKLIST_NAME_ID { get; set; }
    }
    #endregion
}