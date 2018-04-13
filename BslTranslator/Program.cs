using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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


