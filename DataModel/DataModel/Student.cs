using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("Student")]
    public class Student : DbObjectModel<Student, int>
    {
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        [AllowNull]
        public string IdentityNo { set; get; }
        [AllowNull]
        public string School { set; get; }
        public int? StudyTypeId { set; get; }
        public DateTime? JoinSchoolDate { set; get; }
        [AllowNull]
        public string Major { set; get; }
        [AllowNull]
        public string SystemOfEdu { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string AddressOfH { set; get; }
        [AllowNull]
        public string Tip { set; get; }
        public bool? IsDelete { set; get; }
    }
}
