using Microsoft.AspNetCore.Mvc;
using New.Model.LogDto;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Log.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController: ControllerBase
    {//{"AppName":"GwHomeAppApi","Level":"info","Message":"192.168.60.21:80;","ProcName":"GetVocherList","LoginID":"1643139914902016000","SourceType":"Android","Category":""}
        [HttpPost]
        public void Post(LogInputDto input)
        {
            Console.WriteLine("测试并发");
            if (input == null)
                return;
            var logger = LogManager.GetLogger(input.AppName);
            try
            {
                var ip = HttpContext.GetClientUserIp();
                LogLevel level = LogLevel.FromString(input.Level);
                var eventInfo = new LogEventInfo(level, logger.Name, input.Message);
                eventInfo.Properties["CustomValue"] = "My custom string"+ip;
                logger.Log(eventInfo);
                
                //Console.WriteLine("start write log");
                //if (input.Level == "info")
                //{
                //    logger.Info(input.Message);
                //    Console.WriteLine("info");
                //}
                //else if (input.Level == "error")
                //{
                //    logger.Error($"Message:{input.Message};Exceptioin:{input.Exception}");
                //    Console.WriteLine("error");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("end write log");
        }
    }
}
