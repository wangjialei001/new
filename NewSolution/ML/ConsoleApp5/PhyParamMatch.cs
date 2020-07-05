using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;
namespace ConsoleApp5
{
    [Serializable]
    public class PhyParamMatch1
    {//只有从文件加载数据时才需要 LoadColumn
        //[LoadColumn(0)]
        public string efectphytype { get; set; }
       // [LoadColumn(1)]
        public float efectphyvalue { get; set; }

        //[LoadColumn(2)]
        public string efectphytype1 { get; set; }
        //[LoadColumn(3)]
        public float efectphyvalue1 { get; set; }
        //[LoadColumn(4)]
        public float phyvalue { get; set; }
    }
    public class PhyParamMatch2
    {
        [LoadColumn(0)]
        public string efectphytype { get; set; }
        [LoadColumn(1)]
        public float efectphyvalue { get; set; }

        [LoadColumn(2)]
        public string efectphytype1 { get; set; }
        [LoadColumn(3)]
        public float efectphyvalue1 { get; set; }
        [LoadColumn(4)]
        public float phyvalue { get; set; }
    }
    public class PhyParamMatch
    {
        [LoadColumn(0)]
        public string efectphytype = string.Empty;
        [LoadColumn(1)]
        public float efectphyvalue = 0;

        [LoadColumn(2)]
        public string efectphytype1 = string.Empty;
        [LoadColumn(3)]
        public float efectphyvalue1 = 0;

        //[LoadColumn(4)]
        //public string efectphytype2 = string.Empty;
        //[LoadColumn(5)]
        //public float efectphyvalue2 = 0;

        //[LoadColumn(6)]
        //public string efectphytype3 = string.Empty;
        //[LoadColumn(7)]
        //public float efectphyvalue3 = 0;

        //[LoadColumn(8)]
        //public string phytype = string.Empty;
        //[LoadColumn(9)]
        //public float phyvalue = 0;

        [LoadColumn(4)]
        public float phyvalue = 0;

        //[LoadColumn(10)]
        //public string phytype1 = string.Empty;
        //[LoadColumn(11)]
        //public float phyvalue1 = 0;

        //[LoadColumn(12)]
        //public string phytype2 = string.Empty;
        //[LoadColumn(13)]
        //public float phyvalue2 = 0;

        //[LoadColumn(14)]
        //public string phytype3 = string.Empty;
        //[LoadColumn(15)]
        //public float phyvalue3 = 0;

        //[LoadColumn(16)]
        //public bool isstandar = false;

        //[LoadColumn(17)]
        //public string materialtype = string.Empty;
    }
    public class PhyParamPrediction
    {
        [ColumnName("Score")]
        public float phyvalue { get; set; }
    }
}
