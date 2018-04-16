using System.Collections.Generic;
using System.IO;
using BslTranslator;
using Leap;

namespace ArffGenerator
{
    public class SaveDataListener
    {
        HandDataMethods handDataMethods = new HandDataMethods();
        WriteArffMethods writeArffMethods = new WriteArffMethods();

        public void OnFrame(object sender, FrameEventArgs args)
        {
            Frame frame = args.frame;
            if (frame.Hands.Count == 1)
            {
                List<string> handData = writeArffMethods.SingleHandData(frame, Gesture.GestureName);


                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandData.arff",
                    handData);
            }
            if (frame.Hands.Count == 2)
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

                var handData = writeArffMethods.TwoHandData(frame, right, left, Gesture.GestureName);

                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff",
                    handData);
            }
        }
    }
}