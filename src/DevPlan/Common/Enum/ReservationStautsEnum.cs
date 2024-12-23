using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Enum
{
    /// <summary>
    /// 予約種別（仮予約、本予約）TypeSafeEnum。
    /// </summary>
    public sealed class ReservationStautsEnum
    {
        /// <summary>内部定義インスタンス保持ディクショナリ。</summary>
        private static readonly Dictionary<string, ReservationStautsEnum> dic = new Dictionary<string, ReservationStautsEnum>();

        /// <summary>
        /// 仮予約
        /// </summary>
        public static readonly ReservationStautsEnum KARI_YOYAKU = new ReservationStautsEnum("仮予約", "1", "仮");

        /// <summary>
        /// 本予約
        /// </summary>
        public static readonly ReservationStautsEnum HON_YOYAKU = new ReservationStautsEnum("本予約", "0", "本");

        /// <summary>
        /// なし（仮、本予約の概念なし）
        /// </summary>
        public static readonly ReservationStautsEnum NONE = new ReservationStautsEnum("", "", "");

        /// <summary>
        /// スケジュールが保持している文字列
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// FLAG_仮予約の区分
        /// </summary>
        public string Flag { get; private set; }

        /// <summary>
        /// スケジュールが保持している文字列（短）
        /// </summary>
        public string ShortName { get; private set; }

        #region サーチメソッド類

        public static Dictionary<string, ReservationStautsEnum> GetValues()
        {
            return dic;
        }

        public static ReservationStautsEnum KeyOf(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = NONE.Key;
            }
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                return NONE;
            }
        }

        public static ReservationStautsEnum ShortNameOf(string shortName)
        {
            if (string.IsNullOrEmpty(shortName))
            {
                shortName = NONE.ShortName;
            }

            foreach (var item in dic)
            {
                if (item.Value.ShortName == shortName)
                {
                    return item.Value;
                }
            }
            return NONE;
        }

        public static ReservationStautsEnum FlagOf(string flag)
        {
            if (string.IsNullOrEmpty(flag))
            {
                flag = NONE.Flag;
            }

            foreach (var item in dic)
            {
                if (item.Value.Flag == flag)
                {
                    return item.Value;
                }
            }
            return NONE;
        }

        #endregion

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="flag">フラグ</param>
        /// <param name="shortName">短い名称</param>
        public ReservationStautsEnum(string key, string flag, string shortName)
        {
            this.Key = key;
            this.Flag = flag;
            this.ShortName = shortName;

            dic.Add(key, this);
        }
    }
}
