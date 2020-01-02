using GWHome.RocketMQ.Core;
using Microsoft.Extensions.Configuration;
using NewLife.Log;
using NewLife.RocketMQ;
using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestProject
{
    public class Class1
    {
        private ITestOutputHelper output;
        public Class1(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void test1()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\u0001{1}\u0002", "DELAY", 6);
            output.WriteLine(sb.ToString());
        }
        [Fact]
        public void test()
        {
            try
            {
                //var build = new ConfigurationBuilder();
                //build.SetBasePath(Directory.GetCurrentDirectory());
                //build.AddJsonFile("app.json", true, true);
                //var dbConfig = build.Build();
                //Producer producer = new Producer();
                Producer producer = new Producer
                {
                    Topic = "VoucherHolder_VoucherSync",
                    Group = "VoucherHolder",
                    //NameServerAddress = "192.168.240.79:9876",
                    NameServerAddress = "127.0.0.1:9876",
                    Log = XTrace.Log
                };
                //producer.CreateTopic("",);
                producer.Start();
                //string bodyString = @"{"Vouchers":[{"UserCode":"YGO00256","FeeType":"JT","InvoiceType":"QC","IsDeleted":false,"OrderNo":"B191009190001545020","IMGFilePath":"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceIMGFiles/QCPDPG5425TC20191012092317863820191012092319.jpg","PDFFilePath":"http://119.57.117.216:9080/GWHomeUploadFiles/InvoicePDFFiles/QCPDPG5425TC20191012092317863820191012092319.pdf","XMLFilePath":"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceXMLFiles/QCPDPG5425TC20191012092317863820191012092319.xml","HTMLData":"","InsertBy":"1715077318575132672","InsertTime":"2019-10-09 19:54:50","Users":[{"UserId":139655,"UserName":"张少青","UserCode":"YGO00256","IdType":"2","IdNo":"130528198609212441"}]}]}";
                //string bodyString = "{\"Vouchers\":[{\"UserCode\":\"YGO00256\",\"FeeType\":\"JT\",\"InvoiceType\":\"JP\",\"IsDeleted\":false,\"OrderNo\":\"FI19090900008\",\"IMGFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceIMGFiles/004-JP369856369856369820191010112320.jpg\",\"PDFFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoicePDFFiles/004-JP369856369856369820191010112320.pdf\",\"XMLFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceXMLFiles/004-JP369856369856369820191010112320.xml\",\"HTMLData\":\"null\",\"InsertBy\":\"1756606680210083840\",\"InsertTime\":\"2019-10-10T11:23:32\",\"Users\":[{\"UserId\":140701,\"UserName\":\"吉艳\",\"UserCode\":\"YGO00256\",\"IdType\":\"PP\",\"IdNo\":\"HK1356789\"}]}]}"; 
                //string bodyString = "{\"Vouchers\":[{\"UserCode\":\"YGO00256\",\"FeeType\":\"JT\",\"IsDeleted\":true,\"OrderNo\":\"FI19090900008\",\"IMGFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceIMGFiles/004-JP369856369856369820191010112320.jpg\",\"PDFFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoicePDFFiles/004-JP369856369856369820191010112320.pdf\",\"XMLFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceXMLFiles/004-JP369856369856369820191010112320.xml\",\"HTMLData\":\"\",\"InsertBy\":\"1756606680210083840\",\"InsertTime\":\"2019-10-10T11:23:32\",\"Users\":[{\"UserId\":140701,\"UserName\":\"吉艳\",\"UserCode\":\"YGO00256\",\"IdType\":\"PP\",\"IdNo\":\"HK1356789\"}]}]}";
                //string bodyString = "{\"Vouchers\":[{\"UserCode\":\"YGO00256\",\"FeeType\":\"ZS\",\"IsDeleted\":false,\"OrderNo\":\"HE1806051543510001\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/GWH6112EL201808271505300001.jpg\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/GWH6112EL201808271505300001.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/GWH6112EL201808271505300001.xml\",\"HTMLData\":\""+ htmlData + "\",\"InsertUserID\":122,\"InsertBy\":\"13901133132\",\"InsertByName\":\"战冰\",\"InsertTime\":\"2018-08-27 15:05:31.003\"}," +
                //    "{\"UserCode\":\"YGO00256\",\"FeeType\":\"ZS\",\"IsDeleted\":false,\"OrderNo\":\"HE1806051543510001\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/GWH6112EL201807281652040002.jpg\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/GWH6112EL201807281652040002.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/GWH6112EL201807281652040002.xml\",\"HTMLData\":\"" + htmlData + "\",\"InsertUserID\":4411,\"InsertBy\":\"15810742555\",\"InsertByName\":\"耿玉倩\",\"InsertTime\":\"2018-07-28 16:52:08.230\"}," +
                //    "{\"UserCode\":\"YGO00256\",\"FeeType\":\"JT\",\"InvoiceType\":\"JP\",\"IsDeleted\":false,\"OrderNo\":\"F190808220001270952\",\"IMGFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceIMGFiles/004-TP634-634534634620191009090627.jpg\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/GWH6112EL201807281652040002.pdf\",\"XMLFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceXMLFiles/004-TP634-634534634620191009090627.xml\",\"HTMLData\":\"" + htmlData + "\",\"InsertUserID\":127379,\"InsertBy\":\"18215508968\",\"InsertByName\":\"何云东\",\"InsertTime\":\"2019-10-09 09:06:27.000\"}]}";
                //string bodyString = "{\"Vouchers\":[{\"ApplyOrderNo\":\"\",\"FeeType\":\"JT\",\"HTMLData\":\"\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/HCPE950533597113010120180418000112.jpg\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:43:37\",\"InvoiceType\":\"HC\",\"IsDeleted\":false,\"OrderNo\":\"T1806021527560001\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/HCPE950533597113010120180418000112.pdf\",\"UserCode\":\"YGO00256\",\"Users\":[{\"IdNo\":\"130927198704050319\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4392,\"UserName\":\"尹玉龙\"}],\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/HCPE950533597113010120180418000112.xml\"},{\"ApplyOrderNo\":\"CC1000002017091918060032\",\"FeeType\":\"JT\",\"InvoiceType\":\"JP\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:44:54\",\"IsDeleted\":false,\"OrderNo\":\"F1806021526280001\",\"UserCode\":\"YGO00256\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/004-JP2131231231231220180529184442.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/004-JP2131231231231220180529184443.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/004-JP2131231231231220180529184442.jpg\",\"Users\":[{\"IdNo\":\"130927198704050319\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4392,\"UserName\":\"尹玉龙\"}]},{\"ApplyOrderNo\":\"CC1000002017091918060032\",\"FeeType\":\"JT\",\"InvoiceType\":\"JP\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:44:54\",\"IsDeleted\":false,\"OrderNo\":\"F1806021526280001\",\"UserCode\":\"YGO00256\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/004-JP2131231231231220180529184442.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/004-JP2131231231231220180529184443.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/004-JP2131231231231220180529184442.jpg\",\"Users\":[{\"IdNo\":\"430723199306032018\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4399,\"UserName\":\"明楼\"}]},{\"ApplyOrderNo\":\"CC1000002017091918060032\",\"FeeType\":\"BX\",\"InvoiceType\":\"JP\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:44:54\",\"IsDeleted\":false,\"OrderNo\":\"F1806021526280001\",\"UserCode\":\"YGO00256\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/004-JP2131231231231220180529184442.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/006-BXLBBH180428597000030420180428150931.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/006-BXLBBH180428597000030420180428150931.jpg\",\"Users\":[{\"IdNo\":\"130927198704050319\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4392,\"UserName\":\"尹玉龙\"}]},{\"ApplyOrderNo\":\"CC1000002017091918060032\",\"FeeType\":\"BX\",\"InvoiceType\":\"JP\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:44:54\",\"IsDeleted\":false,\"OrderNo\":\"F1806021526280001\",\"UserCode\":\"YGO00256\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/004-JP2131231231231220180529184442.pdf\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/006-BXLBBH180428597000030420180428150931.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/006-BXLBBH180428597000030420180428150931.jpg\",\"Users\":[{\"IdNo\":\"430723199306032018\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4399,\"UserName\":\"明楼\"}]},{\"ApplyOrderNo\":\"\",\"FeeType\":\"ZS\",\"InvoiceType\":\"ZS\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:46:38\",\"IsDeleted\":false,\"OrderNo\":\"H1807261905090001\",\"UserCode\":\"YGO00256\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/GWH6112XH201807281715360001.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/GWH6112XH201807281715360001.jpg\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/GWH6112XH201807281715360001.pdf\",\"Users\":[{\"IdNo\":\"130927198704050319\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4392,\"UserName\":\"尹玉龙\"}]},{\"ApplyOrderNo\":\"\",\"FeeType\":\"ZS\",\"InvoiceType\":\"ZS\",\"HTMLData\":\"\",\"InsertBy\":\"13501286714\",\"InsertTime\":\"2019-10-15 15:46:38\",\"IsDeleted\":false,\"OrderNo\":\"H1807261905090001\",\"UserCode\":\"YGO00256\",\"XMLFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceXMLFiles/GWH6112XH201807281715360002.xml\",\"IMGFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoiceIMGFiles/GWH6112XH201807281715360002.jpg\",\"PDFFilePath\":\"http://192.168.240.243/GWHomeUploadFiles/InvoicePDFFiles/GWH6112XH201807281715360002.pdf\",\"Users\":[{\"IdNo\":\"430723199306038621\",\"IdType\":\"NI\",\"UserCode\":\"YGO00256\",\"UserId\":4399,\"UserName\":\"明楼\"}]}]}";
                //string bodyString = "[{\"UserCode\":\"YGO00713\",\"FeeType\":\"ZS\",\"InvoiceType\":\"ZS\",\"InvoiceSource\":0,\"InvoiceCategory\":0,\"DepartureTime\":null,\"DepartureCode\":null,\"DepartureCity\":null,\"ArrivalTime\":null,\"ArrivalCode\":null,\"ArrivalCity\":null,\"InvoiceAmount\":0,\"IsDeleted\":false,\"OrderNo\":\"HE191119160001183879\",\"TrafficNo\":null,\"TrainBox\":null,\"SeatCode\":null,\"SeatName\":null,\"PayUserId\":null,\"PayUserName\":null,\"PayBankCardNo\":null,\"PayBankCode\":null,\"PayBankName\":null,\"PayAmount\":0,\"PayTime\":null,\"PayUserUnitType\":0,\"ReimRepaymentBankCardType\":0,\"PayType\":null,\"GPBankFullName\":null,\"GPBankCNAPS\":null,\"IMGFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceIMGFiles/GWH6112EL201911191622080002.jpg\",\"PDFFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoicePDFFiles/GWH6112EL201911191622080002.pdf\",\"XMLFilePath\":\"http://119.57.117.216:9080/GWHomeUploadFiles/InvoiceXMLFiles/GWH6112EL201911191622080002.xml\",\"HTMLData\":\"\",\"HotelName\":null,\"IsExtendedHalfDay\":null,\"InvoiceNum\":null,\"InvoiceCode\":null,\"InvoiceName\":null,\"InvoiceDate\":null,\"UnitAddress\":null,\"InsertBy\":\"1877480013540888576\",\"InsertByName\":null,\"InsertTime\":\"2019/11/19 16:21:37\",\"Users\":[{\"UserId\":0,\"UserName\":\"刘涛\",\"UserCode\":null,\"CustomerName\":null,\"IdType\":\"NI\",\"IdNo\":\"54682A4512EF91346A43915FEFE33533A9478030C600A6476A11B23F44CA8C94\",\"Mobile\":null}],\"ApplyOrderNo\":\"CC13070019211900119110016\"}]";
                FileStream fileStream = new FileStream("file.txt", FileMode.Open);
                string bodyString = string.Empty;
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    bodyString = reader.ReadLine();
                }
                var msg = new NewLife.RocketMQ.Protocol.Message
                {
                    Topic = "VoucherHolder_VoucherSync",
                    BodyString = bodyString,
                    DelayTimeLevel=18
                };
                var sr = producer.Publish(msg);
                output.WriteLine("over");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
