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
    public partial class ItemCopyMoveForm : BaseSubForm
    {
        // TODO : 当フォームは過去に実装された未使用フォーム（項目移動は2020年要望対応にて対応）

        #region <<< 画面プロパティの設定 >>>
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "項目コピー・移動"; } }
        #endregion
        public override int FormWidth { get { return 514; } }
        public override int FormHeight { get { return 260; } }
        public override bool IsFormSizeFixed { get { return true; } }
        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GeneralCode { get; set; }

        /// <summary>
        /// 担当
        /// </summary>
        public string SectionGroupID { get; set; }

        /// <summary>
        /// スケジュールID
        /// </summary>
        public long ScheduleID { get; set; }

        /// <summary>
        /// 対象テーブル
        /// </summary>
        public int TableNumber { get; set; }
        #endregion

        public ItemCopyMoveForm()
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

            //担当のパラメータ
            var sectionGroupCond = new SectionGroupSearchModel
            {
                //部ID
                DEPARTMENT_ID = SessionDto.DepartmentID,

                //課ID
                SECTION_ID = SessionDto.SectionID,

                //担当ID
                SECTION_GROUP_ID = SectionGroupID

            };

            //担当
            this.SetSectionGroup(HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, sectionGroupCond)?.Results?.FirstOrDefault());

        }

        /// <summary>
        /// 開発符号マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            using (var form = new GeneralCodeListForm { UNDER_DEVELOPMENT = "1" })
            {
                //開発符号検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //開発符号セット
                    this.SetGeneralCode(new GeneralCodeSearchOutModel { CAR_GROUP = form.CAR_GROUP, GENERAL_CODE = form.GENERAL_CODE });

                }
            }
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
        /// 担当マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupIDComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                //担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //担当をセット
                    this.SetSectionGroup(form.SectionGroup);
                }
            }
        }

        /// <summary>
        /// 担当を設定
        /// </summary>
        /// <param name="sectionGroup">担当</param>
        private void SetSectionGroup(SectionGroupModel sectionGroup)
        {
            var value = new ComboBoxDto
            {
                CODE = sectionGroup.SECTION_GROUP_ID,
                NAME = string.Format("{0} {1} {2}", sectionGroup.DEPARTMENT_CODE, sectionGroup.SECTION_CODE, sectionGroup.SECTION_GROUP_CODE)

            };

            //担当をセット
            FormControlUtil.SetComboBoxItem(this.SectionGroupIDComboBox, new[] { value }, false);
            this.SectionGroupIDComboBox.SelectedIndex = 0;

        }

        /// <summary>
        /// 登録ボタンが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            if (CopyRadioButton.Checked)
            {
                ItemCopy();
            }

            if (MoveRadioButton.Checked)
            {
                ItemMove();
            }
        }

        /// <summary>
        /// 項目コピー
        /// </summary>
        private void ItemCopy()
        {
            #region <<< APIサーバでコピー処理 >>>
            // Postメソッド発行用パラメータ
            var cond = new ScheduleItemCopyInModel
            {
                ID = ScheduleID,
                PERSONEL_ID = SessionDto.UserId,
                GENERAL_CODE = this.GeneralCodeComboBox.SelectedValue.ToString(),
                SECTION_GROUP_ID = SectionGroupID,
                TABLE_NUMBER = TableNumber
            };
            #endregion

            var result = HttpUtil.PostResponse<LoginOutModel>(ControllerType.ScheduleItemCopy, cond, false);

            // 正常終了
            if (result.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
                base.FormOkClose();
            }
            // コピー先に指定された開発符号が既に存在している
            else if (result.ErrorCode == ApiMessageType.KKE03019.ToString())
            {
                Messenger.Warn(Resources.KKM03003);
            }
        }

        /// <summary>
        /// 項目移動
        /// </summary>
        private void ItemMove()
        {
            var cond = new ScheduleItemMoveInModel
            {
                ID = ScheduleID,
                GENERAL_CODE = this.GeneralCodeComboBox.SelectedValue.ToString(),
                //SECTION_GROUP_ID = SectionGroupID,
                //TABLE_NUMBER = TableNumber
            };

            var result = HttpUtil.PostResponse<LoginOutModel>(ControllerType.ScheduleItemMove, cond, false);

            // 正常終了
            if (result.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
                base.FormOkClose();
            }
            // 移動先に指定された開発符号が既に存在している
            else if (result.ErrorCode == ApiMessageType.KKE03019.ToString())
            {
                Messenger.Warn(Resources.KKM03003);
            }
        }
    }
}

