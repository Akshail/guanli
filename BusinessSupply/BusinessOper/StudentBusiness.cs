using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class StudentBusiness
    {
        public static int AddStudentForId()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Student stu = new Student();
                    stu.IsDelete = true;
                    stu.Save();
                    id = stu.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static string AddStudentForNew(int stuId, string name, bool sex, string identityNo, DateTime birthday, String school, int studyTypeId, DateTime joinSchoolDate, String major, String systemOfEdu, String phoneNo, String AddressOfH, String tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Student stu = Student.FindById(stuId);
                    stu.Name = name;
                    stu.Sex = sex;
                    stu.IdentityNo = identityNo;
                    if (DateTime.Compare(birthday, new DateTime(1900, 1, 1)) == 0)
                    {
                        stu.Birthday = null;
                    }
                    else
                    {
                        stu.Birthday = birthday;
                    }
                    stu.School = school;
                    stu.StudyTypeId = studyTypeId;
                    if (DateTime.Compare(joinSchoolDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        stu.JoinSchoolDate = null;
                    }
                    else
                    {
                        stu.JoinSchoolDate = joinSchoolDate;
                    }
                    stu.Major = major;
                    stu.SystemOfEdu = systemOfEdu;
                    stu.PhoneNo = phoneNo;
                    stu.AddressOfH = AddressOfH;
                    stu.Tip = tip;
                    stu.IsDelete = false;
                    stu.Save();
                });
            }
            catch { return "fail"; }
            return "ok";
        }



        public static int AddStudentForImport(string name, bool sex, string identityNo, DateTime birthday, String school, String major, int studyTypeId, DateTime joinSchoolDate, String systemOfEdu, String phoneNo, String AddressOfH, String tip)
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Student stu = new Student();
                    stu.Name = name;
                    stu.Sex = sex;
                    stu.IdentityNo = identityNo;
                    if (DateTime.Compare(birthday, new DateTime(1900, 1, 1)) == 0)
                    {
                        stu.Birthday = null;
                    }
                    else
                    {
                        stu.Birthday = birthday;
                    }
                    stu.School = school;
                    stu.Major = major;
                    stu.StudyTypeId = studyTypeId;
                    if (DateTime.Compare(joinSchoolDate, new DateTime(1900, 1, 1)) == 0)
                    {
                        stu.JoinSchoolDate = null;
                    }
                    else
                    {
                        stu.JoinSchoolDate = joinSchoolDate;
                    }
                    stu.SystemOfEdu = systemOfEdu;
                    stu.PhoneNo = phoneNo;
                    stu.AddressOfH = AddressOfH;
                    stu.Tip = tip;
                    stu.IsDelete = false;
                    stu.Save();
                    id = stu.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static string UpdateStudent(int stuId, string name, bool sex, string identityNo, DateTime birthday, String school, int studyTypeId, DateTime joinSchoolDate, String major, String systemOfEdu, String phoneNo, String AddressOfH, String tip)
        {
            try
            {
                Student stu = Student.FindById(stuId);
                stu.Name = name;
                stu.Sex = sex;
                stu.IdentityNo = identityNo;
                if (DateTime.Compare(birthday, new DateTime(1900, 1, 1)) == 0)
                {
                    stu.Birthday = null;
                }
                else
                {
                    stu.Birthday = birthday;
                }
                stu.School = school;
                stu.StudyTypeId = studyTypeId;
                if (DateTime.Compare(joinSchoolDate, new DateTime(1900, 1, 1)) == 0)
                {
                    stu.JoinSchoolDate = null;
                }
                else
                {
                    stu.JoinSchoolDate = joinSchoolDate;
                }
                stu.Major = major;
                stu.SystemOfEdu = systemOfEdu;
                stu.PhoneNo = phoneNo;
                stu.AddressOfH = AddressOfH;
                stu.Tip = tip;
                stu.Save();
            }
            catch (Exception ex)
            {
                return "fail";
            }
            return "ok";
        }

        public static string deleteStudent(int stuId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Student stu = Student.FindById(stuId);
                    stu.IsDelete = true;
                    stu.Save();
                });
            }
            catch { return "fail"; }
            return "ok";
        }

        public static IPagedSelector<Student> GetStudents(int pagesize, string name, string school, string sex, string startYear, string endYear)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!school.IsNullOrEmpty() && !school.Equals("0"))
                {
                    con &= CK.K["School"].MiddleLike(school);
                }
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                if (!startYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(startYear), 1, 1));
                }
                if (!endYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(endYear), 12, 31));
                }
                var ps = DbEntry.From<Student>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetStudentsAsDataTable(string name, string school, string sex, string startYear, string endYear)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!school.IsNullOrEmpty() && !school.Equals("0"))
                {
                    con &= CK.K["School"].MiddleLike(school);
                }
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                if (!startYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(startYear), 1, 1));
                }
                if (!endYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(endYear), 12, 31));
                }
                var datatable = DbEntry.From<Student>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return datatable;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetStatisticBySex(string school, string startYear, string endYear)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!school.IsNullOrEmpty() && !school.Equals("0"))
                {
                    con &= CK.K["School"].MiddleLike(school);
                }
                if (!startYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(startYear), 1, 1));
                }
                if (!endYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(endYear), 12, 31));
                }
                DataTable dt = DbEntry.From<Student>().Where(con).GroupBy<bool>("Sex").ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetAllData(string name, string school, string sex, string startYear, string endYear)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!school.IsNullOrEmpty() && !school.Equals("0"))
                {
                    con &= CK.K["School"].MiddleLike(school);
                }
                if (!sex.IsNullOrEmpty() && !sex.Equals("99"))
                {
                    con &= CK.K["Sex"] == sex.Equals("True");
                }
                if (!startYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(startYear), 1, 1));
                }
                if (!endYear.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(endYear), 12, 31));
                }
                DataTable dt = DbEntry.From<Student>().Where(con).Select().ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
