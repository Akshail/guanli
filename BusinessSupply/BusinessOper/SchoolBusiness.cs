using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class SchoolBusiness
    {
        public static IPagedSelector<School> GetSchoolList(int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<School>()
                       .Where(CK.K["IsDelete"] == false)
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

        public static IPagedSelector<School> GetSchoolList(string name, int pagesize)
        {
            try
            {
                var ps_adadmin = DbEntry
                       .From<School>()
                       .Where(CK.K["SchoolName"].MiddleLike(name) && CK.K["IsDelete"] == false)
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
        public static string AddSchool(string schoolname, string schoolcode)
        {
            try
            {
                var school = new School();
                school.SchoolName = schoolname;
                school.SchoolCode = schoolcode;
                school.IsDelete = false;
                school.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static string UpdateSchool(int userId, string schoolname, string schoolcode)
        {
            try
            {
                var school = School.FindById(userId);
                school.SchoolName = schoolname;
                school.SchoolCode = schoolcode;
                school.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }
        public static string DeleteUserById(int infoid)
        {
            try
            {
                var delinfo = School.FindById(infoid);
                delinfo.IsDelete = true;
                delinfo.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static DataTable GetAllData()
        {
            try
            {
                DataTable dt = DbEntry.From<School>().Where(CK.K["IsDelete"] == false).Select().ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
