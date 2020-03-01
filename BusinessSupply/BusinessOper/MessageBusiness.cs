using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class MessageBusiness
    {






















        //前科协代码
        //将需要的代码都放在上面，日后将一下代码全部删除
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static TMessage AddMessage(int associationid, int createuserid, DateTime createtime, string content)
        {
            try
            {
                TMessage newMSG = new TMessage();
                newMSG.AssociationId = associationid;
                newMSG.CreateTime = createtime;
                newMSG.CreateUserId = createuserid;
                newMSG.MessageContent = content;
                newMSG.IsDel = false;
                newMSG.Save();
                return newMSG;
            }
            catch
            {
                return null;
            }
        }

        public static TMessage GetMessageById(int id)
        {
            try
            {
                TMessage msg = TMessage.FindById(id);
                return msg;
            }
            catch
            {
                return null;
            }
        }

        public static IPagedSelector<TMessage> GetMessageByAssociationId(int associationid, string start, string end, int pagesize)
        {
            try
            {
                IPagedSelector<TMessage> ps;
                if (start != "" && end != "")
                {
                    DateTime s = Convert.ToDateTime(start);
                    DateTime e = Convert.ToDateTime(end);
                    e = e.AddDays(1);
                    ps = DbEntry.From<TMessage>()
                        .Where(CK.K["AssociationId"] == associationid && CK.K["CreateTime"] >= s && CK.K["CreateTime"] <= e && CK.K["IsDel"] == false)
                        //.OrderBy((DESC)"CreateTime")
                       .OrderBy("Id")
                        .PageSize(pagesize)
                        .GetPagedSelector();
                  //  return ps;
                }
                else
                {
                    ps = DbEntry.From<TMessage>()
                                       .Where(CK.K["AssociationId"] == associationid && CK.K["IsDel"] == false)
                        //.OrderBy((DESC)"Id")
                                          .OrderBy("Id")
                                           .PageSize(pagesize)
                                           .GetPagedSelector();
                   // return ps;
                }

                 return ps;
            }
            catch
            {
                return null;
            }
        }
    }
}
