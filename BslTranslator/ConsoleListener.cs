using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BslTranslator;

namespace Leap
{
    class ConsoleListener
    {
        
        public static Queue<string[]> Queue = new Queue<string[]>(50);
        readonly BslAlphabet alphabet = new BslAlphabet();
        private bool twoHands;
  

        public void OnConnect(object sender, DeviceEventArgs args)
        {
            Console.WriteLine("Connected");
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            var frame = args.frame;
            var possibleGestures = new List<string>();
            var LeapFrame = new LeapFrame();
            if (frame.Hands.Count == 2)
            {
                twoHands = true;
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
                LeapFrame.W = alphabet.W(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.W)
                {

                    possibleGestures.Add("W");
                }
                LeapFrame.S = alphabet.S(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.S)
                {

                    possibleGestures.Add("S");
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
                LeapFrame.Q = alphabet.Q(frame.Hands[0], frame.Hands[1]);
                if (LeapFrame.Q)

                {

                    possibleGestures.Add("Q");
                }
            }
           
            if (frame.Hands.Count == 1)
            {
                if (twoHands)
                {
                    Thread.Sleep(1000);
                    twoHands = false;
                }
                else
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
                    if (alphabet.One(frame.Hands[0]))
                    {
                        possibleGestures.Add("1");
                    }
                    if (alphabet.Two(frame.Hands[0]))
                    {
                        possibleGestures.Add("2");
                    }
                    if (alphabet.Three(frame.Hands[0]))
                    {
                        possibleGestures.Add("3");
                    }
                    if (alphabet.Four(frame.Hands[0]))
                    {
                        possibleGestures.Add("4");
                    }
                    if (alphabet.Five(frame.Hands[0]))
                    {
                        possibleGestures.Add("5");
                    }
                    if (alphabet.Six(frame.Hands[0]))
                    {
                        possibleGestures.Add("6");
                    }
                    if (alphabet.Seven(frame.Hands[0]))
                    {
                        possibleGestures.Add("7");
                    }
                    if (alphabet.Eight(frame.Hands[0]))
                    {
                        possibleGestures.Add("8");
                    }
                    if (alphabet.Nine(frame.Hands[0]))
                    {
                        possibleGestures.Add("9");
                    }
                    if (alphabet.Zero(frame.Hands[0]))
                    {
                        possibleGestures.Add("0");
                    }
                }
            }


            if (possibleGestures.Count != 0) Queue.Enqueue(possibleGestures.ToArray());
            //find most common item in each string array in queue, add that to an array then find 
            //the most common one out of those
            if (Queue.Count % 50 == 0 && Queue.Count != 0)
            {
                List<string> mostCommon = new List<string>();
                foreach (var stringArr in Queue)
                {
                    foreach (string a in stringArr) mostCommon.Add(a);
                }
                var ExpectedTerm = mostCommon.GroupBy(x => x).OrderBy(g => g.Key).First().Key;
                Console.WriteLine(ExpectedTerm);
                Queue.Clear();


            }
            
        }
    }
}