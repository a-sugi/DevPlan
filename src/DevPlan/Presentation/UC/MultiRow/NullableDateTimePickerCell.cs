using System;
using System.ComponentModel;
using System.Windows.Forms;

using DevPlan.UICommon;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Attributes;
using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC;
using System.Drawing;
using System.Globalization;

namespace DevPlan.Presentation.UC.MultiRow
{

    /// <summary>
    /// Null許容日付型セル
    /// </summary>
    /// <remarks>Multirow拡張コントロール</remarks>
    public class NullableDateTimePickerCell : DateTimePickerCell
    {
        /// <summary>
        /// EditType
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(NullableDateTimePickerEditingControl);
            }
        }

        /// <summary>
        /// 編集セルの初期処理
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="cellStyle"></param>
        protected override void InitializeEditingControl(int rowIndex, object initialFormattedValue, CellStyle cellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, cellStyle);

            var ctl = GcMultiRow.EditingControl as NullableDateTimePickerEditingControl;

            ctl.Value = this.Value == null || this.Value == DBNull.Value ? null : (DateTime?)this.Value;

            ctl.SetCustomFormat();
        }

        /// <summary>
        /// 編集セルの終了処理
        /// </summary>
        /// <param name="rowIndex"></param>
        protected override void TerminateEditingControl(int rowIndex)
        {
            var ctl = GcMultiRow.EditingControl as NullableDateTimePickerEditingControl;

            this.Value = !ctl.Checked ? null : (DateTime?)ctl.Value;

            ctl.Clear();

            base.TerminateEditingControl(rowIndex);
        }

        /// <summary>
        /// Clone
        /// </summary>
        public override object Clone()
        {
            return base.Clone() as NullableDateTimePickerCell;
        }
    }

    /// <summary>
    /// Null許可日付型編集セル
    /// </summary>
    /// <remarks>Multirow拡張コントロール</remarks>
    public class NullableDateTimePickerEditingControl : DateTimePickerEditingControl
    {
        string _nullFormat = " ";
        string _customFormat = string.Empty;

        /// <summary>
        /// Value
        /// </summary>
        public new object Value
        {
            get
            {
                try
                {
                    if (base.Checked)
                    {
                        var str = base.Value.ToString(_customFormat);
                        var datetime = new DateTime();

                        if (DateTime.TryParse(str, out datetime))
                        {
                            return datetime;
                        }

                        return base.Value;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    base.Checked = false;
                    return null;
                }
            }
            set
            {
                try
                {
                    if (value == null || value == DBNull.Value)
                    {
                        base.Checked = false;
                    }
                    else
                    {
                        base.Value = Convert.ToDateTime(value);
                        base.Checked = true;
                    }
                }
                catch
                {
                    base.Checked = false;
                }
            }
        }

        /// <summary>
        /// FormattedValue
        /// </summary>
        public override object FormattedValue
        {
            get
            {
                if (base.Checked)
                    return base.Value.ToString(_customFormat);
                else
                    return null;
            }
            set
            {
                base.FormattedValue = value;
            }
        }

        /// <summary>
        /// OnKeyDownイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Delete ||
                e.KeyCode == Keys.Back)
            {
                this.Value = null;
                this.SetCustomFormat();
            }
        }

        /// <summary>
        /// OnKeyPressイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (this.Value == null && char.IsDigit(e.KeyChar))
            {
                this.Value = base.Value;
                this.SetCustomFormat();

                e.Handled = true;

                SendKeys.Send("{RIGHT}");
                SendKeys.Send(e.KeyChar.ToString());
            }
        }

        /// <summary>
        /// OnValueChangedイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);

            this.SetCustomFormat();
        }

        /// <summary>
        /// カスタムフォーマットの設定
        /// </summary>
        public void SetCustomFormat()
        {
            if (_customFormat == string.Empty) _customFormat = base.CustomFormat;

            base.CustomFormat = this.Value == null ? _nullFormat : _customFormat;
        }

        /// <summary>
        /// カスタムフォーマットのクリア
        /// </summary>
        public void Clear()
        {
            _customFormat = string.Empty;
        }
    }
}
