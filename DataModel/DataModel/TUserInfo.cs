using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lephone.Data.Definition;

namespace DataModel.DataModel
{
    [DbTable("UserInfo")]
    public class TUserInfo : DbObjectModel<TUserInfo, int>
    {
        [AllowNull]
        public string Account { get; set; }
        [AllowNull]
        public string Pwd { get; set; }
        [AllowNull]
        public string Name { get; set; }
        public int? UserTypeId { get; set; }
        [AllowNull]
        public string IDCardNum { set; get; }
        [AllowNull]
        public string Phone { set; get; }
        public int? AssociationId { get; set; }
        public int? PersonMemberId { get; set; }
        public int? GroupMemberId { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? UpdataUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? DelUserId { get; set; }
        public DateTime? DelTime { get; set; }
        public bool? IsDel { get; set; }
        [AllowNull]
        public string Memo { get; set; }
    }
}
