using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp5
{
    class Program
    {
        static readonly string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-train.csv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-test.csv");
        static void Main(string[] args)
        {
            var ml1 = new MLRegression();
            var ml2 = new MLRegression();
            #region data
            var data = new List<PhyParamMatch1> {
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-20f,
                    //phytype="p",
                    phyvalue=1.03f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-19f,
                    //phytype="p",
                    phyvalue=1.13f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-18f,
                    //phytype="p",
                    phyvalue=1.25f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-17f,
                    //phytype="p",
                    phyvalue=1.37f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-16f,
                    //phytype="p",
                    phyvalue=1.50f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-15f,
                    //phytype="p",
                    phyvalue=1.65f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-14f,
                    //phytype="p",
                    phyvalue=1.81f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-13f,
                    //phytype="p",
                    phyvalue=1.98f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-12f,
                    //phytype="p",
                    phyvalue=2.17f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-11f,
                    //phytype="p",
                    phyvalue=2.37f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-10f,
                    //phytype="p",
                    phyvalue=2.59f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-9f,
                    //phytype="p",
                    phyvalue=2.83f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-8f,
                    //phytype="p",
                    phyvalue=3.09f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-7f,
                    //phytype="p",
                    phyvalue=3.38f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-6f,
                    //phytype="p",
                    phyvalue=3.68f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-5f,
                    //phytype="p",
                    phyvalue=4.01f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-4f,
                    //phytype="p",
                    phyvalue=4.37f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-3f,
                    //phytype="p",
                    phyvalue=4.75f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-2f,
                    //phytype="p",
                    phyvalue=5.17f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=-1f,
                    //phytype="p",
                    phyvalue=5.62f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=0f,
                    //phytype="p",
                    phyvalue=6.11f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=1f,
                    //phytype="p",
                    phyvalue=6.56f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=2f,
                    //phytype="p",
                    phyvalue=7.05f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=3f,
                    //phytype="p",
                    phyvalue=7.57f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=4f,
                    //phytype="p",
                    phyvalue=8.13f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=5f,
                    //phytype="p",
                    phyvalue=8.72f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=4f,
                    //phytype="p",
                    phyvalue=8.13f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=5f,
                    //phytype="p",
                    phyvalue=8.72f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=6f,
                    //phytype="p",
                    phyvalue=9.38f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=7f,
                    //phytype="p",
                    phyvalue=10.01f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=8f,
                    //phytype="p",
                    phyvalue=10.72f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=9f,
                    //phytype="p",
                    phyvalue=11.47f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=10f,
                    //phytype="p",
                    phyvalue=12.27f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=11f,
                    //phytype="p",
                    phyvalue=13.12f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=12f,
                    //phytype="p",
                    phyvalue=14.01f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=13f,
                    //phytype="p",
                    phyvalue=15.03f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=14f,
                    //phytype="p",
                    phyvalue=15.97f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=15f,
                    //phytype="p",
                    phyvalue=17.04f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=16f,
                    //phytype="p",
                    phyvalue=18.17f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=17f,
                    //phytype="p",
                    phyvalue=19.36f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=18f,
                    //phytype="p",
                    phyvalue=20.62f
                }
            };
            #endregion
            var model1 = ml1.Train(data);
            //var model2 = ml2.Train(_trainDataPath);
            var evaluateData = new List<PhyParamMatch1> {
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=4f,
                    //phytype="p",
                    phyvalue=8.13f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=5f,
                    //phytype="p",
                    phyvalue=8.72f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=6f,
                    //phytype="p",
                    phyvalue=9.38f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=7,
                    //phytype="p",
                    phyvalue=10.01f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=8f,
                    //phytype="p",
                    phyvalue=10.72f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=9,
                    //phytype="p",
                    phyvalue=11.47f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=10,
                    //phytype="p",
                    phyvalue=12.27f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=11,
                    //phytype="p",
                    phyvalue=13.12f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=12f,
                    //phytype="p",
                    phyvalue=14.01f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=13f,
                    //phytype="p",
                    phyvalue=15.03f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=14f,
                    //phytype="p",
                    phyvalue=15.97f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=15f,
                    //phytype="p",
                    phyvalue=17.04f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=16f,
                    //phytype="p",
                    phyvalue=18.17f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=17f,
                    //phytype="p",
                    phyvalue=19.36f
                },
                new PhyParamMatch1{
                    efectphytype="Pa",
                    efectphyvalue=0.1f,
                    efectphytype1="C",
                    efectphyvalue1=18f,
                    //phytype="p",
                    phyvalue=20.62f
                }
            };
            ml1.Evaluate(model1.Item1, evaluateData);
            //ml2.Evaluate(model2.Item1, _testDataPath);
            var predictionData = new PhyParamMatch1
            {
                efectphytype = "Pa",
                efectphyvalue = 0.1f,
                efectphytype1 = "C",
                efectphyvalue1 = -6.63f
            };
            var result1 = ml1.Prediction(model1.Item1, predictionData);

            //var result2 = ml2.Prediction(model2.Item1, predictionData);
            Console.WriteLine(JsonConvert.SerializeObject(result1));
            //Console.WriteLine(JsonConvert.SerializeObject(result2));

            Console.ReadKey();
        }
    }
}
