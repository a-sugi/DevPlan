using System;
using System.Collections.Generic;
using System.Linq;

using DevPlan.Presentation.Base;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon;

namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    /// <summary>
    /// コピー元目標進度リスト一覧
    /// </summary>
    public partial class ProgressListCandidateForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "コピー元目標進度リスト一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressListCandidateForm()
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
        private void ProgressListCandidateForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //グリッド表示
            this.SetGridData();

            //フォーカス
            this.ActiveControl = NameListDataGridView;

        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListCandidateForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.NameListDataGridView.CurrentCell = null;

        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            this.NameListDataGridView.AutoGenerateColumns = false;
            
            //開発符号取得
            var generalCodeList = GetGeneralCodeList();
            if (generalCodeList == null)
            {
                return;
            }

            //性能名取得
            var performanceNameList = GetPerformanceNameList();
            if (performanceNameList == null)
            {
                return;
            }

            //グリッド表示(開発符号-性能名)
            foreach (var generalcode in generalCodeList)
            {
                foreach (var performancename in performanceNameList)
                {
                    NameListDataGridView.Rows.Add();
                    var idx = NameListDataGridView.Rows.Count - 1;
                    NameListDataGridView.Rows[idx].Cells["リスト名"].Value =
                        generalcode.GENERAL_CODE + "-" + performancename.性能名;

                    NameListDataGridView.Rows[idx].Cells["開発符号"].Value = generalcode.GENERAL_CODE;
                    NameListDataGridView.Rows[idx].Cells["性能名ID"].Value = performancename.ID;
                }
            }

            //検索結果がない場合メッセージ表示
            if (NameListDataGridView.Rows.Count == 0)
            {
                MessageLabel.Text = Resources.KKM00005;

            }

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
            FormControlUtil.FormWait(this, () =>
            {
                //登録に成功したかどうか
                if (this.InsertMokuhyouSindo() == true)
                {
                    base.FormOkClose();

                }

            });
        }
        #endregion

        #region データの取得
            /// <summary>
            /// 目標進度リスト一覧の取得
            /// </summary>
            /// <returns>List</returns>
        private List<TargetProgressListNameSearchOutModel> GetCheckList()
        {
            var cond = new TargetProgressListNameSearchInModel()
            {
                DEPARTMENT_ID = SessionDto.DepartmentID,
                PERSONEL_ID = SessionDto.UserId,
                PROCESS_CATEGORY = "1",  //性能別
                DIVISION_CATEGORY = "2", //全部署
            };

            var res = HttpUtil.GetResponse<TargetProgressListNameSearchInModel, TargetProgressListNameSearchOutModel>(ControllerType.TargetProgressListName, cond);

            return res.Results.ToList();

        }

        /// <summary>
        /// 開発符号の取得
        /// </summary>
        private List<GeneralCodeSearchOutModel> GetGeneralCodeList()
        {
            // パラメータ設定
            var cond = new GeneralCodeSearchInModel
            {
                UNDER_DEVELOPMENT = "1",
                PERSONEL_ID = SessionDto.UserId
            };

            // Get実行
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, cond);

            return (res.Results).ToList();
        }

        /// <summary>
        /// 性能名の取得
        /// </summary>
        private List<PerformanceNameOutModel> GetPerformanceNameList()
        {
            // パラメータ設定
            var cond = new PerformanceNameInModel
            {
                SECTION_ID = SessionDto.SectionID,
            };

            // Get実行
            var res = HttpUtil.GetResponse<PerformanceNameInModel, PerformanceNameOutModel>(ControllerType.PerformanceName, cond);

            return (res.Results).ToList();
        }
        #endregion

        #region 目標進度追加
        /// <summary>
        /// 目標進度追加
        /// </summary>
        private bool InsertMokuhyouSindo()
        {
            //一覧を選択しているかどうか
            if (this.NameListDataGridView.CurrentCell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return false;

            }

            //選択行から登録値を設定
            var row = this.NameListDataGridView.CurrentRow;
            var mokuhyou = new TargetProgressListRegistInModel
            {
                //開発符号
                GENERAL_CODE = row.Cells["開発符号"].Value.ToString(),

                //性能名ID
                SPEC_NAME_ID = Convert.ToInt32(row.Cells["性能名ID"].Value),

                //編集者ID
                EDITOR_ID = SessionDto.UserId

            };

            // 登録APIの実行
            var res = HttpUtil.PostResponse(ControllerType.TargetProgressList, new[] { mokuhyou });

            // 正常処理の場合はメッセージ
            var flg = res.Status == Const.StatusSuccess;
            if (flg == true)
            {
                Messenger.Info(Resources.KKM00002);

            }

            return flg;

        }
        #endregion
       
    }
}
