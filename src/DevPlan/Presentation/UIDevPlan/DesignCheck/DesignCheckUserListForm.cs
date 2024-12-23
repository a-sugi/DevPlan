using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 設計チェック参加者一覧
    /// </summary>
    public partial class DesignCheckUserListForm : BaseForm
    {
        #region メンバ変数

        /// <summary>
        /// クラス生成クラス
        /// </summary>
        private UserListFactory Factory;

        /// <summary>
        /// 未登録参加者追加のツールチップ
        /// </summary>
        private ToolTip tip = new ToolTip();

        /// <summary>設計チェック参加者</summary>
        private List<DesignCheckUserGetOutModel> DesignCheckUserList { get; set; } = new List<DesignCheckUserGetOutModel>();

        #endregion

        #region プロパティ

        public override string FormTitle
        {
            get
            {
                return "設計チェック参加者一覧";
            }
        }

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; }

        /// <summary>
        /// 読み取り専用の画面で開くか？
        /// </summary>
        public bool IsReadonly { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckUserListForm()
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
        private void DesignCheckUserListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 初期化
                Factory = new UserListFactory(DesignCheck, base.GetFunction(FunctionID.DesignCheck), IsReadonly);

                //画面初期化
                this.InitForm();
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //設計チェックの取得
            this.DesignCheck = this.GetDesignCheck();

            //開催日
            this.DateLabel.Text = DateTimeUtil.ConvertDateString(this.DesignCheck.開催日);

            //設計チェック名
            var kai = this.DesignCheck.回;
            this.DesignCheckNameLabel.Text = this.DesignCheck.名称 + (kai == null ? "" : string.Format(" {0}回目", kai));

            //列の自動生成可否
            this.ListDataGridView.AutoGenerateColumns = false;

            // 権限設定
            UserAddButton.Visible = Factory.Authority.IsVisible(UserAddButton);
            UnregisteredUserAddbutton.Visible = Factory.Authority.IsVisible(UnregisteredUserAddbutton);
            UserDeletebutton.Visible = Factory.Authority.IsVisible(UserDeletebutton);
            DownloadButton.Visible = Factory.Authority.IsVisible(DownloadButton);
            AllSelectCheckBox.Visible = Factory.Authority.IsVisible(UserDeletebutton);
            ListDataGridView.Columns[this.SelectedColumn.Name].Visible = Factory.Authority.IsVisible(UserDeletebutton);

            // ToolTip設定
            tip.AutoPopDelay = 32000;
            tip.SetToolTip(UnregisteredUserAddbutton, "参加者追加できない場合はこちらで入力してください。");

            // 全選択チェックボックス調整
            AdjustAllSelectCheckBox();

            // データ表示
            this.SetData();
        }

        /// <summary>
        /// 全選択チェックボックス調整
        /// </summary>
        private void AdjustAllSelectCheckBox()
        {
            var grid = this.ListDataGridView;
            var corner = grid.TopLeftHeaderCell;
            var cbox = this.AllSelectCheckBox;

            var x = (corner.Size.Width - cbox.Width) / 2 + grid.Location.X - 3;
            var y = (corner.Size.Height - cbox.Height) / 2 + grid.Location.Y + 2;

            cbox.Location = new Point(x, y);
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckUserListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;

            this.ActiveControl = UserAddButton;
            UserAddButton.Focus();
        }
        #endregion

        #region 参加者追加ボタンクリック
        /// <summary>
        /// 追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAddButton_Click(object sender, EventArgs e)
        {
            using (var form = new UserListMultiSelectForm() { UserAuthority = Factory.Authority.UserAuthorityModel, StatusCode = "a" })
            {
                //ユーザー検索画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        // 登録済以外のユーザ
                        var targets = form.Users.FindAll((x) => this.DesignCheckUserList.Any((y) => x.PERSONEL_ID == y.PERSONEL_ID) == false);

                        // すべて登録済のユーザーの場合
                        if (targets.Count == 0)
                        {
                            Messenger.Warn(Resources.KKM03032);
                            return;
                        }

                        // 取得した情報を追加
                        foreach (var user in targets)
                        {
                            this.DesignCheckUserList.Add(new DesignCheckUserGetOutModel
                            {
                                //開催日ID
                                開催日_ID = this.DesignCheck.ID.Value,

                                //参加者ID
                                PERSONEL_ID = user.PERSONEL_ID,

                                //参加者名
                                NAME = user.NAME,

                                //部ID
                                DEPARTMENT_ID = user.DEPARTMENT_ID,

                                //部コード
                                DEPARTMENT_CODE = user.DEPARTMENT_CODE,

                                //課ID
                                SECTION_ID = user.SECTION_ID,

                                //課コード
                                SECTION_CODE = user.SECTION_CODE,

                                //担当ID
                                SECTION_GROUP_ID = user.SECTION_GROUP_ID,

                                //担当コード
                                SECTION_GROUP_CODE = user.SECTION_GROUP_CODE

                            });
                        }

                        // DB登録
                        this.EntryDesignCheckUser();

                        //画面に表示
                        this.SetData();
                    });
                }
            }
        }
        #endregion

        #region 未登録参加者追加ボタン
        /// <summary>
        /// 未登録参加者追加ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnregisteredUserAddbutton_Click(object sender, EventArgs e)
        {
            using (var form = new UnregisteredUserAddForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 登録済のユーザーの場合
                    if (this.DesignCheckUserList.Any((x) =>
                            x.DEPARTMENT_ID == form.DEPARTMENT_ID &&
                            x.SECTION_ID == form.SECTION_ID &&
                            x.SECTION_GROUP_ID == form.SECTION_GROUP_ID &&
                            x.NAME == form.NAME) == true)
                    {
                        Messenger.Warn(Resources.KKM03032);
                        return;
                    }

                    this.DesignCheckUserList.Add(new DesignCheckUserGetOutModel
                    {
                        // 開催日ID
                        開催日_ID = this.DesignCheck.ID.Value,

                        // 参加者名
                        NAME = form.NAME,

                        // 部ID
                        DEPARTMENT_ID = form.DEPARTMENT_ID,

                        // 部コード
                        DEPARTMENT_CODE = form.DEPARTMENT_CODE,

                        SECTION_ID = form.SECTION_ID,

                        SECTION_CODE = form.SECTION_CODE,

                        SECTION_GROUP_ID = form.SECTION_GROUP_ID,

                        SECTION_GROUP_CODE = form.SECTION_GROUP_CODE,
                    });

                    // DB登録
                    this.EntryDesignCheckUser();

                    //設計チェック参加者の取得
                    this.DesignCheckUserList = this.GetDesignCheckUserList();

                    //画面に表示
                    this.SetData();
                }
            }
        }
        #endregion

        #region 参加者削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDeletebutton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //削除
                this.DeleteDesignCheckUser();

                //画面に表示
                this.SetData();
            });
        }

        /// <summary>
        /// 設計チェック参加者の削除
        /// </summary>
        private void DeleteDesignCheckUser()
        {
            var list = new List<DesignCheckUserGetOutModel>();

            for (var i = 0; i < this.ListDataGridView.RowCount; i++)
            {
                var row = this.ListDataGridView.Rows[i];

                //選択しているかどうか
                if (Convert.ToBoolean(row.Cells[this.SelectedColumn.Name].Value) == true)
                {
                    //削除対象を追加
                    list.Add(this.DesignCheckUserList[i]);
                }
            }

            //削除対象があるかどうか
            if (list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            // 削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;
            }

            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.DesignCheckUser, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除後メッセージ
                Messenger.Info(Resources.KKM00003);
            }
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            var util = new DataGridViewUtil<DesignCheckPointGetOutModel>();
            util.dataGridView = this.ListDataGridView;
            util.TreatmentHeaders = (x) => { x.InsertRange(0, new string[] { "開催日", "設計チェック名" }); return x; };
            util.TreatmentRows = (x) => { x.InsertRange(0, new string[] { DateLabel.Text, DesignCheckNameLabel.Text }); return x; };
            util.SaveFile(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion        

        #region 全選択変更
        /// <summary>
        /// 全選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                var flg = this.AllSelectCheckBox.Checked;

                //全ての行の選択を設定
                foreach (DataGridViewRow row in this.ListDataGridView.Rows)
                {
                    //Append Start 2020/01/27 杉浦
                    // 非表示行は未処理
                    if (!row.Visible) continue;
                    //Append End 2020/01/27 杉浦

                    row.Cells[this.SelectedColumn.Name].Value = flg;

                }
            });
        }
        #endregion

        #region 参加者登録
        /// <summary>
        /// 参加者登録
        /// </summary>
        /// <returns></returns>
        private bool EntryDesignCheckUser()
        {
            var flg = false;

            //登録
            var res = HttpUtil.PostResponse<DesignCheckGetOutModel>(ControllerType.DesignCheckUser, this.DesignCheckUserList);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ
                Messenger.Info(Resources.KKM00002);

                flg = true;

            }

            return flg;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheck()
        {
            var list = new List<DesignCheckGetOutModel>();

            var cond = new DesignCheckGetInModel
            {
                //開催日ID
                ID = this.DesignCheck.ID

            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckGetInModel, DesignCheckGetOutModel>(ControllerType.DesignCheck, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list.FirstOrDefault();

        }

        /// <summary>
        /// 設計チェック参加者の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckUserGetOutModel> GetDesignCheckUserList()
        {
            var list = new List<DesignCheckUserGetOutModel>();

            var cond = new DesignCheckUserGetInModel
            {
                //開催日ID
                開催日_ID = this.DesignCheck.ID

            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckUserGetInModel, DesignCheckUserGetOutModel>(ControllerType.DesignCheckUser, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region 参加者を取得して画面表示します。
        /// <summary>
        /// 参加者を取得して画面表示します。
        /// </summary>
        private void SetData()
        {
            // 設計チェック参加者の取得
            this.DesignCheckUserList = this.GetDesignCheckUserList();

            // バインド
            this.ListDataGridView.DataSource = this.DesignCheckUserList;

            // 全選択チェック初期化
            AllSelectCheckBox.CheckedChanged -= new EventHandler(this.AllSelectCheckBox_CheckedChanged);
            AllSelectCheckBox.Checked = false;
            AllSelectCheckBox.CheckedChanged += new EventHandler(this.AllSelectCheckBox_CheckedChanged);
        }
        #endregion

        #region 画面がアクティブになった時
        /// <summary>
        /// 画面がアクティブになった時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckUserListForm_Activated(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        #endregion

        #region 参加者一覧用クラス生成クラス
        /// <summary>
        /// 参加者一覧用クラス生成クラス
        /// </summary>
        private class UserListFactory : Factory
        {
            /// <summary>
            /// 参加者一覧用クラス生成クラスのコンストラクタ
            /// </summary>
            /// <param name="designCheck"></param>
            /// <param name="userAuthorityModel"></param>
            /// <param name="isReadonly"></param>
            /// <param name="successProc"></param>
            public UserListFactory(DesignCheckGetOutModel designCheck, UserAuthorityOutModel userAuthorityModel, bool isReadonly, Action successProc = null) 
                : base(designCheck, userAuthorityModel, successProc)
            {
                // 権限管理クラスを生成し直す
                Authority = isReadonly ? new ReadOnlyAuth(userAuthorityModel) : new Authority(userAuthorityModel);
            }
        }
        #endregion

        #region 読み取り専用の権限管理クラス
        /// <summary>
        /// 読み取り専用の権限管理クラス
        /// </summary>
        private class ReadOnlyAuth : Authority
        {
            /// <summary>
            /// 読み取り専用の権限管理クラスのコンストラクタ
            /// </summary>
            /// <param name="model"></param>
            public ReadOnlyAuth(UserAuthorityOutModel model) : base(model)
            {
            }

            /// <summary>
            /// 読み取り専用のため更新系ボタンは非表示
            /// </summary>
            /// <returns></returns>
            protected override bool IsVisibleBtnUpdate()
            {
                return false;
            }
        }
        #endregion

    }
}
