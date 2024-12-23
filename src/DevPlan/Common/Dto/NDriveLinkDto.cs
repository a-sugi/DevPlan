//Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
using System;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 写真・動画検索モデルクラス(入力)
    /// </summary>
    public class NDriveLinkInModel
    {
        /// <summary>
        /// ID(個別にURL呼び出す場合は必須)
        /// </summary>
        public long? ID { get; set; }
        /// <summary>
        /// CAP_ID
        /// </summary>
        public int? CAP_ID { get; set; }
    }

    /// <summary>
    /// 写真・動画検索モデルクラス(出力)
    /// </summary>
    public class NDriveLinkOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// CAP_ID
        /// </summary>
        public int? CAP_ID { get; set; }
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
    /// 写真・動画登録モデルクラス(入力)
    /// </summary>
    public class NDriveLinkRegistInModel
    {
        /// <summary>
        /// CAP_ID
        /// </summary>
        public int CAP_ID { get; set; }
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
    /// 写真・動画更新モデルクラス(入力)
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
    /// 写真・動画削除モデルクラス(入力)
    /// </summary>
    public class NDriveLinkDeleteInModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID { get; set; }
    }
}

//Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加