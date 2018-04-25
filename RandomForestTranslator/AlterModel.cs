using ArffGenerator;
using Essy.Tools.InputBox;
using java.io;
using Leap;
using System.Linq;
using System.Threading;
using System.Windows;
using weka.classifiers.functions;
using weka.classifiers.trees;
using weka.core;
using MessageBox = System.Windows.Forms.MessageBox;

namespace RandomForestTranslator
{
    public class AlterModel
    {
        private Controller controller = new Controller();


        public void AddTwoHandedTrainingData()
        {
            AddNewData(false);
            var instances = new Instances(new BufferedReader(new FileReader(FileLocations.TwoHandedDataFilePath)));
            instances.setClassIndex(instances.numAttributes() - 1);
            RandomForest updatedRandomForest = new RandomForest();
            updatedRandomForest.buildClassifier(instances);
            SerializationHelper.write(FileLocations.TwoHandedModelFilePath, updatedRandomForest);
        }

        public void AddOneHandedTrainingData()
        {
            AddNewData(true);
            Instances instances = new Instances(new BufferedReader(new FileReader(FileLocations.OneHandedDataFilePath)));
            instances.setClassIndex(instances.numAttributes() - 1);
            var updatedLogistic = new Logistic();
            updatedLogistic.buildClassifier(instances);
            SerializationHelper.write(FileLocations.OneHandedModelFilePath, updatedLogistic);
        }

        private void AddNewData(bool oneHanded)
        {
            var listener = new SaveDataListener() { OneHandedGesture = oneHanded };

            var gestureList = TextBoxValues.GestureList.Split('\n');
            while (string.IsNullOrEmpty(Gesture.GestureName))
            {
                Gesture.GestureName = InputBox.ShowInputBox("please enter the name of the gesture");
            }
            if (!gestureList.Contains(Gesture.GestureName.ToLower()))
            {
                MessageBox.Show(
                    "that is not a gesture already in the model, please add it or try another gesture from the gesture list tab ");
            }
            else
            {
                MessageBox.Show("please holds gesture until the next message box.");

                controller.FrameReady += listener.OnFrame;
                Thread.Sleep(10000);
                controller.StopConnection();
                controller.Dispose();
                MessageBox.Show("You may now stop performing the gesture, please wait while the algorithm retrains");
            }
        }

        public void AddTwoHandedGesture(Window window)
        {
            var listener = new SaveDataListener { OneHandedGesture = false };
            Thread.Sleep(500);
            if (!controller.IsConnected)
            {
                MessageBox.Show("Please connect the controller and try again");
                window.Close();
            }
            GatherData(controller, listener);
            string text = System.IO.File.ReadAllText(FileLocations.TwoHandedDataFilePath);
            text = text.Replace("}", "," + Gesture.GestureName + "}");
            System.IO.File.WriteAllText(FileLocations.TwoHandedDataFilePath, text);
            Instances instances = new Instances(
               new BufferedReader(
                   new FileReader(
                       FileLocations.TwoHandedDataFilePath)));
            instances.setClassIndex(instances.numAttributes() - 1);
            RandomForest updatedRandomForest = new RandomForest();
            updatedRandomForest.buildClassifier(instances);
            SerializationHelper.write(FileLocations.TwoHandedModelFilePath, updatedRandomForest);
        }

        public void AddOneHandedGesture(Window window)
        {
            var controller = new Controller();
            var listener = new SaveDataListener { OneHandedGesture = true };
            Thread.Sleep(500);
            if (!controller.IsConnected)
            {
                MessageBox.Show("Please connect the controller and try again");
                window.Close();
            }
            GatherData(controller, listener);
            string text = System.IO.File.ReadAllText(FileLocations.OneHandedDataFilePath);
            text = text.Replace("}", "," + Gesture.GestureName + "}");
            System.IO.File.WriteAllText(FileLocations.OneHandedDataFilePath, text);
            Instances instances = new Instances(
                new BufferedReader(
                    new FileReader(
                        FileLocations.OneHandedDataFilePath)));
            instances.setClassIndex(instances.numAttributes() - 1);
            var updatedLogistic = new Logistic();
            updatedLogistic.buildClassifier(instances);
            SerializationHelper.write(FileLocations.OneHandedModelFilePath, updatedLogistic);
        }

        private void GatherData(Controller controller, SaveDataListener listener)
        {
            while (string.IsNullOrEmpty(Gesture.GestureName))
            {
                Gesture.GestureName = InputBox.ShowInputBox("please enter the name of the gesture");
            }
            MessageBox.Show("please holds gesture until the next message box.");

            controller.FrameReady += listener.OnFrame;
            Thread.Sleep(10000);
            controller.StopConnection();
            controller.Dispose();
            MessageBox.Show("You may now stop performing the gesture, please wait while the algorithm retrains");
        }
    }
}