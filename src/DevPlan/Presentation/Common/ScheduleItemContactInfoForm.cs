using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 車系・開発符号・項目連絡先編集
    /// </summary>
    public partial class ScheduleItemContactInfoForm<item> : BaseSubForm where item : class, new()
    {
        /// <summary>機能ID</summary>
        public FunctionID FunctionId { get; set; }

        /// <summary>連絡先区分</summary>
        public ContactInfoType InfoType { get; set; }

        /// <summary>表示対象の車系・開発符号</summary>
        public string Code { get; set; }

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "管理者編集"; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; private set; }
     
        /// <summary>連絡先項目</summary>
        public ScheduleItemModel<item> Item { get; internal set; }

        /// <summary>コンストラクタ</summary>
        public ScheduleItemContactInfoForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemContactInfoForm_Load(object sender, EventArgs e)
        {
            ResponseDto<CarManagerModel> res = null;
            var list = new List<CarManagerModel>();
            CarManagerModel reservedPerson = null;
            CarManagerModel reservedPersonSub = null;

            var cond = new CarManagerSearchModel { GENERAL_CODE = Code, FUNCTION_ID = (int)FunctionId };
            if (InfoType == ContactInfoType.All)
            {
                res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager, cond);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);
                }

                reservedPerson = list.Where(x => x.STATUS == "正" && x.CATEGORY_ID == null).FirstOrDefault();
                reservedPersonSub = list.Where(x => x.STATUS == "副" && x.CATEGORY_ID == null).FirstOrDefault();
            }
            else if (InfoType == ContactInfoType.Item && this.Item.ScheduleItemEdit == ScheduleItemEditType.Update)
            {
                cond.CATEGORY_ID = this.Item.ID.ToString();
                res = HttpUtil.GetResponse<CarManagerSearchModel, CarManagerModel>(ControllerType.CarManager, cond);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);
                }

                reservedPerson = list.Where(x => x.STATUS == "正").FirstOrDefault();
                reservedPersonSub = list.Where(x => x.STATUS == "副").FirstOrDefault();
            }

            if (list.Any())
            {
                if (reservedPerson != null)
                {
                    SetComboBox(ReservedPersonComboBox, reservedPerson);
                    this.RequesterTelTextBox.Text = reservedPerson.TEL;
                    this.RemarksTextBox.Text = reservedPerson.REMARKS;
                }
                if (reservedPersonSub != null)
                {
                    SetComboBox(SubReservedPersonComboBox, reservedPersonSub);
                    this.SubRequesterTelTextBox.Text = reservedPersonSub.TEL;
                    this.RemarksTextBox.Text = reservedPersonSub.REMARKS;
                }
            }
            else
            {
                this.DeleteButton.Visible = false;
            }

            this.UserAuthority = base.GetFunction(FunctionId);
            this.ActiveControl = ReservedPersonComboBox;
        }

        /// <summary>
        /// コンボボックス設定処理
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="model"></param>
        private void SetComboBox(ComboBox combobox, CarManagerModel model)
        {
            var list = new List<ComboBoxSetting>();
            list.Add(new ComboBoxSetting(model.PERSONEL_ID, model));

            combobox.DataSource = list;
            combobox.DisplayMember = "Value";
            combobox.ValueMember = "Key";
            combobox.SelectedIndex = 0;
        }

        /// <summary>
        /// （正）連絡先コンボボックス選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservedPersonComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                StatusCode = "a"
            })
            {
                var result = form.ShowDialog(this);
                
                //現在選択（維持）されているモデルの中身を更新して保持。
                if (result == DialogResult.OK)
                {
                    var setting = (ComboBoxSetting)ReservedPersonComboBox.SelectedItem;
                    setting = GetSetting(form, setting);
                    setting.Model.STATUS = "正";

                    SetComboBox(ReservedPersonComboBox, setting.Model);
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    this.RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                }
            }
        }

        /// <summary>
        /// （副）連絡先コンボボックス選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubReservedPersonComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                StatusCode = "a"
            })
            {
                var result = form.ShowDialog(this);

                //現在選択（維持）されているモデルの中身を更新して保持。
                if (result == DialogResult.OK)
                {
                    var setting = (ComboBoxSetting)SubReservedPersonComboBox.SelectedItem;
                    setting = GetSetting(form, setting);
                    setting.Model.STATUS = "副";

                    SetComboBox(SubReservedPersonComboBox, setting.Model);
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    this.SubRequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                }
            }
        }

        /// <summary>
        /// コンボボックス設定データ更新
        /// </summary>
        /// <param name="form"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        private ComboBoxSetting GetSetting(UserListForm form, ComboBoxSetting setting)
        {
            if (setting == null)
            {
                setting = new ComboBoxSetting(form.User.PERSONEL_ID, new CarManagerModel()
                {
                    GENERAL_CODE = Code,
                    NAME = form.User.NAME,
                    SECTION_CODE = form.User.SECTION_CODE,
                    PERSONEL_ID = form.User.PERSONEL_ID
                });
            }
            setting.Key = form.User.PERSONEL_ID;
            setting.Value = form.User.NAME;
            setting.Model.PERSONEL_ID = form.User.PERSONEL_ID;
            setting.Model.SECTION_CODE = form.User.SECTION_CODE;
            setting.Model.NAME = form.User.NAME;

            return setting;
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            map[this.ReservedPersonComboBox] = (c, name) =>
            {
                var errMsg = "";

                var info = (List<ComboBoxSetting>)ReservedPersonComboBox.DataSource;
                var subInfo = (List<ComboBoxSetting>)SubReservedPersonComboBox.DataSource;

                if (info == null && subInfo == null)
                {
                    errMsg = string.Format(Resources.KKM00001, "管理者");
                }
                
                return errMsg;
            };

            map[this.RequesterTelTextBox] = (c, name) =>
            {
                var errMsg = "";

                var info = (List<ComboBoxSetting>)ReservedPersonComboBox.DataSource;

                if (info != null && string.IsNullOrWhiteSpace(this.RequesterTelTextBox.Text))
                {
                    errMsg = string.Format(Resources.KKM00001, "(正)管理者電話番号");
                }

                return errMsg;
            };

            map[this.SubRequesterTelTextBox] = (c, name) =>
            {
                var errMsg = "";

                var info = (List<ComboBoxSetting>)SubReservedPersonComboBox.DataSource;

                if (info != null && string.IsNullOrWhiteSpace(this.SubRequesterTelTextBox.Text))
                {
                    errMsg = string.Format(Resources.KKM00001, "(副)管理者電話番号");
                }

                return errMsg;
            };

            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                var comboboxList = new List<ComboBoxSetting>();

                var info = (List<ComboBoxSetting>)ReservedPersonComboBox.DataSource;
                var subInfo = (List<ComboBoxSetting>)SubReservedPersonComboBox.DataSource;

                if (info != null)
                {
                    comboboxList.AddRange(info);
                }
                if (subInfo != null)
                {
                    comboboxList.AddRange(subInfo);
                }

                var insertList = new List<CarManagerModel>();
                var updateList = new List<CarManagerModel>();
                foreach (var item in comboboxList)
                {
                    if (item.Model.STATUS == "正")
                    {
                        item.Model.TEL = this.RequesterTelTextBox.Text;
                    }
                    if (item.Model.STATUS == "副")
                    {
                        item.Model.TEL = this.SubRequesterTelTextBox.Text;
                    }
                    item.Model.REMARKS = this.RemarksTextBox.Text;
                    item.Model.FUNCTION_ID = (int)FunctionId;
                    if (string.IsNullOrWhiteSpace(item.Model.ID))
                    {
                        insertList.Add(item.Model);
                    }
                    else
                    {
                        updateList.Add(item.Model);
                    }
                }

                if (this.InfoType == ContactInfoType.Item)
                {
                    long? categoryid = this.Item.ID;
                    var list = new List<CarManagerModel>();
                    list.AddRange(insertList);
                    list.AddRange(updateList);

                    if (FunctionId == FunctionID.CarShare)
                    {
                        #region カーシェア
                        var carItem = this.Item.ScheduleItem as CarShareScheduleItemModel;
                        carItem.CATEGORY = GetName(list);

                        if (this.Item.ScheduleItemEdit == ScheduleItemEditType.Insert)
                        {
                            var res = HttpUtil.PostResponse(ControllerType.CarShareScheduleItem, new[] { carItem });
                            categoryid = res.Results.OfType<CarShareScheduleItemModel>().FirstOrDefault().ID;
                        }
                        else
                        {
                            HttpUtil.PutResponse(ControllerType.CarShareScheduleItem, new[] { carItem });
                        }
                        #endregion
                    }
                    else if (FunctionId == FunctionID.TestCar)
                    {
                        #region 試験車
                        var testCarItem = this.Item.ScheduleItem as TestCarScheduleItemModel;
                        testCarItem.CATEGORY = GetName(list);

                        if (this.Item.ScheduleItemEdit == ScheduleItemEditType.Insert)
                        {
                            var res = HttpUtil.PostResponse(ControllerType.TestCarScheduleItem, new[] { testCarItem });
                            categoryid = res.Results.OfType<TestCarScheduleItemModel>().FirstOrDefault().ID;
                        }
                        else
                        {
                            HttpUtil.PutResponse(ControllerType.TestCarScheduleItem, new[] { testCarItem });
                        }
                        #endregion
                    }
                    else if (FunctionId == FunctionID.OuterCar)
                    {
                        #region 外製車
                        var outerCarItem = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;

                        if (this.Item.ScheduleItemEdit == ScheduleItemEditType.Insert)
                        {
                            var data = new OuterCarScheduleItemPostInModel()
                            {
                                SECTION_GROUP_ID = SessionDto.SectionGroupID,
                                PERSONEL_ID = SessionDto.UserId,
                                CATEGORY = GetName(list),
                                GENERAL_CODE = outerCarItem.GENERAL_CODE,
                                PARALLEL_INDEX_GROUP = outerCarItem.PARALLEL_INDEX_GROUP,
                                SORT_NO = outerCarItem.SORT_NO.Value
                            };
                            var res = HttpUtil.PostResponse<OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, data);
                            categoryid = res.Results.OfType<OuterCarScheduleItemGetOutModel>().FirstOrDefault().CATEGORY_ID;
                        }
                        else
                        {
                            var data = new OuterCarScheduleItemPutInModel()
                            {
                                CATEGORY_ID = outerCarItem.CATEGORY_ID,
                                PERSONEL_ID = SessionDto.UserId,
                                CATEGORY = GetName(list),
                                GENERAL_CODE = outerCarItem.GENERAL_CODE,
                                PARALLEL_INDEX_GROUP = outerCarItem.PARALLEL_INDEX_GROUP,
                                SORT_NO = outerCarItem.SORT_NO.Value
                            };
                            HttpUtil.PutResponse<OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, data);
                        }
                        #endregion
                    }

                    if (insertList.Any())
                    {
                        insertList.ForEach(x => x.CATEGORY_ID = categoryid.ToString());
                    }

                    if (updateList.Any())
                    {
                        updateList.ForEach(x => x.CATEGORY_ID = categoryid.ToString());
                    }
                }

                var resultList = new List<string>();
                if (insertList.Any())
                {
                    var status = HttpUtil.PostResponse(ControllerType.CarManager, insertList)?.Status;
                    resultList.Add(status);
                }

                if (updateList.Any())
                {
                    var status = HttpUtil.PutResponse(ControllerType.CarManager, updateList)?.Status;
                    resultList.Add(status);
                }

                if (resultList.Any(x => x != Const.StatusSuccess) == false)
                {
                    Messenger.Info(Resources.KKM00002);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            });
        }

        /// <summary>
        /// 連絡先名称生成
        /// </summary>
        /// <returns></returns>
        private string GetName(List<CarManagerModel> carManagerModelList)
        {
            string managerName = string.Empty;

            var managerList = carManagerModelList.OrderBy(x => x.STATUS).ToList();

            foreach (var item in managerList)
            {
                managerName += string.Format("(" + item.STATUS + "){0} {1}\n ({2})\n", item.SECTION_CODE, item.NAME, item.TEL);
            }

            if(string.IsNullOrWhiteSpace(managerName) == false)
            {
                managerName = "連絡先\n" + managerName;

                managerName += "\n";
            }
            
            managerName += this.RemarksTextBox.Text;

            return managerName;
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    var comboboxList = new List<ComboBoxSetting>();

                    var info = (List<ComboBoxSetting>)ReservedPersonComboBox.DataSource;
                    var subInfo = (List<ComboBoxSetting>)SubReservedPersonComboBox.DataSource;

                    if (info != null)
                    {
                        comboboxList.AddRange(info);
                    }
                    if (subInfo != null)
                    {
                        comboboxList.AddRange(subInfo);
                    }
                    
                    var deleteList = new List<CarManagerModel>();
                    foreach (var item in comboboxList)
                    {
                        deleteList.Add(item.Model);
                    }
                    var res = HttpUtil.DeleteResponse(ControllerType.CarManager, deleteList);
                    if (res != null && res.Status == Const.StatusSuccess)
                    {
                        if (this.InfoType == ContactInfoType.Item)
                        {
                            if (FunctionId == FunctionID.CarShare)
                            {
                                var carItem = this.Item.ScheduleItem as CarShareScheduleItemModel;
                                HttpUtil.DeleteResponse(ControllerType.CarShareScheduleItem, new[] { carItem });
                            }
                            else if (FunctionId == FunctionID.TestCar)
                            {
                                var testCarItem = this.Item.ScheduleItem as TestCarScheduleItemModel;
                                HttpUtil.DeleteResponse(ControllerType.TestCarScheduleItem, new[] { testCarItem });
                            }
                            else if (FunctionId == FunctionID.OuterCar)
                            {
                                var outerCarItem = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;
                                var data = new OuterCarScheduleItemDeleteInModel
                                {
                                    CATEGORY_ID = outerCarItem.CATEGORY_ID
                                };
                                HttpUtil.DeleteResponse<OuterCarScheduleItemDeleteInModel>(ControllerType.OuterCarScheduleItem, data);
                            }
                        }
                        Messenger.Info(Resources.KKM00003);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            });
        }
    }

    /// <summary>
    /// コンボボックス設定情報クラス
    /// </summary>
    public class ComboBoxSetting
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">コンボボックスキー値</param>
        /// <param name="value">コンボボックス表示値</param>
        public ComboBoxSetting(string key, CarManagerModel value)
        {
            this.Key = key;
            this.Model = value;
            this.Value = value.NAME;
        }

        private const string KeyString = "Key";
        private const string ValueString = "Value";

        /// <summary>
        /// コンボボックスキー値
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// コンボボックス表示値
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 連絡先オブジェクト
        /// </summary>
        public CarManagerModel Model { get; private set; }
    }

    /// <summary>
    /// 連絡先区分
    /// </summary>
    public enum ContactInfoType
    {
        /// <summary>すべての連絡先</summary>
        All,

        /// <summary>任意の各項目の連絡先</summary>
        Item
    }
}
