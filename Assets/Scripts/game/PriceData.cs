using System;

namespace game
{
    [Serializable]
    public class PriceData
    {
        public int Amount;
        public PriceType Type;

        public PriceData(PriceType type, int amount = 0)
        {
            Type = type;
            Amount = amount;
        }
    }
}