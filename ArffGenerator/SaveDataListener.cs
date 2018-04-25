using Leap;
using System.Collections.Generic;
using System.IO;

namespace ArffGenerator
{
    public class SaveDataListener
    {
        private readonly WriteArffMethods _writeArffMethods = new WriteArffMethods();
        public bool OneHandedGesture;
        public string OneHandedFilePath;
        public string TwoHandedFilePath;
        public SaveDataListener()
        {
             OneHandedFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName 
                + @"\DataSets\SingleHandData.arff";
            TwoHandedFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                + @"\DataSets\SignLanguageDataUpdateable.arff";

        }
        public void OnFrame(object sender, FrameEventArgs args)
        {
            Frame frame = args.frame;
            if (frame.Hands.Count == 1 && OneHandedGesture)
            {
                List<string> handData = _writeArffMethods.SingleHandData(frame, Gesture.GestureName);
                
                File.AppendAllLines( OneHandedFilePath, handData);
            }
            if (frame.Hands.Count == 2 && !OneHandedGesture)
            {
                Hand right;
                Hand left;
                if (frame.Hands[0].IsLeft)
                {
                    right = frame.Hands[1];
                    left = frame.Hands[0];
                }
                else
                {
                    right = frame.Hands[0];
                    left = frame.Hands[1];
                }

                var handData = _writeArffMethods.TwoHandData(frame, right, left, Gesture.GestureName);

                File.AppendAllLines(TwoHandedFilePath, handData);
            }
        }
    }
}