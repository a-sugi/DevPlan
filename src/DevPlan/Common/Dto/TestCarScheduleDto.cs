using System;

namespace DevPlan.UICommon.Dto
{
    #region 試験車スケジュール検索条件クラス
    /// <summary>
    /// 試験車スケジュール検索条件クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long? ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>試作時期</summary>
        public string 試作時期 { get; set; }

        /// <summary>号車</summary>
        public string 号車 { get; set; }

        /// <summary>車型</summary>
        public string 車型 { get; set; }

        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }

        /// <summary>試験車日程種別</summary>
        public string 試験車日程種別 { get; set; }

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

        /// <summary>本予約取得フラグ</summary>
        /// <remarks>本予約をセットで取得する場合はTrueを指定</remarks>
        public bool IsGetKettei { get; set; }

        /// <summary>設定者_ID</summary>
        public string 設定者_ID { get; set; }
        #endregion
    }
    #endregion

    #region 試験車スケジュール項目クラス
    /// <summary>
    /// 試験車スケジュール項目クラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleItemModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>試験車名</summary>
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

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>車系</summary>
        public string 車系 { get; set; }

        /// <summary>試作時期</summary>
        public string 試作時期 { get; set; }

        /// <summary>号車</summary>
        public string 号車 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        public short? FLAG_ETC付 { get; set; }

        /// <summary>XEYE_EXIST</summary>
        public string XEYE_EXIST { get; set; }
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

    #region 試験車スケジュールクラス
    /// <summary>
    /// 試験車スケジュールクラス
    /// </summary>
    [Serializable]
    public class TestCarScheduleModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>開始日</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>終了日</summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>スケジュール名</summary>
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

        /// <summary>試験車日程種別</summary>
        public string 試験車日程種別 { get; set; }

        /// <summary>行番号</summary>
        public int? PARALLEL_INDEX_GROUP { get; set; }

        /// <summary>試験内容</summary>
        public string 試験内容 { get; set; }

        /// <summary>完了日</summary>
        public DateTime? 完了日 { get; set; }

        /// <summary>オドメータ</summary>
        public string オドメータ { get; set; }

        /// <summary>脱着部品</summary>
        public string 脱着部品 { get; set; }

        /// <summary>変更箇所</summary>
        public string 変更箇所 { get; set; }

        /// <summary>車両保管場所</summary>
        public string 車両保管場所 { get; set; }

        /// <summary>鍵保管場所</summary>
        public string 鍵保管場所 { get; set; }

        /// <summary>設定者_ID</summary>
        public string 設定者_ID { get; set; }

        /// <summary>設定者_SECTION_CODE</summary>
        public string 設定者_SECTION_CODE { get; set; }

        /// <summary>設定者_NAME</summary>
        public string 設定者_NAME { get; set; }

        /// <summary>予約者_ID</summary>
        public string 予約者_ID { get; set; }

        /// <summary>予約者_SECTION_CODE</summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>予約者_NAME</summary>
        public string 予約者_NAME { get; set; }

        /// <summary>課題フォローリストID</summary>
        public long? FOLLOWLIST_ID { get; set; }

        //Append Start 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
        /// <summary>本予約済フラグ</summary>
        public bool 本予約済_FLG { get; set; }
        //Append End 2023/09/11 杉浦 仮予約者が本予約の編集を不可とするよう修正
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

    #region 試験車日程スケジュール作業簡易入力一覧取得検索条件クラス
    /// <summary>
    /// 試験車日程スケジュール作業簡易入力一覧取得検索条件クラス
    /// </summary>
    public class TestCarCompleteScheduleSearchModel
    {
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 閲覧権限
        /// </summary>
        public bool GeneralCodeFlg { get; set; }

        /// <summary>
        /// 表示対象ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 検索の対象開始日
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 設定者ID
        /// </summary>
        public string 設定者_ID { get; set; }

        /// <summary>
        /// 予約者ID
        /// </summary>
        public string 予約者_ID { get; set; }
    }
    #endregion

    #region 試験車日程スケジュール作業簡易入力クラス
    /// <summary>
    /// 試験車スケジュール作業簡易入力クラス
    /// </summary>
    public class TestCarCompleteScheduleModel
    {
        /// <summary>
        /// スケジュールID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// カテゴリID
        /// </summary>
        public long CATEGORY_ID { get; set; }

        /// <summary>
        /// 車両名
        /// </summary>
        public string CATEGORY { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// スケジュール名
        /// </summary>
        public string DESCRIPTION { get; set; }

        /// <summary>
        /// スケジュール開始
        /// </summary>
        public DateTime START_DATE { get; set; }

        /// <summary>
        /// スケジュール終了
        /// </summary>
        public DateTime END_DATE { get; set; }

        /// <summary>
        /// 予約者Id
        /// </summary>
        public string 予約者_ID { get; set; }

        /// <summary>
        /// 予約者SectionCode
        /// </summary>
        public string 予約者_SECTION_CODE { get; set; }

        /// <summary>
        /// 予約者名
        /// </summary>
        public string 予約者_NAME { get; set; }
    }
    #endregion
}
