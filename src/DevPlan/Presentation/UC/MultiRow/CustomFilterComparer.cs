using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UC.MultiRow
{
    /// <summary>
    /// カスタム比較クラス
    /// </summary>
    /// <remarks>MultiRowのフィルタリング条件を記述</remarks>
    public class CustomFilterComparer : IComparer
    {
        /// <summary>
        /// カスタム比較
        /// </summary>
        /// <returns>int</returns>
        int IComparer.Compare(Object x, Object y)
        {
            // 大文字と小文字を区別しない
            return CustomConverter(y?.ToString())
                .IndexOf(CustomConverter(x?.ToString()), StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : -1;
        }

        /// <summary>
        /// カスタム変換
        /// </summary>
        /// <returns>string</returns>
        private string CustomConverter(string str)
        {
            // 全角を半角に変換する
            str = StringUtil.ConvertFullToHalf(str);

            // [-][:][ ]を除外する
            str = Regex.Replace(str, "[:\\- ]+", string.Empty);

            return str;
        }
    }
}
