using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowItems
{
    public class WorkflowConfig
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public List<Step> Steps { get; set; }
    }
    public class Step
    {
        public string Id { get; set; }
        public string StepType { get; set; }
        public string NextStepId { get; set; }
    }
}
