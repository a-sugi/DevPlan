using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェック指摘検索出力モデルクラス
    /// </summary>
    public class DesignCheckImportModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int? 開催日_ID { get; set; }
        /// <summary>
        /// 指摘NO
        /// </summary>
        public int? 指摘NO { get; set; }

        //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試作管理NO
        /// </summary>
        public int? 試作管理NO { get; set; }
        //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

        //Append Start 2021/06/11 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        //Append End 2021/06/11 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// FLAG_CLOSE
        /// </summary>
        public int? FLAG_CLOSE { get; set; }

        //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
        /// <summary>
        /// FLAG_試作CLOSE
        /// </summary>
        public int? FLAG_試作CLOSE { get; set; }
        //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

        /// <summary>
        /// 指摘部品
        /// </summary>
        public string 部品 { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        public string 状況 { get; set; }
        /// <summary>
        /// FLAG_処置不要
        /// </summary>
        public int? FLAG_処置不要 { get; set; }
        /// <summary>
        /// 処置課
        /// </summary>
        public string 処置課 { get; set; }
        /// <summary>
        /// 処置対象
        /// </summary>
        public string 処置対象 { get; set; }
        /// <summary>
        /// 処置方法
        /// </summary>
        public string 処置方法 { get; set; }
        /// <summary>
        /// FLAG_調整済
        /// </summary>
        public int? FLAG_調整済 { get; set; }
        /// <summary>
        /// 処置調整
        /// </summary>
        public string 処置調整 { get; set; }
        /// <summary>
        /// 織込日程
        /// </summary>
        public DateTime? 織込日程 { get; set; }
        /// <summary>
        /// FLAG_試作改修
        /// </summary>
        public int? FLAG_試作改修 { get; set; }
        /// <summary>
        /// 部品納入日
        /// </summary>
        public DateTime? 部品納入日 { get; set; }
        /// <summary>
        /// 完了確認日
        /// </summary>
        public DateTime? 完了確認日 { get; set; }
        /// <summary>
        /// FLAG_上司承認
        /// </summary>
        public int? FLAG_上司承認 { get; set; }
        /// <summary>
        /// 担当課名
        /// </summary>
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当課_ID
        /// </summary>
        public string 担当課_ID { get; set; }
        /// <summary>
        /// 担当者_ID
        /// </summary>
        public string 担当者_ID { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string 担当者名 { get; set; }
        /// <summary>
        /// 担当者_TEL
        /// </summary>
        public string 担当者_TEL { get; set; }
        /// <summary>
        /// 編集者日
        /// </summary>
        public DateTime? 編集者日 { get; set; }
        /// <summary>
        /// 編集者_ID
        /// </summary>
        public string 編集者_ID { get; set; }
        /// <summary>
        /// 履歴作成日
        /// </summary>
        public DateTime? 履歴作成日 { get; set; }
        /// <summary>
        /// FLAG_最新
        /// </summary>
        public int? FLAG_最新 { get; set; }
    }
    //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修
}
