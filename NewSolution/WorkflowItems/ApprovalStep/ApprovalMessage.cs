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
    }
}
