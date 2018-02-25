using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
        readonly BslAlphabet alphabet = new BslAlphabet();

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



            if (frame.Hands.Count == 2)
            {
                if (alphabet.X(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("X");
                }
                else if (alphabet.K(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("K");
                }

                else if (alphabet.P(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("P");
                }
                else if (alphabet.A(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("A");
                }
                else if (alphabet.D(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("D");
                }
                else if (alphabet.B(frame.Hands[0], frame.Hands[1]))
                {

                    Console.WriteLine("B");
                }
                else if (alphabet.E(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("E");
                }

                else if (alphabet.J(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("J");
                }

                else if (alphabet.F(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("F");
                }

                else if (alphabet.I(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("I");
                }
                else if (alphabet.O(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("O");
                }
                else if (alphabet.U(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("U");
                }
                else if (alphabet.C(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("C");
                }
                else if (alphabet.H(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("H");
                }
                else if (alphabet.T(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("T");
                }
                else if (alphabet.L(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("L");
                }
                else if (alphabet.M(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("M");
                }
                else if (alphabet.N(frame.Hands[0], frame.Hands[1]))
                {
                    Console.WriteLine("N");
                }


            }


            else if (frame.Hands.Count == 1)
            {

                if (alphabet.G(frame.Hands[0]))
                {
                    Console.WriteLine("G");
                }
                if (alphabet.C(frame.Hands[0]))
                {
                    Console.WriteLine("C");
                }
            }
    
        }



    }
}


