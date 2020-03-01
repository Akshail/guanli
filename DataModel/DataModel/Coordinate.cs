using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("CoordinateToEnterprise")]
   public class Coordinate : DbObjectModel<Coordinate, int>
    {
        public int? Enterprise_Id { set; get; }
        public DateTime? Time { set; get; }
        [AllowNull]
        public string Problem { set; get; }
        public bool? IsDelete { set; get; }
    }
}
