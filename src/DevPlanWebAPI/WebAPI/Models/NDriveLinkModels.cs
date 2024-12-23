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
    public class NDriveLinkInModel
    {
        /// <summary>
        /// ID(個別にお知らせを呼び出す場合は必須)
        /// </summary>
        public Int64? ID { get; set; }
        /// <summary>
        /// CAP項目ID
        /// </summary>
        public Int64? CAP_ID { get; set; }
    }

    /// <summary>
    /// お知らせ検索モデルクラス(出力)
    /// </summary>
    public class NDriveLinkOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
        /// <summary>
        /// CAP項目ID
        /// </summary>
        public Int64 CAP_ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 関連URL
        /// </summary>
        public string URL { get; set; }
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
    public class NDriveLinkRegistInModel
    {
        /// <summary>
        /// CAP項目ID
        /// </summary>
        public Int64 CAP_ID { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 関連URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 投稿者
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// お知らせ更新モデルクラス(入力)
    /// </summary>
    public class NDriveLinkUpdateInModel
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
        /// 投稿者
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// お知らせ削除モデルクラス(入力)
    /// </summary>
    public class NDriveLinkDeleteInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
    }
}