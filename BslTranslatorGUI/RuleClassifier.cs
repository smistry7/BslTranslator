using BslTranslator;
using Leap;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using Frame = Leap.Frame;

namespace BslTranslatorGUI
{
    internal class RuleClassifier
    {
        private TextBox HandCount;
        private TextBox GestureText;
        private bool TwoHands;

        public RuleClassifier(TextBox GestureText, TextBox HandCount)
        {
            this.GestureText = GestureText;
            this.HandCount = HandCount;
        }

        public static Queue<string[]> queue = new Queue<string[]>(50);
        private readonly BslAlphabet alphabet = new BslAlphabet();

        public void OnConnect(object sender, DeviceEventArgs args)
        {
        }

        public void OnFrame(object sender, FrameEventArgs args)
        {
            // Get the most recent frame and report some basic information

            var frame = args.frame;
            try
            {
                HandCount.Text = frame.Hands.Count.ToString();
            }
            catch
            {
                return;
            }
            var possibleGestures = new List<string>();
            var leapFrame = new LeapFrame();
            if (frame.Hands.Count == 2)
            {
                TwoHands = true;
                leapFrame.A = alphabet.A(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.A)
                {
                    possibleGestures.Add("A");
                }

                leapFrame.B = alphabet.B(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.B)
                {
                    possibleGestures.Add("B");
                }

                leapFrame.V = alphabet.V(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.V)
                {
                    possibleGestures.Add("V");
                }

                leapFrame.D = alphabet.D(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.D)
                {
                    possibleGestures.Add("D");
                }

                leapFrame.E = alphabet.E(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.E)
                {
                    possibleGestures.Add("E");
                }

                leapFrame.F = alphabet.F(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.F)
                {
                    possibleGestures.Add("F");
                }

                leapFrame.H = alphabet.H(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.H)
                {
                    possibleGestures.Add("H");
                }

                leapFrame.I = alphabet.I(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.I)
                {
                    possibleGestures.Add("I");
                }

                leapFrame.K = alphabet.K(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.K)
                {
                    possibleGestures.Add("K");
                }

                leapFrame.L = alphabet.L(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.L)
                {
                    possibleGestures.Add("L");
                }

                leapFrame.M = alphabet.M(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.M)
                {
                    possibleGestures.Add("M");
                }

                leapFrame.N = alphabet.N(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.N)
                {
                    possibleGestures.Add("N");
                }

                leapFrame.O = alphabet.O(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.O)
                {
                    possibleGestures.Add("O");
                }

                leapFrame.P = alphabet.P(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.P)
                {
                    possibleGestures.Add("P");
                }

                leapFrame.T = alphabet.T(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.T)
                {
                    possibleGestures.Add("T");
                }

                leapFrame.U = alphabet.U(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.U)
                {
                    possibleGestures.Add("U");
                }

                leapFrame.X = alphabet.X(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.X)
                {
                    possibleGestures.Add("X");
                }
                leapFrame.Y = alphabet.Y(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.Y)
                {
                    possibleGestures.Add("Y");
                }
                leapFrame.Z = alphabet.Z(frame.Hands[0], frame.Hands[1]);
                if (leapFrame.Z)

                {
                    possibleGestures.Add("Z");
                }
            }
            if (frame.Hands.Count == 1)
            {
                if (TwoHands)
                {
                    Thread.Sleep(1000);
                    TwoHands = false;
                }
                else
                {
                    leapFrame.C = alphabet.C(frame.Hands[0]);
                    if (leapFrame.C)
                    {
                        possibleGestures.Add("C");
                    }
                    leapFrame.G = alphabet.G(frame.Hands[0]);
                    if (leapFrame.G)
                    {
                        possibleGestures.Add("G");
                    }
                }
            }

            if (possibleGestures.Count != 0) queue.Enqueue(possibleGestures.ToArray());
            //find most common item in each string array in queue, add that to an array then find
            //the most common one out of those
            if (queue.Count % 75 != 0 || queue.Count == 0) return;
            var mostCommon = new List<string>();
            foreach (var stringArr in queue)
            {
                foreach (string a in stringArr) mostCommon.Add(a);
            }
            GestureText.AppendText(mostCommon.GroupBy(x => x).OrderBy(g => g.Key).Distinct().First().Key);
            queue.Clear();
        }
    }
}