using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;

using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System.Configuration;

namespace DevPlan.UICommon
{
    public class Validator
    {
        #region 画面の入力チェック
        /// <summary>
        /// 画面の入力チェック
        /// </summary>
        /// <param name="form">画面</param>
        /// <param name="checkMap">独自の検証を行うコントロールの連想配列</param>
        /// <param name="isInVisible">true:非表示を含む</param>
        /// <param name="isReadOnly">true:読み取り専用を含む</param>
        /// <returns></returns>
        public static string GetFormInputErrorMessage(Form form, IDictionary<Control, Func<Control, string, string>> checkMap = null, bool isInVisible = true, bool isReadOnly = true)
        {
            var controlList = new List<Control>();

            var errTargetList = new List<Control>();

            var msgList = new List<string>();

            Action<Control> setList = null;
            setList = parent =>
            {
                var map = parent.Controls;

                controlList.Add(parent);

                if (map.Count != 0)
                {
                    foreach (Control children in map)
                    {

                        if (!isInVisible)
                        {
                            if (!children.Visible) continue;
                        }

                        if (!isReadOnly)
                        {
                            if (children is TextBox)
                            {
                                if ((children as TextBox).ReadOnly == true) continue;
                            }
                        }

                        setList(children);

                    }

                }

            };

            Action<Control, Color> setColor = null;
            setColor = (parent, color) =>
            {
                var map = parent.Controls;

                if (map.Count != 0)
                {
                    foreach (Control children in map)
                    {
                        setColor(children, color);

                        children.BackColor = color;

                    }

                }

            };

            Func<string, string, bool> isRequired = (name, value) =>
            {
                var flg = !string.IsNullOrWhiteSpace(value);

                //入力しているかどうか
                if (flg == false)
                {
                    msgList.Add(string.Format(Resources.KKM00001, name));

                }

                return flg;

            };

            Func<string, string, int, bool> isWide = (name, value, length) =>
            {
                var flg = value.Length <= length;

                //指定文字数以内かどうか
                if (flg == false)
                {
                    msgList.Add(string.Format(Resources.KKM00027, name));

                }

                return flg;

            };

            Func<string, string, string, bool> isRegex = (name, value, pattern) =>
            {
                var flg = Regex.IsMatch(value, pattern);

                //値がなければOK扱い
                if (string.IsNullOrWhiteSpace(value) == true)
                {
                    flg = true;

                }

                //書式がOKかどうか
                if (flg == false)
                {
                    msgList.Add(string.Format(Resources.KKM00032, name));

                }

                return flg;

            };

            Func<string, string, int, bool> isByte = (name, value, length) =>
            {
                var flg = StringUtil.SjisByteLength(value) <= length;

                //指定文字数以内かどうか
                if (flg == false)
                {
                    msgList.Add(string.Format(Resources.KKM00027, name));

                }

                return flg;

            };

            Func<string, string> getTagValue = s => Regex.Replace(s, @"^.+?\((.+)\)$", "$1");

            Func<object, string[]> split = o =>
            {
                var value = o == null ? "" : o.ToString();

                var list = new List<string>();

                var tag = "";

                foreach (var s in value.Split(';'))
                {
                    tag += s;

                    if (Regex.IsMatch(s, @"([a-z]|\))$") == true)
                    {
                        list.Add(tag);

                        tag = "";

                    }
                    else
                    {
                        tag += ";";

                    }

                }

                return list.ToArray();

            };

            //画面内のコントロールを取得
            setList(form);

            //背景色を初期化
            var targetList = controlList.Where(c => c.Enabled == true && string.IsNullOrWhiteSpace((c.Tag ?? "").ToString()) == false).OrderBy(c => c.TabIndex).ToArray();
            foreach (var c in targetList)
            {
                //データグリッドビューの場合は何もしない
                if (c is DataGridView || c is GcMultiRow)
                {
                    continue;

                }

                //コンテナコントロールかどうか
                if (c is FlowLayoutPanel || c is GroupBox || c is Panel)
                {
                    //子コントロールの背景色を変更
                    setColor(c, c.BackColor);

                }
                //コンボボックスかどうか
                else if (c is ComboBox)
                {
                    //コンボボックスの背景色を設定
                    FormControlUtil.SetComboBoxBackColor((c as ComboBox), Const.DefaultBackColor);

                }
                //チェックボックスかラジオボタンかどうか
                else if (c is CheckBox || c is RadioButton)
                {
                    //親コントロールの背景色を設定
                    c.BackColor = c.Parent.BackColor;

                }
                //ボタンかどうか
                else if (c is Button)
                {
                    //コントロールの背景色
                    c.BackColor = Const.ControlBackColor;

                }
                else
                {
                    //コントロールの背景色
                    c.BackColor = Const.DefaultBackColor;

                }

            }

            //有効なタグ付きのコントロールを検証
            foreach (var c in targetList)
            {
                var tagList = split(c.Tag);

                var itemName = tagList.FirstOrDefault(x => x.Contains(Const.ItemName) == true);
                var name = itemName == null ? "" : getTagValue(itemName);

                var required = tagList.FirstOrDefault(x => x.Contains(Const.Required) == true);
                var wide = tagList.FirstOrDefault(x => x.Contains(Const.Wide) == true);
                var narrow = tagList.FirstOrDefault(x => x.Contains(Const.Narrow) == true);
                var number = tagList.FirstOrDefault(x => x.Contains(Const.Number) == true);
                var date = tagList.FirstOrDefault(x => x.Contains(Const.Date) == true);
                var month = tagList.FirstOrDefault(x => x.Contains(Const.Month) == true);
                var regex = tagList.FirstOrDefault(x => x.Contains(Const.Regex) == true);
                var sjisByte = tagList.FirstOrDefault(x => x.Contains(Const.Byte) == true);

                var errList = new List<bool>();

                //テキストボックス
                if (c is TextBox)
                {
                    var txt = c as TextBox;
                    var value = txt.Text;

                    //必須かどうか
                    if (required != null)
                    {
                        //必須チェック
                        errList.Add(isRequired(name, value));

                    }

                    //全角チェックをするかどうか
                    if (wide != null)
                    {
                        errList.Add(isWide(name, value, int.Parse(getTagValue(wide))));

                    }

                    //正規表現チェックをするかどうか
                    if (regex != null)
                    {
                        errList.Add(isRegex(name, value, getTagValue(regex)));

                    }

                    //バイト数チェックをするかどうか
                    if (sjisByte != null)
                    {
                        errList.Add(isByte(name, value, int.Parse(getTagValue(sjisByte))));

                    }

                }
                //コンボボックス
                else if (c is ComboBox)
                {
                    var cmb = c as ComboBox;

                    var isDropDownList = cmb.DropDownStyle == ComboBoxStyle.DropDownList;

                    //必須かどうか
                    if (required != null)
                    {
                        var value = cmb.SelectedValue == null ? "" : cmb.SelectedValue.ToString();

                        //ドロップダウンリストかどうか
                        if (isDropDownList == true)
                        {
                            //データソースがなければテキストを取得
                            if (cmb.DataSource == null)
                            {
                                value = cmb.Text;

                            }

                        }
                        else
                        {
                            value = cmb.Text;

                        }

                        //必須チェック
                        errList.Add(isRequired(name, value));

                    }

                    //ドロップダウンリスト以外で全角チェックをするかどうか
                    if (isDropDownList == false && wide != null)
                    {
                        errList.Add(isWide(name, cmb.Text, int.Parse(getTagValue(wide))));

                    }

                    //バイト数チェックをするかどうか
                    if (sjisByte != null)
                    {
                        errList.Add(isByte(name, cmb.Text, int.Parse(getTagValue(sjisByte))));

                    }

                }
                //日付選択
                else if (c is DateTimePicker)
                {
                    var dtp = c as DateTimePicker;

                    //必須かどうか
                    if (required != null)
                    {
                        //必須チェック
                        errList.Add(isRequired(name, dtp.Text));

                    }

                }
                //ラベル
                else if(c is Label)
                {
                    var lbl = c as Label;
                    var value = lbl.Text;

                    //必須かどうか
                    if (required != null)
                    {
                        //必須チェック
                        errList.Add(isRequired(name, value));

                    }

                    //全角チェックをするかどうか
                    if (wide != null)
                    {
                        errList.Add(isWide(name, value, int.Parse(getTagValue(wide))));

                    }

                    //正規表現チェックをするかどうか
                    if (regex != null)
                    {
                        errList.Add(isRegex(name, value, getTagValue(regex)));

                    }

                    //バイト数チェックをするかどうか
                    if (sjisByte != null)
                    {
                        errList.Add(isByte(name, value, int.Parse(getTagValue(sjisByte))));

                    }

                }
                //ラジオボタンはチェックしない
                else if (c is RadioButton)
                {
                    continue;


                }
                //データグリッドビューかどうか
                else if (c is DataGridView)
                {
                    var grid = c as DataGridView;

                    //読取専用ならチェックしない
                    if (grid.ReadOnly == true)
                    {
                        continue;
                    }

                    var colList = new List<DataGridViewColumn>();

                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        //タグがある編集可能な列のみチェック
                        if (col.Tag != null && col.ReadOnly == false)
                        {
                            colList.Add(col);

                        }

                    }

                    //チェック対象の列があるかどうか
                    if (colList.Any() == true)
                    {
                        var errCellMap = new Dictionary<string, List<DataGridViewCell>>();

                        foreach (var col in colList)
                        {
                            //列からタグ情報を再取得
                            tagList = split(col.Tag);

                            itemName = tagList.FirstOrDefault(x => x.Contains(Const.ItemName) == true);
                            name = itemName == null ? "" : getTagValue(itemName);

                            required = tagList.FirstOrDefault(x => x.Contains(Const.Required) == true);
                            wide = tagList.FirstOrDefault(x => x.Contains(Const.Wide) == true);
                            narrow = tagList.FirstOrDefault(x => x.Contains(Const.Narrow) == true);
                            number = tagList.FirstOrDefault(x => x.Contains(Const.Number) == true);
                            date = tagList.FirstOrDefault(x => x.Contains(Const.Date) == true);
                            month = tagList.FirstOrDefault(x => x.Contains(Const.Month) == true);
                            regex = tagList.FirstOrDefault(x => x.Contains(Const.Regex) == true);
                            sjisByte = tagList.FirstOrDefault(x => x.Contains(Const.Byte) == true);

                            foreach (DataGridViewRow row in grid.Rows)
                            {
                                var cell = row.Cells[col.Name];

                                //読取専用ならチェック無し
                                if (row.ReadOnly == true || cell.ReadOnly == true)
                                {
                                    continue;

                                }

                                var value = cell.Value;

                                var s = value == null ? "" : value.ToString();

                                //背景色をデフォルトに設定
                                cell.Style.BackColor = Const.DefaultBackColor;

                                //必須がOKかどうか
                                if (required != null && string.IsNullOrWhiteSpace(s) == true)
                                {
                                    var msg = string.Format(Resources.KKM00001, name);

                                    //未追加なら追加
                                    if (errCellMap.ContainsKey(msg) == false)
                                    {
                                        errCellMap[msg] = new List<DataGridViewCell>();

                                    }

                                    //エラーのセルを追加
                                    errCellMap[msg].Add(cell);

                                }

                                //全角チェックがOKかどうか
                                if (wide != null && s.Length > int.Parse(getTagValue(wide)))
                                {
                                    var msg = string.Format(Resources.KKM00027, name);

                                    //未追加なら追加
                                    if (errCellMap.ContainsKey(msg) == false)
                                    {
                                        errCellMap[msg] = new List<DataGridViewCell>();

                                    }

                                    //エラーのセルを追加
                                    errCellMap[msg].Add(cell);

                                }

                                //バイト数チェックがOKかどうか
                                if (sjisByte != null && StringUtil.SjisByteLength(s) > int.Parse(getTagValue(sjisByte)))
                                {
                                    var msg = string.Format(Resources.KKM00027, name);

                                    //未追加なら追加
                                    if (errCellMap.ContainsKey(msg) == false)
                                    {
                                        errCellMap[msg] = new List<DataGridViewCell>();

                                    }

                                    //エラーのセルを追加
                                    errCellMap[msg].Add(cell);

                                }

                                //正規表現チェックがOKかどうか
                                if (regex != null && string.IsNullOrWhiteSpace(s) == false && Regex.IsMatch(s, getTagValue(getTagValue(regex))) == false)
                                {
                                    var msg = string.Format(Resources.KKM00032, name);

                                    //未追加なら追加
                                    if (errCellMap.ContainsKey(msg) == false)
                                    {
                                        errCellMap[msg] = new List<DataGridViewCell>();

                                    }

                                    //エラーのセルを追加
                                    errCellMap[msg].Add(cell);

                                }

                            }

                        }

                        foreach (var kv in errCellMap)
                        {
                            var msg = kv.Key;
                            var list = kv.Value;

                            //エラーメッセージ追加
                            msgList.Add(msg);

                            //エラー箇所の背景色変更
                            list.ForEach(x => x.Style.BackColor = Const.ErrorBackColor);

                        }

                    }

                }
                else if (c is GcMultiRow)
                {
                    // DataGridViewのMultiRow版（中身は同様です。）。一通りのグリッドを差し替え終わったら↑は削除

                    var grid = c as GcMultiRow;

                    //読取専用の場合チェックをしない
                    if (grid.ReadOnly == true)
                    {
                        continue;
                    }

                    if (grid.Rows.Count > 0)
                    {
                        var colList = new List<Cell>();

                        foreach (var col in grid.Rows[0].Cells)
                        {
                            //タグがある列のみチェック。ColのTagへの値の突っ込み方がわからないので１行目のみチェック。
                            if (col.Tag != null)
                            {
                                colList.Add(col);

                            }

                        }

                        //チェック対象の列があるかどうか
                        if (colList.Any() == true)
                        {
                            var errCellMap = new Dictionary<string, List<Cell>>();

                            foreach (var col in colList)
                            {
                                //列からタグ情報を再取得
                                tagList = split(col.Tag);

                                itemName = tagList.FirstOrDefault(x => x.Contains(Const.ItemName) == true);
                                name = itemName == null ? "" : getTagValue(itemName);

                                required = tagList.FirstOrDefault(x => x.Contains(Const.Required) == true);
                                wide = tagList.FirstOrDefault(x => x.Contains(Const.Wide) == true);
                                narrow = tagList.FirstOrDefault(x => x.Contains(Const.Narrow) == true);
                                number = tagList.FirstOrDefault(x => x.Contains(Const.Number) == true);
                                date = tagList.FirstOrDefault(x => x.Contains(Const.Date) == true);
                                month = tagList.FirstOrDefault(x => x.Contains(Const.Month) == true);
                                regex = tagList.FirstOrDefault(x => x.Contains(Const.Regex) == true);
                                sjisByte = tagList.FirstOrDefault(x => x.Contains(Const.Byte) == true);

                                foreach (Row row in grid.Rows)
                                {
                                    var cell = row.Cells[col.Name];

                                    //読取専用ならチェック無し
                                    if (row.ReadOnly == true || cell.ReadOnly == true)
                                    {
                                        continue;

                                    }

                                    var value = cell.Value;

                                    var s = value == null ? "" : value.ToString();

                                    //背景色をデフォルトに設定
                                    cell.Style.BackColor = (Color)new ColorConverter().ConvertFromString(ConfigurationManager.AppSettings["DATA_ROW_BACKCOLOR"]);

                                    //必須がOKかどうか
                                    if (required != null && string.IsNullOrWhiteSpace(s) == true)
                                    {
                                        var msg = string.Format(Resources.KKM00001, name);

                                        //未追加なら追加
                                        if (errCellMap.ContainsKey(msg) == false)
                                        {
                                            errCellMap[msg] = new List<Cell>();

                                        }

                                        //エラーのセルを追加
                                        errCellMap[msg].Add(cell);

                                    }

                                    //全角チェックがOKかどうか
                                    if (wide != null && s.Length > int.Parse(getTagValue(wide)))
                                    {
                                        var msg = string.Format(Resources.KKM00027, name);

                                        //未追加なら追加
                                        if (errCellMap.ContainsKey(msg) == false)
                                        {
                                            errCellMap[msg] = new List<Cell>();

                                        }

                                        //エラーのセルを追加
                                        errCellMap[msg].Add(cell);

                                    }

                                    //バイト数チェックがOKかどうか
                                    if (sjisByte != null && StringUtil.SjisByteLength(s) > int.Parse(getTagValue(sjisByte)))
                                    {
                                        var msg = string.Format(Resources.KKM00027, name);

                                        //未追加なら追加
                                        if (errCellMap.ContainsKey(msg) == false)
                                        {
                                            errCellMap[msg] = new List<Cell>();

                                        }

                                        //エラーのセルを追加
                                        errCellMap[msg].Add(cell);

                                    }

                                    //正規表現チェックがOKかどうか
                                    if (regex != null && string.IsNullOrWhiteSpace(s) == false && Regex.IsMatch(s, getTagValue(getTagValue(regex))) == false)
                                    {
                                        var msg = string.Format(Resources.KKM00032, name);

                                        //未追加なら追加
                                        if (errCellMap.ContainsKey(msg) == false)
                                        {
                                            errCellMap[msg] = new List<Cell>();

                                        }

                                        //エラーのセルを追加
                                        errCellMap[msg].Add(cell);

                                    }

                                }

                            }

                            foreach (var kv in errCellMap)
                            {
                                var msg = kv.Key;
                                var list = kv.Value;

                                //エラーメッセージ追加
                                msgList.Add(msg);

                                //エラー箇所の背景色変更
                                list.ForEach(x => x.Style.BackColor = Const.ErrorBackColor);

                            }

                        }
                    }
                }

                //独自の検証を行うコントロールかどうか
                if (checkMap != null && checkMap.Keys.Contains(c) == true)
                {
                    //エラー結果の文言があるかどうか
                    var msg = checkMap[c](c, name).Trim();
                    if (string.IsNullOrWhiteSpace(msg) == false)
                    {
                        msgList.Add(msg);
                        errList.Add(false);

                    }

                }

                var color = Const.ErrorBackColor;

                //エラーがあるかどうか
                if (errList.Contains(false) == true)
                {
                    errTargetList.Add(c);

                }
                else
                {
                    continue;

                }

                //データグリッドビューの場合は何もしない
                if (c is DataGridView)
                {

                }
                else
                {
                    //コンテナコントロールかどうか
                    if (c is FlowLayoutPanel || c is GroupBox || c is Panel)
                    {
                        //子コントロールの背景色を変更
                        setColor(c, color);

                    }
                    //コンボボックスかどうか
                    else if (c is ComboBox)
                    {
                        //コンボボックスの背景色を設定
                        FormControlUtil.SetComboBoxBackColor((c as ComboBox), color);

                    }
                    else
                    {
                        //コントロールの背景色
                        c.BackColor = color;

                    }

                }

            }

