using Leap;
using Console = System.Console;

namespace BslTranslatorWeka
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            var controller = new Controller();
            var wekaClassifier = new WekaClassifier(false);

            controller.Device += wekaClassifier.OnConnect;

            controller.FrameReady += wekaClassifier.OnFrame;
            //  Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();
            controller.Dispose();
        }
    }
}