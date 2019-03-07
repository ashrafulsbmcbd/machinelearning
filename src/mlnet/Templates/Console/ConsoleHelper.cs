﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.ML.CLI.Templates.Console {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class ConsoleHelper : ConsoleHelperBase {
        

public string Namespace {get;set;}

        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            this.Write(@"//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture( Namespace ));
            this.Write("\n{\n    public static class ConsoleHelper\n    {\n        public static void PrintPr" +
                    "ediction(string prediction)\n        {\n            Console.WriteLine($\"**********" +
                    "***************************************\");\n            Console.WriteLine($\"Predi" +
                    "cted : {prediction}\");\n            Console.WriteLine($\"*************************" +
                    "************************\");\n        }\n\n        public static void PrintRegressio" +
                    "nPredictionVersusObserved(string predictionCount, string observedCount)\n        " +
                    "{\n            Console.WriteLine($\"----------------------------------------------" +
                    "---\");\n            Console.WriteLine($\"Predicted : {predictionCount}\");\n        " +
                    "    Console.WriteLine($\"Actual:     {observedCount}\");\n            Console.Write" +
                    "Line($\"-------------------------------------------------\");\n        }\n\n        p" +
                    "ublic static void PrintRegressionMetrics(string name, RegressionMetrics metrics)" +
                    "\n        {\n            Console.WriteLine($\"*************************************" +
                    "************\");\n            Console.WriteLine($\"*       Metrics for {name} regre" +
                    "ssion model      \");\n            Console.WriteLine($\"*--------------------------" +
                    "----------------------\");\n            Console.WriteLine($\"*       LossFn:       " +
                    " {metrics.LossFn:0.##}\");\n            Console.WriteLine($\"*       R2 Score:     " +
                    " {metrics.RSquared:0.##}\");\n            Console.WriteLine($\"*       Absolute los" +
                    "s: {metrics.L1:#.##}\");\n            Console.WriteLine($\"*       Squared loss:  {" +
                    "metrics.L2:#.##}\");\n            Console.WriteLine($\"*       RMS loss:      {metr" +
                    "ics.Rms:#.##}\");\n            Console.WriteLine($\"*******************************" +
                    "******************\");\n        }\n\n        public static void PrintBinaryClassific" +
                    "ationMetrics(string name, BinaryClassificationMetrics metrics)\n        {\n       " +
                    "     Console.WriteLine($\"*******************************************************" +
                    "*****\");\n            Console.WriteLine($\"*       Metrics for {name} binary class" +
                    "ification model      \");\n            Console.WriteLine($\"*----------------------" +
                    "-------------------------------------\");\n            Console.WriteLine($\"*      " +
                    " Accuracy: {metrics.Accuracy:P2}\");\n            Console.WriteLine($\"*       Auc:" +
                    "      {metrics.Auc:P2}\");\n            Console.WriteLine($\"**********************" +
                    "**************************************\");\n        }\n\n        public static void " +
                    "PrintRegressionFoldsAverageMetrics(string algorithmName,\n                       " +
                    "                                      TrainCatalogBase.CrossValidationResult<Reg" +
                    "ressionMetrics>[] crossValidationResults\n                                       " +
                    "                      )\n        {\n            var L1 = crossValidationResults.Se" +
                    "lect(r => r.Metrics.L1);\n            var L2 = crossValidationResults.Select(r =>" +
                    " r.Metrics.L2);\n            var RMS = crossValidationResults.Select(r => r.Metri" +
                    "cs.L1);\n            var lossFunction = crossValidationResults.Select(r => r.Metr" +
                    "ics.LossFn);\n            var R2 = crossValidationResults.Select(r => r.Metrics.R" +
                    "Squared);\n\n            Console.WriteLine($\"*************************************" +
                    "************************************************************************\");\n    " +
                    "        Console.WriteLine($\"*       Metrics for {algorithmName} Regression model" +
                    "      \");\n            Console.WriteLine($\"*-------------------------------------" +
                    "-----------------------------------------------------------------------\");\n     " +
                    "       Console.WriteLine($\"*       Average L1 Loss:    {L1.Average():0.###} \");\n" +
                    "            Console.WriteLine($\"*       Average L2 Loss:    {L2.Average():0.###}" +
                    "  \");\n            Console.WriteLine($\"*       Average RMS:          {RMS.Average" +
                    "():0.###}  \");\n            Console.WriteLine($\"*       Average Loss Function: {l" +
                    "ossFunction.Average():0.###}  \");\n            Console.WriteLine($\"*       Averag" +
                    "e R-squared: {R2.Average():0.###}  \");\n            Console.WriteLine($\"*********" +
                    "********************************************************************************" +
                    "********************\");\n        }\n\n        public static void PrintBinaryClassif" +
                    "icationFoldsAverageMetrics(\n                                         string algo" +
                    "rithmName,\n                                         TrainCatalogBase.CrossValida" +
                    "tionResult<BinaryClassificationMetrics>[] crossValResults)\n        {\n           " +
                    " var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);\n\n         " +
                    "   var AccuracyValues = metricsInMultipleFolds.Select(m => m.Accuracy);\n        " +
                    "    var AccuracyAverage = AccuracyValues.Average();\n            var AccuraciesSt" +
                    "dDeviation = CalculateStandardDeviation(AccuracyValues);\n            var Accurac" +
                    "iesConfidenceInterval95 = CalculateConfidenceInterval95(AccuracyValues);\n\n\n     " +
                    "       Console.WriteLine($\"*****************************************************" +
                    "********************************************************\");\n            Console." +
                    "WriteLine($\"*       Metrics for {algorithmName} Binary Classification model     " +
                    " \");\n            Console.WriteLine($\"*------------------------------------------" +
                    "------------------------------------------------------------------\");\n          " +
                    "  Console.WriteLine($\"*       Average Accuracy:    {AccuracyAverage:0.###}  - St" +
                    "andard deviation: ({AccuraciesStdDeviation:#.###})  - Confidence Interval 95%: (" +
                    "{AccuraciesConfidenceInterval95:#.###})\");\n            Console.WriteLine($\"*****" +
                    "********************************************************************************" +
                    "************************\");\n\n        }\n\n        public static void PrintMulticla" +
                    "ssClassificationFoldsAverageMetrics(\n                                         st" +
                    "ring algorithmName,\n                                         TrainCatalogBase.Cr" +
                    "ossValidationResult<MultiClassClassifierMetrics>[] crossValResults)\n        {\n  " +
                    "          var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);\n\n" +
                    "            var microAccuracyValues = metricsInMultipleFolds.Select(m => m.Accur" +
                    "acyMicro);\n            var microAccuracyAverage = microAccuracyValues.Average();" +
                    "\n            var microAccuraciesStdDeviation = CalculateStandardDeviation(microA" +
                    "ccuracyValues);\n            var microAccuraciesConfidenceInterval95 = CalculateC" +
                    "onfidenceInterval95(microAccuracyValues);\n\n            var macroAccuracyValues =" +
                    " metricsInMultipleFolds.Select(m => m.AccuracyMacro);\n            var macroAccur" +
                    "acyAverage = macroAccuracyValues.Average();\n            var macroAccuraciesStdDe" +
                    "viation = CalculateStandardDeviation(macroAccuracyValues);\n            var macro" +
                    "AccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(macroAccuracyValu" +
                    "es);\n\n            var logLossValues = metricsInMultipleFolds.Select(m => m.LogLo" +
                    "ss);\n            var logLossAverage = logLossValues.Average();\n            var l" +
                    "ogLossStdDeviation = CalculateStandardDeviation(logLossValues);\n            var " +
                    "logLossConfidenceInterval95 = CalculateConfidenceInterval95(logLossValues);\n\n   " +
                    "         var logLossReductionValues = metricsInMultipleFolds.Select(m => m.LogLo" +
                    "ssReduction);\n            var logLossReductionAverage = logLossReductionValues.A" +
                    "verage();\n            var logLossReductionStdDeviation = CalculateStandardDeviat" +
                    "ion(logLossReductionValues);\n            var logLossReductionConfidenceInterval9" +
                    "5 = CalculateConfidenceInterval95(logLossReductionValues);\n\n            Console." +
                    "WriteLine($\"********************************************************************" +
                    "*****************************************\");\n            Console.WriteLine($\"*  " +
                    "     Metrics for {algorithmName} Multi-class Classification model      \");\n     " +
                    "       Console.WriteLine($\"*----------------------------------------------------" +
                    "--------------------------------------------------------\");\n            Console." +
                    "WriteLine($\"*       Average MicroAccuracy:    {microAccuracyAverage:0.###}  - St" +
                    "andard deviation: ({microAccuraciesStdDeviation:#.###})  - Confidence Interval 9" +
                    "5%: ({microAccuraciesConfidenceInterval95:#.###})\");\n            Console.WriteLi" +
                    "ne($\"*       Average MacroAccuracy:    {macroAccuracyAverage:0.###}  - Standard " +
                    "deviation: ({macroAccuraciesStdDeviation:#.###})  - Confidence Interval 95%: ({m" +
                    "acroAccuraciesConfidenceInterval95:#.###})\");\n            Console.WriteLine($\"* " +
                    "      Average LogLoss:          {logLossAverage:#.###}  - Standard deviation: ({" +
                    "logLossStdDeviation:#.###})  - Confidence Interval 95%: ({logLossConfidenceInter" +
                    "val95:#.###})\");\n            Console.WriteLine($\"*       Average LogLossReductio" +
                    "n: {logLossReductionAverage:#.###}  - Standard deviation: ({logLossReductionStdD" +
                    "eviation:#.###})  - Confidence Interval 95%: ({logLossReductionConfidenceInterva" +
                    "l95:#.###})\");\n            Console.WriteLine($\"*********************************" +
                    "****************************************************************************\");\n" +
                    "\n        }\n\n        public static double CalculateStandardDeviation(IEnumerable<" +
                    "double> values)\n        {\n            double average = values.Average();\n       " +
                    "     double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (" +
                    "val - average)).Sum();\n            double standardDeviation = Math.Sqrt(sumOfSqu" +
                    "aresOfDifferences / (values.Count() - 1));\n            return standardDeviation;" +
                    "\n        }\n\n        public static double CalculateConfidenceInterval95(IEnumerab" +
                    "le<double> values)\n        {\n            double confidenceInterval95 = 1.96 * Ca" +
                    "lculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1));\n            " +
                    "return confidenceInterval95;\n        }\n\n        public static void PrintClusteri" +
                    "ngMetrics(string name, ClusteringMetrics metrics)\n        {\n            Console." +
                    "WriteLine($\"*************************************************\");\n            Con" +
                    "sole.WriteLine($\"*       Metrics for {name} clustering model      \");\n          " +
                    "  Console.WriteLine($\"*------------------------------------------------\");\n     " +
                    "       Console.WriteLine($\"*       AvgMinScore: {metrics.AvgMinScore}\");\n       " +
                    "     Console.WriteLine($\"*       DBI is: {metrics.Dbi}\");\n            Console.Wr" +
                    "iteLine($\"*************************************************\");\n        }\n\n      " +
                    "  public static void ConsoleWriteHeader(params string[] lines)\n        {\n       " +
                    "     var defaultColor = Console.ForegroundColor;\n            Console.ForegroundC" +
                    "olor = ConsoleColor.Yellow;\n            Console.WriteLine(\" \");\n            fore" +
                    "ach (var line in lines)\n            {\n                Console.WriteLine(line);\n " +
                    "           }\n            var maxLength = lines.Select(x => x.Length).Max();\n    " +
                    "        Console.WriteLine(new string(\'#\', maxLength));\n            Console.Foreg" +
                    "roundColor = defaultColor;\n        }\n    }\n}\n");
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ConsoleHelperBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
