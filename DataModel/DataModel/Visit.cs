using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("VisitToPeople")]
   public  class Visit : DbObjectModel<Visit, int>
    {
        [AllowNull]
        public string Name { set; get; }
        [AllowNull]
        public string Relationship { set; get; }
        public DateTime? ArriveTime { set; get; }
        public DateTime? LeaveTime { set; get; }
        [AllowNull]
        public string Address { set; get; }
        [AllowNull]
        public string Phone { set; get; }
        public int? People_Id { set; get; }
        public bool? IsDelete { set; get; }
    }
}
