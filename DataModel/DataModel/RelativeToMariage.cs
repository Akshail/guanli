using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("RelativeToMariage")]
   public  class RelativeToMariage : DbObjectModel<RelativeToMariage, int>
    {
        public bool? IsDelete { set; get; }
        public int? Mariage_Id { set; get; }
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        [AllowNull]
        public string Relationship { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string Tip { set; get; }
    }
}
