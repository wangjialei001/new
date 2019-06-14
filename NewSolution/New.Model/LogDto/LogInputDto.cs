using System;
using System.Collections.Generic;
using System.Text;

namespace New.Model.LogDto
{
    public class LogInputDto
    {
        public string IP { get; set; }
        public string AppName { get; set; }
        public string ProcName { get; set; }
        public string StackTrace { get; set; }
        public int Id { get; set; }
        public DateTime LongDate { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
