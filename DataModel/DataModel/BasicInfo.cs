using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("BasicInfo")]
    public class BasicInfo : DbObjectModel<BasicInfo, int>
    {
        [AllowNull]
        public string ZipCode { set; get; }
        [AllowNull]
        public string DepartmentAddress { set; get; }
        [AllowNull]
        public string Name { set; get; }
        [AllowNull]
        public string Phone { set; get; }
        [AllowNull]
        public string Email { set; get; }
        public bool? IsDelete { set; get; }
    }
}
