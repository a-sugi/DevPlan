﻿using DevPlan.Presentation.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 使用上の注意事項ウィンドウ。
    /// </summary>
    public partial class UsageNotesForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "使用上の注意事項"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面の初期化を行います。
        /// </remarks>
        public UsageNotesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OKボタン押下処理。
        /// </summary>
        /// <remarks>
        /// DialogResultをOKとしてウィンドウを閉じます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
