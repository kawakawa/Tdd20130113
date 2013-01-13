using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TDDBC.VendingMachine;

namespace Test
{
    [TestFixture]
    class VendingMachineTest
    {
        private VendingMachine _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new VendingMachine();
            Helper.SUT = _sut;
        }

        [TestCase]
        public void 初期値の確認()
        {
            _sut.Amount.Is(0);
            _sut.Sales.Is(0);
        }




        [TestCase]
        public void 一〇〇円を追加する()
        {
            _sut.Insert(Money.Yen100);

            _sut.Amount.Is(100);
        }

        [TestCase]
        public void 一六〇円追加する()
        {
            _sut.Insert(Money.Yen100);
            _sut.Insert(Money.Yen50);
            _sut.Insert(Money.Yen10);

            _sut.Amount.Is(160);
        }

        [TestCase]
        public void 六六〇円追加して払い戻す()
        {
            _sut.Insert(Money.Yen500);
            _sut.Insert(Money.Yen100);
            _sut.Insert(Money.Yen50);
            _sut.Insert(Money.Yen10);

            _sut.PayBack().Is(660);
            _sut.Amount.Is(0);
        }

        [TestCase]
        public void 受け付けられないお金はそのまま返す()
        {
            _sut.Insert(Money.Yen1).Is(1);
            _sut.Insert(Money.Yen5).Is(5);
            _sut.Insert(Money.Yen5000).Is(5000);
            _sut.Insert(Money.Yen10000).Is(10000);
        }



        [TestCase]
        public void 初期格納ジュースの確認()
        {
            "Cola".の在庫情報().Name.Is("Cola");
            "Cola".の在庫情報().Count.Is(5);
            "Cola".の在庫情報().Price.Is(120);

            "RedBull".の在庫情報().Name.Is("RedBull");
            "RedBull".の在庫情報().Count.Is(5);
            "RedBull".の在庫情報().Price.Is(200);

            "Water".の在庫情報().Name.Is("Water");
            "Water".の在庫情報().Count.Is(5);
            "Water".の在庫情報().Price.Is(100);
        }


        [TestCase]
        public void ジュースの在庫を新規追加できる()
        {
            _sut.AddStock(new JuiceStockInfo("Coffee", 3, 200));

            "Coffee".の在庫情報().Price.Is(200);
            "Coffee".の在庫情報().Count.Is(3);
        }


        [TestCase]
        public void 既存のジュースの在庫を追加できる()
        {
            _sut.AddStock(new JuiceStockInfo("Cola", 3, 120));
            "Cola".の在庫情報().Count.Is(8);
        }



        [TestCase]
        public void 金額の異なるコーラを追加して在庫が増える事を確認する()
        {
            _sut.AddStock(new JuiceStockInfo("Cola", 1, 200));
            "Cola".の在庫情報().Count.Is(6);

        
        }

        [TestCase]
        public void 金額の異なるコーラを追加した金額になる事を確認する()
        {
            _sut.AddStock(new JuiceStockInfo("Cola", 1, 200));

            1000.円投入();

            _sut.Buy("Cola").Is(800);
            _sut.Sales.Is(200);
            _sut.Amount.Is(0);
        }

        [TestCase]
        public void 金額の異なるコーラを追加した後複数回購入出来る事を確認する()
        {
            _sut.AddStock(new JuiceStockInfo("Cola", 1, 200));

            1000.円投入();
            _sut.Buy("Cola").Is(800);
            _sut.Sales.Is(200);
            _sut.Amount.Is(0);

            1000.円投入();
            _sut.Buy("Cola").Is(800);
            _sut.Sales.Is(400);
            _sut.Amount.Is(0);

        }


        [TestCase]
        public void 既存のRedBullの在庫を追加できる()
        {
            _sut.AddStock(new JuiceStockInfo("RedBull", 5, 200));
            "RedBull".の在庫情報().Count.Is(10);
        }

        [TestCase]
        public void 既存のWaterの在庫を追加できる()
        {
            _sut.AddStock(new JuiceStockInfo("Water", 5, 100));
            "Water".の在庫情報().Count.Is(10);
        }


        [TestCase]
        public void 初期状態ではコーラは買えない()
        {
            _sut.CanBuy("Cola").IsFalse();
        }

        [TestCase]
        public void 一二〇円投入するとコーラが買える()
        {
            120.円投入();

            _sut.CanBuy("Cola").IsTrue();
        }

        [TestCase]
        public void 一一〇円投入ではコーラは買えない()
        {
            110.円投入();

            _sut.CanBuy("Cola").IsFalse();
        }

        
        [TestCase]
        public void コーラを購入後の状態変化を確認()
        {
            1000.円投入();
            _sut.Buy("Cola").Is(880);

            "Cola".の在庫情報().Count.Is(4);
            _sut.Sales.Is(120);
        }

        [TestCase]
        public void コーラを１個購入した時の売り上げ金額を確認()
        {
            120.円投入();
            _sut.Buy("Cola");
            _sut.Sales.Is(120);
        }

