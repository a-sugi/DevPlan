using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Common;

namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    /// <summary>
    /// 駐車場番号指定
    /// </summary>
    public partial class ParkingNumberDesignationForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "駐車場番号指定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        public ApplicationApprovalCarModel TestCar { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
        public bool ActiveCheck { get; set; }
        //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

        //Append Start 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
        //駐車場番号検索結果
        private ParkingModel ParkingInfo = new ParkingModel();
        //Append End 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParkingNumberDesignationForm()
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
        private void DisposalApplicationForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //管理票NO
            this.ManagementNoLabel.Text = this.TestCar.管理票NO;

            //駐車場番号
            this.ActiveControl = this.ParkingNumberComboBox;

            //駐車場番号初期値選択
            this.ParkingNumberComboBox.Items.Clear();
            this.ParkingNumberComboBox.Items.Add(this.TestCar?.駐車場番号 ?? "");
            this.ParkingNumberComboBox.SelectedIndex = 0;

            //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
            if (ActiveCheck)
            {
                this.SelectActiveCheckbox.Enabled = true;
                this.ParkingNumberComboBox.Enabled = false;
            }
            else
            {
                this.SelectActiveCheckbox.Enabled = false;
            }
            //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

        }
        #endregion

        #region 駐車場コンボボックスクリック
        /// <summary>
        /// 駐車場コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParkingNumberComboBox_Click(object sender, EventArgs e)
        {
            using (var form = new ParkingSelectForm { UserAuthority = this.UserAuthority })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //エリアまたは区画が選択された
                    this.ParkingNumberComboBox.Items.Clear();
                    this.ParkingNumberComboBox.Items.Add(form.Data.NAME);
                    this.ParkingNumberComboBox.SelectedIndex = 0;
                    //Append Start 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
                    this.ParkingInfo = form.Data;
                    //Append End 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
                }
            }
        }
        #endregion

        #region OKボタンクリック
        /// <summary>
        /// OKボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            //Append Start 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
            if (this.TestCar.駐車場番号 != this.ParkingNumberComboBox.Text)
            {
                //駐車場選択情報を削除
                this.DeleteParkingInfo();

                if (this.ParkingInfo.SECTION_NO != null)
                {
                    //区画を選択（エリアを選択した場合は駐車場選択情報の更新はなし）

                    //駐車場選択情報を登録
                    this.AddParkingInfo();

                }
            }
            //Append End 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する

            //入力駐車場番号
            this.TestCar.入力駐車場番号 = this.ParkingNumberComboBox.Text;

            //フォームクローズ
            this.FormOkClose();

        }
        #endregion

        //Append Start 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて
        private void SelectActiveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectActiveCheckbox.Checked)
            {
                this.ParkingNumberComboBox.Enabled = true;
            }
            else
            {
                this.ParkingNumberComboBox.Enabled = false;
            }
        }
        //Append End 2022/02/17 杉浦 試験車管理機能の移管承認ステップについて

        //Append Start 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
        #region 駐車場選択情報削除
        /// <summary>
        /// 駐車場選択情報削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteParkingInfo()
        {
            //駐車場選択情報を削除（PARKING_SECTIONのSTATUSを0,PARKING_USEから情報を削除）
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            var cond = new ParkingUseModel
            {
                データID = this.TestCar.データID,
                INPUT_PERSONEL_ID = SessionDto.UserId,
            };
            list.Add(cond);

            var res = HttpUtil.DeleteResponse<ParkingUseModel>(ControllerType.ParkingUse, list);

            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion

        #region 駐車場選択情報登録
        /// <summary>
        /// 駐車場選択情報登録
        /// </summary>
        /// <returns></returns>
        private bool AddParkingInfo()
        {
            //駐車場選択情報を登録
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            var cond = new ParkingUseModel
            {
                データID = this.TestCar.データID,
                LOCATION_NO = this.ParkingInfo.LOCATION_NO,
                AREA_NO = this.ParkingInfo.AREA_NO,
                SECTION_NO = this.ParkingInfo.SECTION_NO,
                INPUT_PERSONEL_ID = SessionDto.UserId,
            };
            list.Add(cond);

            var res = HttpUtil.PostResponse<ParkingUseModel>(ControllerType.ParkingUse, list);
            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion
        //Append End 2023/01/25 杉浦 紐づけが宙に浮いている駐車場が存在する
    }
}
