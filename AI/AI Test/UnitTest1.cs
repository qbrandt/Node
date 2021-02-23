using NUnit.Framework;
using System;

namespace AI_Test
{
    public class Tests
    {
        private AI AI;

        [SetUp]
        public void Setup()
        {
            AI = new AI();
        }

        [Test]
        public void OpeningFirstMove()
        {
            AI.GameSetup("R1Y2B2G1G3XXY3G2B1Y1B3R2R3",true,false);
            string firstMove = AI.GetMove("X00");
            Assert.AreNotEqual(firstMove,"X00");
            Assert.AreEqual(firstMove[0], 'N');
            Assert.AreEqual(firstMove[3], 'B');
            //Assert.AreEqual(AI.GetMove("X00"), "X00");
            //Assert.AreNotEqual(AI.GetMove("X00"), "X00");
        }

        [Test]
        public void OpeningSecondMove()
        {
            AI.GameSetup("R1Y2B2G1G3XXY3G2B1Y1B3R2R3", false, false);
            string firstMove = AI.GetMove("N00B00");
            Assert.AreNotEqual(firstMove, "X00");
            Assert.AreEqual(firstMove[0], 'N');
            Assert.AreEqual(firstMove[3], 'B');
            Console.WriteLine(firstMove);
            //Assert.AreEqual(AI.GetMove("X00"), "X00");
            //Assert.AreNotEqual(AI.GetMove("X00"), "X00");
        }
    }
}