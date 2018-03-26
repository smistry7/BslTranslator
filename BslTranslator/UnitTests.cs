using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Leap;
using NUnit.Framework;
using FluentAssertions;
using LeapInternal;
using Essy.Tools.InputBox;
using System.Threading;
namespace BslTranslator
{
    static class Gesture
    {
        public static string GestureName { get; set; }
    }
    [TestFixture]
    class UnitTests
    {
        private Controller controller;
        private SampleListener listener;
        [SetUp]
        public void setUp()
        {
            controller = new Controller();
            listener = new SampleListener();
        }

        [Test]
        public void TestA()
        {
            RunTest();
        }
        [Test]
        public void TestB()
        {
            RunTest();
        }
    
        [Test]
        public void TestD()
        {
            RunTest();
        }
        [Test]
        public void TestE()
        {
            RunTest();
        }
        [Test]
        public void TestF()
        {
            RunTest();
        }
        [Test]
        public void TestH()
        {
            RunTest();
        }
        [Test]
        public void TestI()
        {
            RunTest();
        }
        [Test]
        public void TestK()
        {
            RunTest();
        }
        [Test]
        public void TestL()
        {
            RunTest();
        }
        [Test]
        public void TestM()
        {
            RunTest();
        }
        [Test]
        public void TestN()
        {
            RunTest();
        }
        [Test]
        public void TestO()
        {
            RunTest();
        }
        [Test]
        public void TestP()
        {
            RunTest();
        }
        [Test]
        public void TestQ()
        {
            RunTest();
        }
        [Test]
        public void TestR()
        {
            RunTest();
        }
        [Test]
        public void TestS()
        {
            RunTest();
        }
        [Test]
        public void TestT()
        {
            RunTest();
        }
        [Test]
        public void TestU()
        {
            RunTest();
        }
        [Test]
        public void TestV()
        {
            RunTest();
        }
        [Test]
        public void TestW()
        {
            RunTest();
        }
        [Test]
        public void TestX()
        {
            RunTest();
        }
        [Test]
        public void TestY()
        {
            RunTest();
        }
        [Test]
        public void TestZ()
        {
            RunTest();
        }

        private void RunTest()
        {
            var expectedString = InputBox.ShowInputBox("please enter the gesture you are about to perform").ToLower();
            Gesture.GestureName = expectedString;
            Thread.Sleep(3000);
            controller.Connect += listener.OnServiceConnect;
            controller.Device += listener.OnConnect;
            controller.FrameReady += listener.OnFrame;
            Thread.Sleep(15000);
            double correctRecognition = 0;
            double incorrectRecognition = 0;
            foreach (var letter in listener.possibleGestures)
            {
                if (letter.Contains(expectedString))
                {
                    correctRecognition++;
                }
                else incorrectRecognition++;
            }
            double accuracy = (correctRecognition / listener.possibleGestures.Count) * 100;

            if (accuracy > 70)
            {
                Assert.Pass("achieved {0:.##}% accuracy for letter {1}\n total instances: {2}\n " +
                            "Correctly identified: {3} ", accuracy, expectedString, listener.possibleGestures.Count,
                    correctRecognition);
            }
            else Assert.Fail("achieved {0:.##}% accuracy for letter {1}\n total instances: {2}\n " +
                             "Correctly identified: {3} ", accuracy, expectedString, listener.possibleGestures.Count,
                             correctRecognition);
        }

        [TearDown]
        public void tearDown()
        {
            controller.StopConnection();
            controller.Dispose();
        }
    }

    class SampleListener
    {
        public List<string> possibleGestures= new List<string>();
        public static Queue<string[]> queue = new Queue<string[]>(50);
        readonly BslAlphabet alphabet = new BslAlphabet();

        public void OnServiceConnect(object sender, ConnectionEventArgs args)
        {
//            Console.WriteLine("Service Connected");

        }

