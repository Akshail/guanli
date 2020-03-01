using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("Enterprise")]
    public class Enterprise : DbObjectModel<Enterprise, int>
    {
        [AllowNull]
        public string Name { set; get; }
        [AllowNull]
        public string Domicile { set; get; }
        [AllowNull]
        public string Referee { set; get; }
        [AllowNull]
        public string Contacts { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string Email { set; get; }
        [AllowNull]
        public string Type { set; get; }
        [AllowNull]
        public string InvestorOfTW { set; get; }
        [AllowNull]
        public string InvestorOfH { set; get; }
        [AllowNull]
        public string Scope { set; get; }
        [AllowNull]
        public string Product { set; get; }
        public bool? IsDelete { set; get; }
    }
}
