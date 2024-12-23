using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UC.MultiRow
{
    public class DateTimeHourFilterItem : AutoFilterToolStripItem
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
            Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>>> dates =
                new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>>>();

            foreach (FilterTreeNodeEntry item in filterEntries)
            {
                if (item.FormattedValue is DateTime)
                {
                    DateTime date = (DateTime)item.FormattedValue;

                    //年
                    Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>> year = null;
                    if (!dates.ContainsKey(date.Year))
                    {
                        dates[date.Year] = new Dictionary<int, Dictionary<int, Dictionary<int, DateTime>>>();

                    }
                    year = dates[date.Year];

                    //月
                    Dictionary<int, Dictionary<int, DateTime>> month = null;
                    if (!year.ContainsKey(date.Month))
                    {
                        year[date.Month] = new Dictionary<int, Dictionary<int, DateTime>>();
                    }
                    month = year[date.Month];

                    //日
                    Dictionary<int, DateTime> day = null;
                    if (!month.ContainsKey(date.Day))
                    {
                        month[date.Day] = new Dictionary<int, DateTime>();
                    }
                    day = month[date.Day];

                    //時
                    if (!day.ContainsKey(date.Hour))
                    {
                        day[date.Hour] = date;
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
                        FilterTreeNode dayNode = new FilterTreeNode(item3.Key + "日", (value) =>
                        {
                            if (value is DateTime)
                            {
                                return object.Equals(((DateTime)value).Day, item3.Key) && object.Equals(((DateTime)value).Month, item2.Key) && object.Equals(((DateTime)value).Year, item.Key);
                            }
                            return false;
                        });
                        monthNode.Nodes.Add(dayNode);

                        foreach (var item4 in item3.Value)
                        {
                            FilterTreeNode node = new FilterTreeNode(item4.Key.ToString(), new object[] { item4.Value });
                            dayNode.Nodes.Add(node);
                        }
                    }
                }
            }
        }
    }
}