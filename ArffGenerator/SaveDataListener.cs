using System.Collections.Generic;
using System.IO;
using BslTranslator;
using Leap;

namespace ArffGenerator
{
    public class SaveDataListener
    {
        HandDataMethods handDataMethods = new HandDataMethods();


        public void OnFrame(object sender, FrameEventArgs args)
        {
            Frame frame = args.frame;
            if (frame.Hands.Count == 1)
            {
                List<string> handData = new List<string>() { frame.Hands.Count.ToString() };

                handData[0] += FingerBends(frame.Hands[0]);
                handData[0] += "," + Gesture.GestureName;
                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\DataSets\ApplicationTest.arff",
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

                

                var handData = new List<string> {args.frame.Hands.Count.ToString()};
                handData[0] += FingerBends(right);
                handData[0] += FingerBends(left);
                handData[0] += NormalisedFingerPositions(right, left);
                handData[0] += NormalisedFingerPositions(left, right);
                handData[0] += FingerDistances(right, left);
                handData[0] += "," +Gesture.GestureName;

                File.AppendAllLines(
                    @"D:\Documents\BSL translator docs\Data mining stuff\ApplicationTest.arff",
                    handData);


            }


        }




        public string FingerDistances(Hand hand1, Hand hand2)
        {
            string distances = "";

            foreach (var finger1 in hand1.Fingers)
            {
                foreach (var finger2 in hand2.Fingers)
                {
                    distances += "," + (finger1.TipPosition.Magnitude - finger2.TipPosition.Magnitude);
                }
            }
            return distances;
        }

        public string FingerBends(Hand hand)
        {
            string fingerBendString = "";
            foreach (Finger finger in hand.Fingers)
            {
                fingerBendString += ", " + handDataMethods.FingerBend(finger);
            }
            return fingerBendString;
        }

        public string NormalisedFingerPositions(Hand hand1, Hand hand2)
        {
            string fingerPositions = "";
            foreach (Finger finger in hand1.Fingers)
            {
                fingerPositions += "," + (finger.TipPosition.Normalized - hand2.PalmPosition.Normalized).Magnitude;
            }
            return fingerPositions;
        }

    }
}