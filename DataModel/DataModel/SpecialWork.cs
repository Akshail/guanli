﻿using Lephone.Data.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.DataModel
{
    [DbTable("SpecialWork")]
    public class SpecialWork : DbObjectModel<SpecialWork, int>
    {
        [AllowNull]
        public string Name { set; get; }
        public int? SpecialWorkTypeId { set; get; }
        public DateTime? WorkStartDate { set; get; }
        public DateTime? WorkEndDate { set; get; }
        public bool? Sex { set; get; }
        public DateTime? Birthday { set; get; }
        [AllowNull]
        public string Company { set; get; }
        [AllowNull]
        public string PhoneNo { set; get; }
        [AllowNull]
        public string Address { set; get; }
        public double? Ballot { set; get; }
        public bool? IsReelect { set; get; }
        [AllowNull]
        public string Background { set; get; }
        [AllowNull]
        public string Exchange { set; get; }
        [AllowNull]
        public string Type { set; get; }
        [AllowNull]
        public string Situation { set; get; }
        [AllowNull]
        public string PhotoName { set; get; }
        public bool? IsDelete { set; get; }
    }
}
