using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBBmok;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddStone()
        {
            Omok game=new Omok();
            game.AddStone(1, 1);
            int result = game.AddStone(1, 1);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestVerticalGameover()
        {
            Omok game = new Omok();
            for(int i=1;i<=4;i++)
            {
                game.AddStone(1, i);    //P1.
                game.AddStone(2, i);    //P2.
            }
            game.AddStone(1, 5);
            int result = game.Check(1, 5);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestHorizontalGameover()
        {
            Omok game = new Omok();
            for (int i = 1; i <= 4; i++)
            {
                game.AddStone(i, 1);    //P1.
                game.AddStone(i, 2);    //P2.
            }
            game.AddStone(5, 1);
            int result = game.Check(5, 1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestDiagonalGameover()
        {
            Omok game = new Omok();
            for(int i=15;i>=12;i--)
            {
                game.AddStone(i, i);    //P1.
                game.AddStone(i - 1, i);//P2.
            }
            game.AddStone(11, 11);
            Assert.AreEqual(1, game.Check(11,11));
        }
    }
}