        [TestCase]
        public void コーラを２個購入した時の売り上げ金額を確認()
        {
            120.円投入();
            _sut.Buy("Cola");

            120.円投入();
            _sut.Buy("Cola");

            _sut.Sales.Is(240);
        }




        [TestCase]
        public void 購入できない場合は購入操作を行っても変化していない()
        {
            110.円投入();
            _sut.Buy("Cola").Is(0);

            "Cola".の在庫情報().Count.Is(5);
            _sut.Sales.Is(0);
            _sut.Amount.Is(110);
        }

    

        [TestCase]
        public void コーラの在庫がなくなれば買えない()
        {
            1000.円投入();
            _sut.Buy("Cola");
            1000.円投入();
            _sut.Buy("Cola");
            1000.円投入();
            _sut.Buy("Cola");
            1000.円投入();
            _sut.Buy("Cola");
            1000.円投入();
            _sut.Buy("Cola");

            _sut.CanBuy("Cola").IsFalse();
            "Cola".の在庫情報().Count.Is(0);
        }

        [TestCase]
        public void 購入できるジュースのリストを表示_二〇〇円入れると全て購入可能()
        {
            200.円投入();

            _sut.ListupAvailableJuices().Count.Is(3);
            _sut.ListupAvailableJuices().ContainsAllValue("Cola", "RedBull", "Water");
        }

        [TestCase]
        public void 購入できるジュースのリストを表示_一二〇円入れるとRedBull以外購入可能()
        {
            120.円投入();

            _sut.ListupAvailableJuices().Count.Is(2);
            _sut.ListupAvailableJuices().ContainsAllValue("Cola", "Water");
        }

        [TestCase]
        public void 購入できるジュースのリストを表示_100円入れるとWaterだけ購入可能()
        {
            100.円投入();
            _sut.ListupAvailableJuices().Count.Is(1);
            _sut.ListupAvailableJuices().ContainsAllValue( "Water");
        }

        [TestCase]
        public void 購入できるジュースのリストを表示_10円入れると全て購入できない()
        {
            10.円投入();
            _sut.ListupAvailableJuices().Count.Is(0);
        }


        [TestCase]
        public void 購入できるジュースのリストを表示_Redbullが売り切れの時200円入れるとRedbull以外購入できる()
        {
            200.円投入();
            _sut.Buy("RedBull");
            200.円投入();
            _sut.Buy("RedBull");
            200.円投入();
            _sut.Buy("RedBull");
            200.円投入();
            _sut.Buy("RedBull");
            200.円投入();
            _sut.Buy("RedBull");


            200.円投入();

            _sut.ListupAvailableJuices().Count.Is(2);
            _sut.ListupAvailableJuices().ContainsAllValue("Cola", "Water");
        }

        [TestCase("Cola", 80)]
        [TestCase("RedBull", 0)]
        public void _200円投入して各ジュースを購入してお釣りを確認する(string juceName, int payBackMoney)
        {

            200.円投入();
            _sut.Buy(juceName).Is(payBackMoney);
        }


        [TestCase]
        public void コーラを購入した後再度お釣りを確認すると0円になっているのを確認()
        {

            200.円投入();
            _sut.Buy("Cola");
            _sut.PayBack().Is(0);
        }

        [TestCase]
        public void ジュースと同額金額を投入するとつり銭が0円になる事を確認()
        {
            120.円投入();
            _sut.Buy("Cola").Is(0);

            _sut.PayBack().Is(0);
        }



    }

    public static class Helper
    {
        public static VendingMachine SUT { private get; set; }

        public static JuiceStockInfo の在庫情報(this string name)
        {
            return SUT.GetJuiceStockInfo().First(i => i.Name == name);
        }

        public static void 円投入(this int amount)
        {
            var availableMoneys = new[]
                                      {
                                          new Tuple<Money, int>(Money.Yen1000, 1000),
                                          new Tuple<Money, int>(Money.Yen500, 500),
                                          new Tuple<Money, int>(Money.Yen100, 100),
                                          new Tuple<Money, int>(Money.Yen50, 50),
                                          new Tuple<Money, int>(Money.Yen10, 10),
                                      };
            foreach (var availableMoney in availableMoneys)
            {
                var count = amount/availableMoney.Item2;
                count.Times(() => SUT.Insert(availableMoney.Item1));
                amount %= availableMoney.Item2;
            }
        }

        public static void Each<T>(this IEnumerable<T> seq, Action<T> action)
        {
            foreach (var i in seq)
            {
                action(i);
            }
        }


        public static void Times(this int count, Action action)
        {
            foreach (var i in Enumerable.Range(0, count))
            {
                action();
            }
        }

        public static void ContainsAllValue<T>(this List<T> list, params T[] vals)
        {
            foreach (var val in vals)
            {
                list.Contains(val).IsTrue();
            }
        }

    }
}
