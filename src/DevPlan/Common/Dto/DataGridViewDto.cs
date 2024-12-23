using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region データグリッドビューソートクラス
    /// <summary>
    /// データグリッドビューソートクラス
    /// </summary>
    public class DataGridViewSortModel
    {
        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>データプロパティ名</summary>
        public string DataPropertyName { get; set; }

        /// <summary>降順可否</summary>
        public bool IsDesc { get; set; }

    }
    #endregion

    #region データグリッドビュー表示クラス
    /// <summary>
    /// データグリッドビュー表示クラス
    /// </summary>
    public class DataGridViewDisplayModel
    {
        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>ヘッダー名</summary>
        public string HeaderText { get; set; }

        /// <summary>データプロパティ名</summary>
        public string DataPropertyName { get; set; }

        /// <summary>列固定可否</summary>
        public bool Frozen { get; set; }

        /// <summary>表示</summary>
        public bool Visible { get; set; }

        /// <summary>表示順</summary>
        public int DisplayIndex { get; set; }

        /// <summary>ソート順</summary>
        public int? SortNo { get; set; }

        /// <summary>昇順可否</summary>
        public bool IsAsc { get; set; }

    }
    #endregion
}
