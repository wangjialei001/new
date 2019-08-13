using Infrastructure.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace WebApp.Tasks
{
    public class TownCrier
    {
        readonly Timer timer;
        public TownCrier()
        {
            timer = new Timer(5000) { AutoReset = true };
            timer.Elapsed += (sender, eventArgs) => Action();
        }
        public void Start() { timer.Start(); }
        public void Stop() { timer.Stop(); }
        private void Action()
        {
            Task.Run(() => {
                LogCore.LogInfo("WebApp.Tasks", "TownCrier", $"MyService100 Run {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
        }
    }
}
