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
    public partial class WayConfirmDateForm : BaseSubForm
    {

        #region <<< 画面プロパティの設定 >>>
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "方向付け確定期限登録"; } }
        public override int FormWidth { get { return 514; } }
        public override int FormHeight { get { return 260; } }
        public override bool IsFormSizeFixed { get { return true; } }
        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }


        /// <summary>データソース</summary>
        private string[] DataSource1;
        #endregion

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GeneralCode { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        public string SelectKind { get; set; }

        /// <summary>
        /// 設定日時
        /// </summary>
        public DateTime SelectDate { get; set; }

        /// <summary>
        /// 対象テーブル
        /// </summary>
        public int TableNumber { get; set; }

        /// <summary>データソース1</summary>
        public string[] DATASOURCE1 { get { return DataSource1; } set { DataSource1 = value; } }

        #endregion

        public WayConfirmDateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WayConfirmDateForm_Load(object sender, EventArgs e)
        {
            // 権限の取得
            this.UserAuthority = this.GetFunction(FunctionID.TestCar);

            //画面初期化
            InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //パラメータ設定
            var generalCodeCond = new GeneralCodeSearchInModel
            {
                //ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //開発符号
                GENERAL_CODE = this.GeneralCode
            };

            //開発符号
            this.SetGeneralCode(HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, generalCodeCond)?.Results?.FirstOrDefault());

            //車種プルダウン項目
            GeneralCodeComboBox.Items.AddRange(DataSource1);

            //種別プルダウン項目
            string[] kindList = (HttpUtil.GetResponse<CapKindModel>(ControllerType.CapKind)?.Results).Select(x => x.種別).ToArray();
            KindComboBox.Items.AddRange(kindList);

            //Append Start 2021/07/27 矢作

            //開発符号が一つのみの場合、それを選択済みにする
            if (DataSource1.Length == 1)
            {
                GeneralCodeComboBox.SelectedIndex = 0;
            }

            //Append End 2021/07/27 矢作
        }

        /// <summary>
        /// 開発符号をセット
        /// </summary>
        /// <param name="generalCode">開発符号</param>
        private void SetGeneralCode(GeneralCodeSearchOutModel generalCode)
        {
            //開発符号があるかどうか
            if (string.IsNullOrWhiteSpace(this.GeneralCode) == false)
            {
                var value = new ComboBoxDto
                {
                    CODE = generalCode.GENERAL_CODE,
                    NAME = string.Format("{0} {1}", generalCode.CAR_GROUP, generalCode.GENERAL_CODE)
                };

                //開発符号をセット
                FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, new[] { value }, false);
                this.GeneralCodeComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 反映ボタンが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            //Append Start 2021/08/30 杉浦 CAP要望対応
            if (InputItemCheck())
            {
                //Append End 2021/08/30 杉浦 CAP要望対応
                GeneralCode = GeneralCodeComboBox.Text;
                SelectKind = KindComboBox.Text;
                SelectDate = dateTimePicker1.Value;

                //画面を閉じる
                this.FormOkClose();
                //Append Start 2021/08/30 杉浦 CAP要望対応
            }
            //Append End 2021/08/30 杉浦 CAP要望対応
        }

        //Append Start 2021/08/30 杉浦 CAP要望対応
        /// <summary>
        /// 入力項目の内容チェック
        /// </summary>
        /// <returns>true/false</returns>
        private bool InputItemCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();
            
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }
            return true;
        }

        private void GeneralCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Append End 2021/08/30 杉浦 CAP要望対応
    }
}

