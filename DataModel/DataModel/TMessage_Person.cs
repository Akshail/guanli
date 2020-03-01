using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("Message_Person")]
    public class TMessage_Person : DbObjectModel<TMessage_Person, int>
    {
        public int? MessageId { get; set; }
        public int? PersonMemberId { get; set; }

        [AllowNull]
        public string Cellphone { get; set; }
    }
}
