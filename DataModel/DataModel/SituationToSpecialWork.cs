using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("SituationToSpecialWork")]
    public class SituationToSpecialWork : DbObjectModel<SituationToSpecialWork, int>
    {
        public int? Communication_Id { set; get; }
        public DateTime? Time { set; get; }
        [AllowNull]
        public string Location { set; get; }
        [AllowNull]
        public string SituationName { set; get; }
        public bool? IsDelete { set; get; }
    }
}
