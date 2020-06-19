using Castle.Core.Logging;
using Moq;
using System;
using Xunit;

namespace XUnitTestProject2
{
    public class AmountServiceTest
    {

        AmountService _amountService;

        //ILogger _logger;

        Mock<ILogger> _mockLog;

        Mock<IInflationRate> _mockRate;
        public AmountServiceTest()
        {
            _mockLog = new Mock<ILogger>(MockBehavior.Loose);
            _mockRate = new Mock<IInflationRate>(MockBehavior.Loose);
            _amountService = new AmountService(_mockLog.Object,_mockRate.Object);
        }
        [Theory]
        [InlineData(100,1999)]
        public void GetAmountByYear_WhenInValidYear_ThrowException(double amount,int year)
        {
            Assert.Throws<Exception>(() => _amountService.GetAmountByYear(amount, year));
            _mockLog.Verify(x => x.Error(It.IsAny<string>()), Times.Exactly(1));
        }

        [Theory]
        [InlineData(-10,2020)]
        public void GetAmountByYear_WhenInValidAmount_ThrowArgumentException(double amount, int year)
        {
            Assert.Throws<ArgumentException>(() => _amountService.GetAmountByYear(amount, year));
        }

        [Theory]
        [InlineData(100, 2020, 110,0.1)]
        public void GetAmountByYear_WhenInValidYearandAmount_ReturnValidAmountAndLogVerify(double amount, int year, double expectedAmount,double rate)
        {
            _mockRate.Setup(x => x.GetTateByYear(It.IsAny<int>())).Returns(rate);
            var actualAmount = _amountService.GetAmountByYear(amount, year);

            Assert.Equal(expectedAmount, actualAmount);

            _mockLog.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(1));
        }

    }
}
