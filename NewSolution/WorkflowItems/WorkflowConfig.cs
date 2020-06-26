using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowItems
{
    //StepSourceV1原始结构
    public class WorkflowConfig
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string DataType { get; set; }
        public List<Step> Steps { get; set; }
    }
    public class Step
    {
        public string Id { get; set; }
        public string StepType { get; set; }
        public string NextStepId { get; set; }
        public Dictionary<string, string> Inputs { get; set; }
        public List<Step> Do { get; set; }
    }
    public class ControlData
    {
        public readonly static string If = "WorkflowCore.Primitives.If, WorkflowCore";
    }
}
