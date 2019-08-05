using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace New.Entity
{
    [Table("T_GWZJ_VoucherSync")]
    public class VoucherSync
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string VoucherId { get; set; }
        public int Status { get; set; }
        public string ReimUserId { get; set; }
        public string ReimUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
