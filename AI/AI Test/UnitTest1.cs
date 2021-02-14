using NUnit.Framework;

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
        public void RandomMove()
        {
            //AI.GameSetup("");
            //Assert.AreEqual(AI.RandomMove(""), "DUMB");
            Assert.Pass();
        }
    }
}