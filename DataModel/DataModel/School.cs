using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("School")]
    public class School : DbObjectModel<School, int>
    {
        [AllowNull]
        public string SchoolName { set; get; }
        [AllowNull]
        public string SchoolCode { set; get; }
        public bool? IsDelete { set; get; }
    }
}
