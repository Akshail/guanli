using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class FamilyOfStudentBusiness
    {
        public static bool AddFamilyOfStudent(int student_id,string name, bool sex, DateTime birthday, String relationship, String company, String tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    FamilyOfStudent stu = new FamilyOfStudent();
                    stu.Student_Id = student_id;
                    stu.Name = name;
                    stu.Sex = sex;
                    stu.Birthday = birthday;
                    stu.Relationship = relationship;
                    stu.Company = company;
                    stu.Tip = tip;
                    stu.IsDelete = false;
                    stu.Save();
                });
            }
            catch { return false; }
            return true;
        }
        public static bool UpdateFamilyOfStudent(int family_id, string name, bool sex, DateTime birthday, String relationship, String company, String tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    FamilyOfStudent stu = FamilyOfStudent.FindById(family_id);
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
        public static bool deleteFamilyOfStudent(int stuId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    FamilyOfStudent stu = FamilyOfStudent.FindById(stuId);
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
        public static IPagedSelector<FamilyOfStudent> GetFamilys(int studentId, int pagesize)
        {
            //查询
            try
            {
                var ps = DbEntry
                    .From<FamilyOfStudent>()
                    .Where(CK.K["Student_Id"] == studentId && CK.K["IsDelete"] == false)
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

        public static DataTable GetFamilysAsDataTable(int studentId)
        {
            //查询
            try
            {
                var datatable = DbEntry.From<FamilyOfStudent>().Where(CK.K["Student_Id"] == studentId && CK.K["IsDelete"] == false).OrderBy((ASC)"Name").Select().ToDataTable();
                return datatable;
            }
            catch
            {
                return null;
            }
        }
    }
}
