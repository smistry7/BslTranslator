using System;
using System.Globalization;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Xml.Serialization;
using Leap;
using static weka.core.SerializationHelper;
using Console = System.Console;


namespace BslTranslatorWeka
{
    class Gesture
    {
        public string GestureName { get; set; }
        public double Probability { get; set; }
    }

    partial class Program
    {


        static void Main(string[] args)
        {


            Controller controller = new Controller();
            Listener listener = new Listener();
            controller.Connect += listener.OnServiceConnect;
            controller.Device += listener.OnConnect;


            DateTime now = DateTime.Now;

            controller.FrameReady += listener.OnFrame;
            //  Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
            controller.Dispose();
    


        }

    }

}

