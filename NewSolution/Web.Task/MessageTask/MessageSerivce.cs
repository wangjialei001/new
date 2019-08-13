using Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Tasks.MessageTask
{
    public class MessageSerivce : IMessageSerivce
    {
        public void ReceiveMessage(string msg)
        {
            Task.Run(() => {
                LogCore.LogInfo("Web.Tasks", "ReceiveMessage", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
            Console.WriteLine($"{msg} ReceiveMessage");
        }

        public void SendMessage(string msg)
        {
            Task.Run(() => {
                LogCore.LogInfo("Web.Tasks", "SendMessage", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
            Console.WriteLine($"{msg} SendMessage");
        }
    }
}
