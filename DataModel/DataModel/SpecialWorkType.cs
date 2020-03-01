using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("SpecialWorkType")]
    public class SpecialWorkType : DbObjectModel<SpecialWorkType, int>
    {
        public int? SpecialWorkTypeId { set; get; }
        [AllowNull]
        public string SpecialWorkTypeText { set; get; }
        public bool? IsDelete { set; get; }
    }
}
