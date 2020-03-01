using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class CommunicationBusiness
    {
        public static int AddCommunication()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Communication com = new Communication();
                    com.IsDelete = true;
                    com.Save();
                    id = com.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static bool UpdateCommunicationForNew(int communicationId, string exchange, string type, string name, bool sex, DateTime birthday, string company, string address, string phoneNo, double ballot, bool isReelect, string servant, string phoneNoOfS, string background, string situation)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Communication com = Communication.FindById(communicationId);
                    com.Exchange = exchange;
                    com.Type = type;
                    com.Name = name;
                    com.Sex = sex;
                    com.Birthday = birthday;
                    com.Company = company;
                    com.Address = address;
                    com.PhoneNo = phoneNo;
                    com.Ballot = ballot;
                    com.IsReelect = isReelect;
                    com.Servant = servant;
                    com.PhoneNoOfS = phoneNoOfS;
                    com.Background = background;
                    com.Situation = situation;
                    com.IsDelete = false;
                    com.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateCommunication(int communicationId, string exchange, string type, string name, bool sex, DateTime birthday, string company, string address, string phoneNo, double ballot, bool isReelect, string servant, string phoneNoOfS, string background, string situation)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Communication com = Communication.FindById(communicationId);
                    com.Exchange = exchange;
                    com.Type = type;
                    com.Name = name;
                    com.Sex = sex;
                    com.Birthday = birthday;
                    com.Company = company;
                    com.Address = address;
                    com.PhoneNo = phoneNo;
                    com.Ballot = ballot;
                    com.IsReelect = isReelect;
                    com.Servant = servant;
                    com.PhoneNoOfS = phoneNoOfS;
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
        public static IPagedSelector<Communication> GetCommunications(int pagesize, string name, string sex)
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
                var ps = DbEntry.From<Communication>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
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
                Communication com = Communication.FindById(communicationId);
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
                DataTable dt = DbEntry.From<Communication>().Where(CK.K["IsDelete"] == false).Select().ToDataTable();
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
                DataTable dt = DbEntry.From<Communication>().Where(CK.K["IsDelete"] == false).GroupBy<bool>("Sex").ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
