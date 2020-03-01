using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("RegisterType")]
    public class RegisterType : DbObjectModel<RegisterType, int>
    {
        public int? RegisterTypeId { set; get; }
        [AllowNull]
        public string RegisterTypeText { set; get; }
        public bool? IsDelete { set; get; }
    }
}
