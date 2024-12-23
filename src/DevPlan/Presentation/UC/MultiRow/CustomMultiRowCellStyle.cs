using DevPlan.UICommon.Attributes;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UC.MultiRow
{
    /// <summary>
    /// MultiRowのヘッダ＆値セルスタイル設定クラス。
    /// </summary>
    /// <remarks>
    /// MultiRow特有のヘッダ＆値セルのスタイル設定クラスです。
    /// </remarks>
    public class CustomMultiRowCellStyle
    {
        private Attribute attribute;

        /// <summary>
        /// ヘッダーへ表示する文言。
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// 列の幅。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// フィルタアイテム（デフォルトはAutoFilterです）
        /// </summary>
        public AutoFilterToolStripItem FilterItem { get; set; }

        /// <summary>
        /// セルのタイプ（デフォルトはテキストボックスです）
        /// </summary>
        public MultiRowCellType Type { get; set; }

        /// <summary>
        /// 日付形式（デフォルトはyyyy/MM/ddです）
        /// </summary>
        public string CustomFormat { get; internal set; }

        /// <summary>
        /// 列の表示（表示させない場合はFalseを設定）
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 列の表示順（タグインデックスもこの通りになります）
        /// </summary>
        public int DisplayIndex { get; internal set; }

        /// <summary>
        /// ヘッダーのスタイル。
        /// </summary>
        public CellStyle HeaderCellStyle { get; internal set; }

        /// <summary>
        /// 値セルのスタイル。
        /// </summary>
        public CellStyle DataCellStyle { get; internal set; }

        /// <summary>
        /// 読み書き有無。デフォルトはFalse（読み取り専用）です。
        /// </summary>
        public bool ReadOnly { get; internal set; }
        
        /// <summary>
        /// コンボボックスを表示する場合のValue値
        /// </summary>
        public string ValueMember { get; internal set; }

        /// <summary>
        /// コンボボックスを表示する場合のDisplay値
        /// </summary>
        public string DisplayMember { get; internal set; }

        /// <summary>
        /// コンボボックスへ設定するデータ
        /// </summary>
        public object ComboBoxDataSource { get; internal set; }

        /// <summary>
        /// Tag（主には当システムで利用している入力チェック文字列を設定する）
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// デフォルト設定を行います。
        /// </remarks>
        public CustomMultiRowCellStyle()
        {
            DefaultSetting();
        }

        private void DefaultSetting()
        {
            HeaderCellStyle = new CellStyle()
            {
                TextAlign = MultiRowContentAlignment.MiddleCenter,
                Font = new Font("MS UI Gothic", 9, FontStyle.Regular),
                WordWrap = MultiRowTriState.True,
                Border = new Border() { Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray) },
                BackColor = GetMultiRowColoer("HADER_ROW_BACKCOLOR"),
                ForeColor = GetMultiRowColoer("HADER_ROW_FORECOLOR")
            };

            DataCellStyle = new CellStyle()
            {
                TextAlign = MultiRowContentAlignment.MiddleLeft,
                Font = new Font("MS UI Gothic", 9, FontStyle.Regular),
                WordWrap = MultiRowTriState.True,
                BackColor = GetMultiRowColoer("DATA_ROW_BACKCOLOR"),
                ForeColor = GetMultiRowColoer("DATA_ROW_FORECOLOR")
            };
            CustomFormat = "yyyy/MM/dd";
            ReadOnly = true;
        }

        /// <summary>
        /// 属性値を解析し、初期値を設定します。
        /// </summary>
        /// <param name="attribute">属性</param>
        /// <param name="defaultHeaderText">ヘッダテキストが属性値に指定されていない場合に表示するヘッダテキスト</param>
        public CustomMultiRowCellStyle(CellSettingAttribute attribute, string defaultHeaderText)
        {
            DefaultSetting();

            HeaderText = string.IsNullOrEmpty(attribute.HeaderText) ? defaultHeaderText : attribute.HeaderText;
            DisplayIndex = attribute.DisplayIndex;
            DataCellStyle.WordWrap = attribute.WordWrap ? MultiRowTriState.True : MultiRowTriState.Inherit;
            DataCellStyle.Multiline = attribute.WordWrap ? MultiRowTriState.True : MultiRowTriState.Inherit;
            ReadOnly = attribute.ReadOnly;
            Visible = attribute.Visible;
            Tag = attribute.Tag;
        }

        /// <summary>
        /// Configより各Coloer名を取得
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        private static Color GetMultiRowColoer(string colorName)
        {
            ColorConverter cc = new ColorConverter();
            Color selectColor = (Color)cc.ConvertFromString(ConfigurationManager.AppSettings[colorName]);
            return selectColor;
        }
    }

    /// <summary>
    /// MultiRowのセルのタイプ。
    /// </summary>
    public enum MultiRowCellType
    {
        /// <summary>
        /// テキストボックス
        /// </summary>
        TEXT,

        /// <summary>
        /// 日付（時間概念なし）
        /// </summary>
        DATETIME,

        /// <summary>
        /// 日付（時間（時））
        /// </summary>
        DATETIME_HOUR,

        /// <summary>
        /// 日付（時間（ロング））
        /// </summary>
        DATETIME_LONG,

        /// <summary>
        /// チェックボックス
        /// </summary>
        CHECKBOX,

        /// <summary>
        /// コンボボックス
        /// </summary>
        COMBOBOX,

        /// <summary>
        /// リンクラベル
        /// </summary>
        LINKLABEL,

        /// <summary>
        /// ボタン
        /// </summary>
        BUTTON,
    }
}
