using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 検討会資料一覧
    /// </summary>
    public partial class MeetingDocumentListForm : BaseSubForm
    {
        #region メンバ変数
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "検討会資料一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>選択検討会資料</summary>
        public List<MeetingDocumentModel> MeetingDocumentList { get; set; } = new List<MeetingDocumentModel>();

        /// <summary>選択検討会資料</summary>
        public MeetingDocumentModel SelectedMeetingDocument { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeetingDocumentListForm()
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
        private void MeetingDocumentListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //検討会資料一覧の設定
            this.MeetingDocumentListDataGridView.AutoGenerateColumns = false;

            //検討会資料の設定
            this.SetMeetingDocumentList();

        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.MeetingDocumentListDataGridView.CurrentCell = null;

        }
        #endregion

        #region 検討会資料一覧
        /// <summary>
        /// 検討会資料一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            //選択列なら終了
            var col = this.MeetingDocumentListDataGridView.Columns[e.ColumnIndex];
            if (col.Name == this.SelectedColumn.Name)
            {
                return;

            }

            //検討会資料のチェック
            if (this.IsEntryMeetingDocument(MeetingDocumentEditType.Show) == false)
            {
                //検討会資料の設定
                this.SetMeetingDocumentList();
                return;

            }

            //検討会資料詳細画面表示
            var siryou = this.GetSelectedMeetingDocumentByRow(this.MeetingDocumentListDataGridView.CurrentCell.OwningRow);
            new MeetingDocumentDetailListForm { MeetingDocument = siryou }.Show();

            //フォームクローズ
            this.Close();

        }
        #endregion

        #region 追加ボタンクリック
        /// <summary>
        /// 追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            //検討会基本情報画面表示
            this.ShowMeetingDocumentDetailForm(MeetingDocumentEditType.Insert);

        }
        #endregion

        #region 修正ボタンクリック
        /// <summary>
        /// 修正ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            //検討会基本情報画面表示
            this.ShowMeetingDocumentDetailForm(MeetingDocumentEditType.Update);

        }
        #endregion

        #region コピーボタンクリック
        /// <summary>
        /// コピーボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            //検討会基本情報画面表示
            this.ShowMeetingDocumentDetailForm(MeetingDocumentEditType.Copy);

        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //検討会基本情報画面表示
            FormControlUtil.FormWait(this, () =>
            {
                //一覧を選択しているかどうか
                if (this.GetSelectedMeetingDocumentList().Any() == false)
                {
                    Messenger.Warn(Resources.KKM00009);
                    return;

                }

                //削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    //検討会資料のチェック
                    if (this.IsEntryMeetingDocument(MeetingDocumentEditType.Delete) == false)
                    {
                        //検討会資料の設定
                        this.SetMeetingDocumentList();
                        return;

                    }

                    //削除が成功したかどうか
                    if (this.DeleteMeetingDocument() == true)
                    {
                        //検討会資料の設定
                        this.SetMeetingDocumentList();
                        return;

                    }

                }

            });

        }
        #endregion

        #region 検討会資料の設定
        /// <summary>
        /// 検討会資料の設定
        /// </summary>
        private void SetMeetingDocumentList()
        {
            //選択検討会資料
            this.MeetingDocumentList = this.GetMeetingDocumentList();

            //検討会資料の設定
            this.MeetingDocumentListDataGridView.DataSource = this.MeetingDocumentList;

            //一覧を未選択状態に設定
            this.MeetingDocumentListDataGridView.CurrentCell = null;

            //一覧が取得できたかどうか
            this.ListDataLabel.Text = this.MeetingDocumentList.Any() == false ? Resources.KKM00005 : "";

        }
        #endregion

        #region 検討会基本情報画面表示
        /// <summary>
        /// 検討会基本情報画面表示
        /// </summary>
        /// <param name="type">検討会資料編集区分</param>
        private void ShowMeetingDocumentDetailForm(MeetingDocumentEditType type)
        {
            //検討会資料のチェック
            if (this.IsEntryMeetingDocument(type) == false)
            {
                //検討会資料の設定
                this.SetMeetingDocumentList();
                return;

            }

            using (var form = new MeetingDocumentDetailForm { MeetingDocument = this.SelectedMeetingDocument, MeetingDocumentEdit = type })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //検討会資料の設定
                    this.SetMeetingDocumentList();

                }

            }
        }
        #endregion

        #region 検討会資料のチェック
        /// <summary>
        /// 検討会資料のチェック
        /// </summary>
        /// <returns>チェック可否</returns>
        private bool IsEntryMeetingDocument(MeetingDocumentEditType type)
        {
            var list = new List<MeetingDocumentModel>();

            //検討会資料編集区分ごとの分岐
            switch (type)
            {
                //追加
                case MeetingDocumentEditType.Insert:
                    //選択討会資料
                    this.SelectedMeetingDocument = new MeetingDocumentModel();
                    break;

                //表示
                case MeetingDocumentEditType.Show:
                    //選択しているかどうか
                    var cell = this.MeetingDocumentListDataGridView.CurrentCell;
                    if (cell == null)
                    {
                        Messenger.Warn(Resources.KKM00009);
                        return false;

                    }

                    //選択行を取得
                    list.Add(this.GetSelectedMeetingDocumentByRow(cell.OwningRow));
                    break;

                //更新
                //削除
                //コピー
                case MeetingDocumentEditType.Update:
                case MeetingDocumentEditType.Delete:
                case MeetingDocumentEditType.Copy:
                    //選択している検討会資料を取得
                    list = this.GetSelectedMeetingDocumentList();

                    //選択しているかどうか
                    if (list.Any() == false)
                    {
                        Messenger.Warn(Resources.KKM00009);
                        return false;

                    }
                    break;

            }

            var count = list.Count();

            //検討会資料編集区分ごとの分岐
            switch (type)
            {
                //表示
                //更新
                //コピー
                case MeetingDocumentEditType.Show:
                case MeetingDocumentEditType.Update:
                case MeetingDocumentEditType.Copy:
                    //選択しているかどうか
                    if (count > 1)
                    {
                        Messenger.Warn(Resources.KKM00019);
                        return false;

                    }

                    //選択討会資料
                    this.SelectedMeetingDocument = list.First();
                    break;

            }

            //検討会資料編集区分ごとの分岐
            switch (type)
            {
                //表示
                //更新
                //削除
                //コピー
                case MeetingDocumentEditType.Show:
                case MeetingDocumentEditType.Update:
                case MeetingDocumentEditType.Delete:
                case MeetingDocumentEditType.Copy:
                    //全て存在しているかどうか
                    if (count != this.GetMeetingDocumentList(new MeetingDocumentSearchModel { ID = list.Select(x => (int?)x.ID).ToArray() }).Count())
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        return false;

                    }
                    break;

            }

            return true;

        }
        #endregion

        #region 検討会資料を取得
        /// <summary>
        /// 選択している検討会資料を取得
        /// </summary>
        /// <returns></returns>
        private List<MeetingDocumentModel> GetSelectedMeetingDocumentList()
        {
            var list = new List<MeetingDocumentModel>();

            foreach (DataGridViewRow row in this.MeetingDocumentListDataGridView.Rows)
            {
                //選択しているかどうか
                if (Convert.ToBoolean(row.Cells[this.SelectedColumn.Name].Value) == true)
                {
                    //選択行の検討会資料を取得
                    list.Add(this.GetSelectedMeetingDocumentByRow(row));

                }

            }

            return list;

        }

        /// <summary>
        /// 検討会資料を行から取得
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        private MeetingDocumentModel GetSelectedMeetingDocumentByRow(DataGridViewRow row)
        {
            var id = Convert.ToInt32(row.Cells[this.IdColumn.Name].Value);

            return this.MeetingDocumentList.First(x => x.ID == id);
            
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        private List<MeetingDocumentModel> GetMeetingDocumentList()
        {
            return this.GetMeetingDocumentList(new MeetingDocumentSearchModel());

        }

        /// <summary>
        /// 検討会資料の取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<MeetingDocumentModel> GetMeetingDocumentList(MeetingDocumentSearchModel cond)
        {
            var list = new List<MeetingDocumentModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<MeetingDocumentSearchModel, MeetingDocumentModel>(ControllerType.MeetingDocument, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region 検討会資料の削除
        /// <summary>
        /// 検討会資料の削除
        /// </summary>
        /// <returns>削除可否</returns>
        private bool DeleteMeetingDocument()
        {
            var flg = false;

            //削除
            var res = HttpUtil.DeleteResponse(ControllerType.MeetingDocument, this.GetSelectedMeetingDocumentList());

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除後メッセージ
                Messenger.Info(Resources.KKM00003);

                flg = true;

            }

            return flg;

        }
        #endregion
    }
}
