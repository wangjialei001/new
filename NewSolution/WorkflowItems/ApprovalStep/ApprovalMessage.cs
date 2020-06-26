using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowItems
{
    public class ApprovalMessage
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public int State { get; set; }
        public int Id { get; set; }
        public ApprovalFlow ApprovalFlow { get; set; }
    }
    public class ApprovalFlow
    {
        public int Id { get; set; }
        public ApprovalMessage ApprovalMessage { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public int State { get; set; }
    }
}
