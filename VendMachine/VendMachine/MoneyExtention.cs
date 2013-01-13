using System;

namespace TDDBC.VendingMachine
{
    public static class MoneyExtention
    {
        public static int ToAmount(this Money money)
        {
            int amount;

            switch (money)
            {
                case Money.Yen10000:
                    amount = 10000;
                    break;
                case Money.Yen5000:
                    amount = 5000;
                    break;
                case Money.Yen1000:
                    amount = 1000;
                    break;
                case Money.Yen500:
                    amount = 500;
                    break;
                case Money.Yen100:
                    amount = 100;
                    break;
                case Money.Yen50:
                    amount = 50;
                    break;
                case Money.Yen10:
                    amount = 10;
                    break;
                case Money.Yen5:
                    amount = 5;
                    break;
                case Money.Yen1:
                    amount = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("money");
            }
            return amount;
        }
    }
}