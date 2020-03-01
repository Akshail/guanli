using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class ExperienceBusiness
    {
        public static bool AddExperience(int studentId, DateTime startTime, DateTime endTime, string school)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Experience exp = new Experience();
                    exp.Student_Id = studentId;
                    exp.StartTime = startTime;
                    exp.EndTime = endTime;
                    exp.School = school;
                    exp.IsDelete = false;
                    exp.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateExperience(int experienceId, DateTime startTime, DateTime endTime, string school)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Experience exp = Experience.FindById(experienceId);
                    exp.StartTime = startTime;
                    exp.EndTime = endTime;
                    exp.School = school;
                    exp.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool DeleteExperience(int experienceId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Experience exp = Experience.FindById(experienceId);
                    exp.IsDelete = true;
                    exp.Save();
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
        public static IPagedSelector<Experience> GetExperiences(int studentId, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<Experience>()
                    .Where(CK.K["Student_Id"] == studentId && CK.K["IsDelete"] == false)
                    .OrderBy((ASC)"StartTime")
                    .PageSize(pagesize)
                    .GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetExperiencesAsDataTable(int studentId)
        {
            //查询
            try
            {
                var ps = DbEntry.From<Experience>().Where(CK.K["Student_Id"] == studentId && CK.K["IsDelete"] == false).OrderBy((ASC)"StartTime").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }
    }
}
