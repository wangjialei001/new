using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace New.Core
{
    public class TransientService: ITransientService
    {
        public async Task SayHello()
        {
            Console.WriteLine("TransientService");
            await Task.CompletedTask;
        }
    }
}
