using System;
using System.Collections.Generic;
using System.Linq;
using Leap;

namespace Leap
{
    class Program
    {
        public static void Main()
        {

            var controller = new Controller();
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

            //            Console.WriteLine(
            //                "Frame id: {0}, timestamp: {1}, hands: {2}",
            //                frame.Id, frame.Timestamp, frame.Hands.Count
            //            );

            foreach (Hand hand in frame.Hands)
            {
                //                Console.WriteLine("  Hand id: {0}, palm position: {1}, fingers: {2}",
                //                    hand.Id, hand.PalmPosition, hand.Fingers.Count);
                // Get the hand's normal vector and direction
                Vector normal = hand.PalmNormal;
                if (frame.Hands.Count == 2)
                {



                    if (alphabet.A(frame.Hands[0], frame.Hands[1]))
                    {
                        Console.WriteLine("A");
                    }
                    else if (alphabet.B(frame.Hands[0], frame.Hands[1]))
                    {
                        Console.WriteLine("B");
                    }
                    else if (alphabet.E(frame.Hands[0], frame.Hands[1]))
                    {
                        Console.WriteLine("E");
                    }
                    else if (alphabet.D(frame.Hands[0], frame.Hands[1]))
                    {
                        Console.WriteLine("D");
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
                }
                if (frame.Hands.Count == 1)
                {
                    if (alphabet.G(frame.Hands[0]))
                    {
                        Console.WriteLine("G");
                    }
                    if (alphabet.C(frame.Hands[0]))
                    {
                        Console.WriteLine("C");
                    }
                    // Console.WriteLine(hand.PalmNormal);
                }
                Vector direction = hand.Direction;

              
            }
        }
    }
}
