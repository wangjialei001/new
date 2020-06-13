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
            Task.Run(() =>
            {
                LogCore.LogInfo("Web.Tasks", "ReceiveMessage", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
            Console.WriteLine($"{msg} ReceiveMessage");
        }

        public void SendMessage(string msg)
        {
            Task.Run(() =>
            {
                LogCore.LogInfo("Web.Tasks", "SendMessage", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
            Console.WriteLine($"{msg} SendMessage");
        }
        public void SendEmail(long userId, DateTime startTime)
        {
            Console.WriteLine($"用户：{userId}已发送邮件；接口请求时间：{startTime.ToString("yyyy-MM-dd HH:mm:ss fff")}；发送时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
        }
    }
}
