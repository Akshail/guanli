using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("MoneyType")]
    public class MoneyType : DbObjectModel<MoneyType, int>
    {
        public int? MoneyTypeId { set; get; }
        [AllowNull]
        public string MoneyTypeText { set; get; }
        public bool? IsDelete { set; get; }
    }
}
