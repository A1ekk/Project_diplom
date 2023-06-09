using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.AppData;

namespace Project.Tests
{
    [TestClass]
    public class UnitTestCalcFinalGrade
    {
        [TestMethod]
        public void SumTest()
        {
            //arrange
            double x1 = 3;
            double x2 = 3;
            double x3 = 4;
            double x4 = 4;
            double x5 = 5;
            double x6 = 4.5;
            double x7 = 4.55;
            double x8 = 3;
            double expected = 3.88;


            //act
            double actual = CalcFinalGrade.final_grade(x1,x2,x3,x4,x5,x6,x7,x8);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
