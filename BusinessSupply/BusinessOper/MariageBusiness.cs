using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class MariageBusiness
    {
        public static int AddMariageForId()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Mariage mariage = new Mariage();
                    mariage.IsDelete = true;
                    mariage.Save();
                    id = mariage.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static string AddMariageForNew(int id, string name, bool sex, DateTime birthday, string addressOfH, bool isGetTW, string identityType, string identityNo, string domicileOfH, string domicileOfTW, string addressOfTW, string education, string company, string post, string consort, bool sexOfC, string addressOfTWPEI, DateTime birthdayOfC, string domicileOfC, string identityTypeOther, string identityNoOther, bool isWordH, string companyOfH, string postOfH, DateTime weddingDay, bool isCouple, string tip)
        {
            //还差用户的身份证号和配偶的身份证号
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Mariage mariage = Mariage.FindById(id);
                    mariage.Name = name;
                    mariage.Sex = sex;
                    mariage.Birthday = birthday;
                    mariage.AddressOfH = addressOfH;
                    mariage.IsGetTW = isGetTW;
                    mariage.IdentityType = identityType;
                    mariage.IdentityNo = identityNo;
                    mariage.DomicileOfH = domicileOfH;
                    mariage.DomicileOfTW = domicileOfTW;
                    mariage.AddressOfTW = addressOfTW;
                    mariage.Education = education;
                    mariage.Company = company;
                    mariage.Post = post;
                    mariage.Consort = consort;
                    mariage.SexOfC = sexOfC;
                    mariage.AddressOfTWPEI = addressOfTWPEI;
                    mariage.BirthdayOfC = birthdayOfC;
                    mariage.DomicileOfC = domicileOfC;
                    mariage.IdentityTypeOther = identityTypeOther;
                    mariage.IdentityNoOther = identityNoOther;
                    mariage.IsWordH = isWordH;
                    mariage.CompanyOfH = companyOfH;
                    mariage.PostOfH = postOfH;
                    mariage.WeddingDay = weddingDay;
                    mariage.IsCouple = isCouple;
                    mariage.Tip = tip;
                    mariage.IsDelete = false;
                    mariage.Save();
                });
            }
            catch { return "fail"; }
            return "ok";
        }

        //public static string AddMariage(string name, bool sex, DateTime birthday, string addressOfH, bool isGetTW, string identityType, string identityNo, string domicileOfH, string domicileOfTW, string addressOfTW, string education, string company, string post, string consort, bool sexOfC, string addressOfTWPEI, DateTime birthdayOfC, string domicileOfC, string identityTypeOther, string identityNoOther, bool isWordH, string companyOfH, string postOfH, DateTime weddingDay, bool isCouple, string tip)
        //{
        //    //还差用户的身份证号和配偶的身份证号
        //    try
        //    {
        //        DbEntry.UsingTransaction(delegate
        //        {
        //            Mariage mariage = new Mariage();
        //            mariage.Name = name;
        //            mariage.Sex = sex;
        //            mariage.Birthday = birthday;
        //            mariage.AddressOfH = addressOfH;
        //            mariage.IsGetTW = isGetTW;
        //            mariage.IdentityType = identityType;
        //            mariage.IdentityNo = identityNo;
        //            mariage.DomicileOfH = domicileOfH;
        //            mariage.DomicileOfTW = domicileOfTW;
        //            mariage.AddressOfTW = addressOfTW;
        //            mariage.Education = education;
        //            mariage.Company = company;
        //            mariage.Post = post;
        //            mariage.Consort = consort;
        //            mariage.SexOfC = sexOfC;
        //            mariage.AddressOfTWPEI = addressOfTWPEI;
        //            mariage.BirthdayOfC = birthdayOfC;
        //            mariage.DomicileOfC = domicileOfC;
        //            mariage.IdentityTypeOther = identityTypeOther;
        //            mariage.IdentityNoOther = identityNoOther;
        //            mariage.IsWordH = isWordH;
        //            mariage.CompanyOfH = companyOfH;
        //            mariage.PostOfH = postOfH;
        //            mariage.WeddingDay = weddingDay;
        //            mariage.IsCouple = isCouple;
        //            mariage.Tip = tip;
        //            mariage.IsDelete = false;
        //            mariage.Save();
        //        });
        //    }
        //    catch { return "fail"; }
        //    return "ok";
        //}

        public static int AddMariageForImport(string name, bool sex, DateTime birthday, string addressOfH, bool isGetTW, string identityType, string identityNo, string domicileOfH, string domicileOfTW, string addressOfTW, string education, string company, string post, string consort, bool sexOfC, string addressOfTWPEI, DateTime birthdayOfC, string domicileOfC, string identityTypeOther, string identityNoOther, bool isWordH, string companyOfH, string postOfH, DateTime weddingDay, bool isCouple, string tip)
        {
            int id = 0;
            //还差用户的身份证号和配偶的身份证号
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Mariage mariage = new Mariage();
                    mariage.Name = name;
                    mariage.Sex = sex;
                    mariage.Birthday = birthday;
                    mariage.AddressOfH = addressOfH;
                    mariage.IsGetTW = isGetTW;
                    mariage.IdentityType = identityType;
                    mariage.IdentityNo = identityNo;
                    mariage.DomicileOfH = domicileOfH;
                    mariage.DomicileOfTW = domicileOfTW;
                    mariage.AddressOfTW = addressOfTW;
                    mariage.Education = education;
                    mariage.Company = company;
                    mariage.Post = post;
                    mariage.Consort = consort;
                    mariage.SexOfC = sexOfC;
                    mariage.AddressOfTWPEI = addressOfTWPEI;
                    mariage.BirthdayOfC = birthdayOfC;
                    mariage.DomicileOfC = domicileOfC;
                    mariage.IdentityTypeOther = identityTypeOther;
                    mariage.IdentityNoOther = identityNoOther;
                    mariage.IsWordH = isWordH;
                    mariage.CompanyOfH = companyOfH;
                    mariage.PostOfH = postOfH;
                    mariage.WeddingDay = weddingDay;
                    mariage.IsCouple = isCouple;
                    mariage.Tip = tip;
                    mariage.IsDelete = false;
                    mariage.Save();
                    id = mariage.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static string UpdateMariage(int stuId, string name, bool sex, DateTime birthday, string addressOfH, bool isGetTW, string identityType, string identityNo, string domicileOfH, string domicileOfTW, string addressOfTW, string education, string company, string post, string consort, bool sexOfC, string addressOfTWPEI, DateTime birthdayOfC, string domicileOfC, string identityTypeOther, string identityNoOther, bool isWordH, string companyOfH, string postOfH, DateTime weddingDay, bool isCouple, string tip)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
               {
                   Mariage mariage = Mariage.FindById(stuId);
                   mariage.Name = name;
                   mariage.Sex = sex;
                   mariage.Birthday = birthday;
                   mariage.AddressOfH = addressOfH;
                   mariage.IsGetTW = isGetTW;
                   mariage.IdentityType = identityType;
                   mariage.IdentityNo = identityNo;
                   mariage.DomicileOfH = domicileOfH;
                   mariage.DomicileOfTW = domicileOfTW;
                   mariage.AddressOfTW = addressOfTW;
                   mariage.Education = education;
                   mariage.Company = company;
                   mariage.Post = post;
                   mariage.Consort = consort;
                   mariage.SexOfC = sexOfC;
                   mariage.AddressOfTWPEI = addressOfTWPEI;
                   mariage.BirthdayOfC = birthdayOfC;
                   mariage.DomicileOfC = domicileOfC;
                   mariage.IdentityTypeOther = identityTypeOther;
                   mariage.IdentityNoOther = identityNoOther;
                   mariage.IsWordH = isWordH;
                   mariage.CompanyOfH = companyOfH;
                   mariage.PostOfH = postOfH;
                   mariage.WeddingDay = weddingDay;
                   mariage.IsCouple = isCouple;
                   mariage.Tip = tip;
                   mariage.Save();
               });
            }
            catch (Exception ex)
            {
                return "fail";
            }
            return "ok";
        }

        public static string deleteMariage(int stuId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Mariage mariage = Mariage.FindById(stuId);
                    mariage.IsDelete = true;
                    mariage.Save();
                });
            }
            catch { return "fail"; }
            return "ok";
        }
        /// <summary>
        /// DbEntry分页查询数据，
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name="pagesize">gridview每页数据行数</param>
        /// <returns></returns>
        public static IPagedSelector<Mariage> GetMariages(int pagesize, string name, string sex_1, string getTw, string birth_start, string birth_end, string tw_name, string tw_sex, string workInDL, string marry_start, string marry_end, string iscouple)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!sex_1.Equals("99") && !sex_1.IsNullOrEmpty())
                {
                    con &= CK.K["Sex"] == sex_1.Equals("1");
                }
                if (!getTw.Equals("99") && !getTw.IsNullOrEmpty())
                {
                    con &= CK.K["IsGetTW"] == getTw.Equals("1");
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!tw_name.IsNullOrEmpty())
                {
                    con &= CK.K["Consort"].MiddleLike(tw_name);
                }
                if (!tw_sex.Equals("99") && !tw_sex.IsNullOrEmpty())
                {
                    con &= CK.K["SexOfC"] == tw_sex.Equals("1");
                }
                if (!workInDL.Equals("99") && !workInDL.IsNullOrEmpty())
                {
                    con &= CK.K["IsWordH"] == workInDL.Equals("1");
                }
                if (!marry_start.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Ge(new DateTime(Convert.ToInt32(marry_start), 1, 1));
                }
                if (!marry_end.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Le(new DateTime(Convert.ToInt32(marry_end), 12, 31));
                }
                if (!iscouple.Equals("99") && !iscouple.IsNullOrEmpty())
                {
                    con &= CK.K["IsCouple"] == iscouple.Equals("1");
                }
                var ps = DbEntry.From<Mariage>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetMariagesAsDataTable(string name, string sex_1, string getTw, string birth_start, string birth_end, string tw_name, string tw_sex, string workInDL, string marry_start, string marry_end, string iscouple)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!sex_1.Equals("99") && !sex_1.IsNullOrEmpty())
                {
                    con &= CK.K["Sex"] == sex_1.Equals("1");
                }
                if (!getTw.Equals("99") && !getTw.IsNullOrEmpty())
                {
                    con &= CK.K["IsGetTW"] == getTw.Equals("1");
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!tw_name.IsNullOrEmpty())
                {
                    con &= CK.K["Consort"].MiddleLike(tw_name);
                }
                if (!tw_sex.Equals("99") && !tw_sex.IsNullOrEmpty())
                {
                    con &= CK.K["SexOfC"] == tw_sex.Equals("1");
                }
                if (!workInDL.Equals("99") && !workInDL.IsNullOrEmpty())
                {
                    con &= CK.K["IsWordH"] == workInDL.Equals("1");
                }
                if (!marry_start.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Ge(new DateTime(Convert.ToInt32(marry_start), 1, 1));
                }
                if (!marry_end.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Le(new DateTime(Convert.ToInt32(marry_end), 12, 31));
                }
                if (!iscouple.Equals("99") && !iscouple.IsNullOrEmpty())
                {
                    con &= CK.K["IsCouple"] == iscouple.Equals("1");
                }
                var ps = DbEntry.From<Mariage>().Where(con).OrderBy((ASC)"Name").Select().ToDataTable();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetStatisticBySex(string sex_1, string birth_start, string birth_end, string marry_start, string marry_end, string domicileOfC)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!sex_1.Equals("99") && !sex_1.IsNullOrEmpty())
                {
                    con &= CK.K["Sex"] == sex_1.Equals("1");
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!marry_start.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Ge(new DateTime(Convert.ToInt32(marry_start), 1, 1));
                }
                if (!marry_end.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Le(new DateTime(Convert.ToInt32(marry_end), 12, 31));
                }
                if (!domicileOfC.Equals(""))
                {
                    con &= CK.K["DomicileOfC"].MiddleLike(domicileOfC);//配偶//con &= CK.K["DomicileOfTW"].MiddleLike(domicileOfTW);//本人
                }
                DataTable dt = DbEntry.From<Mariage>().Where(con).GroupBy<bool>("SexOfC").ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetAllData(string sex_1, string birth_start, string birth_end, string marry_start, string marry_end, string domicileOfC)
        {
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!sex_1.Equals("99") && !sex_1.IsNullOrEmpty())
                {
                    con &= CK.K["Sex"] == sex_1.Equals("1");
                }
                if (!birth_start.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Ge(new DateTime(Convert.ToInt32(birth_start), 1, 1));
                }
                if (!birth_end.IsNullOrEmpty())
                {
                    con &= CK.K["Birthday"].Le(new DateTime(Convert.ToInt32(birth_end), 12, 31));
                }
                if (!marry_start.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Ge(new DateTime(Convert.ToInt32(marry_start), 1, 1));
                }
                if (!marry_end.IsNullOrEmpty())
                {
                    con &= CK.K["WeddingDay"].Le(new DateTime(Convert.ToInt32(marry_end), 12, 31));
                }
                if (!domicileOfC.Equals(""))
                {
                    con &= CK.K["DomicileOfC"].MiddleLike(domicileOfC);//配偶//con &= CK.K["DomicileOfTW"].MiddleLike(domicileOfTW);//本人
                }
                DataTable dt = DbEntry.From<Mariage>().Where(con).Select().ToDataTable();
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
