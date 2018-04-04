using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using LeapInternal;
using NUnit.Framework.Api;

namespace Leap
{
    internal class BslAlphabet
    {

        public bool A(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[0].TipPosition, 15)
               && right.Fingers.FindAll(x => x.IsExtended).Count == 5
               || AreWithin(left.Fingers[0].TipPosition, right.Fingers[1].TipPosition, 15)
               && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool B(Hand left, Hand right)
        {
            return ((left.Fingers.FindAll(x => x.IsExtended == false).Count >= 4) &&
                    (right.Fingers.FindAll(x => x.IsExtended == false).Count >= 4) &&
                    FingerBend(left.Fingers[4]) > 0.1 && FingerBend(right.Fingers[4]) > 0.1)
                    && AreWithin(left.Fingers[4].Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint,
                    right.Fingers[4].Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint, 20);

        }
        public bool C(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 0, 1 }) && FingerBend(hand.Fingers[1]) > 0.01
                && CalcAngle(hand.Fingers[0].Direction, hand.Fingers[1].Direction) < 0.8;
        }
        public bool D(Hand left, Hand right)
        {
            return (
                       ExtendedFingers(right, new[] { 0, 1 })
                       && AreWithin(right.Fingers[0].TipPosition,
                           left.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint, 30))
                   ||
                   ExtendedFingers(left, new[] { 0, 1 })
                       && AreWithin(left.Fingers[0].TipPosition,
                           right.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint, 30);


        }
        public bool E(Hand left, Hand right)
        {
            return (ExtendedFingers(left, new[] { 1 }) && right.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 20)
                    || ExtendedFingers(right, new[] { 1 }) && left.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 20));
        }
        public bool F(Hand left, Hand right)
        {
            return ExtendedFingers(left, new[] { 1, 2 }) && ExtendedFingers(right, new[] { 1, 2 })
                    && AreWithin(right.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center,
                    left.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center, 35)
                    ;
        }
        public bool G(Hand hand)
        {
            return (hand.GrabAngle) > 3;
        }
        public bool H(Hand left, Hand right)
        {
            return ExtendedFingers(right,new []{0,1,2,3,4}) && ExtendedFingers(left, new[] { 0, 1, 2, 3, 4 })
                && (left.PalmNormal.y <= 1 && left.PalmNormal.y >= 0.8 && right.PalmNormal.y >= -1 && right.PalmNormal.y <= -0.8)
                || (right.PalmNormal.y <= 1 && right.PalmNormal.y >= 0.8 && left.PalmNormal.y >= -1 && left.PalmNormal.y <= -0.8);
        }
        public bool I(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[2].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[2].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool K(Hand left, Hand right)
        {
            //prev joint of intermediate of index is within 10 of the same bone on the other index
            return AreWithin(left.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint,
                       right.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint, 20)
                   && (FingerBend(left.Fingers[1]) > 0.1 || FingerBend(right.Fingers[1]) > 0.1)
                   && !(left.Fingers[1].Direction.x + right.Fingers[1].Direction.x < 0.15)
                   && CalcAngle(right.Fingers[1].Direction, left.Fingers[1].Direction) > 1.6;
        }
        public bool L(Hand left, Hand right)
        {

            return left.Fingers.TrueForAll(x => x.IsExtended)
                && ExtendedFingers(right, new[] { 1 })
                && AreWithin(right.Fingers[1].TipPosition, left.PalmPosition, 30)
                    || right.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(left, new[] { 1 }) &&
                   AreWithin(left.Fingers[1].TipPosition, right.PalmPosition, 30);
        }
        public bool M(Hand left, Hand right)
        {
            return (left.Fingers.TrueForAll(x => x.IsExtended) && right.Fingers[1].IsExtended &&
                    right.Fingers[2].IsExtended
                    && right.Fingers[3].IsExtended && AreWithin(right.Fingers[2].TipPosition, left.PalmPosition, 30)
                    || right.Fingers.TrueForAll(x => x.IsExtended) && left.Fingers[1].IsExtended &&
                    left.Fingers[2].IsExtended
                    && left.Fingers[3].IsExtended && AreWithin(left.Fingers[2].TipPosition, right.PalmPosition, 30));
        }
        public bool N(Hand left, Hand right)
        {
            return (left.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(right, new[] { 1, 2 })
                    && AreWithin(right.Fingers[2].TipPosition, left.PalmPosition, 30)
                    && CalcAngle(right.Fingers[1].Direction, right.Fingers[2].Direction) < 0.25
                    || right.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(left, new[] { 1, 2 })
                    && AreWithin(left.Fingers[2].TipPosition, right.PalmPosition, 30)
                    && CalcAngle(left.Fingers[1].Direction, left.Fingers[2].Direction) < 0.25);

        }
        public bool O(Hand left, Hand right)
        {

            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[3].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[3].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool P(Hand left, Hand right)
        {
            return AreWithin(right.Fingers[0].TipPosition, right.Fingers[1].TipPosition, 15)
                && ExtendedFingers(left, new[] { 1 })
                    ||
                   AreWithin(left.Fingers[0].TipPosition, left.Fingers[1].TipPosition, 15) &&
                   ExtendedFingers(right, new[] { 1 });
        }  
        //doesn't work
        //        public bool R(Hand left, Hand right)
        //        {
        //            return (ExtendedFingers(left, new[] { 0, 1, 2, 3, 4 }) && right.Fingers[1].IsExtended
        //                   && AreWithin(right.Fingers[1].TipPosition,
        //                       left.Fingers[0].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint, 50))
        //                       || (ExtendedFingers(right, new[] { 0, 1, 2, 3, 4 }) && left.Fingers[1].IsExtended
        //                           && AreWithin(left.Fingers[1].TipPosition,
        //                               right.Fingers[0].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint, 50));
        //        }
        public bool S(Hand left, Hand right)
        {
            return ExtendedFingers(right, new[] { 4 }) && ExtendedFingers(left, new[] { 4 });
        }
        public bool T(Hand left, Hand right)
        {
             return AreWithin(right.Fingers[1].TipPosition, left.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint,
                20) || AreWithin(right.Fingers[1].TipPosition, left.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint,
                       20) || AreWithin(left.Fingers[1].TipPosition, right.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint,
                       20) || AreWithin(left.Fingers[1].TipPosition, right.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint,
                       20);
        }
        public bool U(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[4].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[4].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool V(Hand left, Hand right)
        {
            return (left.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(right, new[] { 1, 2 })
                    && AreWithin(right.Fingers[2].TipPosition, left.PalmPosition, 30)
                    && CalcAngle(right.Fingers[1].Direction, right.Fingers[2].Direction) > 0.25
                    || right.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(left, new[] { 1, 2 })
                    && AreWithin(left.Fingers[2].TipPosition, right.PalmPosition, 30)
                    && CalcAngle(right.Fingers[1].Direction, right.Fingers[2].Direction) > 0.25

            );

        }
        public bool W(Hand left, Hand right)
        {
            return ExtendedFingers(left, new[] { 0, 1, 2, 3, 4 }) && ExtendedFingers(right, new[] { 0, 1, 2, 3, 4 })
                   && AreWithin(right.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint,
                       left.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint, 20)
                   && AreWithin(right.Fingers[3].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint,
                       left.Fingers[3].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint, 20);
        }
        public bool X(Hand left, Hand right)
        {

            return ExtendedFingers(left, new[] { 1 }) && ExtendedFingers(right, new[] { 1 }) &&
                   AreWithin(right.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center,
                       left.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center, 35)
                       && left.Fingers[1].Direction.x + right.Fingers[1].Direction.x < 0.15
                       && CalcAngle(left.Fingers[1].Direction, right.Fingers[1].Direction) < 1.6
                       ;
        }
        public bool Y(Hand left, Hand right)
        {
            return (ExtendedFingers(left, new[] { 0, 1, 2, 3, 4 }) && right.Fingers.FindAll(x => x.IsExtended).Count == 0
                    || (ExtendedFingers(right, new[] { 0, 1, 2, 3, 4 }) &&
                        left.Fingers.FindAll(x => x.IsExtended).Count == 0));
        }
        public bool Z(Hand left, Hand right)
        {
            if (ExtendedFingers(left, new[] { 0, 1, 2, 3, 4 }) &&
                ExtendedFingers(right, new[] { 0, 1, 2, 3, 4 }))
            {
                if ((Math.Abs(left.Fingers[1].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.Magnitude
                              - right.Fingers[2].TipPosition.Magnitude) < 15 || Math.Abs(
                         left.Fingers[2].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.Magnitude
                         - right.Fingers[3].TipPosition.Magnitude) < 15) ||
                    Math.Abs(right.Fingers[1].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint
                                 .Magnitude
                             - left.Fingers[2].TipPosition.Magnitude) < 15 || Math.Abs(
                        right.Fingers[2].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.Magnitude
                        - left.Fingers[3].TipPosition.Magnitude) < 15)
                {
                    if (CalcAngle(right.Fingers[1].Direction, left.Fingers[1].Direction) > 1.6)
                    {
                        return true;
                    }
                }
            }
            return false;



        }

        public bool Zero(Hand hand)
        {
            return hand.GrabStrength == 0 && !hand.Fingers[0].IsExtended
                   && !hand.Fingers[2].IsExtended
                   && !hand.Fingers[3].IsExtended && !hand.Fingers[4].IsExtended
                   && AreWithin(hand.Fingers[0].TipPosition, hand.Fingers[2].TipPosition,30);
        }

        public bool One(Hand hand)
        {
            return ExtendedFingers(hand, new[] {1})
                   &&!AreWithin(hand.Fingers[0].TipPosition, hand.Fingers[2].TipPosition, 30);
        }
        public bool Two(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 1,2 });
        }
        public bool Three(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 1,2,3 });
        }
        public bool Four(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 1, 2, 3,4 });
        }
        public bool Five(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 0,1, 2, 3,4 });
        }

        public bool Six(Hand hand)
        {
            return ExtendedFingers(hand, new[] {0});
        }
        public bool Seven(Hand hand)
        {
            return ExtendedFingers(hand, new[] {0, 1}) &&
                   CalcAngle(hand.Fingers[0].Direction, hand.Fingers[1].Direction) > 0.7;
        }

        public bool Eight(Hand hand)
        {
            return ExtendedFingers(hand, new[] {0, 1, 2});
        }
        public bool Nine(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 0, 1, 2,3 });
        }
        
        private bool AreWithin(Vector finger1, Vector finger2, int distance)
        {
            return (finger1 - finger2).Magnitude < distance;
        }

        private float FingerBend(Finger finger)
        {
            Bone proximal = finger.Bone(Bone.BoneType.TYPE_PROXIMAL);
            Bone distal = finger.Bone(Bone.BoneType.TYPE_DISTAL);
            float dot = proximal.Direction.Dot(distal.Direction);
            float flexed = 1.0f - (1.0f + dot) / 2.0f;
            return flexed;
        }

        private bool ExtendedFingers(Hand hand, int[] fingerInts)
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
        private float CalcAngle(Vector v1, Vector v2)
        {
            var dotProduct = v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            var magnitudes = v1.Magnitude * v2.Magnitude;
            return (float)Math.Acos(dotProduct / magnitudes);
        }
    }
}
