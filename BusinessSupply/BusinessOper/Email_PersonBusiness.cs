using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataModel;
using Lephone.Data;
using System.Data;

namespace BusinessSupply.BusinessOper
{
    public class Email_PersonBusiness
    {


















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string AddEmail_Person(int emailid, int personmemberid, string emailaddress)
        {
            try
            {
                TEmail_Person emailperson = new TEmail_Person();
                emailperson.EmailAddress = emailaddress;
                emailperson.EmailId = emailid;
                emailperson.PersonMemberId = personmemberid;
                emailperson.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static DataTable GetEmail_PersonByEmailId(int emailid)
        {
            try
            {
                DataTable dt_emailperson = DbEntry.From<TEmail_Person>().Where(CK.K["EmailId"] == emailid).Select().ToDataTable();
                return dt_emailperson;
            }
            catch
            {
                return null;
            }
        }
    }
}
