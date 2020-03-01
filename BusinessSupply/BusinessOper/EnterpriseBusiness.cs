using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class EnterpriseBusiness
    {
        public static int AddEnterpriseForId()
        {
            int id = 0;
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Enterprise ent = new Enterprise();
                    ent.IsDelete = true;
                    ent.Save();
                    id = ent.Id;
                });
            }
            catch { return 0; }
            return id;
        }

        public static bool AddEnterpriseForNew(int id, string name, string domicile,  string referee, string contacts, string phoneNo, string email, string type, string investorOfTW, string investorOfH, string scope, string product)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Enterprise ent = Enterprise.FindById(id);
                    ent.Name = name;
                    ent.Domicile = domicile;
                    ent.Referee = referee;
                    ent.Contacts = contacts;
                    ent.PhoneNo = phoneNo;
                    ent.Email = email;
                    ent.Type = type;
                    ent.InvestorOfTW = investorOfTW;
                    ent.InvestorOfH = investorOfH;
                    ent.Scope = scope;
                    ent.Product = product;
                    ent.IsDelete = false;
                    ent.Save();
                });
            }
            catch { return false; }
            return true;
        }


        public static bool UpdateEnterprise(int enterpriseId, string name, string domicile, string referee, string contacts, string phoneNo, string email, string type,  string investorOfTW, string investorOfH,  string scope, string product)
        {
            try
            {
                Enterprise ent = Enterprise.FindById(enterpriseId);
                ent.Name = name;
                ent.Domicile = domicile;
                ent.Referee = referee;
                ent.Contacts = contacts;
                ent.PhoneNo = phoneNo;
                ent.Email = email;
                ent.Type = type;
                ent.InvestorOfTW = investorOfTW;
                ent.InvestorOfH = investorOfH;
                ent.Scope = scope;
                ent.Product = product;
                ent.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool deleteEnterprise(int enterpriseId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    Enterprise ent = Enterprise.FindById(enterpriseId);
                    ent.IsDelete = true;
                    ent.Save();
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
        public static IPagedSelector<Enterprise> GetEnterprises(int pagesize, string name, string referee, string contact)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (!name.IsNullOrEmpty())
                {
                    con &= CK.K["Name"].MiddleLike(name);
                }
                if (!referee.IsNullOrEmpty())
                {
                    con &= CK.K["Referee"].MiddleLike(referee);
                }
                if (!contact.IsNullOrEmpty())
                {
                    con &= CK.K["Contacts"].MiddleLike(contact);
                }
                var ps = DbEntry.From<Enterprise>().Where(con).OrderBy((ASC)"Name").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }
    }
}
