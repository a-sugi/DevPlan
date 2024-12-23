using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// よく使う目的地選択画面
    /// </summary>
    public partial class FrequentlyUsedDestinationsForm : BaseSubForm
    {
        /// <summary>
        /// よく使う目的地リスト
        /// </summary>
        public List<FrequentlyUsedDestinations> DestinationList { get; private set; }

        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "よく使う目的地"; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FrequentlyUsedDestinationsForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequentlyUsedDestinationsForm_Load(object sender, EventArgs e)
        {
            FrequentlyUsedDestinationsMultiRow.AllowAutoExtend = true;
            FrequentlyUsedDestinationsMultiRow.AllowClipboard = false;
            FrequentlyUsedDestinationsMultiRow.AllowUserToAddRows = false;
            FrequentlyUsedDestinationsMultiRow.AllowUserToDeleteRows = false;
            FrequentlyUsedDestinationsMultiRow.MultiSelect = false;
            FrequentlyUsedDestinationsMultiRow.VerticalScrollMode = ScrollMode.Pixel;
            FrequentlyUsedDestinationsMultiRow.SplitMode = SplitMode.None;
            
            var res = HttpUtil.GetResponse<FrequentlyUsedDestinationsModel>(ControllerType.FrequentlyUsedDestinations, null);
            var list = new List<FrequentlyUsedDestinationsModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }
                        
            this.frequentlyUsedDestinationsMultiRowTemplate1.FrequentlyUsedTextBoxCell.DataField = nameof(FrequentlyUsedDestinationsModel.ルート名);
            this.FrequentlyUsedDestinationsMultiRow.Template = this.frequentlyUsedDestinationsMultiRowTemplate1;

            FrequentlyUsedDestinationsMultiRow.DataSource = list;
            
            this.ActiveControl = FrequentlyUsedDestinationsMultiRow;
        }

        /// <summary>
        /// よく使う目的地クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrequentlyUsedDestinationsMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                var item = (FrequentlyUsedDestinationsModel)((GcMultiRow)sender).Rows[e.RowIndex].DataBoundItem;
                string[] ret = item.ルート.Split(',');

                DestinationList = new List<FrequentlyUsedDestinations>();

                for (int i = 1; i < ret.Count() - 1; i++)
                {
                    DestinationList.Add(new FrequentlyUsedDestinations()
                    {
                        Departure = ret[i - 1],
                        Arrival = ret[i]
                    });
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
    
    /// <summary>
    /// よく使う目的地ルート格納クラス
    /// </summary>
    public class FrequentlyUsedDestinationsModel
    {
        /// <summary>
        /// ルート
        /// </summary>
        public string ルート { get; set; }

        /// <summary>
        /// ルート名
        /// </summary>
        public string ルート名 { get; set; }
    }
}
