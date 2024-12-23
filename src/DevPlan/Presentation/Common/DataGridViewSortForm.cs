using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// ソート指定
    /// </summary>
    public partial class DataGridViewSortForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "ソート指定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>ソート対象</summary>
        public IEnumerable<DataGridViewSortModel> SortTarget { get; set; }

        /// <summary>ソート順</summary>
        public IEnumerable<DataGridViewSortModel> Sort { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataGridViewSortForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewSortForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //ソート対象
            var list = (new[] { new DataGridViewSortModel() }).Concat(this.SortTarget).ToArray();
            this.Sort1ComboBox.DataSource = list.ToArray();
            this.Sort2ComboBox.DataSource = list.ToArray();
            this.Sort3ComboBox.DataSource = list.ToArray();

            //ソート順があるかどうか
            if (this.Sort != null && this.Sort.Any() == true)
            {
                Action<ComboBox, RadioButton, int> setSort = (cmb, radio, index) =>
                {
                    if (this.Sort.Count() >= index)
                    {
                        var sort = this.Sort.ElementAt(index - 1);

                        //ソート対象
                        cmb.SelectedValue = sort.DataPropertyName;

                        //順序
                        radio.Checked = sort.IsDesc;
                    }

                };

                //ソート順
                setSort(this.Sort1ComboBox, this.Sort1DescRadioButton, 1);
                setSort(this.Sort2ComboBox, this.Sort2DescRadioButton, 2);
                setSort(this.Sort3ComboBox, this.Sort3DescRadioButton, 3);

            }

            //第1ソートキー
            this.ActiveControl = this.Sort1ComboBox;

        }
        #endregion

        #region 並び替えボタンクリック
        /// <summary>
        /// 並び替えボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            Func<ComboBox, string, string> isRequired = (cmb, name) => cmb.SelectedIndex > 0 ? "" : string.Format(Resources.KKM00001, name);

            //第3ソートキーを選択しているかどうか
            if (this.Sort3ComboBox.SelectedIndex > 0)
            {
                //第2ソートキー
                map[this.Sort2ComboBox] = (c, name) => isRequired((c as ComboBox), name);

            }

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            var list = new List<DataGridViewSortModel>();

            Action<ComboBox, RadioButton> setSort = (cmb, radio) =>
            {
                if (cmb.SelectedIndex > 0)
                {
                    var item = cmb.SelectedItem as DataGridViewSortModel;
                    item.IsDesc = radio.Checked;

                    list.Add(item);

                }

            };

            //ソート順
            setSort(this.Sort1ComboBox, this.Sort1DescRadioButton);
            setSort(this.Sort2ComboBox, this.Sort2DescRadioButton);
            setSort(this.Sort3ComboBox, this.Sort3DescRadioButton);
            this.Sort = list;

            //フォームクローズ(OK)
            base.FormOkClose();

        }
        #endregion

    }
}
