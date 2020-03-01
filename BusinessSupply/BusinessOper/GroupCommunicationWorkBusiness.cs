using DataModel.DataModel;
using Lephone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSupply.BusinessOper
{
    public class GroupCommunicationWorkBusiness
    {
        public static string DeleteGroupCommunication(int groupCommunicationId)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    GroupCommunicationWork communication = GroupCommunicationWork.FindById(groupCommunicationId);
                    communication.IsDelete = true;
                    communication.Save();
                });
            }
            catch { return "false"; }
            return "ok";
        }

        public static IPagedSelector<GroupCommunicationWork> GetCommunications(int pagesize, int communicationTypeId, string groupArea)
        {
            //查询
            try
            {
                Condition con = CK.K["IsDelete"] == false;
                if (communicationTypeId != 0)
                {
                    con &= CK.K["CommunicationTypeId"] == communicationTypeId;
                }
                if (!groupArea.Equals(""))
                {
                    con &= CK.K["GroupArea"].MiddleLike(groupArea);
                }
                var ps = DbEntry.From<GroupCommunicationWork>().Where(con).OrderBy((ASC)"Id").PageSize(pagesize).GetPagedSelector();
                return ps;
            }
            catch
            {
                return null;
            }
        }

        public static bool AddGroupCommunication(int communicationTypeId, string groupArea, string groupName, string groupMonitor, string groupMembers, string communicationGenerate, string communicationSummarize, string communicationReport, string communicationPhotos)
        {
            try
            {
                DbEntry.UsingTransaction(delegate
                {
                    GroupCommunicationWork gcw = new GroupCommunicationWork();
                    gcw.CommunicationTypeId = communicationTypeId;
                    gcw.GroupArea = groupArea;
                    gcw.GroupName = groupName;
                    gcw.GroupMonitor = groupMonitor;
                    gcw.GroupMembers = groupMembers;
                    gcw.CommunicationGenerate = communicationGenerate;
                    gcw.CommunicationSummarize = communicationSummarize;
                    gcw.CommunicationReport = communicationReport;
                    gcw.CommunicationPhotos = communicationPhotos;
                    gcw.IsDelete = false;
                    gcw.Save();
                });
            }
            catch { return false; }
            return true;
        }

        public static bool UpdateGroupCommunication(int groupCommunicationId, int communicationTypeId, string groupArea, string groupName, string groupMonitor, string groupMembers, string communicationGenerate, string communicationSummarize, string communicationReport, string communicationPhotos)
        {
            try
            {
                GroupCommunicationWork gcw = GroupCommunicationWork.FindById(groupCommunicationId);
                gcw.CommunicationTypeId = communicationTypeId;
                gcw.GroupArea = groupArea;
                gcw.GroupName = groupName;
                gcw.GroupMonitor = groupMonitor;
                gcw.GroupMembers = groupMembers;
                gcw.CommunicationGenerate = communicationGenerate;
                gcw.CommunicationSummarize = communicationSummarize;
                gcw.CommunicationReport = communicationReport;
                gcw.CommunicationPhotos = communicationPhotos;
                gcw.Save();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
