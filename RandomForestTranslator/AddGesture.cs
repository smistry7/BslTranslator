using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ArffGenerator;
using Essy.Tools.InputBox;
using java.io;
using Leap;
using weka.classifiers.functions;
using weka.classifiers.trees;
using weka.core;
using MessageBox = System.Windows.Forms.MessageBox;

namespace RandomForestTranslator
{
   public class AddGesture
    {
        public void AddTwoHandedGesture(Window window)
        {
            Controller controller = new Controller();
            var listener = new SaveDataListener();
            Thread.Sleep(500);
            if (!controller.IsConnected)
            {
               
                MessageBox.Show("Please connect the controller and try again");
                window.Close();
            }
            GatherData(controller, listener);
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
            SerializationHelper.write(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedRandomForest.model", updatedRandomForest);
        }

        public void AddOneHandedGesture(Window window)
        {
            Controller controller = new Controller();
            var listener = new SaveDataListener();
            Thread.Sleep(500);
            if (!controller.IsConnected)
            {
                MessageBox.Show("Please connect the controller and try again");
                window.Close();
            }
            GatherData(controller, listener);
            string text = System.IO.File.ReadAllText(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandData.arff");
            text = text.Replace("}", "," + Gesture.GestureName + "}");
            System.IO.File.WriteAllText(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandData.arff", text);
            Instances instances = new Instances(
                new BufferedReader(
                    new FileReader(
                        @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandData.arff")));
            instances.setClassIndex(instances.numAttributes() - 1);
            var updatedLogistic = new Logistic();
            updatedLogistic.buildClassifier(instances);
            SerializationHelper.write(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedLogistic.model", updatedLogistic);
        }

    
        private void GatherData(Controller controller, SaveDataListener listener)
        {
            while (string.IsNullOrEmpty(Gesture.GestureName))
            {
                Gesture.GestureName = InputBox.ShowInputBox("please enter the name of the gesture");
            }
            MessageBox.Show("please holds gesture until test completes.");
            
            controller.FrameReady += listener.OnFrame;
            Thread.Sleep(10000);
            controller.StopConnection();
            controller.Dispose();
            MessageBox.Show("You may now stop performing the gesture, please wait while the algorithm retrains");
        }
    }
}
