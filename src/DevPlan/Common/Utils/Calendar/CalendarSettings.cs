using DevPlan.UICommon.Enum;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DevPlan.UICommon.Utils.Calendar
{
    /// <summary>
    /// カレンダー設定情報。
    /// </summary>
    public class CalendarSettings
    {
        /// <summary>
        /// カレンダー設定情報保持Action。
        /// </summary>
        /// <remarks>
        /// 処理内容は各画面にて設定。
        /// </remarks>
        public Action<StringCollection> SaveCalendarUserData;

        /// <summary>
        /// カレンダー設定情報保持リスト。
        /// </summary>
        private List<CalendarTemplateTypeSafeEnum> typeList = new List<CalendarTemplateTypeSafeEnum>()
        {
            CalendarTemplateTypeSafeEnum.DEFAULT,
            CalendarTemplateTypeSafeEnum.EXPANSION1,
            CalendarTemplateTypeSafeEnum.EXPANSION2
        };

        /// <summary>
        /// 現在選択されているカレンダーのスタイル。
        /// </summary>
        public CalendarStyle CurrentStyle { get { return Style[CalendarMode]; } }

        /// <summary>
        /// カレンダースタイル情報。
        /// </summary>
        /// <remarks>
        /// 各テンプレート種類ごとのカレンダースタイルを保持します。
        /// </remarks>
        public Dictionary<CalendarTemplateTypeSafeEnum, CalendarStyle> Style { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// 設定ファイル（StringCollection）から各テンプレートのスタイルオブジェクトを生成します。
        /// </remarks>
        /// <param name="styleCollection">スタイル情報（Settings.csへ保存されている情報を渡してください）</param>
        public CalendarSettings(StringCollection styleCollection)
        {
            Style = new Dictionary<CalendarTemplateTypeSafeEnum, CalendarStyle>();

            if (styleCollection == null)
            {
                CalendarMode = CalendarTemplateTypeSafeEnum.DEFAULT;

                var defaultStyle = new CalendarStyle();
                defaultStyle.StyleChanged += CalendarStyle_PropertyChanged;

                foreach (var type in typeList)
                {
                    Style.Add(type, defaultStyle);
                }
            }
            else
            {
                string[] style = styleCollection.Cast<string>().ToArray();

                CalendarMode = CalendarTemplateTypeSafeEnum.KeyOf(int.Parse(style[0]));

                foreach (var type in typeList)
                {
                    Style.Add(type, CreateCalendarStyle(style[type.Key]));
                }
            }

            if (Style.ContainsKey(CalendarTemplateTypeSafeEnum.EXPANSION2))
            {
                Style[CalendarTemplateTypeSafeEnum.EXPANSION2].Range = 1;
            }
        }

        /// <summary>
        /// カレンダースタイルオブジェクト作成。
        /// </summary>
        /// <remarks>
        /// 渡されたstringを元にカレンダースタイルオブジェクトを作成します。
        /// </remarks>
        /// <param name="style">設定値</param>
        /// <returns>CalendarStyle</returns>
        private CalendarStyle CreateCalendarStyle(string style)
        {
            var s = style.Split(',');

            var obj = new CalendarStyle()
            {
                VerticalLengthUpdate = int.Parse(s[0]),
                HorizontalLengthUpdate = int.Parse(s[1]),
                Zoom = float.Parse(s[2]),
                FontSize = s[3]
            };

            obj.StyleChanged += CalendarStyle_PropertyChanged;

            return obj;
        }

        /// <summary>
        /// カレンダースタイル変更イベント。
        /// </summary>
        /// <remarks>
        /// カレンダースタイルのプロパティが変更された場合に保存処理を実施します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarStyle_PropertyChanged(object sender, EventArgs e)
        {
            SaveCalendarStyle();
        }

        /// <summary>
        /// カレンダー選択・スタイル情報保存処理実行。
        /// </summary>
        /// <remarks>
        /// 保存する形式に整形を行い、保存処理を呼び出します。
        /// </remarks>
        private void SaveCalendarStyle()
        {
            if (this.SaveCalendarUserData == null) { return; }

            var ret = new StringCollection();

            ret.Add(CalendarMode.Key.ToString());

            foreach (var type in typeList)
            {
                var list = new List<string>();

                list.Add(Style[type].VerticalLengthUpdate.ToString());
                list.Add(Style[type].HorizontalLengthUpdate.ToString());
                list.Add(Style[type].Zoom.ToString());
                list.Add(Style[type].FontSize.ToString());

                ret.Add(string.Join(",", list));
            }

            this.SaveCalendarUserData.Invoke(ret);
        }

        private CalendarTemplateTypeSafeEnum _calendarMode;
        /// <summary>
        /// 現在選択されているカレンダーのテンプレートタイプ。
        /// </summary>
        /// <remarks>
        /// Value値が変更された場合は保存処理を呼び出します。
        /// </remarks>
        public CalendarTemplateTypeSafeEnum CalendarMode
        {
            get { return _calendarMode; }
            set
            {
                this._calendarMode = value;
                SaveCalendarStyle();
            }
        }
    }

    /// <summary>
    /// カレンダースタイル情報。
    /// </summary>
    /// <remarks>
    /// カレンダーテンプレートのスタイル情報です。
    /// </remarks>
    public class CalendarStyle
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 初期値で初期化を行います。
        /// </remarks>
        public CalendarStyle()
        {
            VerticalLengthUpdate = 0;
            HorizontalLengthUpdate = 0;
            Zoom = 1;
            FontSize = "9";
            ScheduleRowHeaderWidth = 200;
            DefaultStartTime = 8;
            DefaultEndTime = 17;
            Range = 3;
        }

        /// <summary>
        /// 開始日の時間の初期値
        /// </summary>
        public int DefaultStartTime { get; set; }

        /// <summary>
        /// 終了日の時間の初期値
        /// </summary>
        public int DefaultEndTime { get; set; }

        /// <summary>
        /// スケジュールの行ヘッダ―の幅
        /// </summary>
        public int ScheduleRowHeaderWidth { get; set; }

        private int _horizontalLengthUpdate;
        /// <summary>
        /// 列の幅変更値
        /// </summary>
        public int HorizontalLengthUpdate
        {
            get { return _horizontalLengthUpdate; }
            set
            {
                if (_horizontalLengthUpdate != value)
                {
                    _horizontalLengthUpdate = value;
                    this.StyleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int _verticalLengthUpdate;
        /// <summary>
        /// 行の高さ変更値
        /// </summary>
        public int VerticalLengthUpdate
        {
            get { return _verticalLengthUpdate; }
            set
            {
                if (_verticalLengthUpdate != value)
                {
                    _verticalLengthUpdate = value;
                    this.StyleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private float _zoom;
        /// <summary>
        /// ズーム
        /// </summary>
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    this.StyleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private string _fontSize;
        /// <summary>
        /// フォントサイズ
        /// </summary>
        public string FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    this.StyleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// カレンダーへ表示する月数
        /// </summary>
        public int Range { get; internal set; }

        /// <summary>
        /// プロパティが更新された際に発生するイベント。
        /// </summary>
        public event EventHandler StyleChanged;
    }
}
