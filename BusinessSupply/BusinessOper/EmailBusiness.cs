using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataModel;
using Lephone.Data;
using System.Data;

namespace BusinessSupply.BusinessOper
{
    public class EmailBusiness
    {


















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static TEmail AddEmail(int associationid,int createuserid, DateTime createtime, string title, string content)
        {
            try
            {
                TEmail newemail = new TEmail();
                newemail.AssociationId = associationid;
                newemail.CreateTime = createtime;
                newemail.CreateUserId = createuserid;
                newemail.EmailContent = content;
                newemail.Title = title;
                newemail.IsDel = false;
                newemail.Save();
                return newemail;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TEmail> GetEmail(string start, string end, int pagesize)
        {
            try
            {
                IPagedSelector<TEmail> ps;
                if (start != "" && end != "")
                {
                    DateTime s = Convert.ToDateTime(start);
                    DateTime e = Convert.ToDateTime(end);
                    e = e.AddDays(1);
                    ps = DbEntry.From<TEmail>()
                        .Where(CK.K["CreateTime"] >= s && CK.K["CreateTime"] <= e && CK.K["IsDel"] == false)
                       .OrderBy((DESC)"CreateTime")
                        .PageSize(pagesize)
                        .GetPagedSelector();
                }
                else
                {
                    ps = DbEntry.From<TEmail>()
                                       .Where(CK.K["IsDel"] == false)
                                          .OrderBy((DESC)"CreateTime")
                                           .PageSize(pagesize)
                                           .GetPagedSelector();
                }

                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TEmail> GetEmailByAssociationId(int associationid,string start, string end, int pagesize)
        {
            try
            {
                IPagedSelector<TEmail> ps;
                if (start != "" && end != "")
                {
                    DateTime s = Convert.ToDateTime(start);
                    DateTime e = Convert.ToDateTime(end);
                    e = e.AddDays(1);
                    ps = DbEntry.From<TEmail>()
                        .Where(CK.K["AssociationId"] == associationid && CK.K["CreateTime"] >= s && CK.K["CreateTime"] <= e && CK.K["IsDel"] == false)
                       .OrderBy((DESC)"CreateTime")
                        .PageSize(pagesize)
                        .GetPagedSelector();
                }
                else
                {
                    ps = DbEntry.From<TEmail>()
                                       .Where(CK.K["AssociationId"] == associationid&&CK.K["IsDel"] == false)
                                          .OrderBy((DESC)"CreateTime")
                                           .PageSize(pagesize)
                                           .GetPagedSelector();
                }

                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static TEmail GetEmailById(int id)
        {
            try
            {
                TEmail email = TEmail.FindById(id);
                return email;
            }
            catch
            {
                return null;
            }
        }

    }
}
