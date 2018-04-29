using BslTranslator;
using Leap;
using System.Collections.Generic;
using System.Linq;

namespace ArffGenerator
{
    public class WriteArffMethods
    {
        private HandDataMethods handDataMethods = new HandDataMethods();
        /// <summary>
        /// return angle of fingers, finger posistions and distances from other fingers as a single string 
        /// to be added to the arff file as an instance
        /// </summary>
  
        public List<string> TwoHandData(Frame frame, Hand right, Hand left, string gestureName)
        {
            var handData = new List<string> { frame.Hands.Count.ToString() };
            handData[0] += FingerBends(right);
            handData[0] += FingerBends(left);
            handData[0] += NormalisedFingerPositions(right, left);
            handData[0] += NormalisedFingerPositions(left, right);
            handData[0] += FingerDistances(right, left);
            handData[0] += "," + gestureName;
            return handData;
        }

        public List<string> SingleHandData(Frame frame, string gestureName)
        {
            List<string> handData = new List<string>() { frame.Hands.Count.ToString() };

            handData[0] += FingerBends(frame.Hands[0]);
            handData[0] += SingleFingerDistances(frame.Hands[0]);
            handData[0] += "," + gestureName;
            return handData;
        }

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
            return hand.Fingers.Aggregate("", (current, finger) => current + (", " + handDataMethods.FingerBend(finger)));
        }

        public string NormalisedFingerPositions(Hand hand1, Hand hand2)
        {
            return hand1.Fingers.Aggregate("",
                (current, finger) => current + ("," + (finger.TipPosition.Normalized - hand2.PalmPosition.Normalized).Magnitude));
        }
    }
}