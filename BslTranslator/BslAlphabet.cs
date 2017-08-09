using System.Linq;

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
            return ((left.Fingers.FindAll(x => x.IsExtended == false).Count == 5) &&
                   (right.Fingers.FindAll(x => x.IsExtended == false).Count == 5) &&
                   AreWithin(left.Fingers[0].TipPosition, right.Fingers[0].TipPosition,25) &&
                   AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition,25)) &&
                  AreWithin(left.Fingers[2].TipPosition, right.Fingers[2].TipPosition,25) &&
                  AreWithin(left.Fingers[3].TipPosition, right.Fingers[3].TipPosition,25) &&
                  AreWithin(left.Fingers[4].TipPosition, right.Fingers[4].TipPosition,25);
        }

        public bool C(Hand left, Hand right)
        {
            return ((left.Fingers[0].IsExtended && left.Fingers[1].IsExtended && !left.Fingers[2].IsExtended
                     && !left.Fingers[3].IsExtended && !left.Fingers[4].IsExtended &&
                 !AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15) 
                    || (right.Fingers[0].IsExtended && right.Fingers[1].IsExtended && !right.Fingers[2].IsExtended
                        && !right.Fingers[3].IsExtended && !right.Fingers[4].IsExtended) &&
                    !AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15) ));
        }

        public bool C(Hand hand)
        {
            return hand.Fingers[0].IsExtended && hand.Fingers[1].IsExtended && !hand.Fingers[2].IsExtended
                   && !hand.Fingers[3].IsExtended && !hand.Fingers[4].IsExtended;
        }

        public bool D(Hand left, Hand right)
        {
            return (
                !left.Fingers[0].IsExtended && left.Fingers[1].IsExtended
                && !left.Fingers[2].IsExtended && !left.Fingers[3].IsExtended
                && !left.Fingers[4].IsExtended && right.Fingers[0].IsExtended
                && !right.Fingers[2].IsExtended && !right.Fingers[3].IsExtended && !right.Fingers[4].IsExtended
                && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15)
                || !right.Fingers[0].IsExtended && right.Fingers[1].IsExtended
                && !right.Fingers[2].IsExtended && !right.Fingers[3].IsExtended
                && !right.Fingers[4].IsExtended && left.Fingers[0].IsExtended
                && !left.Fingers[2].IsExtended && !left.Fingers[3].IsExtended && !left.Fingers[4].IsExtended
                && AreWithin(left.Fingers[1].TipPosition, left.Fingers[1].TipPosition, 15));
            //     || left.Fingers.FindAll(x=>!x.IsExtended).Count ==5 && right.Fingers[0].IsExtended && right.Fingers[1].IsExtended && !right.Fingers[2].IsExtended
            //    && !right.Fingers[3].IsExtended && !right.Fingers[4].IsExtended && AreTouching(right.Fingers[0].TipPosition, left.Fingers[1].bones[3].Center)
            //     || right.Fingers.FindAll(x => !x.IsExtended).Count == 5 && left.Fingers[0].IsExtended && left.Fingers[1].IsExtended && !left.Fingers[2].IsExtended
            //    && !left.Fingers[3].IsExtended && !left.Fingers[4].IsExtended && AreTouching(left.Fingers[0].TipPosition, right.Fingers[1].bones[3].Center));
        }

        public bool E(Hand left, Hand right)
        {
            return (left.Fingers[1].IsExtended && !left.Fingers[0].IsExtended
                    && !left.Fingers[2].IsExtended && !left.Fingers[3].IsExtended
                    && !left.Fingers[4].IsExtended && right.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15)
                    || right.Fingers[1].IsExtended && !right.Fingers[0].IsExtended
                    && !right.Fingers[2].IsExtended && !right.Fingers[3].IsExtended
                    && !right.Fingers[4].IsExtended && left.Fingers.TrueForAll(x => x.IsExtended)
                    && AreWithin(left.Fingers[1].TipPosition, right.Fingers[1].TipPosition, 15));
        }

        public bool F(Hand left, Hand right)
        {
            return (left.Fingers[1].IsExtended && left.Fingers[2].IsExtended && left.Fingers[1].IsExtended &&
                    left.Fingers[2].IsExtended
                    && !left.Fingers[0].IsExtended && !left.Fingers[3].IsExtended && !left.Fingers[4].IsExtended
                    && !right.Fingers[0].IsExtended && !right.Fingers[3].IsExtended && !right.Fingers[4].IsExtended)
                    ;
        }

//        public bool X(Hand left, Hand right)
//        {
//            return (!left.Fingers[0].IsExtended && left.Fingers[1].IsExtended && !left.Fingers[2].IsExtended
//                    && !left.Fingers[3].IsExtended && !left.Fingers[4].IsExtended
//                    && !right.Fingers[0].IsExtended && right.Fingers[1].IsExtended && !right.Fingers[2].IsExtended
//                    && !right.Fingers[3].IsExtended && !right.Fingers[4].IsExtended
//                    && AreWithin(left.Fingers[1].bones[2].Center, right.Fingers[1].bones[2].Center, 15));
//        }
        public bool G(Hand hand)
        {
            return (hand.Fingers.FindAll(x => !x.IsExtended).Count == 5);
        }

        public bool H(Hand left, Hand right)
        {
            return right.Fingers.TrueForAll(x => x.IsExtended) && left.Fingers.TrueForAll(x => x.IsExtended) 
                && (left.PalmNormal.y<=1 &&left.PalmNormal.y>=0.7&&right.PalmNormal.y>=-1&&right.PalmNormal.y<=-0.7) 
                || (right.PalmNormal.y <= 1 && right.PalmNormal.y >= 0.7 && left.PalmNormal.y >= -1 && left.PalmNormal.y <= -0.7);
        }
        public bool I(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[2].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[2].TipPosition, right.Fingers[1].TipPosition, 15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }


//        public bool K(Hand left, Hand right)
//        {
//            
//        }

        public bool O(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[3].TipPosition, 15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[3].TipPosition, right.Fingers[1].TipPosition,15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }
        public bool U(Hand left, Hand right)
        {
            return (AreWithin(left.Fingers[1].TipPosition, right.Fingers[4].TipPosition,15)
              && right.Fingers.FindAll(x => x.IsExtended).Count == 5
              || AreWithin(left.Fingers[4].TipPosition, right.Fingers[1].TipPosition,15)
              && left.Fingers.FindAll(x => x.IsExtended).Count == 5);
        }


        private bool AreWithin(Vector finger1, Vector finger2, int distance)
        {
            return (finger1 - finger2).Magnitude < distance;
        }
    }
}
