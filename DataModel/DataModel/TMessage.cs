using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("Message")]

    public class TMessage : DbObjectModel<TMessage, int>
    {
        [AllowNull]
        public string MessageContent { get; set; }
        public int? AssociationId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? DelTime { get; set; }
        public int? DelUserId { get; set; }
    }
}
