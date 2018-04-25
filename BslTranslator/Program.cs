using System;

namespace Leap
{
    internal class Program
    {
        public static Controller controller;

        public static void Main()
        {
            controller = new Controller();
            var listener = new ConsoleListener();
            controller.Device += listener.OnConnect;
            controller.FrameReady += listener.OnFrame;

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
            controller.StopConnection();
            controller.Dispose();
        }
    }
}