using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("Mariage")]
   public class Mariage : DbObjectModel<Mariage, int>
    {
        public bool? IsDelete { set; get; }
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        [AllowNull]
        public string AddressOfH { set; get; }
        public bool? IsGetTW { set; get; }
        [AllowNull]
        public string DomicileOfH { set; get; }
        [AllowNull]
        public string IdentityType { set; get; }
        [AllowNull]
        public string IdentityNo { set; get; }
        [AllowNull]
        public string AddressOfTW { set; get; }
        [AllowNull]
        public string DomicileOfTW { set; get; }
        [AllowNull]
        public string Company { set; get; }
        [AllowNull]
        public string Post { set; get; }
        [AllowNull]
        public string Education { set; get; }
        [AllowNull]
        public string Consort { set; get; }
        public bool? SexOfC { set; get; }
        public DateTime? BirthdayOfC { set; get; }
        [AllowNull]
        public string IdentityTypeOther { set; get; }
        [AllowNull]
        public string IdentityNoOther { set; get; }
        [AllowNull]
        public string DomicileOfC { set; get; }
        public bool? IsWordH { set; get; }
        [AllowNull]
        public string CompanyOfH { set; get; }
        [AllowNull]
        public string PostOfH { set; get; }
        public DateTime? WeddingDay { set; get; }
        public bool? IsCouple { set; get; }
        [AllowNull]
        public string Tip { set; get; }
        [AllowNull]
        public string AddressOfTWPEI { set; get; }
    }
}
