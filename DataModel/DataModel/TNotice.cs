using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;


namespace DataModel.DataModel
{
    [DbTable("Notice")]
    public class TNotice : DbObjectModel<TNotice, int>
    {
        [AllowNull]
        public string Title { get; set; }
        [AllowNull]
        public string NoticeContent { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int? UpdataUserId { get; set; }

        public DateTime? ShowTime { get; set; }

        public bool? IsDel { get; set; }

        public DateTime? DelTime { get; set; }

        public int? DelUserId { get; set; }
    }
}
