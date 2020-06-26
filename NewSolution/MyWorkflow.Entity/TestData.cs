using System;
using System.Collections.Generic;

namespace MyWorkflow.Entity
{
    public class TestData
    {
        public static List<User> Users = new List<User>() { new User { Id=1,Name="Jone"},new User{Id=2,Name="Alice" },new User { Id=3,Name="Lee"} };
        public static List<ApprovalInfo> ApprovalInfos = new List<ApprovalInfo> { new ApprovalInfo { Id = 1, UserId = 1, Message = "请假1天", State = 0, CurrentOrder = 1 } };
        public static List<ApprovalFlowInfo> ApprovalFlowInfos = new List<ApprovalFlowInfo> { new ApprovalFlowInfo { Id = 1, UserId = 2, State = 0, Order = 1,ApprovalId=1 }, new ApprovalFlowInfo { Id = 2, UserId = 3, State = 0, Order=2, ApprovalId = 1 } };
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ApprovalInfo
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int State { get; set; }
        public int CurrentOrder { get; set; }
    }
    public class ApprovalFlowInfo
    {
        public int Id { get; set; }
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int State { get; set; }
        public string Remark { get; set; }
        public int Order { get; set; }
    }
}
