using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class PeopleBusiness
    {
        public static int AddPeopleForId()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    People people = new People();
                    people.IsDelete = true;
                    people.Save();
                    id = people.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static string AddPeopleForNew(int id, string name, bool sex, DateTime birthday, DateTime arriveDate, string identityNo, string phoneNo, string address, string tempApplication, int comeReason, string tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    People people = People.FindById(id);
                    people.Name = name;
                    people.Sex = sex;
                    people.Birthday = birthday;
                    people.ArriveDate = arriveDate;
                    people.IdentityNo = identityNo;
                    people.PhoneNo = phoneNo;
                    people.Address = address;
                    people.TempApplication = tempApplication;
                    people.ComeReason = comeReason;
                    people.Tip = tip;
                    people.IsDelete = false;
                    people.Save();
                });
            }
            catch { return "fail"; }
            return "ok";
        }

        public static int AddPeopleForImport(string name, bool sex, DateTime birthday, DateTime arriveDate, string identityNo, string phoneNo, string address, string tempApplication, int comeReason, string tip)
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    People people = new People();
                    people.Name = name;
                    people.Sex = sex;
                    people.Birthday = birthday;
                    people.ArriveDate = arriveDate;
                    people.IdentityNo = identityNo;
                    people.PhoneNo = phoneNo;
                    people.Address = address;
                    people.TempApplication = tempApplication;
                    people.ComeReason = comeReason;
                    people.Tip = tip;
                    people.IsDelete = false;
                    people.Save();
                    id = people.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static bool UpdatePeople(int peopleId, string name, bool sex, DateTime birthday, DateTime arriveDate, string identityNo, string phoneNo, string address, string tempApplication, int comeReason, string tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
               {
                   People people = People.FindById(peopleId);
                   people.Name = name;
                   people.Sex = sex;
                   people.Birthday = birthday;
                   people.ArriveDate = arriveDate;
                   people.IdentityNo = identityNo;
                   people.PhoneNo = phoneNo;
                   people.Address = address;
                   people.TempApplication = tempApplication;
                   people.ComeReason = comeReason;
                   people.Tip = tip;
                   people.Save();
               });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool deletePeople(int peopleId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    People people = People.FindById(peopleId);
                    people.IsDelete = true;
                    people.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static IPagedSelector<People> GetPeoples(int pagesize, string name, string sex, string comereason, string birth_start, string birth_end, string sh_start, string sh_end)
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
                if (!comereason.IsNullOrEmpty() && !comereason.Equals("99"))
                {
                    con &= CK.K["ComeReason"] == Convert.ToInt32(comereason);
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["ArriveDate"].Ge(new DateTime(Convert.ToInt32(sh_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["ArriveDate"].Le(new DateTime(Convert.ToInt32(sh_end), 12, 31));
                }

                var ps = DbEntry.From<People>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetPeoplesAsDataTable(string name, string sex, string comereason, string birth_start, string birth_end, string sh_start, string sh_end)
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
                if (!comereason.IsNullOrEmpty() && !comereason.Equals("99"))
                {
                    con &= CK.K["ComeReason"] == Convert.ToInt32(comereason);
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["ArriveDate"].Ge(new DateTime(Convert.ToInt32(sh_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["ArriveDate"].Le(new DateTime(Convert.ToInt32(sh_end), 12, 31));
                }

                var ps = DbEntry.From<People>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetAllData(string sex, string comereason, string birth_start, string birth_end)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                if (!comereason.IsNullOrEmpty() && !comereason.Equals("99"))
                {
                    con &= CK.K["ComeReason"] == Convert.ToInt32(comereason);
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                DataTable dt = DbEntry.From<People>().Where(con).Select().ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static DataTable GetStatisticBySex(string sex, string comereason, string birth_start, string birth_end)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                if (!comereason.IsNullOrEmpty() && !comereason.Equals("99"))
                {
                    con &= CK.K["ComeReason"] == Convert.ToInt32(comereason);
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                DataTable dt = DbEntry.From<People>().Where(con).GroupBy<bool>("Sex").ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
