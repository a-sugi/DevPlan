using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Enum
{
    /// <summary>
    /// カレンダーで利用するカラー設定情報定義。
    /// </summary>
    public sealed class CalendarScheduleColorEnum
    {
        /// <summary>内部定義インスタンス保持ディクショナリ。</summary>
        private static readonly Dictionary<string, CalendarScheduleColorEnum> dic = new Dictionary<string, CalendarScheduleColorEnum>();

        /// <summary>
        /// スケジュール区分-無し
        /// </summary>
        public static readonly CalendarScheduleColorEnum DefaultColor =
            new CalendarScheduleColorEnum("DefaultColor",
                Color.FromArgb(0xFF, 0xF0, 0x2D), Color.Black, Color.FromArgb(0xFF, 0xF6, 0x7F), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// スケジュール区分-2:■
        /// </summary>
        public static readonly CalendarScheduleColorEnum SquareColor =
            new CalendarScheduleColorEnum("SquareColor",
                Color.FromArgb(0xA3, 0xC9, 0x43), Color.Black, Color.FromArgb(0xC1, 0xDB, 0x81), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// スケジュール区分-3:▲
        /// </summary>
        public static readonly CalendarScheduleColorEnum TriangleColor =
            new CalendarScheduleColorEnum("TriangleColor",
                Color.FromArgb(0x47, 0xDC, 0xFF), Color.Black, Color.FromArgb(0x8F, 0xEA, 0xFF), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// スケジュール区分-4:◎
        /// </summary>
        public static readonly CalendarScheduleColorEnum DoubleCircleColor =
            new CalendarScheduleColorEnum("DoubleCircleColor",
                Color.FromArgb(0xF6, 0xA8, 0x36), Color.Black, Color.FromArgb(0xF9, 0xC2, 0x70), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// 仮予約不要（項目名背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum NoneReservationColor =
            new CalendarScheduleColorEnum("NoneReservationColor", Color.FromArgb(0x86, 0xFF, 0xFF), Color.Black);

        /// <summary>
        /// 仮予約（項目名背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum TentativeReservationColor =
            new CalendarScheduleColorEnum("TentativeReservationColor", Color.FromArgb(0xFF, 0x99, 0xFF), Color.Black);

        /// <summary>
        /// 予約許可必須-本予約
        /// </summary>
        public static readonly CalendarScheduleColorEnum YoyakuKyokaHonyoyaku =
            new CalendarScheduleColorEnum("YoyakuKyokaHonyoyaku",
                Color.FromArgb(0x29, 0xff, 0x29), Color.Black, Color.FromArgb(0x89, 0xFF, 0x89), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// 予約許可必須-仮予約
        /// </summary>
        public static readonly CalendarScheduleColorEnum YoyakuKyokaKariyoyaku =
            new CalendarScheduleColorEnum("YoyakuKyokaKariyoyaku",
                Color.FromArgb(0x64, 0x64, 0x64), Color.White, Color.FromArgb(0x9D, 0x9D, 0x9D), Color.White);

        /// <summary>
        /// 本予約-過去-実使用-返却済
        /// </summary>
        public static readonly CalendarScheduleColorEnum HonyoyakuSiyouHenkyaku =
            new CalendarScheduleColorEnum("HonyoyakuSiyouHenkyaku",
                Color.FromArgb(0x00, 0x00, 0xe6), Color.White, Color.FromArgb(0x5B, 0x5B, 0xFF), Color.White);

        /// <summary>
        /// 本予約-過去-実使用-未返却
        /// </summary>
        public static readonly CalendarScheduleColorEnum HonyoyakuSiyouMihenkyaku =
            new CalendarScheduleColorEnum("HonyoyakuSiyouMihenkyaku",
                Color.FromArgb(0x47, 0xDC, 0xFF), Color.Black, Color.FromArgb(0x8F, 0xEA, 0xFF), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// 本予約-過去-使用中
        /// </summary>
        public static readonly CalendarScheduleColorEnum HonyoyakuSiyoutyuu =
            new CalendarScheduleColorEnum("HonyoyakuSiyoutyuu",
                Color.FromArgb(0xFF, 0x94, 0x05), Color.Black, Color.FromArgb(0xFF, 0xC4, 0x75), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// 本予約-過去-終了
        /// </summary>
        public static readonly CalendarScheduleColorEnum HonyoyakuEnd =
            new CalendarScheduleColorEnum("HonyoyakuEnd",
                Color.FromArgb(0xFF, 0x00, 0x00), Color.White, Color.FromArgb(0xFF, 0x5B, 0x5B), Color.White);

        /// <summary>
        /// 本予約-未来
        /// </summary>
        public static readonly CalendarScheduleColorEnum HonyoyakuFuture =
            new CalendarScheduleColorEnum("HonyoyakuFuture",
                Color.FromArgb(0xFF, 0x21, 0x90), Color.White, Color.FromArgb(0xFF, 0x69, 0xB4), Color.White);

        /// <summary>
        /// 上記以外
        /// </summary>
        public static readonly CalendarScheduleColorEnum Other =
            new CalendarScheduleColorEnum("Other",
                Color.FromArgb(0x9D, 0x9D, 0x9D), Color.Black, Color.FromArgb(0xC7, 0xC7, 0xC7), Color.FromArgb(0x33, 0x33, 0x33));

        /// <summary>
        /// 予約不可（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum NoneReservation =
            new CalendarScheduleColorEnum("NoneReservation", Color.FromArgb(0x00, 0x00, 0x00), Color.Black);

        /// <summary>
        /// 予約期限超過（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum ReservationOver =
            new CalendarScheduleColorEnum("ReservationOver", Color.FromArgb(0xFF, 0xFF, 0xFF), Color.Black);

        /// <summary>
        /// 通常（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum NomalColor =
            new CalendarScheduleColorEnum("NomalColor", Color.WhiteSmoke, Color.Black);

        /// <summary>
        /// 当日（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum TodayColor =
            new CalendarScheduleColorEnum("TodayColor", Color.FromArgb(0xC0, 0xFF, 0xFF), Color.Black);

        /// <summary>
        /// 平日（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum WeekdayColor =
            new CalendarScheduleColorEnum("WeekdayColor", Color.White, Color.Black);

        /// <summary>
        /// 休日（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum HolidayColor =
            new CalendarScheduleColorEnum("HolidayColor", Color.FromArgb(0xCC, 0xCC, 0xCC), Color.Black);

        /// <summary>
        /// 過去日（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum PastColor =
            new CalendarScheduleColorEnum("PastColor", Color.FromArgb(0xE5, 0xE5, 0xE5), Color.Black);

        /// <summary>
        /// クローズ（カレンダーセル背景）
        /// </summary>
        public static readonly CalendarScheduleColorEnum CloseColor =
            new CalendarScheduleColorEnum("CloseColor", Color.FromArgb(0xB3, 0xB3, 0xB3), Color.Black);

        /// <summary>
        /// 目立たせる項目の背景色
        /// </summary>
        public static readonly CalendarScheduleColorEnum CheckItemColor =
            new CalendarScheduleColorEnum("CheckItemColor", Color.Yellow, Color.Black);
        
        /// <summary>
        /// トラック予約各トラック背景色
        /// </summary>
        public static readonly CalendarScheduleColorEnum TruckNormalColor =
            new CalendarScheduleColorEnum("TruckNormalColor", Color.White, Color.Black);

        /// <summary>
        /// 車両管理者（連絡先）項目セル
        /// </summary>
        public static readonly CalendarScheduleColorEnum ContactInfoItemColor =
            new CalendarScheduleColorEnum("ContactInfoItemColor", Color.White, Color.Black, BorderLineStyle.Thick);

        #region プロパティ

        /// <summary>
        /// キー
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 塗りつぶしカラー
        /// </summary>
        public Color MainColor { get; private set; }

        /// <summary>
        /// フォントカラー
        /// </summary>
        public Color FontColor { get; private set; }

        /// <summary>
        /// 塗りつぶし（読み取り専用）カラー
        /// </summary>
        public Color MainReadOnlyColor { get; private set; }

        /// <summary>
        /// 読み取り専用フォントカラー
        /// </summary>
        public Color FontReadOnlyColor { get; private set; }

        /// <summary>枠線</summary>
        public BorderLineStyle BorderStyle { get; private set; }

        #endregion

        #region サーチメソッド類

        /// <summary>
        /// カラー検索処理
        /// </summary>
        /// <remarks>
        /// 定義体から指定された色を検索します。検索値があった場合は最初に見つかった定義体を、
        /// 見つからなかった場合は指定の色を返します。
        /// </remarks>
        /// <returns></returns>
        public static CalendarScheduleColorEnum GetValues(Color colorType, Color fontColor)
        {
            foreach (var item in dic)
            {
                if (item.Value.MainColor == colorType)
                {
                    return item.Value;
                }
            }

            return new CalendarScheduleColorEnum(colorType.Name, colorType, fontColor);
        }
        
        public static Dictionary<string, CalendarScheduleColorEnum> GetValues()
        {
            return dic;
        }

        public static CalendarScheduleColorEnum ValueOf(string key)
        {
            CalendarScheduleColorEnum value;
            if (dic.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        #endregion

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="fillColor">塗りつぶしカラー</param>
        /// <param name="fillReadOnlyColor">塗りつぶし（読み取り専用）カラー</param>
        /// <param name="fontColor">フォントカラー</param>
        /// <param name="fontReadOnlyColor">読み取り専用フォントカラー</param>
        public CalendarScheduleColorEnum(string key, Color fillColor, Color fontColor, Color fillReadOnlyColor, Color fontReadOnlyColor)
        {
            this.Key = key;
            this.MainColor = fillColor;
            this.FontColor = fontColor;
            this.MainReadOnlyColor = fillReadOnlyColor;
            this.FontReadOnlyColor = fontReadOnlyColor;

            dic.Add(Key, this);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="fillColor">塗りつぶしカラー</param>
        /// <param name="fontColor">フォントカラー</param>
        public CalendarScheduleColorEnum(string key, Color fillColor, Color fontColor)
        {
            this.Key = key;
            this.MainColor = fillColor;
            this.FontColor = fontColor;
            this.MainReadOnlyColor = fillColor;
            this.FontReadOnlyColor = fontColor;

            dic.Add(Key, this);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fillColor"></param>
        /// <param name="fontColor"></param>
        /// <param name="thick"></param>
        public CalendarScheduleColorEnum(string key, Color fillColor, Color fontColor, BorderLineStyle border = BorderLineStyle.None) : this(key, fillColor, fontColor)
        {
            this.BorderStyle = border;
        }
    }
}