        public void OnConnect(object sender, DeviceEventArgs args)
        {
//            Console.WriteLine("Connected");
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            // Get the most recent frame and report some basic information
            Frame frame = args.frame;
            
            LeapFrame LeapFrame = new LeapFrame();
            if (frame.Hands.Count == 2)
            {
                LeapFrame.A = alphabet.A(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.A)
                {
                   
                    possibleGestures.Add("A");
                }

                LeapFrame.B = alphabet.B(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.B)
                {
                    
                    possibleGestures.Add("B");
                }

                LeapFrame.V = alphabet.V(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.V)
                {
                   
                    possibleGestures.Add("V");
                }

                LeapFrame.D = alphabet.D(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.D)
                {
                   
                    possibleGestures.Add("D");
                }

                LeapFrame.E = alphabet.E(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.E)
                {
                    
                    possibleGestures.Add("E");
                }

                LeapFrame.F = alphabet.F(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.F)
                {
                
                    possibleGestures.Add("F");
                }

                LeapFrame.H = alphabet.H(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.H)
                {
                    possibleGestures.Add("H");
                }

                LeapFrame.I = alphabet.I(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.I)
                {
                   
                    possibleGestures.Add("I");
                }

               

                LeapFrame.K = alphabet.K(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.K)
                {
                   
                    possibleGestures.Add("K");
                }

                LeapFrame.L = alphabet.L(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.L)
                {
                    
                    possibleGestures.Add("L");
                }

                LeapFrame.M = alphabet.M(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.M)
                {
                   
                    possibleGestures.Add("M");
                }

                LeapFrame.N = alphabet.N(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.N)
                {
                 
                    possibleGestures.Add("N");
                }

                LeapFrame.O = alphabet.O(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.O)
                {
      
                    possibleGestures.Add("O");
                }

                LeapFrame.P = alphabet.P(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.P)
                {
                   
                    possibleGestures.Add("P");
                }

                LeapFrame.T = alphabet.T(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.T)
                {
               
                    possibleGestures.Add("T");
                }

                LeapFrame.U = alphabet.U(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.U)
                {
             
                    possibleGestures.Add("U");
                }

                LeapFrame.X = alphabet.X(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.X)
                {
        
                    possibleGestures.Add("X");
                }
                LeapFrame.Y = alphabet.Y(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.Y)
                {
      
                    possibleGestures.Add("Y");
                }
                LeapFrame.Z = alphabet.Z(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.Z)

                {
                
                    possibleGestures.Add("Z");
                }

            }
            if (args.frame.Hands.Count != 2) return;

            Hand right;
            Hand left;
            if (args.frame.Hands[0].IsLeft)
            {
                right = args.frame.Hands[1];
                left = args.frame.Hands[0];
            }
            else
            {
                right = args.frame.Hands[0];
                left = args.frame.Hands[1];
            }


            var handData = new List<string> { args.frame.Hands.Count.ToString() };
            handData[0] += fingerBends(right);
            handData[0] += fingerBends(left);
            handData[0] += NormalisedFingerPositions(right, left);
            handData[0] += NormalisedFingerPositions(left, right);


            handData[0] += fingerDistances(right, left);
            handData[0] += "," + Gesture.GestureName;
            File.AppendAllLines(
                @"D:\Documents\BSL translator docs\Data mining stuff\SignLanguageGesturesTest-Data2.arff",
                handData);
            if (frame.Hands.Count == 1)
            {
                LeapFrame.C = alphabet.C(frame.Hands[0]);
                if (LeapFrame.C)
                {
                
                    possibleGestures.Add("C");
                }
                LeapFrame.G = alphabet.G(frame.Hands[0]);
                if (LeapFrame.G)
                {
                   
                    possibleGestures.Add("G");
                }

            }

        }
        private string fingerDistances(Hand hand1, Hand hand2)
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

        private string fingerBends(Hand hand)
        {
            string fingerBendString = "";
            foreach (Finger finger in hand.Fingers)
            {
                fingerBendString += ", " + FingerBend(finger);
            }
            return fingerBendString;
        }

        private string NormalisedFingerPositions(Hand hand1, Hand hand2)
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
