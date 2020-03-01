using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class Message_PersonBusiness
    {





















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string AddMessage_Person(int messageid, int personmemberid, string cellphone)
        {
            try
            {
                TMessage_Person MSGperson = new TMessage_Person();
                MSGperson.Cellphone = cellphone;
                MSGperson.MessageId = messageid;
                MSGperson.PersonMemberId = personmemberid;
                MSGperson.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }

        public static DataTable GetMessage_PersonByMessageId(int msgid)
        {
            try
            {
                DataTable dt_msgperson = DbEntry.From<TMessage_Person>().Where(CK.K["MessageId"] == msgid).Select().ToDataTable();
                return dt_msgperson;
            }
            catch
            {
                return null;
            }
        }
    }
}
