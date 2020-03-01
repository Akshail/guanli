using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.DataModel;
using Lephone.Data;
using System.Data;

namespace BusinessSupply.BusinessOper
{
    public class LogInfoBusiness
    {
        public static string AddLogInfo(int createuserid, DateTime createtime, int createusertype, string createusername, int operatetype, string operateobject, string ip, string account)
        {
            try
            {
                TLogInfo newlog = new TLogInfo();
                newlog.CreateUserId = createuserid;
                newlog.CreateTime = createtime;
                newlog.CreateUserName = createusername;
                newlog.CreateUserType = createusertype;
                newlog.OperateObject = operateobject;
                newlog.OperateType = operatetype;
                newlog.Ip = ip;
                newlog.Account = account;
                newlog.Save();
                return "1";
            }
            catch
            {
                return "0";
            }
        }



















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        public static IPagedSelector<TLogInfo> GetLogInfoList(int pagesize)
        {

                try
                {
                    var ps = DbEntry
                        .From<TLogInfo>()
                        .Where(Condition.Empty)
                        .OrderBy((DESC)"CreateTime")
                        .PageSize(pagesize)
                        .GetPagedSelector();
                    return ps;
                }
                catch
                {
                    return null;
                }
      
        }
    }
}
