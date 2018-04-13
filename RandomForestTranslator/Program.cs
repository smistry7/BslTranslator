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
 

    partial class Program
    {


        static void Main(string[] args)
        {


            Controller controller = new Controller();
            LeapMotionClassifier leapMotionClassifier = new LeapMotionClassifier();
            controller.Connect += leapMotionClassifier.OnServiceConnect;
            controller.Device += leapMotionClassifier.OnConnect;
       

            controller.FrameReady += leapMotionClassifier.OnFrame;
            //  Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
            controller.Dispose();
    


        }

    }

}

