using CurrencyConverter.Extensions;

namespace CurrencyConverter.Application
{
    public static class ConversionRates
    {
        public static readonly double CopperToGold    = 1.Over(50d);
        public static readonly double GoldToPlatinum  = 1.Over(10d);
        public static readonly double PlatinumToSteel = 2.Over(1d);
        public static readonly double GoldToIron      = 3.Over(5d);
        public static readonly double IronToSteel     = 1.Over(4d);
    }
}
