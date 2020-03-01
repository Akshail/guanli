using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataModel;
using Lephone.Data;
using System.Data;

namespace BusinessSupply.BusinessOper
{
    public class UserBusiness
    {

        public static IPagedSelector<TUserInfo> GetUserList(int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["IsDel"] == false && CK.K["UserTypeId"].Le(3))
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TUserInfo> GetUserList(string name, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["Account"].MiddleLike(name) && CK.K["IsDel"] == false && CK.K["UserTypeId"].Le(3))
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static string ResetPwdById(int infoid)
        {
            try
            {
                var info = TUserInfo.FindById(infoid);
                info.Pwd = "123456";
                info.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string AddUser(string account, string pwd, string name ,int userTypeId,string IDCardNum,string Phone,int createuserid, DateTime createtime)
        {
            try
            {
                var AsAdmin = new TUserInfo();
                AsAdmin.Account = account;
                AsAdmin.Pwd = pwd;
                AsAdmin.Name = name;
                AsAdmin.UserTypeId = userTypeId;
                AsAdmin.IDCardNum = IDCardNum;
                AsAdmin.Phone = Phone;

                AsAdmin.IsDel = false;
                AsAdmin.CreateUserId = createuserid;
                AsAdmin.CreateTime = createtime;
                AsAdmin.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string UpdateUser(int userId, string account, string pwd, string name, int userTypeId, string IDCardNum, string Phone, int createuserid, DateTime createtime)
        {
            try
            {
                var AsAdmin = TUserInfo.FindById(userId);
                AsAdmin.Account = account;
                AsAdmin.Pwd = pwd;
                AsAdmin.Name = name;
                AsAdmin.UserTypeId = userTypeId;
                AsAdmin.IDCardNum = IDCardNum;
                AsAdmin.Phone = Phone;

                AsAdmin.CreateUserId = createuserid;
                AsAdmin.CreateTime = createtime;
                AsAdmin.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static bool IsExist(string account,int exceptUserId)
        {
            try
            {
                Condition con = CK.K["Account"] == account && CK.K["IsDel"] == false;
                if(exceptUserId != 0)
                {
                    con &= CK.K["Id"] != exceptUserId;
                }
                var user = TUserInfo.FindOne(con);
                if (user == null)
                    return false;
                else
                    return true;
            }
            catch
            {
                return true;
            }
        }






        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static DataTable Login(string account, string pwd)
        {
            try
            {
                DataTable dt_user = DbEntry.From<TUserInfo>().Where(CK.K["IsDel"] == "False" && CK.K["Account"] == account && CK.K["Pwd"] == pwd).Select().ToDataTable();
                return dt_user;

            }
            catch
            {
                return null;
            }
        }

        public static TUserInfo GetUserById(int id)
        {
            try
            {
                TUserInfo user = TUserInfo.FindById(id);
                return user;
            }
            catch
            {
                return null;
            }
        }



        public static string AddAsAdmin(string account, string pwd, string name, int associationid, int createuserid, DateTime createtime)
        {
            try
            {
                var AsAdmin = new TUserInfo();
                AsAdmin.Account = account;
                AsAdmin.Pwd = pwd;
                AsAdmin.Name = name;
                AsAdmin.UserTypeId = 2;
                AsAdmin.AssociationId = associationid;

                AsAdmin.IsDel = false;
                AsAdmin.CreateUserId = createuserid;
                AsAdmin.CreateTime = createtime;
                AsAdmin.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string UpdatePersonMemberAccount(int personmemberid, string account, int createuserid, DateTime createtime)
        {
            try
            {
                TUserInfo person = TUserInfo.FindOne(CK.K["PersonMemberId"] == personmemberid && CK.K["IsDel"] == "False");
                person.Account = account;


                person.IsDel = false;
                person.CreateUserId = createuserid;
                person.CreateTime = createtime;
                person.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static TUserInfo GetPersonMemberByPersonMemberId(int personmemberid)
        {
            try
            {
                TUserInfo person = TUserInfo.FindOne(CK.K["PersonMemberId"] == personmemberid && CK.K["IsDel"] == "False");
                return person;
            }
            catch
            {
                return null;
            }
        }

        public static TUserInfo GetGroupMemberByGroupMemberId(int groupmemberid)
        {
            try
            {
                TUserInfo group = TUserInfo.FindOne(CK.K["GroupMemberId"] == groupmemberid && CK.K["IsDel"] == "False");
                return group;
            }
            catch
            {
                return null;
            }
        }

        //新增个人会员
        public static string AddPersonMember(string account, string pwd, string name, int personmemberid, int createuserid, DateTime createtime)
        {
            try
            {
                var personmember = new TUserInfo();
                personmember.Account = account;
                personmember.Pwd = pwd;
                personmember.Name = name;
                personmember.UserTypeId = 4;
                personmember.PersonMemberId = personmemberid;

                personmember.IsDel = false;
                personmember.CreateUserId = createuserid;
                personmember.CreateTime = createtime;
                personmember.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string AddGroupMember(string account, string pwd, string name, int groupmemberid, int createuserid, DateTime createtime)
        {
            try
            {
                var groupmember = new TUserInfo();
                groupmember.Account = account;
                groupmember.Pwd = pwd;
                groupmember.Name = name;
                groupmember.UserTypeId = 5;
                groupmember.GroupMemberId = groupmemberid;

                groupmember.IsDel = false;
                groupmember.CreateUserId = createuserid;
                groupmember.CreateTime = createtime;
                groupmember.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static IPagedSelector<TUserInfo> GetAsAdminList(int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["IsDel"] == false && CK.K["UserTypeId"] == 2)
                       .OrderBy("AssociationId")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TUserInfo> GetAsAdminListByAsAdminName(string name, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["Name"] == name && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 2)
                       .OrderBy("AssociationId")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static string AddVipMember(string account, string pwd, string name, int createuserid, DateTime createtime)
        {
            try
            {
                var VipMember = new TUserInfo();
                VipMember.Account = account;
                VipMember.Pwd = pwd;
                VipMember.Name = name;
                VipMember.IsDel = false;
                VipMember.CreateTime = createtime;
                VipMember.CreateUserId = createuserid;
                VipMember.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string DeleteUserById(int infoid, int deluserid, DateTime deltime)
        {
            try
            {
                var delinfo = TUserInfo.FindById(infoid);
                delinfo.IsDel = true;
                delinfo.DelUserId = deluserid;
                delinfo.DelTime = deltime;
                delinfo.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string DeleteUserByPersonMemberId(int id, int deluserid, DateTime deltime)
        {
            try
            {
                var delinfo = TUserInfo.FindOne(CK.K["PersonMemberId"] == id && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 4);
                delinfo.IsDel = true;
                delinfo.DelUserId = deluserid;
                delinfo.DelTime = deltime;
                delinfo.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string DeleteUserByAssociationId(int id, int deluserid, DateTime deltime)
        {
            try
            {
                var delinfo = TUserInfo.FindOne(CK.K["AssociationId"] == id && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 2);
                delinfo.IsDel = true;
                delinfo.DelUserId = deluserid;
                delinfo.DelTime = deltime;
                delinfo.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string DeleteUserByGroupMemberId(int infoid, int deluserid, DateTime deltime)
        {
            try
            {
                var delinfo = TUserInfo.FindOne(CK.K["GroupMemberId"] == infoid && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 5);
                delinfo.IsDel = true;
                delinfo.DelUserId = deluserid;
                delinfo.DelTime = deltime;
                delinfo.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

       
        public static IPagedSelector<TUserInfo> GetVipList(int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["IsDel"] == false && CK.K["UserTypeId"] == 3)
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TUserInfo> GetVipListByVipName(string name, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["Account"] == name && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 3)
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static string AddVip(string account, string pwd, string name, int createuserid, DateTime createtime)
        {
            try
            {
                var Vip = new TUserInfo();
                Vip.Account = account;
                Vip.Pwd = pwd;
                Vip.Name = name;
                Vip.UserTypeId = 3;


                Vip.IsDel = false;
                Vip.CreateUserId = createuserid;
                Vip.CreateTime = createtime;
                Vip.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static IPagedSelector<TUserInfo> GetAsAdminListByAssociationId(int associationid, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["IsDel"] == false && CK.K["UserTypeId"] == 2 && CK.K["AssociationId"] == associationid)
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TUserInfo> GetAsAdminListByAssociationIdAndAsAdminName(int associationid, string name, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<TUserInfo>()
                       .Where(CK.K["Name"] == name && CK.K["IsDel"] == false && CK.K["UserTypeId"] == 2 && CK.K["AssociationId"] == associationid)
                       .OrderBy("Id")
                       .PageSize(pagesize)
                       .GetPagedSelector();
                return ps_adadmin;
            }
            catch
            {
                return null;
            }
        }

        public static bool HasPersonAccount(string account)
        {
            try
            {
                DataTable dt = DbEntry
                      .From<TUserInfo>()
                      .Where(CK.K["Account"] == account && CK.K["IsDel"] == false)
                      .Select()
                      .ToDataTable();
                if (dt.Rows.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        public static bool HasAnotherPersonAccount(int personmemberid, string account)
        {
            try
            {
                DataTable dt = DbEntry
                      .From<TUserInfo>()
                      .Where((CK.K["PersonMemberId"] != personmemberid || CK.K["PersonMemberId"] == null) && CK.K["Account"] == account && CK.K["IsDel"] == false)
                      .Select()
                      .ToDataTable();
                if (dt.Rows.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
        public static bool HasAnotherGroupAccount(int groupmemberid, string account)
        {
            try
            {
                DataTable dt = DbEntry
                      .From<TUserInfo>()
                      .Where((CK.K["GroupMemberId"] != groupmemberid || CK.K["GroupMemberId"] == null) && CK.K["Account"] == account && CK.K["IsDel"] == false)
                      .Select()
                      .ToDataTable();
                if (dt.Rows.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        public static string UpdatePwdById(int userid, string pwd)
        {
            try
            {
                TUserInfo person = TUserInfo.FindById(userid);
                person.Pwd = pwd;

                person.IsDel = false;
                //person.CreateUserId = createuserid;
                //person.CreateTime = createtime;
                person.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        
    }
}
