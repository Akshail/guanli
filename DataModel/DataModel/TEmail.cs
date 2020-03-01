using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("Email")]
    public class TEmail : DbObjectModel<TEmail, int>
    {
        [AllowNull]
        public string Title { get; set; }
        [AllowNull]
        public string EmailContent { get; set; }
        public int? AssociationId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? DelTime { get; set; }
        public int? DelUserId { get; set; }
    }
}
