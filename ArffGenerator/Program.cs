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

            bool onehanded = false;
            Controller controller = new Controller();
            Console.WriteLine("how many hands does this gesture require");
            var response = Console.ReadLine();
            switch (response)
            {
                case "1":
                    onehanded = true;
                    break;
                case "2":
                    onehanded = false;
                    break;
                default:
                    Console.WriteLine("please enter a value that is either 1 or 2");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
            }
            var listener = new SaveDataListener() { OneHandedGesture = onehanded };

            Console.WriteLine("Enter the name of the gesture");
            Gesture.GestureName = Console.ReadLine();
            Console.WriteLine("please hold gesture within 5 seconds");
            Thread.Sleep(5000);
            controller.FrameReady += listener.OnFrame;
            DateTime now = DateTime.Now;
            while (DateTime.Now < now.AddSeconds(15))
            {
            }

            Application.Restart();
        }
    }
}