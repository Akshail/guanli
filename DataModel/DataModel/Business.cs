using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("BusinessToPeople")]
   public class Business : DbObjectModel<Business, int>
    {
        public int? People_Id { set; get; }
        [AllowNull]
        public string Scope { set; get; }
        [AllowNull]
        public string Type { set; get; }
        [AllowNull]
        public string RegisteredCapital { set; get; }
        [AllowNull]
        public string RegisteredPlace { set; get; }
        [AllowNull]
        public string Name { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string Postalcode { set; get; }
        public bool? IsDelete { set; get; }
    }
}
