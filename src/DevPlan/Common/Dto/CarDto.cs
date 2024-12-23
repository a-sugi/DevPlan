using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region カーシェア一覧検索条件クラス
    /// <summary>
    /// カーシェア一覧検索条件モデルクラス
    /// </summary>
    [Serializable]
    public class CarInModel
    {
        #region プロパティ
        /// <summary>車両区分</summary>
        public int CAR_CLASS { get; set; }

        /// <summary>空車期間FROM</summary>
        public DateTime? EMPTY_DATE_FROM { get; set; }

        /// <summary>空車期間TO</summary>
        public DateTime? EMPTY_DATE_TO { get; set; }

        /// <summary>予約方法</summary>
        public short? RESERVATION { get; set; }

        /// <summary>車系</summary>
        public string 車系 { get; set; }

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        public string 所在地 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        public int? FLAG_ETC付 { get; set; }

        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }

        #endregion
    }
    #endregion

    #region 車両検索結果モデルクラス
    /// <summary>
    /// 車両検索結果モデルクラス
    /// </summary>
    public class CarOutModel
    {
        #region プロパティ
        /// <summary>今どこ</summary>
        public string 今どこ { get; set; }

        /// <summary>車系</summary>
        public string 車系 { get; set; }

        /// <summary>開発符号</summary>
        public string 開発符号 { get; set; }

        /// <summary>メーカー名</summary>
        public string メーカー名 { get; set; }

        /// <summary>外製車名</summary>
        public string 外製車名 { get; set; }

        /// <summary>SECTION_CODE</summary>
        public string SECTION_CODE { get; set; }

        /// <summary>SECTION_GROUP_CODE</summary>
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>FLAG_空時間貸出可</summary>
        public short? FLAG_空時間貸出可 { get; set; }

        /// <summary>登録ナンバー</summary>
        public string 登録ナンバー { get; set; }

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>所在地</summary>
        public string 所在地 { get; set; }

        /// <summary>FLAG_要予約許可</summary>
        public short? FLAG_要予約許可 { get; set; }

        /// <summary>FLAG_ETC付</summary>
        public short? FLAG_ETC付 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        public short? FLAG_ナビ付 { get; set; }

        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }

        /// <summary>排気量</summary>
        public string 排気量 { get; set; }

        /// <summary>E_G型式</summary>
        public string E_G型式 { get; set; }

        /// <summary>駆動方式</summary>
        public string 駆動方式 { get; set; }

        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }

        /// <summary>車型</summary>
        public string 車型 { get; set; }

        /// <summary>グレード</summary>
        public string グレード { get; set; }

        /// <summary>型式符号</summary>
        public string 型式符号 { get; set; }

        /// <summary>車体色</summary>
        public string 車体色 { get; set; }

        /// <summary>試作時期</summary>
        public string 試作時期 { get; set; }

        /// <summary>リース満了日</summary>
        public DateTime? リース満了日 { get; set; }

        /// <summary>処分予定年月</summary>
        public DateTime? 処分予定年月 { get; set; }

        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }

        /// <summary>車体番号</summary>
        public string 車体番号 { get; set; }

        /// <summary>E_G番号</summary>
        public string E_G番号 { get; set; }

        /// <summary>固定資産NO</summary>
        public string 固定資産NO { get; set; }

        /// <summary>リースNO</summary>
        public string リースNO { get; set; }

        /// <summary>研命ナンバー</summary>
        public string 研命ナンバー { get; set; }

        /// <summary>研命期間</summary>
        public DateTime? 研命期間 { get; set; }

        /// <summary>車検登録日</summary>
        public DateTime? 車検登録日 { get; set; }

        /// <summary>車検期限</summary>
        public DateTime? 車検期限 { get; set; }

        /// <summary>車検期限まで残り</summary>
        public string 車検期限まで残り { get; set; }

        /// <summary>廃艦日</summary>
        public DateTime? 廃艦日 { get; set; }

        /// <summary>号車</summary>
        public string 号車 { get; set; }

        /// <summary>名称備考</summary>
        public string 名称備考 { get; set; }

        /// <summary>試験目的</summary>
        public string 試験目的 { get; set; }

        /// <summary>メモ</summary>
       public string メモ { get; set; }

        /// <summary>FLAG_空時間貸出可</summary>
        public short? FLAG_返却済 { get; set; }

        /// <summary>FLAG_実使用</summary>
        public short? FLAG_実使用 { get; set; }

        /// <summary>DEVELOPMENT_SCHEDULE_ID</summary>
        public long? ID { get; set; }

        /// <summary>FLAG_CLASS</summary>
        public string FLAG_CLASS { get; set; }

        /// <summary>予約現況</summary>
        public string 予約現況 { get; set; }

        /// <summary>部コード</summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>分類</summary>
        public string 分類 { get; set; }
        #endregion
    }
    #endregion
}
