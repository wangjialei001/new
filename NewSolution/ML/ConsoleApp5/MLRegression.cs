using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5
{
    public class MLRegression
    {
        MLContext mlContext = new MLContext(seed: 0);
        public List<PhyParamMatch> LoadData()
        {
            var phyParamList = new List<PhyParamMatch> { };
            //mlContext.Data.LoadFromEnumerable(model);
            return phyParamList;
        }
        public ITransformer Train(string path)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<PhyParamMatch1>(path, hasHeader: true, separatorChar: ',');
            //var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(PhyParamMatch.phyvalue))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytypeEncoded", inputColumnName: nameof(PhyParamMatch.efectphytype)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype1Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype1)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype2Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype2)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype3Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype3)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "isstandarEncoded", inputColumnName: nameof(PhyParamMatch.isstandar)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "materialtypeEncoded", inputColumnName: nameof(PhyParamMatch.materialtype)))
            //                         .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue1)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue2)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue3)))

            //                          .Append(mlContext.Transforms.Concatenate("Features", "efectphytypeEncoded", "efectphytype1Encoded", "efectphytype2Encoded", "efectphytype3Encoded", "isstandarEncoded", "materialtypeEncoded",
            //                             nameof(PhyParamMatch.efectphyvalue), nameof(PhyParamMatch.efectphyvalue1), nameof(PhyParamMatch.efectphyvalue2), nameof(PhyParamMatch.efectphyvalue3)))
            //                         .Append(mlContext.Regression.Trainers.FastTree());
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(PhyParamMatch1.phyvalue))
                                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytypeEncoded", inputColumnName: nameof(PhyParamMatch1.efectphytype)))
                                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype1Encoded", inputColumnName: nameof(PhyParamMatch1.efectphytype1)))
                                    .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch1.efectphyvalue)))
                                     .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch1.efectphyvalue1)))

                                     .Append(mlContext.Transforms.Concatenate("Features", "efectphytypeEncoded", "efectphytype1Encoded", nameof(PhyParamMatch1.efectphyvalue), nameof(PhyParamMatch1.efectphyvalue1)))
                                    .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(dataView);
            return model;
        }
        public ITransformer Train(IEnumerable<PhyParamMatch> trainData)
        {
            IDataView dataView = mlContext.Data.LoadFromEnumerable(trainData);

            //var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "phyvalue")
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytypeEncoded", inputColumnName: "efectphytype"))
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype1Encoded", inputColumnName: "efectphytype1"))
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype2Encoded", inputColumnName: "efectphytype2"))
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype3Encoded", inputColumnName: "efectphytype3"))
            //                //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "phytypeEncoded", inputColumnName: "phytype"))
            //                //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "phytype1Encoded", inputColumnName: "phytype1"))
            //                //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "phytype2Encoded", inputColumnName: "phytype2"))
            //                //.Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "phytype3Encoded", inputColumnName: "phytype3"))
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "isstandarEncoded", inputColumnName: "isstandar"))
            //                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "materialtypeEncoded", inputColumnName: "materialtype"))
            //                .Append(mlContext.Transforms.Concatenate("Features", "efectphytypeEncoded", "efectphyvalue", "efectphytype1Encoded", "efectphyvalue1", "efectphytype2Encoded", "efectphyvalue2", "efectphytype3Encoded", "efectphyvalue3"))
            //                .Append(mlContext.Regression.Trainers.FastTree());

            //var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(PhyParamMatch.phyvalue))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytypeEncoded", inputColumnName: nameof(PhyParamMatch.efectphytype)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype1Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype1)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype2Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype2)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype3Encoded", inputColumnName: nameof(PhyParamMatch.efectphytype3)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "isstandarEncoded", inputColumnName: nameof(PhyParamMatch.isstandar)))
            //                         .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "materialtypeEncoded", inputColumnName: nameof(PhyParamMatch.materialtype)))
            //                         .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue1)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue2)))
            //                          .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch.efectphyvalue3)))
            //                          .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "phytypeEncoded", inputColumnName: nameof(PhyParamMatch.phytype)))

            //                          .Append(mlContext.Transforms.Concatenate("Features", "efectphytypeEncoded", "efectphytype1Encoded", "efectphytype2Encoded", "efectphytype3Encoded", "isstandarEncoded", "materialtypeEncoded",
            //                             nameof(PhyParamMatch.efectphyvalue), nameof(PhyParamMatch.efectphyvalue1), nameof(PhyParamMatch.efectphyvalue2), nameof(PhyParamMatch.efectphyvalue3), "phytypeEncoded"))
            //                         .Append(mlContext.Regression.Trainers.FastTree());
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(PhyParamMatch1.phyvalue))
                                   .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytypeEncoded", inputColumnName: nameof(PhyParamMatch1.efectphytype)))
                                   .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "efectphytype1Encoded", inputColumnName: nameof(PhyParamMatch1.efectphytype1)))
                                   .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch1.efectphyvalue)))
                                    .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(PhyParamMatch1.efectphyvalue1)))

                                    .Append(mlContext.Transforms.Concatenate("Features", "efectphytypeEncoded", "efectphytype1Encoded", nameof(PhyParamMatch1.efectphyvalue), nameof(PhyParamMatch1.efectphyvalue1)))
                                   .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(dataView);
            return model;
        }
        public void Evaluate(ITransformer model, IEnumerable<PhyParamMatch> evaluateData)
        {
            IDataView dataView = mlContext.Data.LoadFromEnumerable(evaluateData);
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
        }
        
        public void Evaluate(ITransformer model,string _testDataPath)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<PhyParamMatch1>(_testDataPath, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
        }

        public PhyParamPrediction Prediction(ITransformer model, PhyParamMatch phyParam)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<PhyParamMatch, PhyParamPrediction>(model);
            var prediction = predictionFunction.Predict(phyParam);
            return prediction;
        }
    }
}
