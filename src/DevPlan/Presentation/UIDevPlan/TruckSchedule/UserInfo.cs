using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// ユーザー情報管理クラス
    /// </summary>
    public class UserInfo
    {
        public static string UserName { get; internal set; }
        public static string UserTel { get; internal set; }

        internal static string GetUserSectionFullName(string sectionCode, string userName)
        {
            return sectionCode + " " + userName;
        }

        internal static void SetTruckManageUser()
        {
            var res2 = HttpUtil.GetResponse<TruckManagementUserModel>(ControllerType.TruckManagementUser);
            var list2 = new List<TruckManagementUserModel>();
            if (res2 != null && res2.Status == Const.StatusSuccess)
            {
                list2.AddRange(res2.Results);
            }

            UserName = list2[0].NAME;
            UserTel = list2[0].TEL;
        }

        /// <summary>
        /// スケジュール編集権限確認処理
        /// </summary>
        /// <param name="schedule">確認対象スケジュール</param>
        /// <param name="userAuthority">ログインユーザー権限オブジェクト</param>
        /// <returns></returns>
        internal static EditStatus CheckScheduleEdit(TruckScheduleModel schedule, UserAuthorityOutModel userAuthority)
        {
            var status = new EditStatus();

            string level = GetAccessFlag(SessionDto.AccessLevel);

            var isManagement = userAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = userAuthority.UPDATE_FLG == '1';

            if (isManagement)
            {
                status.IsEdit = true;
                status.IsDelete = true;
            }
            else if (isUpdate == false)
            {
                status.IsEdit = false;
                status.IsDelete = false;
            }
            else if (schedule.FLAG_仮予約 == 0 && schedule.FLAG_定期便 == 1)
            {
                status.IsEdit = false;
                status.IsDelete = false;
            }
            else
            {
                List<string> idList = new List<string>();

                if (string.IsNullOrEmpty(schedule.予約者_ID) == false)
                {
                    idList.Add(schedule.予約者_ID);
                }
                if (string.IsNullOrEmpty(schedule.運転者A_ID) == false)
                {
                    idList.Add(schedule.運転者A_ID);
                }
                if (string.IsNullOrEmpty(schedule.運転者B_ID) == false)
                {
                    idList.Add(schedule.運転者B_ID);
                }
                if (string.IsNullOrEmpty(schedule.定期便依頼者_ID) == false)
                {
                    idList.Add(schedule.定期便依頼者_ID);
                }

                var res = HttpUtil.GetResponse<UserSearchInModel, UserSearchOutModel>(ControllerType.User, new UserSearchInModel { PERSONEL_ID = idList.ToArray() });
                var userlist = res.Results?.ToList();

                if (schedule.FLAG_定期便 == 1)
                {
                    #region 定期便予約

                    //ログイン者＝　予約者 or 依頼者の時はOK
                    if (SessionDto.UserId == schedule.予約者_ID || SessionDto.UserId == schedule.定期便依頼者_ID)
                    {
                        status.IsEdit = true;
                        status.IsDelete = true;
                    }
                    else
                    {
                        if (userlist != null)
                        {
                            var userSearchOutModel = userlist.Where(x => x.PERSONEL_ID == schedule.予約者_ID).FirstOrDefault();
                            if (userSearchOutModel != null)
                            {
                                schedule.予約者_ACCESS_LEVEL = userSearchOutModel.ACCESS_LEVEL;
                                schedule.予約者_SECTION_ID = userSearchOutModel.SECTION_ID;
                                schedule.予約者_SECTION_GROUP_ID = userSearchOutModel.SECTION_GROUP_ID;
                            }

                            var userSearchOutModel1 = userlist.Where(x => x.PERSONEL_ID == schedule.定期便依頼者_ID).FirstOrDefault();
                            if (userSearchOutModel1 != null)
                            {
                                schedule.定期便依頼者_ACCESS_LEVEL = userSearchOutModel1.ACCESS_LEVEL;
                                schedule.定期便依頼者_SECTION_ID = userSearchOutModel1.SECTION_ID;
                                schedule.定期便依頼者_SECTION_GROUP_ID = userSearchOutModel1.SECTION_GROUP_ID;
                            }

                            string yoyakuLevel = GetAccessFlag(schedule.予約者_ACCESS_LEVEL);
                            string iraiLevel = GetAccessFlag(schedule.定期便依頼者_ACCESS_LEVEL);

                            //ログイン者が課長以上 かつ予約者 or 依頼者が課長以上 or ログイン者が課長以上 かつ 予約者 or 依頼者が同一課内の課長未満
                            if (
                                (level == "0" && (yoyakuLevel == "0" || iraiLevel == "0")) ||
                                 (level == "0" && 
                                    (
                                        (schedule.予約者_SECTION_ID == SessionDto.SectionID && yoyakuLevel != "0") ||
                                        (schedule.定期便依頼者_SECTION_ID == SessionDto.SectionID && iraiLevel != "0")
                                    )
                                ))
                            {
                                status.IsEdit = true;
                                status.IsDelete = true;
                            }
                            //ログイン者が担当以上 かつ予約者 or 依頼者が同一担当内の担当未満。
                            else if (
                                    (level == "0" || level == "1") &&
                                (
                                    (schedule.予約者_SECTION_GROUP_ID == SessionDto.SectionGroupID && yoyakuLevel != "0" && yoyakuLevel != "1") ||
                                    (schedule.定期便依頼者_SECTION_GROUP_ID == SessionDto.SectionGroupID && iraiLevel != "0" && iraiLevel != "1"))
                                )
                            {
                                status.IsEdit = true;
                                status.IsDelete = true;
                            }
                            else
                            {
                                status.IsEdit = false;
                                status.IsDelete = false;
                            }
                        }
                        else
                        {
                            status.IsEdit = false;
                            status.IsDelete = false;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 各トラック予約

                    //ログイン者　＝　予約者
                    if (SessionDto.UserId == schedule.予約者_ID)
                    {
                        status.IsEdit = true;
                        status.IsDelete = true;
                    }
                    else
                    {
                        if (userlist != null)
                        {
                            var userSearchOutModel = userlist.Where(x => x.PERSONEL_ID == schedule.予約者_ID).FirstOrDefault();
                            if (userSearchOutModel != null)
                            {
                                schedule.予約者_ACCESS_LEVEL = userSearchOutModel.ACCESS_LEVEL;
                                schedule.予約者_SECTION_ID = userSearchOutModel.SECTION_ID;
                                schedule.予約者_SECTION_GROUP_ID = userSearchOutModel.SECTION_GROUP_ID;
                            }

                            var userSearchOutModel1 = userlist.Where(x => x.PERSONEL_ID == schedule.運転者A_ID).FirstOrDefault();
                            if (userSearchOutModel1 != null)
                            {
                                schedule.運転者A_ACCESS_LEVEL = userSearchOutModel1.ACCESS_LEVEL;
                                schedule.運転者A_SECTION_ID = userSearchOutModel1.SECTION_ID;
                                schedule.運転者A_SECTION_GROUP_ID = userSearchOutModel1.SECTION_GROUP_ID;
                            }

                            var userSearchOutModel2 = userlist.Where(x => x.PERSONEL_ID == schedule.運転者B_ID).FirstOrDefault();
                            if (userSearchOutModel2 != null)
                            {
                                schedule.運転者B_ACCESS_LEVEL = userSearchOutModel2.ACCESS_LEVEL;
                                schedule.運転者B_SECTION_ID = userSearchOutModel2.SECTION_ID;
                                schedule.運転者B_SECTION_GROUP_ID = userSearchOutModel2.SECTION_GROUP_ID;
                            }

                            string yoyakuLevel = GetAccessFlag(schedule.予約者_ACCESS_LEVEL);
                            string untenALevel = GetAccessFlag(schedule.運転者A_ACCESS_LEVEL);
                            string untenBLevel = GetAccessFlag(schedule.運転者B_ACCESS_LEVEL);

                            //ログイン者が課長以上　かつ　予約者が課長以上 or ログイン者が課長以上　かつ予約者が同一課内の課長未満
                            if (
                                (level == "0" && yoyakuLevel == "0") ||
                                (level == "0" && (schedule.予約者_SECTION_ID == SessionDto.SectionID && yoyakuLevel != "0"))
                                )
                            {
                                status.IsEdit = true;
                            }
                            //ログイン者が担当以上　かつ　予約者が同一担当内の担当未満
                            else if (
                                (level == "0" || level == "1") &&
                                (schedule.予約者_SECTION_GROUP_ID == SessionDto.SectionGroupID && yoyakuLevel != "0" && yoyakuLevel != "1"))
                            {
                                status.IsEdit = true;
                            }
                            else
                            {
                                status.IsEdit = false;
                            }

                            //ログイン者　＝　予約者 or 運転者A or 運転者B
                            if (SessionDto.UserId == schedule.予約者_ID || SessionDto.UserId == schedule.運転者A_ID || SessionDto.UserId == schedule.運転者B_ID)
                            {
                                status.IsDelete = true;
                            }
                            //ログイン者が課長以上　かつ　予約者 or 運転者A or 運転者Bが課長以上 or ログイン者が課長以上　かつ予約者 or 運転者A or 運転者Bが同一課内の課長未満
                            else if (
                                (level == "0" &&
                                    (yoyakuLevel == "0" || untenALevel == "0" || untenBLevel == "0")
                                ) ||
                                (level == "0" &&
                                (
                                    (schedule.予約者_SECTION_ID == SessionDto.SectionID && yoyakuLevel != "0") ||
                                    (schedule.運転者A_SECTION_ID == SessionDto.SectionID && untenALevel != "0") ||
                                    (schedule.運転者B_ACCESS_LEVEL == SessionDto.SectionID && untenBLevel != "0")
                                )
                                )
                            )
                            {
                                status.IsDelete = true;
                            }
                            //ログイン者が担当以上　かつ　予約者 or 運転者A or ログイン者が担当以上　かつ　運転者Bが同一担当内の担当未満
                            else if (
                                (level == "0" && level == "1") &&
                                (
                                    (schedule.予約者_SECTION_GROUP_ID == SessionDto.SectionGroupID && yoyakuLevel != "0" && yoyakuLevel != "1") ||
                                    (schedule.運転者A_SECTION_GROUP_ID == SessionDto.SectionGroupID && untenALevel != "0" && untenALevel != "1") ||
                                    (schedule.運転者B_SECTION_GROUP_ID == SessionDto.SectionGroupID && untenBLevel != "0" && untenBLevel != "1")
                                )
                            )
                            {
                                status.IsDelete = true;
                            }
                            else
                            {
                                status.IsDelete = false;
                            }
                        }
                        else
                        {
                            status.IsEdit = false;
                            status.IsDelete = false;
                        }
                    }
                    #endregion
                }
            }
            
            return status;
        }

        /// <summary>
        /// アクセスフラグ取得処理
        /// </summary>
        /// <param name="accessLevel">ユーザーアクセスレベル</param>
        /// <returns></returns>
        private static string GetAccessFlag(string accessLevel)
        {
            return string.IsNullOrWhiteSpace(accessLevel) == true ? "" : accessLevel.Substring(0, 1);
        }
    }

    /// <summary>
    /// スケジュール編集権限クラス
    /// </summary>
    public class EditStatus
    {
        /// <summary>
        /// スケジュール編集権限
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// スケジュール削除権限
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
