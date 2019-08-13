using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Tasks;

namespace WebApp.Tasks
{
    #region snippet_CustomWebHostService
    [DesignerCategory("Code")]
    internal class CustomWebHostService : WebHostService
    {
        private ILogger _logger;
        TownCrier townCrier = null;

        public CustomWebHostService(IWebHost host) : base(host)
        {
            _logger = host.Services
                .GetRequiredService<ILogger<CustomWebHostService>>();
            townCrier = new TownCrier();
        }

        protected override void OnStarting(string[] args)
        {
            townCrier.Start();
            _logger.LogInformation("OnStarting method called.");
            base.OnStarting(args);
        }

        protected override void OnStarted()
        {
            _logger.LogInformation("OnStarted method called.");
            base.OnStarted();
        }

        protected override void OnStopping()
        {
            townCrier.Stop();
            _logger.LogInformation("OnStopping method called.");
            base.OnStopping();
        }
    }
    #endregion
}
