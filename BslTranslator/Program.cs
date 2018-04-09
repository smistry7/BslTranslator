using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BslTranslator;
using Leap;

namespace Leap
{
    class Program
    {
        public static Controller controller;

        public static void Main()
        {

            controller = new Controller();
            var listener = new SampleListener();
            controller.Connect += listener.OnServiceConnect;
            controller.Device += listener.OnConnect;
            controller.FrameReady += listener.OnFrame;

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
            controller.StopConnection();
            controller.Dispose();
        }
    }

    class SampleListener
    {

        public static Queue<string[]> queue = new Queue<string[]>(50);
        readonly BslAlphabet alphabet = new BslAlphabet();
        private bool NewGesture;
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


            // Get the most recent frame and report some basic information
            Frame frame = args.frame;
            List<string> possibleGestures = new List<string>();
            LeapFrame LeapFrame = new LeapFrame();
            if (frame.Hands.Count == 2)
            {
                if (frame.Hands[1].PalmVelocity.Magnitude > 20 || frame.Hands[0].PalmVelocity.Magnitude > 20)
                {
                    NewGesture = true;
                }

                if (!NewGesture)
                {
                }
                else
                {
                    LeapFrame.A = alphabet.A(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.A)
                    {
                        Console.WriteLine("A");
                        possibleGestures.Add("A");
                    }

                    LeapFrame.B = alphabet.B(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.B)
                    {
                        Console.WriteLine("B");
                        possibleGestures.Add("B");
                    }

                    LeapFrame.V = alphabet.V(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.V)
                    {
                        Console.WriteLine("V");
                        possibleGestures.Add("V");
                    }

                    LeapFrame.D = alphabet.D(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.D)
                    {
                        Console.WriteLine("D");
                        possibleGestures.Add("D");
                    }

                    LeapFrame.E = alphabet.E(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.E)
                    {
                        Console.WriteLine("E");
                        possibleGestures.Add("E");
                    }

                    LeapFrame.F = alphabet.F(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.F)
                    {
                        Console.WriteLine("F");
                        possibleGestures.Add("F");
                    }

                    LeapFrame.H = alphabet.H(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.H)
                    {
                        Console.WriteLine("H");
                        possibleGestures.Add("H");
                    }

                    LeapFrame.I = alphabet.I(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.I)
                    {
                        Console.WriteLine("I");
                        possibleGestures.Add("I");
                    }


                    LeapFrame.K = alphabet.K(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.K)
                    {
                        Console.WriteLine("K");
                        possibleGestures.Add("K");
                    }

                    LeapFrame.L = alphabet.L(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.L)
                    {
                        Console.WriteLine("L");
                        possibleGestures.Add("L");
                    }

                    LeapFrame.M = alphabet.M(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.M)
                    {
                        Console.WriteLine("M");
                        possibleGestures.Add("M");
                    }

                    LeapFrame.N = alphabet.N(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.N)
                    {
                        Console.WriteLine("N");
                        possibleGestures.Add("N");
                    }

                    LeapFrame.O = alphabet.O(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.O)
                    {
                        Console.WriteLine("O");
                        possibleGestures.Add("O");
                    }

                    LeapFrame.P = alphabet.P(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.P)
                    {
                        Console.WriteLine("P");
                        possibleGestures.Add("P");
                    }

                    LeapFrame.T = alphabet.T(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.T)
                    {
                        Console.WriteLine("T");
                        possibleGestures.Add("T");
                    }

                    LeapFrame.U = alphabet.U(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.U)
                    {
                        Console.WriteLine("U");
                        possibleGestures.Add("U");
                    }

                    LeapFrame.X = alphabet.X(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.X)
                    {
                        Console.WriteLine("X");
                        possibleGestures.Add("X");
                    }
                    LeapFrame.W = alphabet.W(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.W)
                    {
                        Console.WriteLine("W");
                        possibleGestures.Add("W");
                    }
                    LeapFrame.S = alphabet.S(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.S)
                    {
                        Console.WriteLine("S");
                        possibleGestures.Add("S");
                    }
                    LeapFrame.Y = alphabet.Y(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.Y)
                    {
                        Console.WriteLine("Y");
                        possibleGestures.Add("Y");
                    }
                    LeapFrame.Z = alphabet.Z(frame.Hands[0], frame.Hands[1]);
                    if (LeapFrame.Z)

                    {
                        Console.WriteLine("Z");
                        possibleGestures.Add("Z");
                    }
                }
            }
            if (frame.Hands.Count == 1)
            {
                if (frame.Hands[0].PalmVelocity.Magnitude > 20)
                {
                    NewGesture = true;
                }

                if (!NewGesture)
                {
                }
                else
                {
                    LeapFrame.C = alphabet.C(frame.Hands[0]);
                    if (LeapFrame.C)
                    {
                        Console.WriteLine("C");
                        possibleGestures.Add("C");
                    }
                    LeapFrame.G = alphabet.G(frame.Hands[0]);
                    if (LeapFrame.G)
                    {
                        Console.WriteLine("G");
                        possibleGestures.Add("G");
                    }
                    //                if (alphabet.One(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("1");
                    //                }
                    //                if (alphabet.Two(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("2");
                    //                }
                    //                if (alphabet.Three(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("3");
                    //                }
                    //                if (alphabet.Four(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("4");
                    //                }
                    //                if (alphabet.Five(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("5");
                    //                }
                    //                if (alphabet.Six(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("6");
                    //                }
                    //                if (alphabet.Seven(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("7");
                    //                }
                    //                if (alphabet.Eight(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("8");
                    //                }
                    //                if (alphabet.Nine(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("9");
                    //                }
                    //                if (alphabet.Zero(frame.Hands[0]))
                    //                {
                    //                    Console.WriteLine("0");
                    //                }

                }
            }

            if (possibleGestures.Count != 0) queue.Enqueue(possibleGestures.ToArray());
            //find most common item in each string array in queue, add that to an array then find 
            //the most common one out of those
            if (queue.Count % 50 == 0 && queue.Count != 0)
            {
                List<string> mostCommon = new List<string>();
                foreach (var stringArr in queue)
                {
                    foreach (string a in stringArr) mostCommon.Add(a);
                }
                var ExpectedTerm = mostCommon.GroupBy(x => x).OrderBy(g => g.Key).First().Key;
                queue.Clear();
                NewGesture = false;

            }


        }
    }
}


