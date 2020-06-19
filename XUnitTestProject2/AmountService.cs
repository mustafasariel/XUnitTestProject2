using Castle.Core.Logging;
using System;

namespace XUnitTestProject2
{
    public class AmountService
    {
        private readonly ILogger _logger;
        private readonly IInflationRate _inflationRate;

        public AmountService(ILogger logger, IInflationRate inflationRate)
        {
            _logger = logger;
            this._inflationRate = inflationRate;
        }
        public double GetAmountByYear(double amount, int year)
        {
            _logger.Debug("GetAmountByYear çalışmaya başladı");
            if (year < 2000)
            {
                _logger.Error("Yıl geçersiz");
                throw new System.Exception("Yıl geçersiz");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Geçeriz miktar.");
            }

            return amount + amount * _inflationRate.GetTateByYear(year);
        }
    }

    public interface IInflationRate
    {
        double GetTateByYear(int year);
    }



}