using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// コンボボックス設定情報クラス
    /// </summary>
    public class ComboBoxSetting
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">コンボボックスキー値</param>
        /// <param name="value">コンボボックス表示値</param>
        public ComboBoxSetting(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        private const string KeyString = "Key";
        private const string ValueString = "Value";

        /// <summary>
        /// コンボボックスキー値
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// コンボボックス表示値
        /// </summary>
        public string Value { get; set; }

        internal static void SetComboBox(ComboBox combobox, string key, string value)
        {
            List<ComboBoxSetting> src = new List<ComboBoxSetting>();
            src.Add(new ComboBoxSetting(key, value));

            combobox.DataSource = src;
            combobox.DisplayMember = ValueString;
            combobox.ValueMember = KeyString;
            combobox.SelectedIndex = 0;
        }

        internal static void SetComboBox(ComboBox combobox, List<ComboBoxSetting> srcStart)
        {
            combobox.DataSource = srcStart;
            combobox.DisplayMember = ValueString;
            combobox.ValueMember = KeyString;
            combobox.SelectedIndex = 0;
        }

        internal static void SetComboBox(CheckedListBox listbox, List<ComboBoxSetting> srcStart)
        {
            listbox.DataSource = srcStart;
            listbox.DisplayMember = ValueString;
            listbox.ValueMember = KeyString;
            listbox.SelectedIndex = 0;
        }
        
        internal static string GetValue(ComboBox combobox)
        {
            if (combobox.SelectedItem != null)
            {
                return ((ComboBoxSetting)combobox.SelectedItem).Value;
            }
            else
            {
                return combobox.Text;
            }
        }
    }
}
