using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp
{
    public class TestDelegate
    {
        public Dictionary<string, ReturnStrDelegate> Dic = new Dictionary<string, ReturnStrDelegate>();
        public delegate void ReturnStrDelegate(int i);
        public ReturnStrDelegate returnStr;
        public void GetId()
        {
            Thread td = new Thread(() =>
            {
                int i = 0;
                while (true)
                {
                    Thread.Sleep(2000);
                    i++;
                    if (returnStr != null)
                        returnStr(i);
                }
            });
            td.IsBackground = true;
            td.Start();
        }

        public delegate void ReturnIntDelegate(int i);
        public event ReturnIntDelegate returnInt;
    }
}
