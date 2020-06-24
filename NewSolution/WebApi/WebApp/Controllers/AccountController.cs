using Data.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.TestDelegate;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly TestDelegate _testDelegate;
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService, TestDelegate testDelegate)
        {
            this.accountService = accountService;
            _testDelegate = testDelegate;
        }
        [HttpGet]
        public async Task GetId(string name)
        {
            ReturnStrDelegate action = (i) =>
             {
                 Console.WriteLine($"name:{name},i={i}");
             };
            _testDelegate.returnStr += action;
            _testDelegate.Dic.Add(name, action);
            _testDelegate.returnInt += (i)=> { };
            await Task.CompletedTask;
        }
        [HttpGet]
        public async Task GetRemove(string name)
        {
            ReturnStrDelegate returnStrDelegate;
            _testDelegate.Dic.TryGetValue(name, out returnStrDelegate);
            _testDelegate.Dic.Remove(name);
            _testDelegate.returnStr -= returnStrDelegate;
            await Task.CompletedTask;
        }
        [HttpGet]
        public async Task<int> GetDelegateCount()
        {
            return await Task.FromResult(_testDelegate.returnStr.GetInvocationList().Length);
        }
        [HttpGet]
        public async Task<object> Transfer()
        {
            return await accountService.Transfer();
        }
    }
}
