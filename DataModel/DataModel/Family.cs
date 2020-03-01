using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("FamilyToPeople")]
    public class Family : DbObjectModel<Family, int>
    {
        public int? People_Id { set; get; }
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        [AllowNull]
        public string Relationship { set; get; }
        [AllowNull]
        public string Company { set; get; }
        [AllowNull]
        public string Tip { set; get; }
        public bool? IsDelete { set; get; }
    }
}
