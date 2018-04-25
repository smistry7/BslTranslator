using System;
using System.Collections.Generic;
using System.Linq;
using ArffGenerator;
using java.io;
using Leap;
using RandomForestTranslator;
using weka.classifiers.functions;
using weka.classifiers.trees;
using weka.core;
using Console = System.Console;
using File = System.IO.File;
using Frame = Leap.Frame;


namespace BslTranslatorWeka
{

    public class WekaClassifier
    {
        private readonly WriteArffMethods _writeArffMethods = new WriteArffMethods();
        private readonly RandomForest _twoHandedGestures;
        private readonly Logistic _oneHandedGestures;
        public readonly string[] _twoHandedClasses;
        private readonly string _fileLocationOneHand;
        private readonly string _fileLocationTwoHand;
        public readonly string[] _oneHandedClasses;
        public static List<string> MostProbable = new List<string>();
        public static List<string> SecondMostProbable = new List<string>();
        private bool _twoHands;
        private readonly bool isGui;

        private int _frameCount;


        public WekaClassifier(bool isGui)
        {
            this.isGui = isGui;
            
            _oneHandedGestures =
                (Logistic)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedLogistic.model");
            _twoHandedGestures =
                (RandomForest)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\updatedRandomForest.model");
            _fileLocationTwoHand = @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageProgramData.arff";
            _fileLocationOneHand =
                @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandProgramData.arff";
            var updatedClassesOneHand = File.ReadAllLines(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandData.arff")
                .Skip(18).Take(1).First();
            var updatedClassesTwoHand =
                File.ReadAllLines(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff")
                .Skip(48).Take(1).First();
            LineChanger(updatedClassesOneHand, _fileLocationOneHand, 19);
            LineChanger(updatedClassesTwoHand, _fileLocationTwoHand, 49);
            var classline = File.ReadAllLines(_fileLocationTwoHand).Skip(48).Take(1).First();
            var sub = getStringArray(classline);
            _twoHandedClasses = sub.Split(',');
            classline = File.ReadAllLines(_fileLocationOneHand).Skip(18).Take(1).First();
            sub = getStringArray(classline);
            _oneHandedClasses = sub.Split(',');


        }
        public void LoadGestures()
        {
            foreach (var gesture in _oneHandedClasses)
            {
                TextBoxValues.GestureList += gesture + "\n";
                
            }
            foreach (var gesture in _twoHandedClasses)
            {
                TextBoxValues.GestureList += gesture + "\n";

            }
        }

       
        public void OnConnect(object sender, DeviceEventArgs args)
        {
            if (!isGui)
            {
                Console.WriteLine("Connected");
            }
            else
            {
                TextBoxValues.Text += "Connected\n";
            }
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            //To allow more time for processing, only classify every 20th frame
            if (_frameCount % 20 != 0)
            {
                _frameCount++;
                return;

            }
            var frame = args.frame;


            if (!isGui)
            {
                ConsoleHandler(frame);
            }
            else
            {
                TextBoxHandler(frame);
            }


        }


        private void LineChanger(string newText, string fileName, int lineToEdit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[lineToEdit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
        private string getStringArray(string classline)
        {
            int startPos = classline.LastIndexOf("{", StringComparison.Ordinal) + "{".Length;
            int length = classline.IndexOf("}", StringComparison.Ordinal) - startPos;
            string sub = classline.Substring(startPos, length);
            return sub;
        }

        private void TextBoxHandler(Frame frame)
        {

            TextBoxValues.HandCount = frame.Hands.Count.ToString();
            if (frame.Hands.Count == 1)
            {
                if (_twoHands)
                {
                    System.Threading.Thread.Sleep(500);
                    _twoHands = false;
                }
                else
                {
                    var handData = _writeArffMethods.SingleHandData(frame, "?");

                    File.AppendAllLines(_fileLocationOneHand, handData);
                    var OneHandInstances = new Instances(new BufferedReader(new FileReader(_fileLocationOneHand)));
                    OneHandInstances.setClassIndex(OneHandInstances.numAttributes() - 1);
                    var predictionInt =
                        (int)_oneHandedGestures.classifyInstance(OneHandInstances.lastInstance());
                    double prob = _oneHandedGestures.distributionForInstance(OneHandInstances.lastInstance())
                        .Max();
                    if (prob > 0.50)
                    {
                        MostProbable.Add(_oneHandedClasses[predictionInt]);
                   

                    }
                }
            }
            else if (frame.Hands.Count == 2)
            {
                _twoHands = true;
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


                var handData = _writeArffMethods.TwoHandData(frame, right, left, "?");
                File.AppendAllLines(
                    _fileLocationTwoHand,
                    handData);
                var twoHandedInstances = new Instances(new BufferedReader(new FileReader(_fileLocationTwoHand)));
                twoHandedInstances.setClassIndex(twoHandedInstances.numAttributes() - 1);
                var instance = twoHandedInstances.lastInstance();
                var predictionInt = (int)_twoHandedGestures.classifyInstance(instance);
                var probabilities =
                    _twoHandedGestures.distributionForInstance(twoHandedInstances.lastInstance()).ToList();
                var prob = probabilities.Max();
                var secondHighest = (from number in probabilities
                                     orderby number descending
                                     select number).Distinct().Skip(1).First();
                int secondPrediction = probabilities.FindIndex(a => a.Equals(secondHighest));


                if (prob - secondHighest < 0.15 || prob < 0.6)
                {
                    MostProbable.Add(_twoHandedClasses[predictionInt]);
                    SecondMostProbable.Add(_twoHandedClasses[secondPrediction]);

                }
                else if(prob >0.6)
                {
                    MostProbable.Add(_twoHandedClasses[predictionInt]);
                }

            }
            if (MostProbable.Count % 10 == 0 && MostProbable.Count != 0)
            {
                var listOccurances = CreateDictionary(MostProbable);
                var gesture = (from x in listOccurances where x.Value == listOccurances.Max(v => v.Value) select x.Key).ToList();
                if (gesture[0].Length > 1)
                {
                    TextBoxValues.Text += gesture[0] + " ";
                }
                else
                {
                    TextBoxValues.Text += gesture[0];
                }
                MostProbable = new List<string>();
            }
            if (SecondMostProbable.Count % 10 == 0 && SecondMostProbable.Count != 0)
            {
                var listOccurances = CreateDictionary(SecondMostProbable);
                var gesture = (from x in listOccurances where x.Value == listOccurances.Max(v => v.Value) select x.Key).ToList();

                TextBoxValues.SecondOption = gesture[0];
                SecondMostProbable = new List<string>();
            }
            _frameCount++;
        }
        private void ConsoleHandler(Frame frame)
        {
            if (frame.Hands.Count == 1)
            {
                if (_twoHands)
                {
                    System.Threading.Thread.Sleep(500);
                    _twoHands = false;
                }
                else
                {
                    var handData = _writeArffMethods.SingleHandData(frame, "?");

                    File.AppendAllLines(_fileLocationOneHand, handData);
                    var OneHandInstances = new Instances(new BufferedReader(new FileReader(_fileLocationOneHand)));
                    OneHandInstances.setClassIndex(OneHandInstances.numAttributes() - 1);
                    var predictionInt =
                        (int)_oneHandedGestures.classifyInstance(OneHandInstances.lastInstance());
                    double prob = _oneHandedGestures.distributionForInstance(OneHandInstances.lastInstance())
                        .Max();
                    if (prob > 0.80)
                    {

                        Console.WriteLine("Prediction: " + _oneHandedClasses[predictionInt] +
                                          "  Probability: " + prob);

                    }
                }
            }
            else if (frame.Hands.Count == 2)
            {
                _twoHands = true;
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


                var handData = _writeArffMethods.TwoHandData(frame, right, left, "?");

                File.AppendAllLines(
                    _fileLocationTwoHand,
                    handData);
                var twoHandedInstances = new Instances(new BufferedReader(new FileReader(_fileLocationTwoHand)));
                twoHandedInstances.setClassIndex(twoHandedInstances.numAttributes() - 1);
                var instance = twoHandedInstances.lastInstance();
                var predictionInt = (int)_twoHandedGestures.classifyInstance(instance);
                var probabilities =
                    _twoHandedGestures.distributionForInstance(twoHandedInstances.lastInstance()).ToList();
                var prob = probabilities.Max();
                var secondHighest = (from number in probabilities
                                     orderby number descending
                                     select number).Distinct().Skip(1).First();
                int secondInt = probabilities.FindIndex(a => a.Equals(secondHighest));


                if (prob - secondHighest < 0.15)
                {
                    Console.WriteLine("Prediction: " + _twoHandedClasses[predictionInt] +
                                      "  Probability: " +
                                      prob + " OR Prediction: " + _twoHandedClasses[secondInt] +
                                      "  Probability: " +
                                      secondHighest);
                }
                else
                {
                    Console.WriteLine("Prediction: " + _twoHandedClasses[predictionInt] +
                                  "  Probability: " + prob);
                }


            }
            _frameCount++;
        }

        private Dictionary<string, int> CreateDictionary(List<string> stringList)
        {
            var dictionary = new Dictionary<string, int>();
            foreach (var a in stringList)
            {
                if (dictionary.ContainsKey(a)) continue;
                int count = MostProbable.Count(b => a == b);
                dictionary.Add(a, count);
            }
            return dictionary;
        }
    }
}


