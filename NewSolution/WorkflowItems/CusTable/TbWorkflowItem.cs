using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkflowItems.CusTable
{
    [Table("Tb_WorkflowItem")]
    public class TbWorkflowItem
    {
        public int Id { get; set; }
        
        public string Item { get; set; }

        public int? CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int IsDeleted { get; set; }
    }
}
