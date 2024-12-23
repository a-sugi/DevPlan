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

namespace DevPlan.Presentation.UIDevPlan.Announce
{
    public partial class AnnounceDetailForm : BaseSubForm
    {
        #region プロパティ

        public override string FormTitle { get { return "お知らせ詳細"; } }

        public override int FormHeight { get { return this.Height; } }

        public override int FormWidth { get { return this.Width; } }

        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>お知らせID</summary>
        public long? AnnounceID { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion
        public AnnounceDetailForm()
        {
            InitializeComponent();
        }

        private List<InformationOutModel> AnnounceList;

        /// <summary>
        /// お知らせAPI検索の呼び出し
        /// </summary>
        /// <returns></returns>
        private List<InformationOutModel> GetAnnounceList()
        {
            var cond = new InformationInModel
            {
                ID = AnnounceID
            };

            var res = HttpUtil.GetResponse<InformationInModel, InformationOutModel>(ControllerType.Information, cond);

            return res.Results.ToList();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            //初期表示フォーカス
            this.ActiveControl = TitleTextBox;

            if (!isUpdate)
            {
                this.RegistButton.Visible = false;
                this.DeleteButton.Visible = false;
            }

            // 既存のお知らせ編集処理
            if (AnnounceID != null)
            {
                AnnounceList = GetAnnounceList();
                if (AnnounceList.Count > 0)
                {
                    TitleTextBox.Text = AnnounceList[0].TITLE;
                    URLTextBox.Text = AnnounceList[0].URL;
                    ReleaseStartDateTimePicker.Value = AnnounceList[0].RELEASE_START_DATE;
                    ReleaseEndDateTimePicker.Value = AnnounceList[0].RELEASE_END_DATE;
                }
            }
            else
            {
                DeleteButton.Visible = false;
                ReleaseStartDateTimePicker.Value = null;
                ReleaseEndDateTimePicker.Value = null;

            }
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceDetailForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        /// <summary>
        /// 登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            if (InputItemCheck())
            {
                RegistData();

            }
        }

        /// <summary>
        /// 入力項目の内容チェック
        /// </summary>
        /// <returns>true/false</returns>
        private bool InputItemCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
            map[this.ReleaseEndDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //対象期間（開始）と対象期間（終了）がすべて入力してある場合のみチェック
                if (this.ReleaseStartDateTimePicker.Value != null && this.ReleaseEndDateTimePicker.Value != null)
                {
                    //開始日が終了日より大きい場合はエラー
                    if (this.ReleaseStartDateTimePicker.SelectedDate.Value > this.ReleaseEndDateTimePicker.SelectedDate.Value)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.ReleaseStartDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.ReleaseEndDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
                }
                return errMsg;
            };

            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }
            return true;
        }

        /// <summary>
        /// データ登録APIの呼び出し
        /// </summary>
        private void RegistData()
        {
            // 新規登録処理
            if (AnnounceID == null)
            {
                var AnnounceListCond = new InformationRegistInModel
                {
                    CHANGE_PERSONEL_ID = SessionDto.UserId,
                    TITLE = this.TitleTextBox.Text,
                    URL = this.URLTextBox.Text,
                    RELEASE_START_DATE = Convert.ToDateTime(this.ReleaseStartDateTimePicker.Value),
                    RELEASE_END_DATE = Convert.ToDateTime(this.ReleaseEndDateTimePicker.Value)
                };

                var res = HttpUtil.PostResponse<InformationRegistInModel>
                (ControllerType.Information, AnnounceListCond);

                if (res.Status == Const.StatusSuccess)
                {
                    Messenger.Info(Resources.KKM00002);
                    base.FormOkClose();
                }
                else
                {
                    Messenger.Warn(Resources.KKM03000);
                }
            }
            else
            // 既存のお知らせ編集処理
            {
                var AnnounceListUpdateCond = new InformationUpdateInModel
                {
                    CHANGE_PERSONEL_ID = SessionDto.UserId,
                    ID = (int)AnnounceID,
                    TITLE = this.TitleTextBox.Text,
                    URL = this.URLTextBox.Text,
                    RELEASE_START_DATE = Convert.ToDateTime(this.ReleaseStartDateTimePicker.Value),
                    RELEASE_END_DATE = Convert.ToDateTime(this.ReleaseEndDateTimePicker.Value)
                };

                var res = HttpUtil.PutResponse<InformationUpdateInModel>
                    (ControllerType.Information, AnnounceListUpdateCond);

                if (res.Status == Const.StatusSuccess)
                {
                    Messenger.Info(Resources.KKM00002);
                    base.FormOkClose();
                }
                else
                {
                    Messenger.Warn(Resources.KKM03000);
                }
            }
        }

        /// <summary>
        /// 削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
            {
                DeleteData();
            }
        }

        /// <summary>
        /// データ削除APIの呼び出し
        /// </summary>
        /// <returns></returns>
        private void DeleteData()
        {
            var AnnounceListCond = new InformationDeleteInModel
            {
                ID = (int)AnnounceID
            };

            var res = HttpUtil.DeleteResponse<InformationDeleteInModel>
                (ControllerType.Information, AnnounceListCond);

            if (res.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00003);
                base.FormOkClose();
            }
            else
            {
                Messenger.Warn(Resources.KKM03000);
            }
        }
    }

}

