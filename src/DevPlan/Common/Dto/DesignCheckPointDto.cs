using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// 設計チェック指摘検索入力モデルクラス
    /// </summary>
    public class DesignCheckPointGetInModel
    {
        /// <summary>
        /// 指摘ID
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
        //Append Start 2021/07/28 杉浦 設計チェックインポート
        /// <summary>
        /// 指摘NO
        /// </summary>
        public int? 試作管理NO { get; set; }
        //Append End 2021/07/28 杉浦 設計チェックインポート
        /// <summary>
        /// 試験車ID
        /// </summary>
        public int? 試験車_ID { get; set; }
        /// <summary>
        /// 状況記号
        /// </summary>
        public string 状況記号 { get; set; }
        /// <summary>
        /// 担当課コード
        /// </summary>
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当課長承認フラグ
        /// </summary>
        public bool? APPROVAL_FLG { get; set; }
        /// <summary>
        /// オープンフラグ
        /// </summary>
        public bool? OPEN_FLG { get; set; }
        /// <summary>
        /// 最新フラグ
        /// </summary>
        public bool? NEW_FLG { get; set; }
    }
    /// <summary>
    /// 設計チェック指摘検索出力モデルクラス
    /// </summary>
    public class DesignCheckPointGetOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
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
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// 試験車_ID
        /// </summary>
        public int? 試験車_ID { get; set; }
        /// <summary>
        /// 対象車両_ID
        /// </summary>
        public int? 対象車両_ID { get; set; }
        /// <summary>
        /// 状況記号
        /// </summary>
        public string 状況記号 { get; set; }
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
        /// 編集者名
        /// </summary>
        public string 編集者名 { get; set; }
        /// <summary>
        /// 履歴作成日
        /// </summary>
        public DateTime? 履歴作成日 { get; set; }
        /// <summary>
        /// FLAG_最新
        /// </summary>
        public int? FLAG_最新 { get; set; }
        /// <summary>
        /// 履歴件数
        /// </summary>
        public long? HISTORY_COUNT { get; set; }
        /// <summary>
        /// ステータス（DTO用）
        /// </summary>
        public string STATUS { get; set; } = string.Empty;

        //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
        /// <summary>
        /// 試作ステータス（DTO用）
        /// </summary>
        public string 試作CLOSE { get; set; } = string.Empty;
        //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修

        /// <summary>
        /// 削除フラグ（DTO用）
        /// </summary>
        public bool? DELETE_FLG { get; set; } = false;
    }
    /// <summary>
    /// 設計チェック指摘登録入力モデルクラス
    /// </summary>
    public class DesignCheckPointPostInModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
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
        /// 担当者_TEL
        /// </summary>
        public string 担当者_TEL { get; set; }
        /// <summary>
        /// 編集者_ID
        /// </summary>
        public string 編集者_ID { get; set; }
        /// <summary>
        /// 状況（対象）リスト
        /// </summary>
        public List<DesignCheckProgressPostInModel> PROGRESS_LIST { get; set; } = new List<DesignCheckProgressPostInModel>();
    }
    /// <summary>
    /// 設計チェック指摘登録出力モデルクラス
    /// </summary>
    public class DesignCheckPointPostOutModel: DesignCheckPointGetOutModel
    {
        /// <summary>
        /// 仮指摘ID
        /// </summary>
        public int TEMP_ID { get; set; }
    }
    /// <summary>
    /// 設計チェック指摘削除入力モデルクラス
    /// </summary>
    public class DesignCheckPointDeleteInModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        public int ID { get; set; }
    }
}
