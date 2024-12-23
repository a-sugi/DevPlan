using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 閲覧権限状況検索モデル（入力）
    /// </summary>
    public class BrowsingAuthorityStatusInModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
    }

    /// <summary>
    /// 閲覧権限状況検索モデル（出力）
    /// </summary>
    public class BrowsingAuthorityStatusOutModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 会社名
        /// </summary>
        public string COMPANY { get; set; }

        /// <summary>
        /// 他部署閲覧権限
        /// </summary>
        public int? 他部署閲覧権限 { get; set; }

        /// <summary>
        /// 他部署閲覧権限
        /// </summary>
        public int? 派遣者閲覧権限 { get; set; }

        /// <summary>
        /// 他部署閲覧権限
        /// </summary>
        public int? 開発日程閲覧権限 { get; set; }

        /// <summary>
        /// 他部署閲覧権限
        /// </summary>
        public int? 外製車日程閲覧権限 { get; set; }

        /// <summary>
        /// 他部署閲覧権限
        /// </summary>
        public int? PU制御開発日程閲覧権限 { get; set; }

        /// <summary>
        /// メッセージ非表示
        /// </summary>
        public short? メッセージ非表示 { get; set; }

        /// <summary>
        /// 解除日
        /// </summary>
        public DateTime 解除日 { get; set; }
    }

    /// <summary>
    /// 閲覧権限状況更新モデル
    /// </summary>
    public class BrowsingAuthorityStatusPutModel
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// メッセージ非表示(1:非表示)
        /// </summary>
        public short? メッセージ非表示 { get; set; }
    }
}