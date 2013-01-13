using System;

namespace TDDBC.VendingMachine
{
    public class InvalidMoneyException : Exception
    {
        public Money Money { get; private set; }

        public InvalidMoneyException(Money money)
        {
            Money = money;
        }
    }
}