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
    {
        [HttpPost]
        public void Post([FromBody] LogInputDto input)
        {
            if (input == null)
                return;
            var logger = LogManager.GetLogger(input.AppName);
            try
            {
                Console.WriteLine("start write log");
                if (input.Level == "info")
                {
                    logger.Info(input.Message);
                    Console.WriteLine("info");
                }
                else if (input.Level == "error")
                {
                    logger.Error($"Message:{input.Message};Exceptioin:{input.Exception}");
                    Console.WriteLine("error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("end write log");
        }
    }
}
