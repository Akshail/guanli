using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("WorkToPeople")]
  public class Work : DbObjectModel<Work, int>
    {
        public int? People_Id { set; get; }
        [AllowNull]
        public string Company { set; get; }
        [AllowNull]
        public string Post { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string Type { set; get; }
        public bool? IsDelete { set; get; }
    }
}
