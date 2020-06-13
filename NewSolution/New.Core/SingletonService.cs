using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace New.Core
{
    public class SingletonService: ISingletonService
    {
        private readonly ITransientService testService;
        public SingletonService(ITransientService _testService)
        {
            testService = _testService;
        }
        public async Task SayHello()
        {
            Console.WriteLine($"在SingletonService调用testService哈希code是:{testService.GetHashCode()}");
            Console.WriteLine("I am Singleton");
            await Task.CompletedTask;
        }
    }
}
