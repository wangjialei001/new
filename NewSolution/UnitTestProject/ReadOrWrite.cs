using Infrastructure.SyncData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject
{
    public class ReadOrWrite
    {
        [Fact]
        public void Test1()
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                int j = i;
                tasks.Add(Task.Run(() =>
                {
                    Test2((j + 1).ToString());
                }));
            }
            tasks.Add(Task.Run(() =>
            {
                Test3();
            }));
            for (int i = 0; i < 10; i++)
            {
                int j = i;
                tasks.Add(Task.Run(() =>
                {
                    Test2((j+1).ToString());
                }));
            }
            Task.WaitAll(tasks.ToArray());
        }
        private void Test3()
        {
            string postData = "Rc6JYJb474IMFFXkviMNpTEy+LfokT/su0AwX464Y8LsV5Yrvp96v/Vna63dHwhS1GJ13Nv2V/8XUTj+n3hspyGSjaho82wOVmqv8cQu4WEPTz7c9l+f2TOLBFonMfhY2hbibeMmsL4h+/WWH1tUMp4rqhBHmbgCjldTvD7rKSWUyvuhLX9FJ3oNAG2ehbjzvOq6aYD2dId8RqyZjIBgdg42Q4LeV18ieMrwdKBTMNzVNwQMzllnIue4uawbmGKD";
            string result = HttpPost("http://localhost:8091/api/TripApply/TripApprovalResultNingXia", postData);
            Console.WriteLine($"{postData};{result}");
        }
        private void Test2(string m)
        {
            string postData = "{\"companyCode\":\"173104\",\"admDivCode\":110000,\"startTime\":\"2019-"+m+"-01\",\"endTime\":\"2019-"+m+"-26\"}";
            var dic = new Dictionary<string, string>();
            dic.Add("companycode", "173104");
            dic.Add("admdivcode", "110000");
            string result = HttpPost("http://localhost:8091/api/Voucher/GetARPVouchersAsync", postData, DctHeaderParams: dic);
            Console.WriteLine($"{postData};{result}");
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }
        public string HttpPost(string posturl, string postData, string contentType = "application/json", Dictionary<string, string> DctHeaderParams = null)
        {
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postData);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                // 设置参数
                HttpWebRequest request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                #region  添加头部参数
                if (DctHeaderParams != null && DctHeaderParams.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in DctHeaderParams)
                    {
                        if (item.Key == "Date")
                        {
                            request.Date = Convert.ToDateTime(item.Value);
                            // CallPrivateMethod(request, "SetSpecialHeaders", "Date", item.Value); // 反射更改Headers条件参数
                        }
                        else
                        {
                            request.Headers.Add(item.Key, Convert.ToString(item.Value));
                        }
                    }
                }
                #endregion
                request.ContentType = contentType;
                request.ContentLength = data.Length;
                byte[] buffer = encoding.GetBytes(postData); // 数据编码UTF-8格式
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
