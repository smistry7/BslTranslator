using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using NUnit.Framework;
using FluentAssertions;
using LeapInternal;

namespace BslTranslator
{
    [TestFixture]
    class UnitTests
    {
        //write tests for different hand scenarios to confirm certain letters.
        [Test]
        public void TestC()
        {
            BslAlphabet alphabet = new BslAlphabet();
            var right = new Hand();
            var left = new Hand();
            right.Fingers[0].IsExtended = true;
            right.Fingers[1].IsExtended = true;
            right.Fingers[2].IsExtended = false;
            right.Fingers[3].IsExtended = false;
            right.Fingers[4].IsExtended = false;
            alphabet.C(right).Should().BeTrue();
            right.Fingers[1].TipPosition = new Vector(2, 2, 2);
            left.Fingers[1].TipPosition = new Vector(22, 2, 222);
            alphabet.C(right, left).Should().BeTrue();
            alphabet.C(left, right).Should().BeTrue();


        }


    }
}
