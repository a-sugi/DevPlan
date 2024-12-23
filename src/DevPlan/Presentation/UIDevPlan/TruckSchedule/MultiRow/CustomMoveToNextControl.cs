using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.MultiRow
{
    internal class CustomMoveToNextControl : IAction
    {
        public bool CanExecute(GcMultiRow target)
        {
            return true;
        }

        public string DisplayName
        {
            get { return this.ToString(); }
        }

        public void Execute(GcMultiRow target)
        {
            Boolean isLastRow = (target.CurrentCellPosition.RowIndex == target.RowCount - 1);
            Boolean isLastCell = (target.CurrentCellPosition.CellIndex == target.Template.Row.Cells.Count - 1);

            if (!(isLastRow & isLastCell))
            {
                SelectionActions.MoveToNextCell.Execute(target);
            }
            else
            {
                ComponentActions.SelectNextControl.Execute(target);
            }
        }
    }
}
