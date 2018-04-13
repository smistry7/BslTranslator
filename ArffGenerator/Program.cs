using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using Leap;

namespace ArffGenerator
{
    public static class Gesture
    {
        public static string GestureName { get; set; }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            SaveDataListener listener = new SaveDataListener();
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


