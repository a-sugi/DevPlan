using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 受領票選択
    /// </summary>
    public partial class ReceiptForm : BaseSubForm
    {
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return this.printType == "administration"　? "管理票選択" :"受領票選択"; } }
        public override bool IsFormSizeFixed { get { return true; } }

        public enum CarKind
        {
            TestCarG,
            FixedAssetG,
            TestCarT,
            OtherT
        }

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// 対象データ
        /// </summary>
        public TestCarCommonModel data { get; set; }

        /// <summary>
        /// 受領票種別(戻り値)
        /// </summary>
        public CarKind ReceiptKind { get; set; }

        /// <summary>登録タイプ</summary>
        public string printType { get; set; }
        #endregion

        public ReceiptForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCopyMoveForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            if (this.printType == "administration")
            {
                this.RadioButton1.Text = "試作車";
                this.RadioButton1.Tag = CarKind.TestCarT;
                this.RadioButton2.Text = "試作車以外";
                this.RadioButton2.Tag = CarKind.OtherT;

                if (string.IsNullOrWhiteSpace(this.data.号車) == false)
                {
                    //号車があれば試作車
                    this.RadioButton1.Select();
                }
                else
                {
                    //その他は試作車以外
                    this.RadioButton2.Select();
                }
                this.label1.Text = "管理表";
                this.RegistButton.Text = "登録";
            }
            else
            {
                if (this.data.ESTABLISHMENT == "g")
                {
                    //群馬
                    this.RadioButton1.Text = "試作車";
                    this.RadioButton1.Tag = CarKind.TestCarG;
                    this.RadioButton2.Text = "生産車";
                    this.RadioButton2.Tag = CarKind.FixedAssetG;

                    if (string.IsNullOrWhiteSpace(this.data.号車) == false)
                    {
                        //号車があれば試作
                        this.RadioButton1.Select();
                    }
                    else
                    {
                        //その他は生産車
                        this.RadioButton2.Select();
                    }
                }
                else if (this.data.ESTABLISHMENT == "t")
                {
                    //三鷹
                    this.RadioButton1.Text = "試作車";
                    this.RadioButton1.Tag = CarKind.TestCarT;
                    this.RadioButton2.Text = "試作車以外";
                    this.RadioButton2.Tag = CarKind.OtherT;

                    if (string.IsNullOrWhiteSpace(this.data.号車) == false)
                    {
                        //号車があれば試作車
                        this.RadioButton1.Select();
                    }
                    else
                    {
                        //その他は試作車以外
                        this.RadioButton2.Select();
                    }
                }
                else
                {
                    this.RadioButton1.Visible = false;
                    this.RadioButton2.Visible = false;
                    this.RegistButton.Visible = false;
                }
                this.label1.Text = "受領票";
                this.RegistButton.Text = "印刷";
            }
        }

        /// <summary>
        /// 印刷ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            //選択項目設定
            this.SetPrint();

            //閉じる
            base.FormOkClose();
        }

        /// <summary>
        /// 選択項目設定
        /// </summary>
        private void SetPrint()
        {
            var status = FormControlUtil.GetRadioButtonValue(this.StatusPanel);
            if (status != null)
            {
                this.ReceiptKind = (CarKind)Enum.Parse(typeof(CarKind), status.ToString());
            }
        }
    }
}
