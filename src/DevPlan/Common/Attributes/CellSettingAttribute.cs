using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Attributes
{
    /// <summary>
    /// MultiRowセル設定属性。
    /// </summary>
    /// <remarks>
    /// とりあえずのところ、～ModelをグリッドへBindしている場合に利用。
    /// </remarks>
    public class CellSettingAttribute : Attribute
    {
        /// <summary>
        /// 列の表示順（タグインデックスもこの通りになります）
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// ヘッダーへ表示する文言。
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// 改行させる場合はTrue
        /// </summary>
        public bool WordWrap { get; set; }

        /// <summary>
        /// 読み取り専用を解除する場合はfalse。
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 列を非表示にする場合はFalse。
        /// </summary>
        public bool Visible { get; set; }

        public object Tag { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public CellSettingAttribute()
        {
            ReadOnly = true;
            Visible = true;
        }


    }
}
