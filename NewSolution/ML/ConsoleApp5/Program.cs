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
            var ml = new MLRegression();
            #region data
            var data = new List<PhyParamMatch> {
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-20F,
                    //phytype="p",
                    phyvalue=1.03F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-19F,
                    //phytype="p",
                    phyvalue=1.13F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-18F,
                    //phytype="p",
                    phyvalue=1.25F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-17F,
                    //phytype="p",
                    phyvalue=1.37F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-16F,
                    //phytype="p",
                    phyvalue=1.50F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-15F,
                    //phytype="p",
                    phyvalue=1.65F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-14F,
                    //phytype="p",
                    phyvalue=1.81F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-13F,
                    //phytype="p",
                    phyvalue=1.98F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-12F,
                    //phytype="p",
                    phyvalue=2.17F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-11F,
                    //phytype="p",
                    phyvalue=2.37F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-10F,
                    //phytype="p",
                    phyvalue=2.59F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-9F,
                    //phytype="p",
                    phyvalue=2.83F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-8F,
                    //phytype="p",
                    phyvalue=3.09F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-7F,
                    //phytype="p",
                    phyvalue=3.38F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-6F,
                    //phytype="p",
                    phyvalue=3.68F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-5F,
                    //phytype="p",
                    phyvalue=4.01F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-4F,
                    //phytype="p",
                    phyvalue=4.37F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-3F,
                    //phytype="p",
                    phyvalue=4.75F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-2F,
                    //phytype="p",
                    phyvalue=5.17F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=-1F,
                    //phytype="p",
                    phyvalue=5.62F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=0F,
                    //phytype="p",
                    phyvalue=6.11F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=1F,
                    //phytype="p",
                    phyvalue=6.56F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=2F,
                    //phytype="p",
                    phyvalue=7.05F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=3F,
                    //phytype="p",
                    phyvalue=7.57F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=4F,
                    //phytype="p",
                    phyvalue=8.13F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=5F,
                    //phytype="p",
                    phyvalue=8.72F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=4F,
                    //phytype="p",
                    phyvalue=8.13F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=5F,
                    //phytype="p",
                    phyvalue=8.72F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=6F,
                    //phytype="p",
                    phyvalue=9.38F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=7F,
                    //phytype="p",
                    phyvalue=10.01F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=8F,
                    //phytype="p",
                    phyvalue=10.72F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=9F,
                    //phytype="p",
                    phyvalue=11.47F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=10F,
                    //phytype="p",
                    phyvalue=12.27F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=11F,
                    //phytype="p",
                    phyvalue=13.12F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=12F,
                    //phytype="p",
                    phyvalue=14.01F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=13F,
                    //phytype="p",
                    phyvalue=15.03F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=14F,
                    //phytype="p",
                    phyvalue=15.97F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=15F,
                    //phytype="p",
                    phyvalue=17.04F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=16F,
                    //phytype="p",
                    phyvalue=18.17F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=17F,
                    //phytype="p",
                    phyvalue=19.36F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=18F,
                    //phytype="p",
                    phyvalue=20.62F
                }
            };
            #endregion
            var model1 = ml.Train(data);
            var model2 = ml.Train(_trainDataPath);
            var evaluateData = new List<PhyParamMatch> {
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=4F,
                    //phytype="p",
                    phyvalue=8.13F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=5F,
                    //phytype="p",
                    phyvalue=8.72F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=6F,
                    //phytype="p",
                    phyvalue=9.38F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=7F,
                    //phytype="p",
                    phyvalue=10.01F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=8F,
                    //phytype="p",
                    phyvalue=10.72F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=9F,
                    //phytype="p",
                    phyvalue=11.47F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=10F,
                    //phytype="p",
                    phyvalue=12.27F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=11F,
                    //phytype="p",
                    phyvalue=13.12F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=12F,
                    //phytype="p",
                    phyvalue=14.01F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=13F,
                    //phytype="p",
                    phyvalue=15.03F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=14F,
                    //phytype="p",
                    phyvalue=15.97F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=15F,
                    //phytype="p",
                    phyvalue=17.04F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=16F,
                    //phytype="p",
                    phyvalue=18.17F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=17F,
                    //phytype="p",
                    phyvalue=19.36F
                },
                new PhyParamMatch{
                    efectphytype="Pa",
                    efectphyvalue=0.1F,
                    efectphytype1="C",
                    efectphyvalue1=18F,
                    //phytype="p",
                    phyvalue=20.62F
                }
            };
            ml.Evaluate(model1, evaluateData);
            ml.Evaluate(model2, _testDataPath);
            var predictionData = new PhyParamMatch
            {
                efectphytype = "Pa",
                efectphyvalue = 0.1F,
                efectphytype1 = "C",
                efectphyvalue1 = -8F
            };
            var result1 = ml.Prediction(model1, predictionData);
            var result2 = ml.Prediction(model2, predictionData);
            Console.WriteLine(JsonConvert.SerializeObject(result1));
            Console.WriteLine(JsonConvert.SerializeObject(result2));
            
            Console.ReadKey();
        }
    }
}
