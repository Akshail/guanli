using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("Email_Person")]
    public class TEmail_Person : DbObjectModel<TEmail_Person, int>
    {
        public int? EmailId { get; set; }
        public int? PersonMemberId { get; set; }

        [AllowNull]
        public string EmailAddress { get; set; }
    }
}
