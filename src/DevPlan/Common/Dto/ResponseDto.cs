using System.IO;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// HTTP レスポンス結果（HTTP Body）モデルクラス
    /// </summary>
    /// <typeparam name="Result">データの型</typeparam>
    public class ResponseDto<Result>
    {
        /// <summary>
        /// ステータス（実行結果）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// エラーコード
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// エラー内容
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 取得データ
        /// </summary>
        public IEnumerable<Result> Results { get; set; } = null;

        /// <summary>
        /// ファイル
        /// </summary>
        public IDictionary<Stream, string> FileMap { get; set; } = null;

        /// <summary>
        /// ページ情報
        /// </summary>
        public PageInfoModel PageInfo { get; set; } = null;

    }

    /// <summary>
    /// ページ情報モデルクラス
    /// </summary>
    public class PageInfoModel
    {
        /// <summary>
        /// 総ページ数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 現ページ数
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 次ページの有無
        /// </summary>
        public bool HasNextPage { get; set; }
        /// <summary>
        /// 前ページの有無
        /// </summary>
        public bool HasPrevPage { get; set; }
        /// <summary>
        /// 取得データ件数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 取得データ件数の上限
        /// </summary>
        public int Limit { get; set; }

    }
}