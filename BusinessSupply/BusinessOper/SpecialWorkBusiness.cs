using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class SpecialWorkBusiness
    {
        public static int AddCommunication()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SpecialWork com = new SpecialWork();
                    com.IsDelete = true;
                    com.Save();
                    id = com.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static bool UpdateCommunicationForNew(int communicationId, string exchange, int specialWorkTypeId, DateTime workStartDate, DateTime workEndDate, string type, string name, bool sex, DateTime birthday, string company, string address, string phoneNo, double ballot, bool isReelect, string background, string situation)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SpecialWork com = SpecialWork.FindById(communicationId);
                    com.Exchange = exchange;
                    com.SpecialWorkTypeId = specialWorkTypeId;
                    if (DateTime.Compare(workStartDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.WorkStartDate = null;
                    }
                    else
                    {
                        com.WorkStartDate = workStartDate;
                    }
                    if (DateTime.Compare(workEndDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.WorkEndDate = null;
                    }
                    else
                    {
                        com.WorkEndDate = workEndDate;
                    }
                    com.Type = type;
                    com.Name = name;
                    com.Sex = sex;
                    if (DateTime.Compare(birthday, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.Birthday = null;
                    }
                    else
                    {
                        com.Birthday = birthday;
                    }
                    com.Company = company;
                    com.Address = address;
                    com.PhoneNo = phoneNo;
                    com.Ballot = ballot;
                    com.IsReelect = isReelect;
                    com.Background = background;
                    com.Situation = situation;
                    com.IsDelete = false;
                    com.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateCommunication(int communicationId, string exchange, int specialWorkTypeId, DateTime workStartDate, DateTime workEndDate, string type, string name, bool sex, DateTime birthday, string company, string address, string phoneNo, double ballot, bool isReelect, string background, string situation)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    SpecialWork com = SpecialWork.FindById(communicationId);
                    com.Exchange = exchange;
                    com.SpecialWorkTypeId = specialWorkTypeId;
                    if (DateTime.Compare(workStartDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.WorkStartDate = null;
                    }
                    else
                    {
                        com.WorkStartDate = workStartDate;
                    }
                    if (DateTime.Compare(workEndDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.WorkEndDate = null;
                    }
                    else
                    {
                        com.WorkEndDate = workEndDate;
                    }
                    com.Type = type;
                    com.Name = name;
                    com.Sex = sex;
                    if (DateTime.Compare(birthday, new DateTime(1900, 1, 1)) == 0)
                    {
                        com.Birthday = null;
                    }
                    else
                    {
                        com.Birthday = birthday;
                    }

                    com.Company = company;
                    com.Address = address;
                    com.PhoneNo = phoneNo;
                    com.Ballot = ballot;
                    com.IsReelect = isReelect;
                    com.Background = background;
                    com.Situation = situation;
                    com.Save();
                });
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// DbEntry分页查询数据，
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name="pagesize">gridview每页数据行数</param>
        /// <returns></returns>
        public static IPagedSelector<SpecialWork> GetCommunications(int pagesize, string name, string sex)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                var ps = DbEntry.From<SpecialWork>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static bool DeleteCommunication(int communicationId)
        {
            try
            {
                SpecialWork com = SpecialWork.FindById(communicationId);
                com.IsDelete = true;
                com.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static DataTable GetAllData()
        {
            try
            {
                DataTable dt = DbEntry.From<SpecialWork>().Where(CK.K["IsDelete"] == false).Select().ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetStatisticBySex()
        {
            try
            {
                DataTable dt = DbEntry.From<SpecialWork>().Where(CK.K["IsDelete"] == false).GroupBy<bool>("Sex").ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
