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
                    right.Fingers[4].Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint,20);

        }
        //make this harder to obtain
        public bool C(Hand left, Hand right)
        {
            return (

                ExtendedFingers(left, new[] { 0, 1 })
                && !AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15)
                && FingerBend(left.Fingers[1]) > 0.01)
                || ExtendedFingers(right, new[] { 0, 1 })
                && !AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15)
                && FingerBend(right.Fingers[1]) > 0.01 && !AreWithin(left.PalmPosition, right.PalmPosition, 100);
        }

        public bool C(Hand hand)
        {
            return ExtendedFingers(hand, new[] { 0, 1 }) && FingerBend(hand.Fingers[1]) > 0.01;
        }

        public bool D(Hand left, Hand right)
        {
            return (
                       ExtendedFingers(right, new[] { 0, 1 })
                       && AreWithin(right.Fingers[0].TipPosition,
                           left.Fingers[1].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint, 50))
                   ||
                   ExtendedFingers(left, new[] { 0, 1 })
                       && AreWithin(left.Fingers[0].TipPosition,
                           right.Fingers[1].Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint, 50);


        }

        public bool E(Hand left, Hand right)
        {
            return (ExtendedFingers(left, new[] { 1 }) && right.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15)
                    || ExtendedFingers(right, new[] { 1 }) && left.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15));
        }

        public bool F(Hand left, Hand right)
        {
            return ExtendedFingers(left, new[] { 1, 2 }) && ExtendedFingers(right, new[] { 1, 2 })
                    && AreWithin(right.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center,
                    left.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center, 35)
                    ;
        }
        //too easy to obtain
        public bool X(Hand left, Hand right)
        {

            return ExtendedFingers(left, new[] { 1 }) && ExtendedFingers(right, new[] { 1 }) &&
                   AreWithin(right.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center,
                       left.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).Center, 35)
                       && (FingerBend(left.Fingers[1]) < 0.1 && FingerBend(right.Fingers[1]) < 0.1)
                       ;
        }
        public bool J(Hand left, Hand right)
        {
            // fix, doesn't work
            return (left.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(right,new []{1})
                &&  (AreWithin(right.Fingers[1].TipPosition, left.Fingers[2].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint, 20) ||
            AreWithin(right.Fingers[1].TipPosition, left.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint, 20)))
            || (right.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(left, new []{1})
                && (AreWithin(right.Fingers[1].TipPosition, left.Fingers[2].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint, 20) ||
                    AreWithin(right.Fingers[1].TipPosition, left.Fingers[2].Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint, 20)));
        }
        public bool L(Hand left, Hand right)
        {
            return left.Fingers.TrueForAll(x => x.IsExtended)
                && ExtendedFingers(right, new[] { 1 })
                && AreWithin(right.Fingers[1].TipPosition, left.PalmPosition, 30)
                    && AreWithin(right.Fingers[2].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint, left.WristPosition, 50)
                    || right.Fingers.TrueForAll(x => x.IsExtended) && left.Fingers[1].IsExtended &&
                   AreWithin(left.Fingers[1].TipPosition, right.PalmPosition, 30)
                    && AreWithin(left.Fingers[2].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint, right.WristPosition, 50)
                   ;
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
        //add distance between index and middle finger being within a certain distance of each other
        public bool N(Hand left, Hand right)
        {
            return (left.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(right, new []{1,2})
                     && AreWithin(right.Fingers[2].TipPosition, left.PalmPosition, 30)
                     && AreWithin(right.Fingers[1].TipPosition,right.Fingers[2].TipPosition,20)
                    || right.Fingers.TrueForAll(x => x.IsExtended) && ExtendedFingers(left, new[] { 1, 2 })
                    && AreWithin(left.Fingers[2].TipPosition, right.PalmPosition, 30)
                    && AreWithin(left.Fingers[1].TipPosition,left.Fingers[2].TipPosition,20)
                    );
        }

        public bool P(Hand left, Hand right)
        {
            return AreWithin(right.Fingers[0].TipPosition, right.Fingers[1].TipPosition, 15) &&
                   left.Fingers[1].IsExtended &&
                   AreWithin(right.Fingers[1].TipPosition, left.Fingers[1].TipPosition, 200) ||
                   AreWithin(left.Fingers[0].TipPosition, left.Fingers[1].TipPosition, 15) &&
                   right.Fingers[1].IsExtended &&
                   AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 200);
        }
        public bool G(Hand hand)
        {
            return (Math.Abs(hand.GrabStrength - 1.00) < 0.05);
        }

        public bool H(Hand left, Hand right)
        {
            return right.Fingers.TrueForAll(x => x.IsExtended) && left.Fingers.TrueForAll(x => x.IsExtended)
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
                   && (FingerBend(left.Fingers[1]) > 0.1 || FingerBend(right.Fingers[1]) > 0.1);
        }
        public bool T(Hand left, Hand right)
        {
            float xL = (left.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.x
                        + left.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.x) / 2;
            float yL = (left.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.y
                       + left.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.y) / 2;
            float zL = (left.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.z
                       + left.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.z) / 2;

            float xR = (right.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.x
                        + right.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.x) / 2;
            float yR = (right.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.y
                        + right.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.y) / 2;
            float zR = (right.Fingers[4].Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint.z
                        + right.Fingers[4].Bone(Bone.BoneType.TYPE_PROXIMAL).Center.z) / 2;
            Vector midpointRight = new Vector(xR, yR, zR);
            Vector midpointLeft = new Vector(xL, yL, zL);

            return AreWithin(right.Fingers[1].TipPosition, midpointLeft,
                20)
                || AreWithin(left.Fingers[1].TipPosition, midpointRight,
                       20);
        }
        public bool O(Hand left, Hand right)
        {

            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[3].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[3].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool U(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[4].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[4].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
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
    }
}
