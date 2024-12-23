using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.Base
{
    /// <summary>
    /// 基底フォーム(試験車管理)
    /// </summary>
    public partial class BaseTestCarForm : BaseForm
    {
        #region プロパティ
        /// <summary>
        /// システム名
        /// </summary>
        public override string SystemName { get { return "試験車管理システム"; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseTestCarForm()
        {
            InitializeComponent();

        }
        #endregion
    }
}
