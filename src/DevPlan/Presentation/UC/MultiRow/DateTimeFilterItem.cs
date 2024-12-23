using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UC.MultiRow
{
    public class DateTimeFilterItem : AutoFilterToolStripItem
    {
        protected override void InitializeFilterTree(FilterTreeNode rootNode, List<FilterTreeNodeEntry> filterEntries, int filteringCellIndex)
        {
            if (filterEntries != null && filterEntries.Count > 0
                && filterEntries[0].FormattedValue is DateTime)
            {
                BuildTree(rootNode, filterEntries, filteringCellIndex);
            }
            else
            {
                base.InitializeFilterTree(rootNode, filterEntries, filteringCellIndex);
            }
        }

        private void BuildTree(FilterTreeNode rootNode, List<FilterTreeNodeEntry> filterEntries, int filteringCellIndex)
        {

            Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>> dates = new Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>>();

            foreach (FilterTreeNodeEntry item in filterEntries)
            {
                if (item.FormattedValue is DateTime)
                {

                    DateTime date = (DateTime)item.FormattedValue;

                    Dictionary<int, Dictionary<int, DateTime>> year = null;

                    if (!dates.ContainsKey(date.Year))
                    {
                        dates[date.Year] = new Dictionary<int, Dictionary<int, DateTime>>();

                    }
                    year = dates[date.Year];


                    Dictionary<int, DateTime> month = null;
                    if (!year.ContainsKey(date.Month))
                    {
                        year[date.Month] = new Dictionary<int, DateTime>();
                    }
                    month = year[date.Month];

                    if (!month.ContainsKey(date.Day))
                    {
                        month[date.Day] = date;
                    }



                }

            }

            foreach (var item in dates)
            {
                FilterTreeNode yearNode = new FilterTreeNode(item.Key + "年", (value) =>
                {
                    if (value is DateTime)
                    {
                        return object.Equals(((DateTime)value).Year, item.Key);
                    }
                    return false;
                });
                yearNode.AutoExpand = true;
                rootNode.Nodes.Add(yearNode);

                foreach (var item2 in item.Value)
                {
                    FilterTreeNode monthNode = new FilterTreeNode(item2.Key + "月", (value) =>
                    {
                        if (value is DateTime)
                        {
                            return object.Equals(((DateTime)value).Month, item2.Key) && object.Equals(((DateTime)value).Year, item.Key);
                        }
                        return false;
                    });
                    yearNode.Nodes.Add(monthNode);

                    foreach (var item3 in item2.Value)
                    {
                        FilterTreeNode dayNode = new FilterTreeNode(item3.Key.ToString(), new object[] { item3.Value });
                        monthNode.Nodes.Add(dayNode);
                    }

                }

            }
        }
    }
}