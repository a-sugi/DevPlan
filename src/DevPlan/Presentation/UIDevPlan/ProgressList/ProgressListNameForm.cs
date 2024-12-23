using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

using DevPlan.Presentation.Base;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System.Drawing;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon;

namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    /// <summary>
    /// 目標進度リスト名編集
    /// </summary>
    public partial class ProgressListNameForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "目標進度リスト名編集"; } }

        /// <summary>目標進度リスト名(XXX-YYY-ZZZ)</summary>
        public string ListName { get; set; }

        /// <summary>目標進度リスト名ID</summary>
        public int ListNameID { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressListNameForm()
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
        private void ProgressListSectionForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }
        
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var width = NameLabel.Width;

            if (this.ListName == null || 0 == this.ListName.Length)
            {
                NameLabel.Text = "";
                return;
            }

            var name = this.ListName.Split('-');

            var cnt = name.Count();

            //ラベル部分表示
            if (2 <= cnt)
            {
                this.NameLabel.Text = name[0] + "-" + name[1] + "-";

            }
            else
            {
                this.NameLabel.Text = "";

            }

            //テキストボックス部分表示
            if (3 <= cnt)
            {
                this.NameTextBox.Text = string.Join("-", name.Skip(3).ToArray());

            }

            //テキストボックスの位置修正
            this.NameTextBox.Location = new Point(NameTextBox.Location.X + (this.NameLabel.Width - width), this.NameTextBox.Location.Y);
            this.NameTextBox.Width = this.NameTextBox.Width - (this.NameLabel.Width - width);

            //フォーカス
            this.ActiveControl = this.NameTextBox;

        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            map[this.NameTextBox] = (c, name) =>
            {
                var errMsg = "";

                //入力されているかどうか
                if (string.IsNullOrEmpty(this.NameTextBox.Text) == false)
                {
                    //重複した名称があるかどうか
                    var checkListName = (this.NameLabel.Text + this.NameTextBox.Text);
                    if (this.GetCheckList().Any(x => x.CHECKLIST_NAME == checkListName) == true)
                    {
                        errMsg = Resources.KKM03009;

                    }
                    
                }

                return errMsg;

            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;

            }

            // リスト名変更APIにパラメータを渡す
            var mokuhyou = new TargetProgressListNameUpdateInModel
            {
                CHECKLIST_NAME_ID = this.ListNameID,
                NEW_LISTNAME = this.NameTextBox.Text,
            };

            //DB(目標進度_チェックリスト名)に登録
            var res = HttpUtil.PutResponse<TargetProgressListNameUpdateInModel>(ControllerType.TargetProgressListName, mokuhyou);

            //登録が成功したかどうか
            if (res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ
                Messenger.Info(Resources.KKM00002);

                //画面を閉じる
                base.FormOkClose();

            }

        }
        #endregion

        #region 目標進度リスト一覧の取得
        /// <summary>
        /// 目標進度リスト一覧の取得
        /// </summary>
        /// <returns>List</returns>
        private List<TargetProgressListNameSearchOutModel> GetCheckList()
        {
            var listGetParam = new TargetProgressListNameSearchInModel()
            {
                DEPARTMENT_ID = SessionDto.DepartmentID,
                PERSONEL_ID = SessionDto.UserId,
                PROCESS_CATEGORY = "1",  //性能別
                DIVISION_CATEGORY = "2", //全部署
            };

            var res = HttpUtil.GetResponse<TargetProgressListNameSearchInModel, TargetProgressListNameSearchOutModel>(ControllerType.TargetProgressListName, listGetParam);

            return res.Results.ToList();

        }
        #endregion

    }
}
