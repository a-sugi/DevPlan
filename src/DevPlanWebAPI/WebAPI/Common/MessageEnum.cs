using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Common
{
    #region メッセージ区分
    /// <summary>
    /// メッセージ区分
    /// </summary>
    public enum MessageType : int
    {
        /// <summary>なし</summary>
        None,

        /// <summary>予期しないエラーが発生しました。システム管理者に連絡してください。</summary>
        KKE03001,

        /// <summary>該当データが見つかりませんでした。</summary>
        KKE03002,

        /// <summary>ユーザーIDが登録されていません。</summary>
        KKE03003,

        /// <summary>パスワードに誤りがあります。</summary>
        KKE03004,

        /// <summary>{0}が未入力です。</summary>
        KKE03005,

        /// <summary>{0}が数値として判断できません。</summary>
        KKE03006,

        /// <summary>{0}が日付として判断できません。</summary>
        KKE03007,

        /// <summary>{0}が桁数の最大値を超えています。</summary>
        KKE03008,

        /// <summary>入力された{0}はすでに登録されています。</summary>
        KKE03009,

        /// <summary>{0}にはこのシステムを利用する権限がありません。</summary>
        KKE03010,

        /// <summary>{0}が桁数の最大値を超えています。半角{1}桁または全角{2}桁以内で入力してください。</summary>
        KKE03011,

        /// <summary>{0}にカタカナ以外の文字が含まれています。</summary>
        KKE03012,

        /// <summary>{0}のファイルパスが取得できませんでした。</summary>
        KKE03013,

        /// <summary>指定されたユーザーIDはActiveDirectoryに存在しません。</summary>
        KKE03014,

        /// <summary>日付範囲に誤りがあります。{0}に{1}を超える日付は入力できません。</summary>
        KKE03015,

        /// <summary>{0}に指定されたファイルは存在しません。</summary>
        KKE03016,

        /// <summary>{0}が許容範囲値外です。</summary>
        KKE03017,

        /// <summary>コピー・移動元の情報が正しく取得できませんでした。</summary>
        KKE03018,

        /// <summary>コピー・移動先に指定された開発符号は既に存在しています。</summary>
        KKE03019,

        /// <summary>パラメータが不正です。</summary>
        KKE03020,

        /// <summary>ログインに失敗しました。入力内容を確認して下さい。</summary>
        KKE03021,

        /// <summary>ログイン権限がありません。</summary>
        KKE03022,

        /// <summary>入力されたスケジュールは重複しています。</summary>
        KKE03023,

        /// <summary>本予約作業出来ませんでした。</summary>
        KKE03024,

        /// <summary>トラック予約定期便用番号付与失敗</summary>
        KKE03025
    }
    #endregion

}