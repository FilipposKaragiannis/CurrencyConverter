namespace CurrencyConverter.Domain
{
    public struct Asset
    {
        public int    Amount { get; }
        public string Ccy    { get; }

        public Asset(int amount, string ccy)
        {
            Amount = amount;
            Ccy    = ccy;
        }
    }
}
