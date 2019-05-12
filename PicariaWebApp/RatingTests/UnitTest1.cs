using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace RatingTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRate()
        {
            List<Position> positions = new List<Positions> { };
            //positions.Add(new Position)
            int i = PicariaWebApp.Player.Rating.RateBoard();
            bool value = true;
            Assert.IsTrue(value);
        }
    }
}
