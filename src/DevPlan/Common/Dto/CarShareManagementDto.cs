using DevPlan.UICommon.Attributes;
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
    public class CarShareManagementInModel
    {
        #region プロパティ
        /// <summary>所在地</summary>
        public string 所在地 { get; set; }

        /// <summary>検索日付（貸出リスト）</summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>検索日付（返却リスト）</summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>準備状況</summary>
        public short? FLAG_準備済 { get; set; }

        /// <summary>貸出状況</summary>
        public short? FLAG_実使用 { get; set; }

        /// <summary>返却状況</summary>
        public short? FLAG_返却済 { get; set; }

        /// <summary>給油状況</summary>
        public short? FLAG_給油済 { get; set; }

        /// <summary>予約者</summary>
        public string NAME { get; set; }

        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }
        #endregion
    }
    #endregion

    #region 車両検索結果モデルクラス
    /// <summary>
    /// 車両検索結果モデルクラス
    /// </summary>
    public class CarShareManagementOutModel
    {
        #region プロパティ
        /// <summary>スケジュールID</summary>
        [CellSettingAttribute(DisplayIndex = 1)]
        public long SCHEDULE_ID { get; set; }

        /// <summary>駐車場番号</summary>
        [CellSettingAttribute(DisplayIndex = 2)]
        public string 駐車場番号 { get; set; }

        /// <summary>車系</summary>
        [CellSettingAttribute(DisplayIndex = 3)]
        public string 車系 { get; set; }

        /// <summary>貸出日時</summary>
        [CellSettingAttribute(DisplayIndex = 4, HeaderText = "貸出日時")]
        public DateTime? START_DATE { get; set; }

        /// <summary>返却予定日時</summary>
        [CellSettingAttribute(DisplayIndex = 5, HeaderText = "返却予定日時")]
        public DateTime? END_DATE { get; set; }

        /// <summary>予約種別</summary>
        [CellSettingAttribute(DisplayIndex = 6)]
        public string 予約種別 { get; set; }

        /// <summary>開発符号</summary>
        [CellSettingAttribute(DisplayIndex = 7, HeaderText = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>登録ナンバー</summary>
        [CellSettingAttribute(DisplayIndex = 8)]
        public string 登録ナンバー { get; set; }

        /// <summary> 説明 </summary>
        [CellSettingAttribute(DisplayIndex = 9, HeaderText = "予約コメント", WordWrap = true)]
        public string DESCRIPTION { get; set; }

        /// <summary>予約者所属課</summary>
        //Update Start 2021/06/14 杉浦
        //[CellSettingAttribute(DisplayIndex = 10, HeaderText = "予約者所属課")]
        [CellSettingAttribute(DisplayIndex = 10, HeaderText = "予約者所属課", WordWrap = true)]
        //Update End 2021/06/14 杉浦
        public string SECTION_CODE{ get; set; }

        /// <summary>予約者</summary>
        [CellSettingAttribute(DisplayIndex = 11, HeaderText = "予約者", WordWrap = true)]
        public string NAME { get; set; }

        //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
        ////Append Start 2021/06/14 杉浦
        ///// <summary>前回予約者所属課</summary>
        //[CellSettingAttribute(DisplayIndex = 33, HeaderText = "前回予約者所属課", Visible = false)]
        //public string PREV_SECTION_CODE { get; set; }

        ///// <summary>前回予約者</summary>
        //[CellSettingAttribute(DisplayIndex = 34, HeaderText = "前回予約者", WordWrap = true, Visible = false)]
        //public string PREV_NAME { get; set; }
        ////Append End 2021/06/14 杉浦
        //Delete End 2021/10/12 杉浦 カーシェア一覧追加要望

        /// <summary>準備状況</summary>
        [CellSettingAttribute(DisplayIndex = 12, HeaderText = "準備済", ReadOnly = false)]
        public short? FLAG_準備済 { get; set; }

        /// <summary>貸出状況</summary>
        [CellSettingAttribute(DisplayIndex = 13, HeaderText = "貸出済", ReadOnly = false)]
        public short? FLAG_実使用 { get; set; }

        /// <summary>返却状況</summary>
        [CellSettingAttribute(DisplayIndex = 14, HeaderText = "返却済", ReadOnly = false)]
        public short? FLAG_返却済 { get; set; }

        /// <summary>給油状況</summary>
        [CellSettingAttribute(DisplayIndex = 15, HeaderText = "給油済", ReadOnly = false)]
        public short? FLAG_給油済 { get; set; }

        /// <summary>貸出備考</summary>
        [CellSettingAttribute(DisplayIndex = 16, WordWrap = true, ReadOnly = false)]
        public string 貸出備考 { get; set; }

        /// <summary>返却備考</summary>
        [CellSettingAttribute(DisplayIndex = 17, WordWrap = true, ReadOnly = false)]
        public string 返却備考 { get; set; }

        //Update Start 2021/05/25 杉浦 翌日貸出リストを追加する
        ///// <summary>目的</summary>
        //[CellSettingAttribute(DisplayIndex = 18, WordWrap = true)]
        //public string 目的 { get; set; }

        ///// <summary>行先</summary>
        //[CellSettingAttribute(DisplayIndex = 19, WordWrap = true)]
        //public string 行先 { get; set; }

        ///// <summary>使用者Tel</summary>
        //[CellSettingAttribute(DisplayIndex = 20, HeaderText = "使用者Tel")]
        //public string TEL { get; set; }

        ///// <summary>FLAG_ETC付</summary>
        //[CellSettingAttribute(DisplayIndex = 21, HeaderText = "ETC")]
        //public string FLAG_ETC付 { get; set; }

        ///// <summary>FLAG_ナビ付</summary>
        //[CellSettingAttribute(DisplayIndex = 22, HeaderText = "ナビ")]
        //public string FLAG_ナビ付 { get; set; }

        ///// <summary>仕向地</summary>
        //[CellSettingAttribute(DisplayIndex = 23)]
        //public string 仕向地 { get; set; }

        ///// <summary>排気量</summary>
        //[CellSettingAttribute(DisplayIndex = 24)]
        //public string 排気量 { get; set; }

        ///// <summary>トランスミッション</summary>
        //[CellSettingAttribute(DisplayIndex = 25, HeaderText = "T/M")]
        //public string トランスミッション { get; set; }

        ///// <summary>駆動方式</summary>
        //[CellSettingAttribute(DisplayIndex = 26)]
        //public string 駆動方式 { get; set; }

        ///// <summary>車型</summary>
        //[CellSettingAttribute(DisplayIndex = 27)]
        //public string 車型 { get; set; }

        ///// <summary>グレード</summary>
        //[CellSettingAttribute(DisplayIndex = 28, WordWrap = true)]
        //public string グレード { get; set; }

        ///// <summary>車体色</summary>
        //[CellSettingAttribute(DisplayIndex = 29, WordWrap = true)]
        //public string 車体色 { get; set; }

        ///// <summary>管理票No.</summary>
        //[CellSettingAttribute(DisplayIndex = 30, HeaderText = "管理票No.")]
        //public string 管理票番号 { get; set; }

        ///// <summary>項目ID</summary>
        //[CellSettingAttribute(DisplayIndex = 31, Visible = false)]
        //public long CATEGORY_ID { get; set; }

        /// <summary>翌日貸出備考</summary>
        [CellSettingAttribute(DisplayIndex = 18, WordWrap = true, ReadOnly = false)]
        public string 翌日貸出備考 { get; set; }

        /// <summary>目的</summary>
        [CellSettingAttribute(DisplayIndex = 19, WordWrap = true)]
        public string 目的 { get; set; }

        /// <summary>行先</summary>
        [CellSettingAttribute(DisplayIndex = 20, WordWrap = true)]
        public string 行先 { get; set; }

        /// <summary>使用者Tel</summary>
        //Update Start 2021/06/14 杉浦
        //[CellSettingAttribute(DisplayIndex = 21, HeaderText = "使用者Tel")]
        [CellSettingAttribute(DisplayIndex = 21, HeaderText = "使用者Tel", WordWrap = true)]
        //Update End 2021/06/14 杉浦
        public string TEL { get; set; }

        //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
        ////Append Start 2021/06/14 杉浦
        ///// <summary>前回使用者Tel</summary>
        //[CellSettingAttribute(DisplayIndex = 35, HeaderText = "前回使用者", Visible = false)]
        //public string PREV_TEL { get; set; }
        ////Append End 2021/06/14 杉浦
        //Delete End 2021/10/12 杉浦 カーシェア一覧追加要望

        //Append Start 2021/10/12 杉浦
        /// <summary>前回の予約者</summary>
        [CellSettingAttribute(DisplayIndex = 22, HeaderText = "前回の予約者")]
        public string PREV_RESERVE { get; set; }
        //Append End 2021/10/12 杉浦
        
        //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
        /// <summary>入れ替え中車両</summary>
        [CellSettingAttribute(DisplayIndex = 23, HeaderText = "入れ替え中車両", ReadOnly = false, WordWrap =true)]
        public string 入れ替え中車両 { get; set; }
        //Append End 2022/01/17 杉浦 入れ替え中車両の処理

        //Update Start 2022/01/17 杉浦 入れ替え中車両の処理
        ////Update Start 2021/10/12 杉浦 カーシェア一覧追加要望
        /////// <summary>FLAG_ETC付</summary>
        ////[CellSettingAttribute(DisplayIndex = 22, HeaderText = "ETC")]
        ////public string FLAG_ETC付 { get; set; }

        /////// <summary>FLAG_ナビ付</summary>
        ////[CellSettingAttribute(DisplayIndex = 23, HeaderText = "ナビ")]
        ////public string FLAG_ナビ付 { get; set; }

        /////// <summary>仕向地</summary>
        ////[CellSettingAttribute(DisplayIndex = 24)]
        ////public string 仕向地 { get; set; }

        /////// <summary>排気量</summary>
        ////[CellSettingAttribute(DisplayIndex = 25)]
        ////public string 排気量 { get; set; }

        /////// <summary>トランスミッション</summary>
        ////[CellSettingAttribute(DisplayIndex = 26, HeaderText = "T/M")]
        ////public string トランスミッション { get; set; }

        /////// <summary>駆動方式</summary>
        ////[CellSettingAttribute(DisplayIndex = 27)]
        ////public string 駆動方式 { get; set; }

        /////// <summary>車型</summary>
        ////[CellSettingAttribute(DisplayIndex = 28)]
        ////public string 車型 { get; set; }

        /////// <summary>グレード</summary>
        ////[CellSettingAttribute(DisplayIndex = 29, WordWrap = true)]
        ////public string グレード { get; set; }

        /////// <summary>車体色</summary>
        ////[CellSettingAttribute(DisplayIndex = 30, WordWrap = true)]
        ////public string 車体色 { get; set; }

        /////// <summary>管理票No.</summary>
        ////[CellSettingAttribute(DisplayIndex = 31, HeaderText = "管理票No.")]
        ////public string 管理票番号 { get; set; }

        /////// <summary>項目ID</summary>
        ////[CellSettingAttribute(DisplayIndex = 32, Visible = false)]
        ////public long CATEGORY_ID { get; set; }

        ///// <summary>FLAG_ETC付</summary>
        //[CellSettingAttribute(DisplayIndex = 23, HeaderText = "ETC")]
        //public string FLAG_ETC付 { get; set; }

        ///// <summary>FLAG_ナビ付</summary>
        //[CellSettingAttribute(DisplayIndex = 24, HeaderText = "ナビ")]
        //public string FLAG_ナビ付 { get; set; }

        ///// <summary>仕向地</summary>
        //[CellSettingAttribute(DisplayIndex = 25)]
        //public string 仕向地 { get; set; }

        ///// <summary>排気量</summary>
        //[CellSettingAttribute(DisplayIndex = 26)]
        //public string 排気量 { get; set; }

        ///// <summary>トランスミッション</summary>
        //[CellSettingAttribute(DisplayIndex = 27, HeaderText = "T/M")]
        //public string トランスミッション { get; set; }

        ///// <summary>駆動方式</summary>
        //[CellSettingAttribute(DisplayIndex = 28)]
        //public string 駆動方式 { get; set; }

        ///// <summary>車型</summary>
        //[CellSettingAttribute(DisplayIndex = 29)]
        //public string 車型 { get; set; }

        ///// <summary>グレード</summary>
        //[CellSettingAttribute(DisplayIndex = 30, WordWrap = true)]
        //public string グレード { get; set; }

        ///// <summary>車体色</summary>
        //[CellSettingAttribute(DisplayIndex = 31, WordWrap = true)]
        //public string 車体色 { get; set; }

        ///// <summary>管理票No.</summary>
        //[CellSettingAttribute(DisplayIndex = 32, HeaderText = "管理票No.")]
        //public string 管理票番号 { get; set; }

        ///// <summary>項目ID</summary>
        //[CellSettingAttribute(DisplayIndex = 33, Visible = false)]
        //public long CATEGORY_ID { get; set; }
        /// <summary>FLAG_ETC付</summary>
        [CellSettingAttribute(DisplayIndex = 24, HeaderText = "ETC")]
        public string FLAG_ETC付 { get; set; }

        /// <summary>FLAG_ナビ付</summary>
        [CellSettingAttribute(DisplayIndex = 25, HeaderText = "ナビ")]
        public string FLAG_ナビ付 { get; set; }

        /// <summary>仕向地</summary>
        [CellSettingAttribute(DisplayIndex = 26)]
        public string 仕向地 { get; set; }

        /// <summary>排気量</summary>
        [CellSettingAttribute(DisplayIndex = 27)]
        public string 排気量 { get; set; }

        /// <summary>トランスミッション</summary>
        [CellSettingAttribute(DisplayIndex = 28, HeaderText = "T/M")]
        public string トランスミッション { get; set; }

        /// <summary>駆動方式</summary>
        [CellSettingAttribute(DisplayIndex = 29)]
        public string 駆動方式 { get; set; }

        /// <summary>車型</summary>
        [CellSettingAttribute(DisplayIndex = 30)]
        public string 車型 { get; set; }

        /// <summary>グレード</summary>
        [CellSettingAttribute(DisplayIndex = 31, WordWrap = true)]
        public string グレード { get; set; }

        /// <summary>車体色</summary>
        [CellSettingAttribute(DisplayIndex = 32, WordWrap = true)]
        public string 車体色 { get; set; }

        /// <summary>管理票No.</summary>
        [CellSettingAttribute(DisplayIndex = 33, HeaderText = "管理票No.")]
        public string 管理票番号 { get; set; }

        /// <summary>項目ID</summary>
        [CellSettingAttribute(DisplayIndex = 34, Visible = false)]
        public long CATEGORY_ID { get; set; }
        //Update End 2022/01/17 杉浦 入れ替え中車両の処理
        //Update End 2021/05/25 杉浦 翌日貸出リストを追加する
        //Update End 2021/10/12 杉浦 カーシェア一覧追加要望
        #endregion
    }
    #endregion
}
