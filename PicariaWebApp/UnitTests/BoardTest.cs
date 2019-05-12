using PicariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class BoardTest
    {
        [Fact]
        public void EmptyBoardEualsTest()
        {
            Assert.Equal(new Board(), new Board());
        }
    }
}
