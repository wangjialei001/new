//using LogDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.Api
{
    public class ApplicationLogModel//: LogModel
    {
        public string Application { get; set; }

        public string RequestMethod { get; set; }

        public string MachineName { get; set; }
    }
}
