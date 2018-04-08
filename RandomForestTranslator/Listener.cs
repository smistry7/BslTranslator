using System;
using System.Collections.Generic;
using System.Linq;
using java.io;
using Leap;
using weka.classifiers.trees;
using weka.core;
using Console = System.Console;
using File = System.IO.File;


namespace BslTranslatorWeka
{
    partial class Program
    {
        public class Listener
        {
            private RandomForest RandomForest;
            private String[] classes =
                { "a", "b", "d", "e", "f", "i", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            public Listener()
            {
                RandomForest = (RandomForest)SerializationHelper.read(@"D:\Documents\BSL translator docs\Data mining stuff\models\RandomForest.model");
            }

            public void OnServiceConnect(object sender, ConnectionEventArgs args)
            {
                Console.WriteLine("Service Connected");
            }

            public void OnConnect(object sender, DeviceEventArgs args)
            {
                Console.WriteLine("Connected");
            }

            public void OnFrame(object sender, FrameEventArgs args)
            {
                Frame frame = args.frame;

                if (frame.Hands.Count != 2) return;

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
                handData[0] += fingerBends(right);
                handData[0] += fingerBends(left);
                handData[0] += NormalisedFingerPositions(right, left);
                handData[0] += NormalisedFingerPositions(left, right);
                handData[0] += fingerDistances(right, left);
                handData[0] += ",?";

                File.AppendAllLines(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageProgramData.arff",
                                    handData);
                Instances unlabeled = new Instances(
                    new BufferedReader(
                        new FileReader(@"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageProgramData.arff")));
                unlabeled.setClassIndex(unlabeled.numAttributes() - 1);
                var predictionInt = (int)RandomForest.classifyInstance(unlabeled.lastInstance());
                var prob = RandomForest.distributionForInstance(unlabeled.lastInstance()).Max();
                Console.WriteLine("Prediction: " + classes[predictionInt] + "  Probability: " + prob);

                //                List<Gesture> predList = new List<Gesture>();

                //                if (unlabeled.numInstances() % 50 == 0)
                //                {
                //                    double probability;
                //                    for (int i = unlabeled.numInstances() - 50; i < unlabeled.numInstances(); i++)
                //                    {
                //                        var predictionInt = (int)RandomForest.classifyInstance(unlabeled.get(i));
                //                        var prob = RandomForest.distributionForInstance(unlabeled.get(i)).Max();
                //
                //                        predList.Add(new Gesture
                //                        {
                //                            GestureName = classes[predictionInt],
                //                            Probability = prob
                //                        });
                //
                //                    }
                //
                //                    List<Gesture> prediction = (List<Gesture>) predList.GroupBy(x => x.GestureName)
                //                        .Select(group => new { Location = group.Key, Count = group.Count() })
                //                        .OrderByDescending(x => x.Count);
                //                    Console.WriteLine("Prediction: "+  prediction.First().GestureName + "probability: " + prediction.First().Probability);
                //                }



            }


            public static string VectorString(Vector vector)
            {
                return "," + vector.x + "," + vector.y + ","
                       + vector.z + "," + vector.Roll + "," + vector.Yaw + "," + vector.Magnitude;
            }



            public string fingerDistances(Hand hand1, Hand hand2)
            {
                string distances = "";

                foreach (var finger1 in hand1.Fingers)
                {
                    foreach (var finger2 in hand2.Fingers)
                    {
                        distances += "," + (finger1.TipPosition.Magnitude - finger2.TipPosition.Magnitude);
                    }
                }
                return distances;
            }

            public string fingerBends(Hand hand)
            {
                string fingerBendString = "";
                foreach (Finger finger in hand.Fingers)
                {
                    fingerBendString += ", " + FingerBend(finger);
                }
                return fingerBendString;
            }

            public string NormalisedFingerPositions(Hand hand1, Hand hand2)
            {
                string fingerPositions = "";
                foreach (Finger finger in hand1.Fingers)
                {
                    fingerPositions += "," + (finger.TipPosition.Normalized - hand2.PalmPosition.Normalized).Magnitude;
                }
                return fingerPositions;
            }

            private float CalcAngle(Vector v1, Vector v2)
            {
                var dotProduct = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
                var magnitudes = v1.Magnitude * v2.Magnitude;
                return (float)Math.Acos(dotProduct / magnitudes);
            }

            private bool AreWithin(Vector finger1, Vector finger2, int distance)
            {
                return (finger1 - finger2).Magnitude < distance;
            }

            private float FingerBend(Finger finger)
            {
                Bone proximal = finger.Bone(Bone.BoneType.TYPE_PROXIMAL);
                Bone distal = finger.Bone(Bone.BoneType.TYPE_DISTAL);
                float dot = proximal.Direction.Dot(distal.Direction);
                float flexed = 1.0f - (1.0f + dot) / 2.0f;
                return flexed;
            }

            private bool ExtendedFingers(Hand hand, int[] fingerInts)
            {

                for (int i = 0; i <= 4; i++)
                {
                    if (fingerInts.Contains(i))
                    {
                        if (!hand.Fingers[i].IsExtended) return false;

                    }
                    else
                    {
                        if (hand.Fingers[i].IsExtended) return false;
                    }
                }
                return true;

            }
        }
    }
}

