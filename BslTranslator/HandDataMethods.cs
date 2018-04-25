using Leap;
using System;
using System.Linq;

namespace BslTranslator
{
    public class HandDataMethods
    {
        public bool AreWithin(Vector finger1, Vector finger2, int distance)
        {
            return (finger1 - finger2).Magnitude < distance;
        }

        public float FingerBend(Finger finger)
        {
            Bone proximal = finger.Bone(Bone.BoneType.TYPE_PROXIMAL);
            Bone distal = finger.Bone(Bone.BoneType.TYPE_DISTAL);
            float dot = proximal.Direction.Dot(distal.Direction);
            float flexed = 1.0f - (1.0f + dot) / 2.0f;
            return flexed;
        }

        public bool ExtendedFingers(Hand hand, int[] fingerInts)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (fingerInts.Contains(i))
                {
                    if (!hand.Fingers[i].IsExtended) return false;
                }
                else
                {
                    if (hand.Fingers[i].IsExtended) return false;
                }
            }
            return true;
        }

        public float CalcAngle(Vector v1, Vector v2)
        {
            var dotProduct = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            var magnitudes = v1.Magnitude * v2.Magnitude;
            return (float)Math.Acos(dotProduct / magnitudes);
        }
    }
}