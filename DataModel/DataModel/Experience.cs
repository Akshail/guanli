using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
     [DbTable("ExperienceToStudent")]
    public class Experience : DbObjectModel<Experience, int>
    {
         public int? Student_Id { set; get; }
         public DateTime? StartTime { set; get; }
         public DateTime? EndTime { set; get; }
         [AllowNull]
         public string School { set; get; }
         public bool? IsDelete { set; get; }
    }
}
