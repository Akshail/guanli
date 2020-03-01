using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("StudyToPeople")]
    public class Study : DbObjectModel<Study, int>
    {
        public int? People_Id { set; get; }
        [AllowNull]
        public string School { set; get; }
        [AllowNull]
        public string Grade { set; get; }
        [AllowNull]
        public string Major { set; get; }
        [AllowNull]
        public string Protector { set; get; }
        [AllowNull]
        public string Relationship { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        public bool? IsDelete { set; get; }
    }
}
