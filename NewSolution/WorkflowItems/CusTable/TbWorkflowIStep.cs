using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorkflowItems.CusTable
{
    [Table("Tb_WorkflowIStep")]
    public class TbWorkflowIStep
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string StepId { get; set; }
        [StringLength(100)]
        public string StepType { get; set; }
        [StringLength(100)]
        public string Condition { get; set; }

        public int? CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}
