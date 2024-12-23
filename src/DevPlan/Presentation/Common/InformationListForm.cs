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

using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// お知らせ一覧
    /// </summary>
    public partial class InformationListForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InformationListForm()
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
            var AnnounceListCond = new InformationInModel
            {
                STATUS = 2
            };

            var res = HttpUtil.GetResponse<InformationInModel, InformationOutModel>
                (ControllerType.Information, AnnounceListCond);
            return res.Results.ToList();
        }



        /// <summary>
        /// InformationListForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationListForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {

            // カラムの自動生成抑止
            this.AnnouceDataGridView.AutoGenerateColumns = false;

            AnnounceList = GetAnnounceList();

            //お知らせ一覧
            this.AnnouceDataGridView.DataSource = AnnounceList;
        }

        private void AnnouceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Process.Start(AnnounceList[e.RowIndex].URL);
        }
    }
}
