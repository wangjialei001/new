using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Polly.Extensions.Http;

namespace ConsoleApp1
{
    public interface IHttpHelper
    {
        HttpClient GetInstance();
    }
    public class HttpHelper: IHttpHelper
    {
        private readonly IHttpClientFactory httpClientFactory;
        public HttpHelper(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
        }
        public HttpClient GetInstance()
        {
            return httpClientFactory.CreateClient();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services = services.AddHttpClient();
            services = services.AddSingleton<IHttpHelper, HttpHelper>();
            var httpHelper = services.BuildServiceProvider().GetService<IHttpHelper>();

            //var policyWait = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK).RetryAsync(3, (msg, count) =>
            //{
            //    Console.WriteLine(count);
            //});

            //var p = policyWait.ExecuteAsync(() =>
            //  {
            //      var _client = httpHelper.GetInstance();
            //      return _client.GetAsync("http://localhost:5000/api/values/2");
            //  });

            

            Console.WriteLine("Hello World!");
            //var client = new HttpClient(new RetryHandler(new HttpClientHandler()));
            var client = new HttpClient();
            var tks = new List<Task>();

            //try
            //{
            //    var _client = httpHelper.GetInstance();
            //    var rrr= _client.GetAsync("http://localhost:5000/api/values/" + 100).Result;
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            
            Thread th1 = new Thread(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    try
                    {
                        //Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK).Retry(1).Execute(() =>
                        //{
                        //    return client.GetAsync("http://localhost:5000/api/values/" + j).Result;
                        //});
                        //using (var _client = new HttpClient())
                        {
                            //var _client = new HttpClient();
                            //var _client = httpHelper.GetInstance();
                            //var r = _client.GetAsync("http://localhost:5000/api/values/" + i).Result;
                            //Console.WriteLine(r.Content.ReadAsStringAsync().Result);

                            Console.WriteLine(i);
                            var policyWait = Policy.HandleResult<HttpResponseMessage>(r => r.IsSuccessStatusCode == false || r.StatusCode != HttpStatusCode.OK).OrInner<Exception>().RetryAsync(3, (msg, count) =>
                               {
                                   Console.WriteLine(count);
                               });

                            var p = policyWait.ExecuteAsync(() =>
                            {
                                var _client = httpHelper.GetInstance();
                                return _client.GetAsync("http://localhost:5000/api/values/"+i);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });
            th1.IsBackground = true;
            th1.Start();

            //Thread th2 = new Thread(() =>
            //{
            //    for (var i = 100; i < 200; i++)
            //    {
            //        try
            //        {
            //            //Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK).Retry(1).Execute(() =>
            //            //{
            //            //    return client.GetAsync("http://localhost:5000/api/values/" + j).Result;
            //            //});
            //            //using (var _client = new HttpClient())
            //            {
            //                //var _client = new HttpClient();
            //                var _client = httpHelper.GetInstance();
            //                var r = _client.GetAsync("http://localhost:5000/api/values/" + i).Result;
            //                Console.WriteLine(r.Content.ReadAsStringAsync().Result);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }
            //    }
            //});
            //th2.IsBackground = true;
            //th2.Start();


            //Thread th3 = new Thread(() =>
            //{
            //    for (var i = 200; i < 300; i++)
            //    {
            //        try
            //        {
            //            //Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK).Retry(1).Execute(() =>
            //            //{
            //            //    return client.GetAsync("http://localhost:5000/api/values/" + j).Result;
            //            //});
            //            //using (var _client = new HttpClient())
            //            {
            //                //var _client = new HttpClient();
            //                var _client = httpHelper.GetInstance();
            //                var r = _client.GetAsync("http://localhost:5000/api/values/" + i).Result;
            //                Console.WriteLine(r.Content.ReadAsStringAsync().Result);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }
            //    }
            //});
            //th3.IsBackground = true;
            //th3.Start();

            //Thread th4 = new Thread(() =>
            //{
            //    for (var i = 300; i < 400; i++)
            //    {
            //        try
            //        {
            //            //Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.OK).Retry(1).Execute(() =>
            //            //{
            //            //    return client.GetAsync("http://localhost:5000/api/values/" + j).Result;
            //            //});
            //            //using (var _client = new HttpClient())
            //            {
            //                //var _client = new HttpClient();
            //                var _client = httpHelper.GetInstance();
            //                var r = _client.GetAsync("http://localhost:5000/api/values/" + i).Result;
            //                Console.WriteLine(r.Content.ReadAsStringAsync().Result);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw;
            //        }
            //    }
            //});
            //th4.IsBackground = true;
            //th4.Start();


            //client.GetAsync("http://localhost:5000/api/values");
            //client.GetAsync("http://localhost:5000/api/values");
            //client.GetAsync("http://localhost:5000/api/values");

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
    public class RetryHandler : DelegatingHandler
    {
        int count = 0;
        private const int maxRetries = 3;

        /// <summary>
        /// 重试
        /// </summary>
        /// <param name="innerHandler"></param>
        public RetryHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
        { }
        /// <summary>
        /// 重试
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            count++;
            HttpResponseMessage response = null;
            for (int i = 0; i < maxRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(count + "次；结果：" + result + "；时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
                    return response;
                }
            }
            return response;
        }
    }
}
