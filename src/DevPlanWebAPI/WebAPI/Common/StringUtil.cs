using System.Text;
using System.Text.RegularExpressions;

namespace DevPlanWebAPI.Common
{
    /// <summary>
    /// 文字列変換クラス
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 文字列のバイト数を取得する
        /// </summary>
        /// <param name="str"></param>
        public static int LenB(string str)
        {
            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            return enc.GetByteCount(str);
        }

        /// <summary>
        /// 文字列の左端から指定したバイト数分の文字列を返す
        /// </summary>
        /// <param name="stTarget">取り出す元になる文字列</param>
        /// <param name="iByteSize">取り出すバイト数</param>
        /// <returns>左端から指定されたバイト数分の文字列</returns>
        public static string LeftB(string stTarget, int iByteSize)
        {
            return MidB(stTarget, 1, iByteSize);
        }

        /// <summary>
        /// 文字列の指定されたバイト位置以降のすべての文字列を返す
        /// </summary>
        /// <param name="stTarget">取り出す元になる文字列。</param>
        /// <param name="iStart">取り出しを開始する位置。</param>
        /// <returns>指定されたバイト位置以降のすべての文字列。</returns>
        public static string MidB(string stTarget, int iStart)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            if (iStart >= hEncoding.GetByteCount(stTarget))
            {
                return string.Empty;
            }
            byte[] btBytes = hEncoding.GetBytes(stTarget);
            return hEncoding.GetString(btBytes, iStart - 1, btBytes.Length - iStart + 1);
        }

        /// <summary>
        /// 文字列の指定されたバイト位置から、指定されたバイト数分の文字列を返す
        /// </summary>
        /// <param name="stTarget">取り出す元になる文字列。</param>
        /// <param name="iStart">取り出しを開始する位置。</param>
        /// <param name="iByteSize">取り出すバイト数。</param>
        /// <returns>指定されたバイト位置から指定されたバイト数分の文字列。</returns>
        public static string MidB(string stTarget, int iStart, int iByteSize)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            if (iStart >= hEncoding.GetByteCount(stTarget))
            {
                return string.Empty;
            }
            if (iByteSize >= hEncoding.GetByteCount(stTarget) - iStart)
            {
                return MidB(stTarget, iStart);
            }
            else
            {
                byte[] btBytes = hEncoding.GetBytes(stTarget);
                return hEncoding.GetString(btBytes, iStart - 1, iByteSize);
            }
        }

        /// <summary>
        /// 文字列の右端から指定されたバイト数分の文字列を返します。
        /// </summary>
        /// <param name="stTarget">取り出す元になる文字列。</param>
        /// <param name="iByteSize">取り出すバイト数。</param>
        /// <returns>右端から指定されたバイト数分の文字列。</returns>
        public static string RightB(string stTarget, int iByteSize)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(stTarget);

            return hEncoding.GetString(btBytes, btBytes.Length - iByteSize, iByteSize);
        }

        /// <summary>
        /// ひらがなチェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHiragana(string str)
        {
            return Regex.IsMatch(str.Replace("　", "").Replace(" ", ""), @"^\p{IsHiragana}*$");
        }

        /// <summary>
        /// 全角カタカナチェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsKatakana(string str)
        {
            return Regex.IsMatch(str.Replace("　", "").Replace(" ", ""), @"^\p{IsKatakana}*$");
        }

        /// <summary>
        /// 半角カタカナチェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHanKatakana(string str)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(str);
            string strTarget = hEncoding.GetString(btBytes);

            return Regex.IsMatch(strTarget.Replace(" ", ""), @"^[ｦ-ﾟ]*$");
        }

        /// <summary>
        /// 半角チェック(半角英数と半角カタカナと半角記号)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHankaku(string str)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(str);
            string strTarget = hEncoding.GetString(btBytes);

            return Regex.IsMatch(strTarget.Replace(" ", ""), @"^[ -~｡-ﾟ]*$");
        }

        /// <summary>
        /// 半角数字チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(str);
            string strTarget = hEncoding.GetString(btBytes);

            return Regex.IsMatch(strTarget, @"^[0-9]+$");
        }

        /// <summary>
        /// 文字列内のカタカナだけを全角に変換します。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertHanKatakanaToZenKatakana(string s)
        {
            string returnString = string.Empty;

            System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
            byte[] btBytes = hEncoding.GetBytes(s);
            string strTarget = hEncoding.GetString(btBytes);

            string p1 = @"[ﾊ-ﾎ]ﾟ";              // 「ﾊﾟ」など「゜」がつく文字
            string p2 = @"[ｶ-ｺｻ-ｿﾀ-ﾄﾊ-ﾎｳ]ﾞ";    // 「ﾊﾞ」など「゛」がつく文字
            string p3 = @"[ｦ-ﾝ]";               // 上記にあてはまらない半角カタカナ
            string p4 = @"[^ｦ-ﾝ]";              // 半角カタカナ以外

            string pattern = p1 + "|" + p2 + "|" + p3 + "|" + p4;
            string notConvPattern = @"^" + p4;  // ※変換対象から除外

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern);
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(strTarget, pattern);
            foreach (System.Text.RegularExpressions.Match m in matches)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(m.ToString(), notConvPattern))
                {
                    // 変換の対象外（記号や数字など）
                    returnString += m.ToString();
                }
                else
                {
                    // 変換
                    returnString += System.Text.RegularExpressions.Regex.Replace(
                      m.ToString(),
                      pattern,
                      Microsoft.VisualBasic.Strings.StrConv(m.ToString(), Microsoft.VisualBasic.VbStrConv.Wide, 0x411));
                }
            }
            return returnString;
        }
    }
}
