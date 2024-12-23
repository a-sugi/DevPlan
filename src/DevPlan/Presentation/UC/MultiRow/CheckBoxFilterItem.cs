using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UC.MultiRow
{
    /// <summary>
    /// チェックボックスフィルタアイテム
    /// </summary>
    public class CheckBoxFilterItem : AutoFilterToolStripItem
    {
        /// <summary>
        /// チェックボックスにチェックがある場合
        /// </summary>
        private string trueText;

        /// <summary>
        /// チェックボックスにチェックがない
        /// </summary>
        private string falseText;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="trueText"></param>
        /// <param name="falseText"></param>
        public CheckBoxFilterItem(string trueText, string falseText)
        {
            this.trueText = trueText;
            this.falseText = falseText;
        }

        /// <summary>
        /// これはオーバーライド。
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="filterEntries"></param>
        /// <param name="filteringCellIndex"></param>
        protected override void InitializeFilterTree(FilterTreeNode rootNode, List<FilterTreeNodeEntry> filterEntries, int filteringCellIndex)
        {
            if (filterEntries != null && filterEntries.Count > 0)
            {
                BuildTree(rootNode, filterEntries, filteringCellIndex);
            }
            else
            {
                base.InitializeFilterTree(rootNode, filterEntries, filteringCellIndex);
            }
        }

        /// <summary>
        /// これは独自の関数。
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="filterEntries"></param>
        /// <param name="filteringCellIndex"></param>
        private void BuildTree(FilterTreeNode rootNode, List<FilterTreeNodeEntry> filterEntries, int filteringCellIndex)
        {
            Dictionary<string, Dictionary<string, string>> adds = new Dictionary<string, Dictionary<string, string>>();

            foreach (FilterTreeNodeEntry item in filterEntries)
            {
                if ((bool)item.FormattedValue == false)
                {
                    FilterTreeNode node = new FilterTreeNode(this.falseText, new object[] { item.FormattedValue });
                    rootNode.Nodes.Add(node);

                }
                else
                {
                    FilterTreeNode node = new FilterTreeNode(this.trueText, new object[] { item.FormattedValue });
                    rootNode.Nodes.Add(node);

                }
            }
        }
    }
}
