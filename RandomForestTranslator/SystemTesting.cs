using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ArffGenerator;
using Essy.Tools.InputBox;
using java.io;
using Leap;
using NUnit.Framework;
using weka.classifiers.functions;
using weka.classifiers.trees;
using weka.core;
using Console = System.Console;

namespace RandomForestTranslator
{
    [TestFixture]
    class SystemTesting
    {
        private RandomForest RandomForest;
        private Logistic OneHandedGestures;
        private String[] classes;
        private string[] OneHandedClasses;
        [SetUp]
        public void SetUp()
        {

            classes = new[] { "a", "b", "d", "e", "f", "i", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            RandomForest = (RandomForest)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\RandomForest.model");
            OneHandedClasses = new[] { "c", "g", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            OneHandedGestures =
            (Logistic)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\SingleHandLogistic.model");
        }
        [Test]
        public void TwoHandRandomForestTest()
        {
            int correctInstances = 0;
            double TotalErrorPrediction = 0;

            Instances TestSet = new Instances(
                new BufferedReader(
                    new FileReader(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageNewTestData.arff")));
            TestSet.setClassIndex(TestSet.numAttributes() - 1);
            foreach (Instance instance in TestSet)
            {
                var predictionInt = (int)RandomForest.classifyInstance(instance);
                var prob = RandomForest.distributionForInstance(TestSet.lastInstance()).Max();
                var actual = (int)instance.classValue();
                if (classes[actual] == classes[predictionInt])
                {
                    correctInstances++;
                    TotalErrorPrediction += prob;
                }
            }
            var accuracy = ((double)correctInstances) / TestSet.numInstances();
            var averageErrorPrediction = TotalErrorPrediction / ((double)correctInstances);
            Console.WriteLine("Accuracy: " + accuracy + " Average Error Prediction: " + averageErrorPrediction);
            Assert.Greater(accuracy, 0.75);



        }
        [Test]
        public void OneHandRandomForestTest()
        {
            int correctInstances = 0;
            double TotalErrorPrediction = 0;

            Instances TestSet = new Instances(
                new BufferedReader(
                    new FileReader(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandTestData.arff")));
            TestSet.setClassIndex(TestSet.numAttributes() - 1);
            foreach (Instance instance in TestSet)
            {
                var predictionInt = (int)OneHandedGestures.classifyInstance(instance);
                var prob = OneHandedGestures.distributionForInstance(TestSet.lastInstance()).Max();
                var actual = (int)instance.classValue();
                if (OneHandedClasses[actual] == OneHandedClasses[predictionInt])
                {
                    correctInstances++;
                    TotalErrorPrediction += prob;
                }
            }
            var accuracy = ((double)correctInstances) / TestSet.numInstances();
            var averageErrorPrediction = TotalErrorPrediction / ((double)correctInstances);
            Console.WriteLine("Accuracy: " + accuracy + " Average Error Prediction: " + averageErrorPrediction);
            Assert.Greater(accuracy, 0.75);



        }
        [Test]
        public void AddGesture()
        {
            Controller controller = new Controller();
            var listener = new SaveDataListener();
            Gesture.GestureName = InputBox.ShowInputBox("please enter the name of the gesture");
            MessageBox.Show("please holds gesture until test completes.");
            controller.FrameReady += listener.OnFrame;
            Thread.Sleep(10000);
            controller.StopConnection();
            controller.Dispose();
            string text = System.IO.File.ReadAllText(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff");
            text = text.Replace("}", "," + Gesture.GestureName + "}");
            System.IO.File.WriteAllText(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff", text);
            Instances instances = new Instances(
               new BufferedReader(
                   new FileReader(
                       @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff")));
            instances.setClassIndex(instances.numAttributes() - 1);
            RandomForest updatedRandomForest = new RandomForest();
            updatedRandomForest.buildClassifier(instances);
            weka.core.SerializationHelper.write(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedRandomForest.model", updatedRandomForest);
        }
    }
}
