using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("RelativeToPeople")]
   public class Relative : DbObjectModel<Relative, int>
    {
        [AllowNull]
        public string Name { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string WorkingCondition { set; get; }
        [AllowNull]
        public string Company { set; get; }
        public int? People_Id { set; get; }
        [AllowNull]
        public string Relationship { set; get; }
        public bool? IsDelete { set; get; }
    }
}
