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

namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{
    //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
    public partial class NDriveLinkDetailForm : BaseSubForm
    {
        #region プロパティ

        public override string FormTitle { get { return "写真・動画詳細"; } }

        public override int FormHeight { get { return this.Height; } }

        public override int FormWidth { get { return this.Width; } }

        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>お知らせID</summary>
        public long? NDriveLinkID { get; set; }

        /// <summary>お知らせCapID</summary>
        public int NDriveLinkCapID { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion
        public NDriveLinkDetailForm()
        {
            InitializeComponent();
        }

        private List<NDriveLinkOutModel> NDriveLinkList;

        /// <summary>
        /// 写真・動画API検索の呼び出し
        /// </summary>
        /// <returns></returns>
        private List<NDriveLinkOutModel> GetNDriveLinkList()
        {
            var cond = new NDriveLinkInModel
            {
                ID = NDriveLinkID
            };

            var res = HttpUtil.GetResponse<NDriveLinkInModel, NDriveLinkOutModel>(ControllerType.NDriveLink, cond);

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

            // 既存の写真・動画編集処理
            if (NDriveLinkID != null)
            {
                NDriveLinkList = GetNDriveLinkList();
                if (NDriveLinkList.Count > 0)
                {
                    TitleTextBox.Text = NDriveLinkList[0].TITLE;
                    URLTextBox.Text = NDriveLinkList[0].URL;
                }
            }
            else
            {
                DeleteButton.Visible = false;

            }
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NDriveLinkDetailForm_Load(object sender, EventArgs e)
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

            //URLの正規チェック(ローカルパス、ネットワークパス、URLのどれかに合致できるものであることが条件)
            map[this.URLTextBox] = (c, name) =>
            {
                var errMsg = "";

                //関連パスのファイルパスチェック
                var path = this.URLTextBox.Text;
                if (!string.IsNullOrEmpty(path))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(path, @"[a-zA-Z]:\\[\w\\\.]*") && !System.Text.RegularExpressions.Regex.IsMatch(path, @"\\\\[\w\\\.]*|""\\\\[\w\.].* """) && !System.Text.RegularExpressions.Regex.IsMatch(path, @"^[hH][tT][tT][pP]([sS])?://"))
                    {
                        errMsg = string.Format(Resources.KKM00032, name);
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
            if (NDriveLinkID == null)
            {
                var NDriveLinkListCond = new NDriveLinkRegistInModel
                {
                    CHANGE_PERSONEL_ID = SessionDto.UserId,
                    TITLE = this.TitleTextBox.Text,
                    URL = this.URLTextBox.Text,
                    CAP_ID = this.NDriveLinkCapID,
                };

                var res = HttpUtil.PostResponse<NDriveLinkRegistInModel>
                (ControllerType.NDriveLink, NDriveLinkListCond);

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
            // 既存の写真・動画編集処理
            {
                var NDriveLinkListUpdateCond = new NDriveLinkUpdateInModel
                {
                    CHANGE_PERSONEL_ID = SessionDto.UserId,
                    ID = (int)NDriveLinkID,
                    TITLE = this.TitleTextBox.Text,
                    URL = this.URLTextBox.Text
                };

                var res = HttpUtil.PutResponse<NDriveLinkUpdateInModel>
                    (ControllerType.NDriveLink, NDriveLinkListUpdateCond);

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
            var NDriveLinkListCond = new NDriveLinkDeleteInModel
            {
                ID = (int)NDriveLinkID
            };

            var res = HttpUtil.DeleteResponse<NDriveLinkDeleteInModel>
                (ControllerType.NDriveLink, NDriveLinkListCond);

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
    //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
}

