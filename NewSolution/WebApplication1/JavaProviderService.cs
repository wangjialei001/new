using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class JavaProviderService : IJavaProviderService
    {
        private readonly HttpClient _httpClient;

        public JavaProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetValueAsync()
        {
            var result = await _httpClient.GetStringAsync("hello/sayhello/java");
            return result;
        }
    }
}
