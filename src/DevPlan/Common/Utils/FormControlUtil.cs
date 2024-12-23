using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;

using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Util;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// フォーム制御ユーティリティ
    /// </summary>
    public class FormControlUtil
    {
        #region テキストボックスの値を全てクリア
        /// <summary>
        /// テキストボックスの値を全てクリア
        /// </summary>
        /// <param name="ctl"></param>
        public static void ClearTextBox(Control ctl)
        {
            foreach (Control ctl2 in ctl.Controls)
            {
                if (ctl2.Controls.Count > 0) ClearTextBox(ctl2);

                if (ctl2 is TextBox)
                {
                    ((TextBox)ctl2).Clear();
                }
                else if (ctl2 is MaskedTextBox)
                {
                    ((MaskedTextBox)ctl2).Clear();
                }
            }
        }
        #endregion

        #region コントロールに含まれる子コントロールを全て取得
        /// <summary>
        /// コントロールに含まれる子コントロールを全て取得
        /// </summary>
        /// <param name="ctl">対象コントロール</param>
        /// <param name="list">子コントロール格納用リスト</param>
        public static void GetControls(Control ctl, List<Control> list)
        {
            foreach (Control ctl2 in ctl.Controls)
            {
                if (ctl2.Controls.Count > 0) GetControls(ctl2, list);

                list.Add(ctl2);
            }
        }

        /// <summary>
        /// コントロールに含まれる、入力用の子コントロールを全て取得
        /// </summary>
        /// <param name="ctl">対象コントロール</param>
        /// <param name="list">子コントロール格納用リスト</param>
        /// <param name="includeReadOnly">編集不可コントロールを含める場合はTrue</param>
        public static void GetInputControls(Control ctl, List<Control> list, bool includeReadOnly)
        {
            foreach (Control ctl2 in ctl.Controls)
            {
                if (ctl2.Controls.Count > 0) GetInputControls(ctl2, list, includeReadOnly);

                if (includeReadOnly)
                {
                    list.Add(ctl2);
                }
                else
                {
                    if (ctl2.Name != string.Empty)
                    {
                        if (ctl2 is TextBox)
                        {
                            if (!((TextBox)ctl2).ReadOnly) list.Add(ctl2);
                        }
                        else if (ctl2 is MaskedTextBox)
                        {
                            if (!((MaskedTextBox)ctl2).ReadOnly) list.Add(ctl2);
                        }
                        else if (ctl2 is ComboBox)
                        {
                            if (((ComboBox)ctl2).Enabled) list.Add(ctl2);
                        }
                        else if (ctl2 is CheckBox)
                        {
                            if (((CheckBox)ctl2).Enabled) list.Add(ctl2);
                        }
                        else if (ctl2 is DateTimePicker)
                        {
                            if (((DateTimePicker)ctl2).Enabled) list.Add(ctl2);
                        }
                    }
                }
            }
        }
        #endregion

        #region マスクテキストボックスの値を取得
        /// <summary>
        /// マスクテキストボックスの書式込みの値を取得
        /// </summary>
        /// <param name="maskedTextBox">MaskedTextBox</param>
        /// <returns></returns>
        public static string GetMaskedTextBoxValue(MaskedTextBox maskedTextBox)
        {
            string strValue = string.Empty;
            // 書式込みの値を取得
            if (maskedTextBox.TextMaskFormat == MaskFormat.IncludeLiterals)
            {
                strValue = maskedTextBox.Text.Trim();
            }
            else
            {
                // 書式退避
                MaskFormat mask = maskedTextBox.TextMaskFormat;
                maskedTextBox.TextMaskFormat = MaskFormat.IncludeLiterals;
                strValue = maskedTextBox.Text.Trim();
                // 書式を元に戻す
                maskedTextBox.TextMaskFormat = mask;
            }
            return strValue;
        }

        /// <summary>
        /// マスクテキストボックスの書式無し（入力した値のみ）の値を取得
        /// </summary>
        /// <param name="maskedTextBox">MaskedTextBox</param>
        /// <returns></returns>
        public static string GetMaskedTextBoxPlainValue(MaskedTextBox maskedTextBox)
        {
            string strValue = string.Empty;
            // 入力したテキストのみ取得
            if (maskedTextBox.TextMaskFormat == MaskFormat.ExcludePromptAndLiterals)
            {
                strValue = maskedTextBox.Text.Trim();
            }
            else
            {
                // 書式退避
                MaskFormat mask = maskedTextBox.TextMaskFormat;
                maskedTextBox.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                strValue = maskedTextBox.Text.Trim();
                // 書式を元に戻す
                maskedTextBox.TextMaskFormat = mask;
            }
            return strValue;
        }
        #endregion

        #region コンボボックスのドロップダウン時の表示幅を設定
        /// <summary>
        /// コンボボックスのドロップダウン時の表示幅を設定
        /// </summary>
        /// <param name="cmb"></param>
        public static void SetDropDownWidth(ComboBox cmb)
        {
            var width = cmb.Width;

            //項目があるかどうか
            if (cmb.Items.Count != 0)
            {
                var max = 0;

                var g = cmb.CreateGraphics();

                foreach (var item in cmb.Items)
                {
                    var text = "";

                    if (item.GetType() == typeof(string))
                    {
                        text = item as string;

                    }
                    else
                    {
                        var pi = item.GetType().GetProperty(cmb.DisplayMember);
                        var value = pi.GetValue(item);
                        text = value == null ? "" : value.ToString();

                    }

                    max = (int)Math.Max(max, g.MeasureString(text, cmb.Font).Width);

                }

                width = width >= max ? width : max;

            }

            //幅
            cmb.DropDownWidth = width;

        }
        #endregion

        #region コンボボックスにアイテムをセット
        /// <summary>
        /// コンボボックスにアイテムをセット
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="cmb">コンボボックス</param>
        /// <param name="list">データ</param>
        /// <param name="isAddEmptyItem">空白行追加可否</param>
        public static void SetComboBoxItem<T>(ComboBox cmb, IEnumerable<T> list, bool isAddEmptyItem = true)
            where T : class, new()
        {
            SetComboBoxItem(cmb, cmb.ValueMember, cmb.DisplayMember, list, isAddEmptyItem);

        }

        /// <summary>
        /// コンボボックスにアイテムをセット
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="cmb">コンボボックス</param>
        /// <param name="valueMember">値の名前</param>
        /// <param name="displayMember">テキストん名前</param>
        /// <param name="list">データ</param>
        /// <param name="isAddEmptyItem">空白行追加可否</param>
        public static void SetComboBoxItem<T>(ComboBox cmb, string valueMember, string displayMember, IEnumerable<T> list, bool isAddEmptyItem = true)
            where T : class, new()
        {
            var dataSource = (list ?? Enumerable.Empty<T>()).ToList();

            //バインドの設定
            cmb.ValueMember = valueMember;
            cmb.DisplayMember = displayMember;

            //空のアイテムを追加
            if (isAddEmptyItem == true)
            {
                dataSource.Insert(0, new T());

            }

            //バインド
            cmb.DataSource = dataSource;

            //幅の設定
            SetDropDownWidth(cmb);

        }

        /// <summary>
        /// コンボボックスにアイテムをセット
        /// </summary>
        /// <typeparam name="T">データの型</typeparam>
        /// <param name="cmb">コンボボックス</param>
        /// <param name="valueMember">値の名前</param>
        /// <param name="displayMember">テキストん名前</param>
        /// <param name="list">データ</param>
        /// <param name="bind">バインドソース</param>
        /// <param name="isAddEmptyItem">空白行追加可否</param>
        public static void SetComboBoxItem<T>(ComboBox cmb, string valueMember, string displayMember, IEnumerable<T> list, BindingSource bind, bool isAddEmptyItem = true)
            where T : class, new()
        {
            // バインドソース
            cmb.DataSource = bind;

            var dataSource = (list ?? Enumerable.Empty<T>()).ToList();

            //空のアイテムを追加
            if (isAddEmptyItem == true)
            {
                dataSource.Insert(0, new T());

            }

            //バインド
            bind.DataSource = dataSource;

            //バインドの設定
            cmb.ValueMember = valueMember;
            cmb.DisplayMember = displayMember;

            //幅の設定
            SetDropDownWidth(cmb);

        }

        /// <summary>
        /// コンボボックスにアイテムをセット
        /// </summary>
        /// <typeparam name="T1">キー</typeparam>
        /// <typeparam name="T2">値</typeparam>
        /// <param name="cmb">コンボボックス</param>
        /// <param name="map">データ</param>
        /// <param name="isAddEmptyItem">空白行追加可否</param>
        public static void SetComboBoxItem<T1, T2>(ComboBox cmb, IDictionary<T1, T2> map, bool isAddEmptyItem = true)
        {
            var dataSource = map.ToList();

            //バインドの設定
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";

            //空のアイテムを追加
            if (isAddEmptyItem == true)
            {
                dataSource.Insert(0, new KeyValuePair<T1, T2>());

            }

            //バインド
            cmb.DataSource = dataSource;

            //幅の設定
            SetDropDownWidth(cmb);

        }
        #endregion

        #region コンボボックスの背景色を設定
        /// <summary>
        /// コンボボックスの背景色を設定
        /// </summary>
        /// <param name="cmb">コンボボックス</param>
        /// <param name="color">色</param>
        public static void SetComboBoxBackColor(ComboBox cmb, Color color)
        {
            //背景色を変更するかどうか
            if (cmb.BackColor != color)
            {
                //背景色とスタイルを設定
                cmb.BackColor = color;
                

            }

            //ドロップダウンリストスタイルかどうか
            if (cmb.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                cmb.FlatStyle = color == Const.DefaultBackColor ? FlatStyle.Standard : FlatStyle.Flat;

            }

        }
        #endregion

        #region コンボボックスのデーターソースをクリア
        /// <summary>
        /// コンボボックスのデーターソースをクリア
        /// </summary>
        /// <param name="cmb">コンボボックス</param>
        public static void ClearComboBoxDataSource(ComboBox cmb)
        {
            var displayMember = cmb.DisplayMember;
            cmb.DataSource = null;
            cmb.DisplayMember = displayMember;

        }
        #endregion

        #region ラジオボタンに値をセット
        /// <summary>
        /// ラジオボタンに値をセット
        /// </summary>
        /// <typeparam name="T">値の型</typeparam>
        /// <param name="parent">親コントロール</param>
        /// <param name="value">値</param>
        public static void SetRadioButtonValue<T>(Control parent, T value)
        {
            var v = value == null ? "" : value.ToString();

            foreach (Control c in parent.Controls)
            {
                //ラジオボタンかどうか
                if (c is RadioButton)
                {
                    //ラジオボタンのタグと値が同じかどうか
                    var radio = c as RadioButton;
                    if (radio.Tag.ToString() == v)
                    {
                        radio.Checked = true;
                        break;

                    }

                }

            }

        }
        #endregion

        #region ラジオボタンから値を取得
        /// <summary>
        /// ラジオボタンに値をセット
        /// </summary>
        /// <typeparam name="T">値の型</typeparam>
        /// <param name="parent">親コントロール</param>
        /// <param name="value">値</param>
        public static string GetRadioButtonValue(Control parent)
        {
            var v = "";

            foreach (Control c in parent.Controls)
            {
                //ラジオボタンかどうか
                if (c is RadioButton)
                {
                    //ラジオボタンを選択しているかどうか
                    var radio = c as RadioButton;
                    if (radio.Checked == true)
                    {
                        v = radio.Tag != null ? radio.Tag.ToString() : "";
                        break;

                    }

                }

            }

            return v;

        }
        #endregion

        #region 処理完了までカーソルを待機
        /// <summary>
        /// 処理完了までカーソルを待機
        /// </summary>
        /// <param name="form">画面</param>
        /// <param name="action">処理</param>
        public static void FormWait(Form form, Action action)
        {
            using (var util = new FormUtil(form))
            {
                util.CursorWait();

                action?.Invoke();
            }
        }
        #endregion

        #region コントロールにマスクを設定・解除する
        /// <summary>
        /// 子コントロールにマスクを設定・解除する
        /// </summary>
        /// <param name="ctl">Control</param>
        /// <param name="flg">Boolean(true:設定, false:解除)</param>
        /// <param name="list">除外リスト</param>
        /// <returns></returns>
        public static void SetMaskingControls(Control ctl, Boolean flg, List<Control> list = null)
        {
            var ctllist = new List<Control>();

            FormControlUtil.GetInputControls(ctl, ctllist, true);

            foreach (var c in ctllist)
            {
                if (list != null && list.Exists(no => no.Name == c.Name)) continue;

                SetMaskingControl(c, flg);
            }
        }
        /// <summary>
        /// コントロールにマスクを設定・解除する
        /// </summary>
        /// <param name="ctl">Control</param>
        /// <param name="flg">Boolean(true:設定, false:解除)</param>
        /// <returns></returns>
        public static void SetMaskingControl(Control ctl, Boolean flg)
        {
            if (ctl is TextBox)
            {
                ((TextBox)ctl).Enabled = flg;
            }
            else if (ctl is MaskedTextBox)
            {
                ((MaskedTextBox)ctl).Enabled = flg;
            }
            else if (ctl is ComboBox)
            {
                ((ComboBox)ctl).Enabled = flg;
            }
            else if (ctl is RadioButton)
            {
                ((RadioButton)ctl).Enabled = flg;
            }
            else if (ctl is CheckBox)
            {
                ((CheckBox)ctl).Enabled = flg;
            }
            else if (ctl is DateTimePicker)
            {
                ((DateTimePicker)ctl).Enabled = flg;
            }
            else if (ctl is Button)
            {
                ((Button)ctl).Enabled = flg;
            }
        }
        #endregion

        #region TabStopプロパティ初期設定
        /// <summary>
        /// TabStopプロパティ初期設定
        /// </summary>
        /// <param name="ctl"></param>
        public static void InitTabStopProperty(Control ctl)
        {
            foreach (Control ctl2 in ctl.Controls)
            {
                if (ctl2.Controls.Count > 0)
                {
                    InitTabStopProperty(ctl2);
                }

                if (ctl2 is TextBox)
                {
                    if (((TextBox)ctl2).ReadOnly)
                    {
                        ((TextBox)ctl2).TabStop = false;
                    }
                }
                else if (ctl2 is MaskedTextBox)
                {
                    if (((MaskedTextBox)ctl2).ReadOnly)
                    {
                        ((MaskedTextBox)ctl2).TabStop = false;
                    }
                }
                else if (ctl2 is Panel)
                {
                    ctl2.TabStop = false;
                }
            }
        }
        #endregion

        #region Labelプロパティ初期設定
        /// <summary>
        /// Labelプロパティ初期設定
        /// </summary>
        /// <param name="form">画面</param>
        public static void InitLabel(Form form)
        {
            Action<Control> set = null;
            set = parent =>
            {
                var map = parent.Controls;

                //Labelのキー使用(UseMnemonic)を全てfalseとする
                if (parent is Label)
                {
                    var label = parent as Label;
                    label.UseMnemonic = false;

                }

                if (map.Count != 0)
                {
                    foreach (Control children in map)
                    {
                        set(children);

                    }

                }

            };

            set(form);

        }
        #endregion
    }
}
