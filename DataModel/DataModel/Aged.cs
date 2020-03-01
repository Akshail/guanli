using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
   [DbTable("AgedToPeople")]
    public class Aged : DbObjectModel<Aged, int>
    {
       [AllowNull]
       public string Health { set; get; }
       [AllowNull]
       public string Tendance { set; get; }
       [AllowNull]
       public string Relationship { set; get; }
       public int? People_Id { set; get; }
       [AllowNull]
       public string Phone { set; get; }
       [AllowNull]
       public string Type { set; get; }
       public bool? IsDelete { set; get; }
    }
}
