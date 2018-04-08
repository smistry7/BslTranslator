using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.io;
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
        private String[] classes;
        [SetUp]
        public void SetUp()
        {

            classes = new[] { "a", "b", "d", "e", "f", "i", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            RandomForest = (RandomForest)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\RandomForest.model");

        }
        [Test]
        public void RandomForestTest()
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
                var actual =(int) instance.classValue();
                if (classes[actual] == classes[predictionInt])
                {
                    correctInstances++;
                    TotalErrorPrediction += prob;
                }
            }
            var accuracy = ((double) correctInstances) / TestSet.numInstances();
            var averageErrorPrediction = TotalErrorPrediction / ((double) correctInstances);
            Console.WriteLine("Accuracy: " + accuracy + " Average Error Prediction: " + averageErrorPrediction);
            Assert.Greater(accuracy,0.75);



        }
    }
}
