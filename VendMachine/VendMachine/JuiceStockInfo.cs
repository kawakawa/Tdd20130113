namespace TDDBC.VendingMachine
{
    public class JuiceStockInfo
    {
        public JuiceStockInfo(string name, int count, int price)
        {
            Price = price;
            Count = count;
            Name = name;
        }

        public string Name { get; private set; }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}