using System;
using System.Collections.Generic;
using System.Linq;
using ArffGenerator;
using java.io;
using Leap;
using weka.classifiers.functions;
using weka.classifiers.lazy;
using weka.classifiers.trees;
using weka.core;
using Console = System.Console;
using File = System.IO.File;


namespace BslTranslatorWeka
{
    partial class Program
    {
       
       
        public class LeapMotionClassifier
        {
            private readonly WriteArffMethods writeArffMethods = new WriteArffMethods();
            private readonly RandomForest TwoHandedGestures;
            private readonly Logistic OneHandedGestures;
            private readonly string[] TwoHandedClasses;
            private readonly string fileLocationOneHand;
            private readonly string fileLocationTwoHand;
            private readonly string[] OneHandedClasses;
            private bool twoHands;

            private int _frameCount;
            public LeapMotionClassifier()
            {
                OneHandedGestures =
                    (Logistic)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\SingleHandLogistic.model");
                TwoHandedGestures =
                    (RandomForest)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedRandomForest.model");
                fileLocationTwoHand = @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageProgramData.arff";
                fileLocationOneHand =
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandProgramData.arff";

                var updatedClassesTwoHand = 
                    File.ReadAllLines(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff")
                    .Skip(48).Take(1).First();
                lineChanger(updatedClassesTwoHand,fileLocationTwoHand,49);
                var classline = File.ReadAllLines(fileLocationTwoHand).Skip(48).Take(1).First();
                var sub = getStringArray(classline);
                TwoHandedClasses = sub.Split(',');
                classline = File.ReadAllLines(fileLocationOneHand).Skip(18).Take(1).First();
                sub = getStringArray(classline);
                OneHandedClasses = sub.Split(',');
              

            }


            public void OnServiceConnect(object sender, ConnectionEventArgs args)
            {

            }

            public void OnConnect(object sender, DeviceEventArgs args)
            {
                Console.WriteLine("Connected");
            }

            public void OnFrame(object sender, FrameEventArgs args)
            {
                if (_frameCount % 20 != 0)
                {
                    _frameCount++;
                    return;
                }
                Frame frame = args.frame;
                switch (frame.Hands.Count)
                {
                    case 1:
                        {
                            if (twoHands)
                            {
                                System.Threading.Thread.Sleep(1000);
                                twoHands = false;
                            }
                            else
                            {
                                List<string> handData = new List<string>() { frame.Hands.Count.ToString() };

                                handData[0] += writeArffMethods.FingerBends(frame.Hands[0]);
                                handData[0] += writeArffMethods.SingleFingerDistances(frame.Hands[0]);
                                handData[0] += ",?";

                                File.AppendAllLines(fileLocationOneHand, handData);
                                Instances OneHandInstances = new Instances(new BufferedReader(new FileReader(fileLocationOneHand)));
                                OneHandInstances.setClassIndex(OneHandInstances.numAttributes() - 1);
                                var predictionInt =
                                    (int)OneHandedGestures.classifyInstance(OneHandInstances.lastInstance());
                                var prob = OneHandedGestures.distributionForInstance(OneHandInstances.lastInstance())
                                    .Max();
                                if (prob > 0.80)
                                {
                                    Console.WriteLine("Prediction: " + OneHandedClasses[predictionInt] + "  Probability: " + prob);
                                }
                            }
                            _frameCount++;
                            break;
                        }
                    case 2:
                        {
                            twoHands = true;
                            Hand right;
                            Hand left;
                            if (frame.Hands[0].IsLeft)
                            {
                                right = frame.Hands[1];
                                left = frame.Hands[0];
                            }
                            else
                            {
                                right = frame.Hands[0];
                                left = frame.Hands[1];
                            }


                            var handData = new List<string> { args.frame.Hands.Count.ToString() };
                            handData[0] += writeArffMethods.FingerBends(right);
                            handData[0] += writeArffMethods.FingerBends(left);
                            handData[0] += writeArffMethods.NormalisedFingerPositions(right, left);
                            handData[0] += writeArffMethods.NormalisedFingerPositions(left, right);
                            handData[0] += writeArffMethods.FingerDistances(right, left);
                            handData[0] += ",?";

                            File.AppendAllLines(
                                fileLocationTwoHand,
                                handData);
                            Instances TwoHandedInstances = new Instances(new BufferedReader(new FileReader(fileLocationTwoHand)));
                            TwoHandedInstances.setClassIndex(TwoHandedInstances.numAttributes() - 1);
                            var instance = TwoHandedInstances.lastInstance();
                            var predictionInt = (int)TwoHandedGestures.classifyInstance(instance);
                            var probabilities =
                                TwoHandedGestures.distributionForInstance(TwoHandedInstances.lastInstance());
                            var prob = probabilities.Max();
                            if (prob > 0)
                            {
                                Console.WriteLine("Prediction: " + TwoHandedClasses[predictionInt] + "  Probability: " +
                                                  prob);
                            }
                            _frameCount++;
                            break;
                        }

                }

            }
            static void lineChanger(string newText, string fileName, int line_to_edit)
            {
                string[] arrLine = File.ReadAllLines(fileName);
                arrLine[line_to_edit - 1] = newText;
                File.WriteAllLines(fileName, arrLine);
            }
            private string getStringArray(string classline)
            {
                int startPos = classline.LastIndexOf("{", StringComparison.Ordinal) + "{".Length;
                int length = classline.IndexOf("}", StringComparison.Ordinal) - startPos;
                string sub = classline.Substring(startPos, length);
                return sub;
            }
        }
    }
}

