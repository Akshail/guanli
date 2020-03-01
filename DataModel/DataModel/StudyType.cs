using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("StudyType")]
    public class StudyType : DbObjectModel<StudyType, int>
    {
        public int? StudyTypeId { set; get; }
        [AllowNull]
        public string StudyTypeText { set; get; }
        public bool? IsDelete { set; get; }
    }
}
