using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace New.Core
{
    public class ScopeService: IScopeService
    {
        private readonly ITransientService transientService;
        public ScopeService(ITransientService _transientService)
        {
            transientService = _transientService;
        }
        public async Task SayHello(ITransientService _transientService)
        {
            Console.WriteLine(_transientService == transientService);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Task.Run(()=> {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(_transientService == transientService);
            });
            Console.WriteLine($"在ScopeService调用testService哈希code是:{transientService.GetHashCode()}");
            Console.WriteLine("ScopeService");
            await Task.CompletedTask;
        }
    }
}
