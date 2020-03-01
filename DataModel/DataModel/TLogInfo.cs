using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("LogInfo")]
    public class TLogInfo : DbObjectModel<TLogInfo, int>
    {
        public int? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        [AllowNull]
        public string CreateUserName { get; set; }
        public int? CreateUserType { get; set; }
        public int? OperateType { get; set; }
        [AllowNull]
        public string OperateObject { get; set; }
        [AllowNull]
        public string Ip { get; set; }
        [AllowNull]
        public string Account { get; set; }
    }
}
