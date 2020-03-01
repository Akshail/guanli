using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class FamilyOfPeopleBusiness
    {
        public static bool AddFamilyOfPeople(int people_id, string name, bool sex, DateTime birthday, String relationship, String company, String tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Family fam = new Family();
                    fam.People_Id = people_id;
                    fam.Name = name;
                    fam.Sex = sex;
                    fam.Birthday = birthday;
                    fam.Relationship = relationship;
                    fam.Company = company;
                    fam.Tip = tip;
                    fam.IsDelete = false;
                    fam.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool UpdateFamilyOfPeople(int family_id, string name, bool sex, DateTime birthday, String relationship, String company, String tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Family stu = Family.FindById(family_id);
                    stu.Name = name;
                    stu.Sex = sex;
                    stu.Birthday = birthday;
                    stu.Relationship = relationship;
                    stu.Company = company;
                    stu.Tip = tip;
                    stu.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool deleteFamilyOfPeople(int family_id)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Family stu = Family.FindById(family_id);
                    stu.IsDelete = true;
                    stu.Save();
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
        public static IPagedSelector<Family> GetFamilys(int peopleId, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<Family>()
                    .Where(CK.K["People_Id"] == peopleId && CK.K["IsDelete"] == false)
                    .OrderBy((ASC)"Name")
                    .PageSize(pagesize)
                    .GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetFamilysAsDataTable(int peopleId)
        {
            //查询
            try
            {
                var ps = DbEntry.From<Family>().Where(CK.K["People_Id"] == peopleId && CK.K["IsDelete"] == false).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }
    }
}
