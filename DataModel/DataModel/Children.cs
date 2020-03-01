using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("ChildrenToMariage")]
   public  class Children : DbObjectModel<Children, int>
    {
        public bool? IsDelete { set; get; }
        public int? Mariage_Id { set; get; }
        [AllowNull]
        public string Name { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string Tip { set; get; }
    }
}
