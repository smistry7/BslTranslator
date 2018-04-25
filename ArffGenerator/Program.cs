using Leap;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ArffGenerator
{
    public static class Gesture
    {
        public static string GestureName { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
           

            Controller controller = new Controller();
            SaveDataListener listener = new SaveDataListener() { OneHandedGesture = true };

            Console.WriteLine("Enter the name of the gesture");
            Gesture.GestureName = Console.ReadLine();
            Console.WriteLine("please hold gesture within 5 seconds");
            Thread.Sleep(5000);
            controller.FrameReady += listener.OnFrame;
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(5))
            {
            }

            Application.Restart();
        }
    }
}