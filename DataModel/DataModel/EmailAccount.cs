using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("EmailAccount")]
    public class EmailAccount : DbObjectModel<EmailAccount, int>
    {
        public int? UserId { set; get; }
        [AllowNull]
        public string EmailServer { set; get; }
        [AllowNull]
        public string Account { set; get; }
        [AllowNull]
        public string Password { set; get; }
        public bool? IsDelete { set; get; }
    }
}
