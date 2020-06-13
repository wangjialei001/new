using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Tasks.MessageTask
{
    public interface IMessageSerivce
    {
        void SendMessage(string msg);
        void ReceiveMessage(string msg);
        void SendEmail(long userId,DateTime startTime);
    }
}
