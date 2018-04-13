using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BslTranslator;
using Leap;

namespace ArffGenerator
{
    public class WriteArffMethods
    {
        HandDataMethods handDataMethods = new HandDataMethods();

        public string SingleFingerDistances(Hand hand)
        {
            string distance = "";
            for (int i = 0; i < hand.Fingers.Count - 1; i++)
            {
                for (int j = i + 1; j <= hand.Fingers.Count - 1; j++)
                {
                    distance += "," + (hand.Fingers[i].TipPosition - hand.Fingers[j].TipPosition).Magnitude;
                }
            }
            return distance;
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
