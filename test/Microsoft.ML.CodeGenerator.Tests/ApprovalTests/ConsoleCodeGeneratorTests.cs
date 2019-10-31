﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.CodeGenerator.CSharp;
using Microsoft.ML.Data;
using Xunit;
using CodeGenerator = Microsoft.ML.CodeGenerator.CSharp.CodeGenerator;

namespace mlnet.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class ConsoleCodeGeneratorTests
    {
        private Pipeline mockedPipeline;
        private Pipeline mockedOvaPipeline;
        private ColumnInferenceResults columnInference = default;
        private string namespaceValue = "TestNamespace";


        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ConsoleAppModelBuilderCSFileContentOvaTest()
        {
            (Pipeline pipeline,
                        ColumnInferenceResults columnInference) = GetMockedOvaPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.MulticlassClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.modelBuilderCSFileContent);
        }

        [Fact]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void IgniteDemoTest()
        {
            // construct pipeline
            var onnxPipeLineNode = new PipelineNode(nameof(SpecialTransformer.ApplyOnnxModel), PipelineNodeType.Transform, new[] { "input.1" }, new[] { "output.1" },
                new Dictionary<string, object>()
                {
                    { "outputColumnNames", "output1" },
                    { "inputColumnNames", "input1"},
                    { "modelFile" , "awesomeModel.onnx"},
                });
            var loadImageNode = new PipelineNode(EstimatorName.ImageLoading.ToString(), PipelineNodeType.Transform, "ImageSource", "ImageSource_featurized");
            var resizeImageNode = new PipelineNode(
                nameof(SpecialTransformer.ResizeImage),
                PipelineNodeType.Transform,
                "ImageSource_featurized",
                "ImageSource_featurized",
                new Dictionary<string, object>()
                {
                    { "imageWidth", 224 },
                    { "imageHeight", 224 },
                });
            var extractPixelsNode = new PipelineNode(nameof(SpecialTransformer.ExtractPixel), PipelineNodeType.Transform, "ImageSource_featurized", "ImageSource_featurized");
            var customePipeline = new PipelineNode(nameof(SpecialTransformer.NormalizeMapping), PipelineNodeType.Transform, string.Empty, string.Empty);
            var bestPipeLine = new Pipeline(new PipelineNode[]
            {
                loadImageNode,
                resizeImageNode,
                extractPixelsNode,
                customePipeline,
                onnxPipeLineNode,
            });

            // construct column inference
            var textLoaderArgs = new TextLoader.Options()
            {
                Columns = new[] {
                        new TextLoader.Column("Label", DataKind.String, 0),
                        new TextLoader.Column("ImageSource", DataKind.String, 1), // 0?
                    },
                AllowQuoting = true,
                AllowSparse = true,
                HasHeader = true,
                Separators = new[] { '\t' }
            };

            var columnInference = new ColumnInferenceResults()
            {
                TextLoaderOptions = textLoaderArgs,
                ColumnInformation = new ColumnInformation() { LabelColumnName = "Label" }
            };

            // construct CodeGen option
            var setting = new CodeGeneratorSettings()
            {
                TrainDataset = @"C:\Users\xiaoyuz\Desktop\flower_photos_tiny_set_for_unit_tests\data.tsv",
                ModelPath = @"C:\Users\xiaoyuz\Desktop\flower_photos_tiny_set_for_unit_tests\CodeGenTest\MLModel.zip",
                MlTask = TaskKind.MulticlassClassification,
                OutputName = @"CodeGenTest",
                OutputBaseDir = @"C:\Users\xiaoyuz\Desktop\flower_photos_tiny_set_for_unit_tests\CodeGenTest",
                LabelName = "Label",
                Target = GenerateTarget.ModelBuilder,
            };

            // generate project
            var codeGen = new CodeGenerator(bestPipeLine, columnInference, setting);
            codeGen.GenerateAzureRemoteImageOutput();
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ConsoleAppModelBuilderCSFileContentBinaryTest()
        {
            (Pipeline pipeline,
                        ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.modelBuilderCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ConsoleAppModelBuilderCSFileContentRegressionTest()
        {
            (Pipeline pipeline,
                        ColumnInferenceResults columnInference) = GetMockedRegressionPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.Regression,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.modelBuilderCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ModelProjectFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateModelProjectContents(namespaceValue, typeof(float), true, true, true,
                false, false);

            Approvals.Verify(result.ModelProjectFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ConsumeModelContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip",
                Target = GenerateTarget.Cli,
            });
            var result = consoleCodeGen.GenerateModelProjectContents(namespaceValue, typeof(float), true, true, false,
                false, false);

            Approvals.Verify(result.ConsumeModelCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ObservationCSFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateModelProjectContents(namespaceValue, typeof(float), true, true, false,
                false, false);

            Approvals.Verify(result.ModelInputCSFileContent);
        }


        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PredictionCSFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateModelProjectContents(namespaceValue, typeof(float), true, true, false,
                false, false);

            Approvals.Verify(result.ModelOutputCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void PredictionProgramCSFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.ConsoleAppProgramCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]

        public void ConsoleAppProgramCSFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.ConsoleAppProgramCSFileContent);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ConsoleAppProjectFileContentTest()
        {
            (Pipeline pipeline,
                       ColumnInferenceResults columnInference) = GetMockedBinaryPipelineAndInference();

            var consoleCodeGen = new CodeGenerator(pipeline, columnInference, new CodeGeneratorSettings()
            {
                MlTask = TaskKind.BinaryClassification,
                OutputBaseDir = null,
                OutputName = "MyNamespace",
                TrainDataset = "x:\\dummypath\\dummy_train.csv",
                TestDataset = "x:\\dummypath\\dummy_test.csv",
                LabelName = "Label",
                ModelPath = "x:\\models\\model.zip"
            });
            var result = consoleCodeGen.GenerateConsoleAppProjectContents(namespaceValue, typeof(float), true, true,
                false, false, false);

            Approvals.Verify(result.ConsoleAppProjectFileContent);
        }

        private (Pipeline, ColumnInferenceResults) GetMockedBinaryPipelineAndInference()
        {
            if (mockedPipeline == null)
            {
                MLContext context = new MLContext();
                // same learners with different hyperparams
                var hyperparams1 = new Microsoft.ML.AutoML.ParameterSet(new List<Microsoft.ML.AutoML.IParameterValue>() { new LongParameterValue("NumLeaves", 2) });
                var trainer1 = new SuggestedTrainer(context, new LightGbmBinaryExtension(), new ColumnInformation(), hyperparams1);
                var transforms1 = new List<SuggestedTransform>() { ColumnConcatenatingExtension.CreateSuggestedTransform(context, new[] { "In" }, "Out") };
                var inferredPipeline1 = new SuggestedPipeline(transforms1, new List<SuggestedTransform>(), trainer1, context, true);

                this.mockedPipeline = inferredPipeline1.ToPipeline();
                var textLoaderArgs = new TextLoader.Options()
                {
                    Columns = new[] {
                        new TextLoader.Column("Label", DataKind.Boolean, 0),
                        new TextLoader.Column("col1", DataKind.Single, 1),
                        new TextLoader.Column("col2", DataKind.Single, 0),
                        new TextLoader.Column("col3", DataKind.String, 0),
                        new TextLoader.Column("col4", DataKind.Int32, 0),
                        new TextLoader.Column("col5", DataKind.UInt32, 0),
                    },
                    AllowQuoting = true,
                    AllowSparse = true,
                    HasHeader = true,
                    Separators = new[] { ',' }
                };

                this.columnInference = new ColumnInferenceResults()
                {
                    TextLoaderOptions = textLoaderArgs,
                    ColumnInformation = new ColumnInformation() { LabelColumnName = "Label" }
                };
            }
            return (mockedPipeline, columnInference);
        }

        private (Pipeline, ColumnInferenceResults) GetMockedRegressionPipelineAndInference()
        {
            if (mockedPipeline == null)
            {
                MLContext context = new MLContext();
                // same learners with different hyperparams
                var hyperparams1 = new Microsoft.ML.AutoML.ParameterSet(new List<Microsoft.ML.AutoML.IParameterValue>() { new LongParameterValue("NumLeaves", 2) });
                var trainer1 = new SuggestedTrainer(context, new LightGbmRegressionExtension(), new ColumnInformation(), hyperparams1);
                var transforms1 = new List<SuggestedTransform>() { ColumnConcatenatingExtension.CreateSuggestedTransform(context, new[] { "In" }, "Out") };
                var inferredPipeline1 = new SuggestedPipeline(transforms1, new List<SuggestedTransform>(), trainer1, context, true);

                this.mockedPipeline = inferredPipeline1.ToPipeline();
                var textLoaderArgs = new TextLoader.Options()
                {
                    Columns = new[] {
                        new TextLoader.Column("Label", DataKind.Boolean, 0),
                        new TextLoader.Column("col1", DataKind.Single, 1),
                        new TextLoader.Column("col2", DataKind.Single, 0),
                        new TextLoader.Column("col3", DataKind.String, 0),
                        new TextLoader.Column("col4", DataKind.Int32, 0),
                        new TextLoader.Column("col5", DataKind.UInt32, 0),
                    },
                    AllowQuoting = true,
                    AllowSparse = true,
                    HasHeader = true,
                    Separators = new[] { ',' }
                };

                this.columnInference = new ColumnInferenceResults()
                {
                    TextLoaderOptions = textLoaderArgs,
                    ColumnInformation = new ColumnInformation() { LabelColumnName = "Label" }
                };
            }
            return (mockedPipeline, columnInference);
        }
        private (Pipeline, ColumnInferenceResults) GetMockedOvaPipelineAndInference()
        {
            if (mockedOvaPipeline == null)
            {
                MLContext context = new MLContext();
                // same learners with different hyperparams
                var hyperparams1 = new Microsoft.ML.AutoML.ParameterSet(new List<Microsoft.ML.AutoML.IParameterValue>() { new LongParameterValue("NumLeaves", 2) });
                var trainer1 = new SuggestedTrainer(context, new FastForestOvaExtension(), new ColumnInformation(), hyperparams1);
                var transforms1 = new List<SuggestedTransform>() { ColumnConcatenatingExtension.CreateSuggestedTransform(context, new[] { "In" }, "Out") };
                var inferredPipeline1 = new SuggestedPipeline(transforms1, new List<SuggestedTransform>(), trainer1, context, true);

                this.mockedOvaPipeline = inferredPipeline1.ToPipeline();
                var textLoaderArgs = new TextLoader.Options()
                {
                    Columns = new[] {
                        new TextLoader.Column("Label", DataKind.Boolean, 0),
                        new TextLoader.Column("col1", DataKind.Single, 1),
                        new TextLoader.Column("col2", DataKind.Single, 0),
                        new TextLoader.Column("col3", DataKind.String, 0),
                        new TextLoader.Column("col4", DataKind.Int32, 0),
                        new TextLoader.Column("col5", DataKind.UInt32, 0),
                    },
                    AllowQuoting = true,
                    AllowSparse = true,
                    HasHeader = true,
                    Separators = new[] { ',' }
                };


                this.columnInference = new ColumnInferenceResults()
                {
                    TextLoaderOptions = textLoaderArgs,
                    ColumnInformation = new ColumnInformation() { LabelColumnName = "Label" }
                };

            }
            return (mockedOvaPipeline, columnInference);
        }
    }
}