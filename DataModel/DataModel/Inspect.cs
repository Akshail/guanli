using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("InspectToEnterprise")]
    public class Inspect : DbObjectModel<Inspect, int>
    {
        public int? Enterprise_Id { set; get; }
        public DateTime? Time { set; get; }
        [AllowNull]
        public string Unit { set; get; }
        [AllowNull]
        public string Result { set; get; }
        [AllowNull]
        public string PhotoName { set; get; }
        public bool? IsDelete { set; get; }
    }
}
