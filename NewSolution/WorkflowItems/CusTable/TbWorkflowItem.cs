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
    //{"Id":"Approval","Version":1,"DataType":"WorkflowItems.ApprovalMessage,WorkflowItems","Steps":[{"Id":"ApprovalWrokflowStep","StepType":"WorkflowItems.ApprovalWrokflowStep,WorkflowItems","NextStepId":"ApprovalNext","Inputs":{"approvalMessage":"data"}},{"Id":"ApprovalNext","StepType":"WorkflowCore.Primitives.If, WorkflowCore","Inputs":{"Condition":"data.State==0 || data.State!=99"},"Do":[[{"Id":"Approvaling","NextStepId":"ApprovalFinished","StepType":"WorkflowItems.ApprovalingWrokflowStep,WorkflowItems","Inputs":{"approvalMessage":"data"},"Outputs":{"Message":"approvalMessage.Message","Id":"approvalMessage.Id","State":"approvalMessage.State"}}]]},{"Id":"ApprovalFinished","StepType":"WorkflowCore.Primitives.If, WorkflowCore","NextStepId":"ApprovalStateStep2","Inputs":{"Condition":"data.State==1"},"Do":[[{"Id":"ApprovedSuccess","StepType":"WorkflowItems.ApprovedWrokflowStep,WorkflowItems","Inputs":{"approvalMessage":"data"}}]]},{"Id":"ApprovalStateStep2","StepType":"WorkflowCore.Primitives.If, WorkflowCore","Inputs":{"Condition":"data.State==2"},"Do":[[{"Id":"ApprovedRefused","StepType":"WorkflowItems.ApprovedFailedWrokflowStep,WorkflowItems","Inputs":{"approvalMessage":"data"}}]]}]}
}
