using Microsoft.AspNetCore.Mvc;
using New.Model.Log;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogControlloer: ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] LogInputDto input)
        {
            if (input == null)
                return;
            var logger = LogManager.GetLogger(input.AppName);
            try
            {
                if (input.Level == "info")
                {
                    logger.Info(input.Message);
                }
                else if (input.Level == "error")
                {
                    logger.Error($"Message:{input.Message};Exceptioin:{input.Exception}");
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
