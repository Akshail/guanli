using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("People")]
    public class People : DbObjectModel<People, int>
    {
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        public DateTime? ArriveDate { set; get; }
        [AllowNull]
        public string IdentityNo { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string TempApplication { set; get; }
        public int? ComeReason { set; get; }
        [AllowNull]
        public string Tip { set; get; }
        public bool? IsDelete { set; get; }
    }
}
