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
                List<string> handData = new List<string>() { frame.Hands.Count.ToString() };

                handData[0] += writeArffMethods.FingerBends(frame.Hands[0]);
                handData[0] += writeArffMethods.SingleFingerDistances(frame.Hands[0]);
                handData[0] += "," + Gesture.GestureName;

                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SingleHandTestData.arff",
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
                
                var handData = new List<string> { args.frame.Hands.Count.ToString() };
                handData[0] += writeArffMethods.FingerBends(right);
                handData[0] += writeArffMethods.FingerBends(left);
                handData[0] += writeArffMethods.NormalisedFingerPositions(right, left);
                handData[0] += writeArffMethods.NormalisedFingerPositions(left, right);
                handData[0] += writeArffMethods.FingerDistances(right, left);
                handData[0] += "," + Gesture.GestureName;

                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\SignLanguageDataUpdateable.arff",
                    handData);
            }
        }
    }
}