using DevPlan.UICommon.Attributes;
using System;

namespace DevPlan.UICommon.Dto
{
    #region カーシェア一覧検索条件クラス
    /// <summary>
    /// カーシェア一覧検索条件モデルクラス
    /// </summary>
    [Serializable]
    public class CarShareReserveInModel
    {
        #region プロパティ
        /// <summary>予約者ID</summary>
        public string 予約者_ID { get; set; }

        ///// <summary>貸出前</summary>
        //public int? LEND { get; set; }

        ///// <summary>貸出中</summary>
        //public int? FLAG_実使用 { get; set; }

        ///// <summary>予約済み</summary>
        //public int? FLAG_返却済 { get; set; }

        /// <summary>予約済み（0：全て、1:当日以降</summary>
        public int? FLAG_RESERVE { get; set; }

        #endregion
    }
    #endregion

    #region 車両検索結果モデルクラス
    /// <summary>
    /// 車両検索結果モデルクラス
    /// </summary>
    public class CarShareReserveOutModel
    {
        #region プロパティ
        [CellSettingAttribute(DisplayIndex = 0, HeaderText = "今どこ")]
        public string XEYE_EXIST { get; set; }

        /// <summary>車系</summary>
        [CellSettingAttribute(DisplayIndex = 1, HeaderText = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>開発符号</summary>
        [CellSettingAttribute(DisplayIndex = 2)]
        public string 開発符号 { get; set; }

        /// <summary>メーカー名</summary>
        [CellSettingAttribute(DisplayIndex = 3)]
        public string メーカー名 { get; set; }

        //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
        ///// <summary>外製車名</summary>
        //[CellSettingAttribute(DisplayIndex = 4)]
        //public string 外製車名 { get; set; }
        /// <summary>外製車名</summary>
        [CellSettingAttribute(DisplayIndex = 4)]
        public string 車名 { get; set; }
        //Update End 2022/01/11 杉浦 トラック予約一覧を追加

        /// <summary>駐車場番号</summary>
        [CellSettingAttribute(DisplayIndex = 7)]
        public string 駐車場番号 { get; set; }

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        ///// <summary>貸出開始日時</summary>
        //[CellSettingAttribute(DisplayIndex = 8, HeaderText = "貸出開始日時")]
        //public DateTime? START_DATE { get; set; }

        ///// <summary>貸出終了日時</summary>
        //[CellSettingAttribute(DisplayIndex = 9, HeaderText = "貸出終了日時")]
        //public DateTime? END_DATE { get; set; }

        /// <summary>貸出開始日時</summary>
        [CellSettingAttribute(DisplayIndex = 8, HeaderText = "予約開始時刻")]
        public DateTime? START_DATE { get; set; }

        /// <summary>貸出終了日時</summary>
        [CellSettingAttribute(DisplayIndex = 9, HeaderText = "予約終了時刻")]
        public DateTime? END_DATE { get; set; }
        //Append End 2022/01/11 杉浦 トラック予約一覧を追加

        /// <summary>予約者</summary>
        [CellSettingAttribute(DisplayIndex = 10, HeaderText = "予約者")]
        public string NAME { get; set; }

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        ///// <summary>予約表示</summary>
        //[CellSettingAttribute(DisplayIndex = 11, HeaderText = "予約表示")]
        //public string DESCRIPTION { get; set; }

        ///// <summary>管理票番号</summary>
        //[CellSettingAttribute(DisplayIndex = 13)]
        //public string 管理票番号 { get; set; }

        ///// <summary>FLAG_CLASS</summary>
        //[CellSettingAttribute(DisplayIndex = 5, Visible = false)]
        //public string FLAG_CLASS { get; set; }

        ///// <summary>種別（FLAG_CLASS）</summary>
        //[CellSettingAttribute(DisplayIndex = 6)]
        //public string 種別 { get { return FLAG_CLASS.Replace("日程", ""); } }

        ///// <summary>ID</summary>
        //[CellSettingAttribute(DisplayIndex = 14)]
        //public long ID { get; set; }

        ///// <summary>行先</summary>
        //[CellSettingAttribute(DisplayIndex = 12)]
        //public string 行先 { get; set; }


        /// <summary>依頼者</summary>
        [CellSettingAttribute(DisplayIndex = 10, HeaderText = "依頼者")]
        public string 依頼者 { get; set; }

        /// <summary>発送者</summary>
        [CellSettingAttribute(DisplayIndex = 11, HeaderText = "発送者")]
        public string 発送者 { get; set; }

        /// <summary>受領者</summary>
        [CellSettingAttribute(DisplayIndex = 12, HeaderText = "受領者")]
        public string 受領者 { get; set; }

        /// <summary>運転者A</summary>
        [CellSettingAttribute(DisplayIndex = 13, HeaderText = "運転者A")]
        public string 運転者A { get; set; }

        /// <summary>運転者B</summary>
        [CellSettingAttribute(DisplayIndex = 14, HeaderText = "運転者B")]
        public string 運転者B { get; set; }

        /// <summary>予約表示</summary>
        [CellSettingAttribute(DisplayIndex = 15, HeaderText = "予約表示")]
        public string DESCRIPTION { get; set; }

        /// <summary>管理票番号</summary>
        [CellSettingAttribute(DisplayIndex = 17)]
        public string 管理票番号 { get; set; }

        /// <summary>FLAG_CLASS</summary>
        [CellSettingAttribute(DisplayIndex = 5, Visible = false)]
        public string FLAG_CLASS { get; set; }

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        /// <summary>種別（FLAG_CLASS）</summary>
        [CellSettingAttribute(DisplayIndex = 6)]
        //public string 種別 { get { return FLAG_CLASS.Replace("日程", ""); } }
        public string 種別 { get; set; }
        //Append End 2022/01/11 杉浦 トラック予約一覧を追加

        /// <summary>ID</summary>
        [CellSettingAttribute(DisplayIndex = 18)]
        public long ID { get; set; }

        /// <summary>行先</summary>
        [CellSettingAttribute(DisplayIndex = 16)]
        public string 行先 { get; set; }
        //Append End 2022/01/11 杉浦 トラック予約一覧を追加
        #endregion
    }
    #endregion
    
    //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
    #region トラック予約一覧検索条件モデルクラス
    /// <summary>
    /// トラック予約一覧検索条件モデルクラス
    /// </summary>
    [Serializable]
    public class TruckReserveInModel
    {
        #region プロパティ
        /// <summary>予約者ID</summary>
        public string 予約者_ID { get; set; }

        /// <summary>依頼者_ID</summary>
        public string 依頼者_ID { get; set; }

        /// <summary>発送者_ID</summary>
        public string 発送者_ID { get; set; }

        /// <summary>受領者_ID</summary>
        public string 受領者_ID { get; set; }

        /// <summary>運転者A_ID</summary>
        public string 運転者A_ID { get; set; }

        /// <summary>運転者B_ID</summary>
        public string 運転者B_ID { get; set; }

        /// <summary>予約済み（0：全て、1:当日以降</summary>
        public int? FLAG_RESERVE { get; set; }

        #endregion
    }
    //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
    #endregion
}
