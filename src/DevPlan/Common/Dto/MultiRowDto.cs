using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region MultiRowソートクラス
    /// <summary>
    /// MultiRowソートクラス
    /// </summary>
    public class MultiRowSortModel
    {
        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>データプロパティ名</summary>
        public string DataPropertyName { get; set; }

        /// <summary>降順可否</summary>
        public bool IsDesc { get; set; }
    }
    #endregion

    #region MultiRow表示クラス
    /// <summary>
    /// MultiRow表示クラス
    /// </summary>
    public class MultiRowDisplayModel
    {
        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>ヘッダー名</summary>
        public string HeaderText { get; set; }

        /// <summary>データプロパティ名</summary>
        public string DataPropertyName { get; set; }

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

    #region MultiRow表示クラス
    /// <summary>
    /// MultiRow表示クラス
    /// </summary>
    public class GridViewDisplayModel
    {
        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>ヘッダー名</summary>
        public string HeaderText { get; set; }

        /// <summary>データプロパティ名</summary>
        public string DataPropertyName { get; set; }

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

    public class GridViewDisplayItemModel
    {
        public string category { get; set; }

        public string item { get; set; }

        public int? checkByte { get; set; }

        public bool modified { get; set; }
    }
}
