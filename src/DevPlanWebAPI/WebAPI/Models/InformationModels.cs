//
// 業務計画表システム
// KKA00130～160 お知らせAPI
// 作成者 : 岸　義将
// 作成日 : 2017/02/13

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// お知らせ検索モデルクラス(入力)
    /// </summary>
    public class InformationInModel
    {
        /// <summary>
        /// ID(個別にお知らせを呼び出す場合は必須)
        /// </summary>
        public Int64? ID { get; set; }
        /// <summary>
        /// ステータス(1:公開中 2:全て)
        /// </summary>
        public int? STATUS { get; set; }
    }

    /// <summary>
    /// お知らせ検索モデルクラス(出力)
    /// </summary>
    public class InformationOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 関連URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 公開開始日
        /// </summary>
        public DateTime RELEASE_START_DATE { get; set; }
        /// <summary>
        /// 公開終了日
        /// </summary>
        public DateTime RELEASE_END_DATE { get; set; }
        /// <summary>
        /// 投稿日
        /// </summary>
        public DateTime INPUT_DATETIME { get; set; }
        /// <summary>
        /// 投稿者
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 日付とタイトル
        /// </summary>
        public string DATETITLE { get; set; }
    }

    /// <summary>
    /// お知らせ登録モデルクラス(入力)
    /// </summary>
    public class InformationRegistInModel
    {
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 関連URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 公開開始日
        /// </summary>
        public DateTime RELEASE_START_DATE { get; set; }
        /// <summary>
        /// 公開終了日
        /// </summary>
        public DateTime RELEASE_END_DATE { get; set; }
        /// <summary>
        /// 投稿者
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// お知らせ更新モデルクラス(入力)
    /// </summary>
    public class InformationUpdateInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 関連URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 公開開始日
        /// </summary>
        public DateTime RELEASE_START_DATE { get; set; }
        /// <summary>
        /// 公開終了日
        /// </summary>
        public DateTime RELEASE_END_DATE { get; set; }
        /// <summary>
        /// 投稿者
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// お知らせ削除モデルクラス(入力)
    /// </summary>
    public class InformationDeleteInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
    }
}