using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Tasks.MessageTask;

namespace Web.Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly IMessageSerivce messageSerivce;
        public SendMessageController(IMessageSerivce messageSerivce)
        {
            this.messageSerivce = messageSerivce;
        }
        public object SendEmail(int minutes, long userId)
        {
            try
            {
                var date = DateTime.Now;
                string jobId = BackgroundJob.Schedule(() => messageSerivce.SendEmail(userId, date), TimeSpan.FromMinutes(minutes));
                Console.WriteLine($"延时任务ID：{jobId}；{date}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return new { Ok = true };
        }
    }
}