            //エラーがある場合は最初のコントロールにフォーカスをセット
            errTargetList.FirstOrDefault()?.Focus();

            return string.Join(Const.CrLf, msgList.ToArray());

        }
        #endregion

        /// <summary>
        /// 未入力チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="ctl">コントロール</param>
        /// <returns></returns>
        public static bool ValidInput(Form form, string itemname, Control ctl)
        {
            bool flg = true;
            if (ctl is TextBox)
            {
                TextBox textbox = (TextBox)ctl;
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    flg = false;
                }
            }
            else if (ctl is MaskedTextBox)
            {
                MaskedTextBox maskedTextbox = (MaskedTextBox)ctl;
                string text = FormControlUtil.GetMaskedTextBoxPlainValue(maskedTextbox);
                if (string.IsNullOrEmpty(text))
                {
                    flg = false;
                }
            }
            if (!flg)
            {
                Messenger.Error(string.Format(Resources.KKM00001, itemname), null);
                ctl.Focus();
            }

            return flg;
        }

        /// <summary>
        /// 未入力チェック（グリッド）
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        public static bool ValidInput(Form form, string itemname, DataGridViewCell cell)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (string.IsNullOrEmpty(cellText))
            {
                DataGridView dg = cell.OwningColumn.DataGridView;
                Messenger.Error(string.Format(Resources.KKM00001, itemname), null);
                dg.CurrentCell = cell;
                dg.BeginEdit(true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 文字列の長さ（文字数）チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">テキストボックス</param>
        /// <param name="length">最大長</param>
        /// <returns></returns>
        public static bool ValidLength(Form form, string itemname, TextBox textbox, int length)
        {
            if (textbox.Text.Length > length)
            {
                Messenger.Error(string.Format(Resources.KKM00027, itemname, length.ToString()), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// グリッドセルの文字列の長さ（文字数）チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <param name="length">最大長</param>
        /// <returns></returns>
        public static bool ValidLength(Form form, string itemname, DataGridViewCell cell, int length)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (cellText.Length > length)
            {
                Messenger.Error(string.Format(Resources.KKM00027, itemname, length.ToString()), null);
                DataGridView dg = cell.OwningColumn.DataGridView;
                dg.CurrentCell = cell;
                dg.BeginEdit(true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 文字列の長さ（バイト数）チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">テキストボックス</param>
        /// <param name="length">最大長</param>
        /// <returns></returns>
        public static bool ValidLengthB(Form form, string itemname, TextBox textbox, int length)
        {
            if (StringUtil.SjisByteLength(textbox.Text) > length)
            {
                Messenger.Error(string.Format(Resources.KKM00027, itemname, length.ToString(), Math.Floor((decimal)(length / 2))), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">TextBox</param>
        /// <returns></returns>
        public static bool ValidDate(Form form, string itemname, TextBox textbox)
        {
            if (!ValidDate(textbox.Text))
            {
                Messenger.Error(string.Format(Resources.KKM00026, itemname), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">MaskedTextBox</param>
        /// <returns></returns>
        public static bool ValidDate(Form form, string itemname, MaskedTextBox maskedTextbox)
        {
            string text = string.Empty;
            text = FormControlUtil.GetMaskedTextBoxValue(maskedTextbox);
            if (!ValidDate(text))
            {
                Messenger.Error(string.Format(Resources.KKM00026, itemname), null);
                maskedTextbox.Focus();
                maskedTextbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 日付チェック（グリッド）
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        public static bool ValidDate(Form form, string itemname, DataGridViewCell cell)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (!ValidDate(cellText))
            {
                Messenger.Error(string.Format(Resources.KKM00026, itemname), null);
                DataGridView dg = cell.OwningColumn.DataGridView;
                dg.CurrentCell = cell;
                dg.BeginEdit(true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="text">値</param>
        /// <returns></returns>
        public static bool ValidDate(string text)
        {
            DateTime ret;
            if (!DateTime.TryParse(text, out ret))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 年月チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">MaskedTextBox</param>
        /// <returns></returns>
        public static bool ValidDateYearMonth(Form form, string itemname, MaskedTextBox maskedTextbox)
        {
            string maskedValue = FormControlUtil.GetMaskedTextBoxValue(maskedTextbox);
            if (!ValidDateYearMonth(maskedValue))
            {
                Messenger.Error(string.Format(Resources.KKM00026, itemname), null);
                maskedTextbox.Focus();
                maskedTextbox.SelectAll();
                return false;
            }
            DateTime d = DateTime.Parse(maskedValue + "/01");
            maskedTextbox.Text = d.ToString("yyyy/MM");
            return true;
        }

        /// <summary>
        /// 年月チェック（グリッド）
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        public static bool ValidDateYearMonth(Form form, string itemname, DataGridViewCell cell)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (!ValidDateYearMonth(cellText))
            {
                Messenger.Error(string.Format(Resources.KKM00026, itemname), null);
                DataGridView dg = cell.OwningColumn.DataGridView;
                dg.CurrentCell = cell;
                dg.BeginEdit(true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 年月チェック
        /// </summary>
        /// <param name="strYearMonth">年月</param>
        /// <returns></returns>
        public static bool ValidDateYearMonth(string strYearMonth)
        {
            strYearMonth = strYearMonth + "/01";
            if (!ValidDate(strYearMonth))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 整数チェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">テキストボックス</param>
        /// <returns></returns>
        public static bool ValidLong(Form form, string itemname, TextBox textbox)
        {
            if (!isLongValue(textbox.Text))
            {
                Messenger.Error(string.Format(Resources.KKM00025, itemname), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 整数チェック（グリッド）
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        public static bool ValidLong(Form form, string itemname, DataGridViewCell cell)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (!isLongValue(cellText))
            {
                Messenger.Error(string.Format(Resources.KKM00025, itemname), null);
                DataGridView dg = cell.OwningColumn.DataGridView;
                dg.CurrentCell = cell;
                dg.BeginEdit(true);

                return false;
            }
            return true;
        }

        /// <summary>
        /// 整数(long)かどうか判定
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool isLongValue(string strValue)
        {
            long ret;
            string text = strValue.Replace(",", "");
            if (!long.TryParse(text, out ret))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Decimalチェック
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="textbox">テキストボックス</param>
        /// <returns></returns>
        public static bool ValidDecimal(Form form, string itemname, TextBox textbox)
        {
            if (!isDecimal(textbox.Text))
            {
                Messenger.Error(string.Format(Resources.KKM00025, itemname), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Decimalチェック（グリッド）
        /// </summary>
        /// <param name="form">フォーム</param>
        /// <param name="itemname">項目名</param>
        /// <param name="cell">セル</param>
        /// <returns></returns>
        public static bool ValidDecimal(Form form, string itemname, DataGridViewCell cell)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();
            if (!isDecimal(cellText))
            {
                Messenger.Error(string.Format(Resources.KKM00025, itemname), null);
                DataGridView dg = cell.OwningColumn.DataGridView;
                dg.CurrentCell = cell;
                dg.BeginEdit(true);

                return false;
            }
            return true;
        }

        /// <summary>
        /// Decimal値の桁数チェック
        /// </summary>
        /// <param name="form"></param>
        /// <param name="itemname"></param>
        /// <param name="textbox"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ValidDecimalLength(Form form, string itemname, TextBox textbox, int length)
        {
            if (isDecimal(textbox.Text))
            {
                decimal value = Math.Floor(decimal.Parse(textbox.Text.Trim().Replace(",", "")));
                if (value.ToString().Length > length)
                {
                    Messenger.Error(string.Format(Resources.KKM00027, itemname, length.ToString()), null);
                    textbox.Focus();
                    textbox.SelectAll();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Decimal値の桁数チェック（グリッド）
        /// </summary>
        /// <param name="form"></param>
        /// <param name="itemname"></param>
        /// <param name="cell"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool ValidDecimalLength(Form form, string itemname, DataGridViewCell cell, int length)
        {
            string cellText = string.Empty;
            if (cell.Value != null) cellText = cell.Value.ToString();

            if (isDecimal(cellText))
            {
                decimal value = Math.Floor(decimal.Parse(cellText.Trim().Replace(",", "")));
                if (value.ToString().Length > length)
                {
                    Messenger.Error(string.Format(Resources.KKM00027, itemname, length.ToString()), null);
                    DataGridView dg = cell.OwningColumn.DataGridView;
                    dg.CurrentCell = cell;
                    dg.BeginEdit(true);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 数値(decimal)かどうか判定
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool isDecimal(string strValue)
        {
            decimal ret;
            string text = strValue.Replace(",", "");
            if (!decimal.TryParse(text, out ret))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// カタカナチェック
        /// </summary>
        /// <param name="form"></param>
        /// <param name="itemname"></param>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public static bool ValidKatakana(Form form, string itemname, TextBox textbox)
        {
            if (!StringUtil.IsKatakana(textbox.Text) &&
              !StringUtil.IsHanKatakana(textbox.Text))
            {
                Messenger.Error(string.Format(Resources.KKM00028, itemname), null);
                textbox.Focus();
                textbox.SelectAll();
                return false;
            }
            return true;
        }

        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <param name="form"></param>
        /// <param name="itemname"></param>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public static bool ValidExistFile(Form form, string itemname, TextBox textbox)
        {
            if (!string.IsNullOrEmpty(textbox.Text))
            {
                if (!System.IO.File.Exists(textbox.Text))
                {
                    Messenger.Error(string.Format(Resources.KKM00029, itemname), null);
                    return false;
                }
            }
            return true;
        }
    }
}
