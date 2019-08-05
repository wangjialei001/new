using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit;

namespace UnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string str=HttpPost("http://218.95.136.224:9001/NK_I/ws/rest/getAccessToken", "{\"sysCode\":\"GW_HOME\",\"sysPassword\":\"598BB15A80E4452F98A4593664F825D6\"}");
            Console.WriteLine(str);
            string tokenStr = string.Empty;
            var dic=new Dictionary<string, string>();
            dic.Add("token",tokenStr);
            string str1= HttpPost("http://218.95.136.224:9001/NK_I/ws/rest/dataExchange/tripAuditUsers", "{\"CreateBy\":\"211456\",\"RequestNo\":\"3aff76621e5e42649f49bfdf36311be7\",\"RequestTime\":\"2019-06-18 01:22:41\",\"CompanyCode\":\"640000999999\"}}",
                DctHeaderParams: dic);
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
        [Fact]
        public void Test3()
        {
            var eT = T1.TT;
            var i1 = (int)eT;
            var i = (int)T1.TT;
        }
        public enum T1
        {
            TT=1
        }

        [Fact]
        public void Test2()
        {
            List<Item> items = new List<Item>
            {
                new Item{ Id="1"},
                new Item{ Id="2"},
                new Item{ Id="3,4"},
                new Item{ Id="5,6"},
                new Item{ Id="7,8"},
                new Item{ },
                new Item{ Id="9,10,"},
            };
            var r = items.Where(t => t.Id != null && t.Id != "").Select(t => t.Id).SelectMany(t=> {
                return t.TrimEnd(',').Split(',');
            }).ToList();
            var its = new List<Item> {
                new Item{ Id="1"},
                new Item{ Id="4"}
            };
            items.RemoveAll(t=>!its.Select(i=>i.Id).Contains(t.Id));
        }
        public class Item
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
