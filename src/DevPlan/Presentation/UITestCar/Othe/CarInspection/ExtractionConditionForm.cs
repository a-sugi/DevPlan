using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UITestCar.Othe.CarInspection
{
    /// <summary>
    /// 表示設定
    /// </summary>
    public partial class ExtractionConditionForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "表示設定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>検索条件</summary>
        public CarInspectionSearchModel SearchCondition { get; set; }

        /// <summary>検索可否</summary>
        public bool IsSearch
        {
            get { return this.SearchCheckBox.Checked; }
            set { this.SearchCheckBox.Checked = value; }

        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExtractionConditionForm()
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
        private void ExtractionConditionForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //車系
            this.SetCheckBoxValue(this.CarGroupGroupBox, this.SearchCondition.車検区分1);

            //点検区分
            this.SetCheckBoxValue(this.InspectionGroupBox, this.SearchCondition.点検区分);

        }

        /// <summary>
        /// チェックボックスに値をセット
        /// </summary>
        /// <typeparam name="T">値の型</typeparam>
        /// <param name="parent">親コントロール</param>
        /// <param name="list">値</param>
        public void SetCheckBoxValue<T>(Control parent, IEnumerable<T> list)
        {
            var isAll = list == null;
            var target = list == null ? Enumerable.Empty<string>() : list.Select(x => x == null ? "" : x.ToString());

            foreach (Control c in parent.Controls)
            {
                //チェックボックスかどうか
                if (c is CheckBox)
                {
                    var chk = c as CheckBox;

                    //全選択かどうか
                    if (isAll == true)
                    {
                        chk.Checked = true;

                    }
                    else
                    {
                        var value = chk.Tag == null ? "" : chk.Tag.ToString();
                        chk.Checked = target.Contains(value);

                    }

                }

            }

        }
        #endregion

        #region 設定ボタンクリック
        /// <summary>
        /// 設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetButton_Click(object sender, EventArgs e)
        {
            //検索条件がOKかどうか
            if (this.IsSearchCondition() == true)
            {
                //車検区分1
                this.SearchCondition.車検区分1 = this.GetCheckBoxValue<string>(this.CarGroupGroupBox);

                //点検区分
                this.SearchCondition.点検区分 = this.GetCheckBoxValue<short?>(this.InspectionGroupBox);

                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 検索条件チェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchCondition()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            Func<Control, string, string> isChk = (parent, name) =>
            {
                var errMsg = "";

                var flg = false;

                //全て未選択はNG
                foreach (Control c in parent.Controls)
                {
                    if (c is CheckBox)
                    {
                        var chk = c as CheckBox;
                        if (chk.Checked == true)
                        {
                            flg = true;
                            break;

                        }

                    }

                }

                //全て未選択かどうか
                if (flg == false)
                {
                    errMsg = string.Format(Resources.KKM00001, name);

                }

                return errMsg;

            };

            //チェック
            map[this.CarGroupGroupBox] = isChk;
            map[this.InspectionGroupBox] = isChk;

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }

        /// <summary>
        /// チェックボックスの値を取得
        /// </summary>
        /// <typeparam name="T">値の型</typeparam>
        /// <param name="parent">親コントロール</param>
        private T[] GetCheckBoxValue<T>(Control parent)
        {
            var list = new List<T>();
            var type = typeof(T);

            //NULL許容型かどうか
            if (type.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                //元の型を取得
                type = Nullable.GetUnderlyingType(type);

            }

            foreach (Control c in parent.Controls)
            {
                //チェックボックスかどうか
                if (c is CheckBox)
                {
                    //選択しているかどうか
                    var chk = c as CheckBox;
                    if (chk.Checked == true)
                    {
                        var value = chk.Tag == null ? "" : chk.Tag.ToString();
                        list.Add((T)Convert.ChangeType(value, type));

                    }

                }

            }

            return list.ToArray();

        }
        #endregion
    }
}
