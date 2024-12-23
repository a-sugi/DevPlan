using System;

namespace DevPlan.UICommon.Dto
{
    #region カーシェアスケジュール検索条件クラス
    /// <summary>
    /// カーシェアスケジュール検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long? ID { get; set; }

        /// <summary>空車期間(From)</summary>
        public DateTime? BLANK_START_DATE { get; set; }

        /// <summary>空車期間(To)</summary>
        public DateTime? BLANK_END_DATE { get; set; }

        /// <summary>車系</summary>
        public string CAR_GROUP { get; set; }

        /// <summary>管理票No</summary>
        public string 管理票番号 { get; set; }

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        public string 所在地 { get; set; }

        /// <summary>車型</summary>
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        public short? FLAG_ETC付 { get; set; }

        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }

        /// <summary>Openフラグ</summary>        
        public bool? OPEN_FLG { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>期間(From)</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>期間(To)</summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>行番号</summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>予約者ID</summary>
        public string INPUT_PERSONEL_ID { get; set; }
        #endregion
    }
    #endregion

    #region カーシェアスケジュール項目クラス
    /// <summary>
    /// カーシェアスケジュール項目クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleItemModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>車系</summary>
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>車両名</summary>
        public string CATEGORY { get; set; }

        /// <summary>ソート順</summary>
        public double? SORT_NO { get; set; }

        /// <summary>課グループID</summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>入力者パーソナルID</summary>
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>終了日</summary>
        public DateTime? CLOSED_DATE { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>行数</summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        public short? FLAG_要予約許可 { get; set; }

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>最終予約可能日</summary>
        public DateTime? 最終予約可能日 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        public short? FLAG_ETC付 { get; set; }

        /// <summary>入力者部ID</summary>
        public string INPUT_DEPARTMENT_ID { get; set; }

        /// <summary>入力者課ID</summary>
        public string INPUT_SECTION_ID { get; set; }

        /// <summary>入力者担当ID</summary>
        public string INPUT_SECTION_GROUP_ID { get; set; }

        /// <summary>GPS搭載</summary>
        public string XEYE_EXIST { get; set; }

        //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
        /// <summary>入れ替え中車両</summary>
        public string 入れ替え中車両 { get; set; }
        //Append End 2022/01/17 杉浦 入れ替え中車両の処理

        #endregion

        #region ユーザー情報設定
        /// <summary>
        /// ユーザー情報設定
        /// </summary>
        public void SetUserInfo()
        {
            //課グループID
            this.SECTION_GROUP_ID = SessionDto.SectionGroupID;

            //入力者パーソナルID
            this.INPUT_PERSONEL_ID = SessionDto.UserId;

        }
        #endregion

    }
    #endregion

    #region カーシェアスケジュールクラス
    /// <summary>
    /// カーシェアスケジュールクラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>車系</summary>
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>開始日</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日</summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>タイトル</summary>
        public string DESCRIPTION { get; set; }

        /// <summary>区分</summary>
        public int? SYMBOL { get; set; }

        /// <summary>課グループID</summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>登録日時</summary>
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>入力者パーソナルID</summary>
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }

        /// <summary>予約種別</summary>
        public string 予約種別 { get; set; }

        /// <summary>行番号</summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>目的</summary>
        public string 目的 { get; set; }

        /// <summary>行先</summary>
        public string 行先 { get; set; }

        /// <summary>TEL</summary>
        public string TEL { get; set; }

        /// <summary>利用実績</summary>
        public short? FLAG_実使用 { get; set; }

        /// <summary>空き時間貸出</summary>
        public short? FLAG_空時間貸出可 { get; set; }

        /// <summary>予約者_ID</summary>
        public string 予約者_ID { get; set; }

        /// <summary>予約者_課コード</summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>予約者_NAME</summary>
        public string 予約者_NAME { get; set; }

        /// <summary>FLAG_返却済</summary>
        public short? FLAG_返却済 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        public short? FLAG_要予約許可 { get; set; }

        //Append Start 2022/02/21 杉浦 入れ替え中車両の処理
        public string REPLACEMENT_TEXT { get; set; }
        //Append End 2022/02/21 杉浦 入れ替え中車両の処理
        #endregion

        #region ユーザー情報設定
        /// <summary>
        /// ユーザー情報設定
        /// </summary>
        public void SetUserInfo()
        {
            //課グループID
            this.SECTION_GROUP_ID = SessionDto.SectionGroupID;

            //入力者パーソナルID
            this.INPUT_PERSONEL_ID = SessionDto.UserId;

            //予約者_ID
            this.予約者_ID = SessionDto.UserId;

        }
        #endregion

    }
    #endregion

    //Append Start 2021/08/25 矢作

    #region カーシェアスケジュール複数取得用クラス
    /// <summary>
    /// カーシェアスケジュール項目検索条件クラス
    /// </summary>
    [Serializable]
    public class CarShareScheduleSearchListModel
    {
        #region プロパティ
        /// <summary>IDリスト</summary>
        public long[] IDList { get; set; }
        #endregion
    }
    #endregion


}
