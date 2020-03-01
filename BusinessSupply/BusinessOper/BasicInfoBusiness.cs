using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class BasicInfoBusiness
    {
        public static bool AddBasicInfo(string name, string phone, string email, string address, string zipcode)
        {
            try
            {
                BasicInfo basic = new BasicInfo();
                basic.Name = name;
                basic.Phone = phone;
                basic.Email = email;
                basic.DepartmentAddress = address;
                basic.ZipCode = zipcode;
                basic.IsDelete = false;
                basic.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UpdateBasicInfo(int basicInfoId, string name, string phone, string email, string address, string zipcode)
        {
            try
            {
                BasicInfo basic = BasicInfo.FindById(basicInfoId);
                basic.Name = name;
                basic.Phone = phone;
                basic.Email = email;
                basic.DepartmentAddress = address;
                basic.ZipCode = zipcode;
                basic.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool DeleteBasicInfo(int basicInfoId)
        {
            try
            {
                BasicInfo basic = BasicInfo.FindById(basicInfoId);
                basic.IsDelete = true;
                basic.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// DbEntry分页查询数据，
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name="pagesize">gridview每页数据行数</param>
        /// <returns></returns>
        public static IPagedSelector<BasicInfo> GetBasicInfos(int pagesize, string name, string phone, string email)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!phone.IsNullOrEmpty())
                {
                    con &= CK.K["Phone"].MiddleLike(phone);
                }
                if (!email.IsNullOrEmpty())
                {
                    con &= CK.K["Email"].MiddleLike(email);
                }
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBasicInfosForMessage()
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false && CK.K["Phone"] != "";
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((DESC)"Phone").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBasicInfosForEmail()
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false && CK.K["Email"] != "";
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((DESC)"Email").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBasicInfoForHasPhoneById(int id)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false && CK.K["Phone"] != null && CK.K["Phone"] != "" && CK.K["Id"] == id;
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBasicInfoForHasEmailById(int id)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false && CK.K["Email"] != null && CK.K["Email"] != "" && CK.K["Id"] == id;
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static BasicInfo GetBasicInfoById(int id)
        {
            try
            {
                var ps = BasicInfo.FindById(id);
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetBasicInfoDataTable(string name)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                var ps = DbEntry.From<BasicInfo>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
