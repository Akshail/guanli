using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("GroupCommunicationWork")]
    public class GroupCommunicationWork : DbObjectModel<GroupCommunicationWork, int>
    {
        public int? CommunicationTypeId { set; get; }
        [AllowNull]
        public string GroupArea { set; get; }
        [AllowNull]
        public string GroupName { set; get; }
        [AllowNull]
        public string GroupMonitor { set; get; }
        [AllowNull]
        public string GroupMembers { set; get; }
        [AllowNull]
        public string CommunicationGenerate { set; get; }
        [AllowNull]
        public string CommunicationSummarize { set; get; }
        [AllowNull]
        public string CommunicationReport { set; get; }
        [AllowNull]
        public string CommunicationPhotos { set; get; }
        public bool? IsDelete { set; get; }
    }
}
