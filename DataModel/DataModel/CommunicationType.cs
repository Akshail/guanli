using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("CommunicationType")]
    public class CommunicationType : DbObjectModel<CommunicationType, int>
    {
        public int? CommunicationTypeId { set; get; }
        [AllowNull]
        public string CommunicationTypeText { set; get; }
        public bool? IsDelete { set; get; }
    }
}
