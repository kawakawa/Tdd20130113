using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TDDBC.VendingMachine
{
    public class VendingMachine
    {
        private readonly List<JuiceStockInfo> _juiceStockInfoCollection =
            new List<JuiceStockInfo>
                {
                    new JuiceStockInfo("Cola", 5, 120),
                    new JuiceStockInfo("RedBull", 5, 200),
                    new JuiceStockInfo("Water", 5, 100),
                };

        public VendingMachine()
        {
            Amount = 0;
        }

        public int Amount { get; private set; }

        public int Sales { get; private set; }

        public int PayBack()
        {
            var ret = Amount;
            Amount = 0;
            return ret;
        }

        public int Insert(Money money)
        {
            int amount = money.ToAmount();

            if (IsInvalidMoney(money))
            {
                return amount;
            }

            Amount += amount;
            return 0;
        }

        private bool IsInvalidMoney(Money money)
        {
            return money == Money.Yen10000 || money == Money.Yen5000
                   || money == Money.Yen5 || money == Money.Yen1;
        }


        public ReadOnlyCollection<JuiceStockInfo> GetJuiceStockInfo()
        {
            return _juiceStockInfoCollection.AsReadOnly();
        }

        public void AddStock(JuiceStockInfo juiceStockInfo)
        {
            var stock = GetStock(juiceStockInfo);

            if (stock != null)
            {
                stock.Count += juiceStockInfo.Count;
                stock.Price = juiceStockInfo.Price;
            }
            else
            {
                _juiceStockInfoCollection.Add(juiceStockInfo);
            }
        }

        private JuiceStockInfo GetStock(JuiceStockInfo stockInfo)
        {
            return _juiceStockInfoCollection
                .FirstOrDefault(info =>
                                info.Name == stockInfo.Name);
        }

        public bool CanBuy(string drinkName)
        {
            var stockInfo = PickStockInfo(drinkName);
            return  stockInfo != null && stockInfo.Price <= Amount && stockInfo.Count > 0;
        }

        public int Buy(string drinkName)
        {
            var selectedStockInfo = PickStockInfo(drinkName);
            if (selectedStockInfo != null && CanBuy(drinkName))
            {
                selectedStockInfo.Count -= 1;
                Amount -= selectedStockInfo.Price;
                Sales += selectedStockInfo.Price;

                return this.PayBack();
            }

            return 0;
        }

        private JuiceStockInfo PickStockInfo(string drinkName)
        {
            return _juiceStockInfoCollection.FirstOrDefault(j => j.Name == drinkName);
        }

        public List<string> ListupAvailableJuices()
        {
            return _juiceStockInfoCollection
                .Where(info => CanBuy(info.Name))
                .Select(info => info.Name).ToList();
        }
    }
}
