using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

namespace WPFViewSwitchNavigationDependencyInjection.Services
{
    internal class GoodOnSell
    {
        public Good GooD { get; set; }
        public int Count { get; set; }
        public float ActualPrice { get; set; }
        public string ActualPriceStr { get => $"For {ActualPrice:0.00}"; }
        public int idx { get; set; }
        public GoodOnSell(Good good, int count = 0, float actualPrice = 0, int idx = 0)
        {
            Count = count;
            GooD = good;
            ActualPrice = actualPrice;
            this.idx = idx;
        }
    }

    internal class PlayerService : ObservableObject
    {

        private bool _isAlive;

        public bool IsAlive
        {
            get => _isAlive;
            set { _isAlive = value; OnPropertyChanged(); }
        }

        private Dictionary<Good, int> inventory;

        public ObservableCollection<GoodOnSell> GainGoodsOnSell(Dictionary<GoodType,float>? mods) { 
            ObservableCollection<GoodOnSell> inv = new ObservableCollection<GoodOnSell>();
            foreach (var item in inventory) 
            {
                inv.Add(new GoodOnSell(item.Key, item.Value, item.Key.getCost(mods)));
            }
            return inv;
        }

        private float _gold;
        public float Gold
        {
            get => _gold;
            private set
            {
                _gold = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GoldStr));
            }
        }
        public string GoldStr
        {
            get => $"Money: {Gold:0.00}";
        }

        public int RationsLeft {
            get
            {
                int rat = 0;
                if (inventory.ContainsKey(Goods.Ration))
                    rat = inventory[Goods.Ration];
                return rat;
            }
        }

        public bool Sell(Good good, int count, float totPrice)
        {
            if (!inventory.ContainsKey(good))
                return false;
            if (inventory[good] < count)
                return false;

            inventory[good] -= count;
            if (inventory[good] == 0)
                inventory.Remove(good);
            Gold += totPrice;
            OnPropertyChanged(nameof(RationsLeft));
            return true;
        }

        public bool Buy(Good good, int count, float totPrice)
        {
            if (Gold < totPrice) return false;

            if (inventory.ContainsKey(good))
                inventory[good] += count;
            else
                inventory[good] = count;
            Gold -= totPrice;

            OnPropertyChanged(nameof(RationsLeft));
            return true;
        }

        public PlayerService()
        {
            Restart();
        }

        public bool Eat(int count) 
        {
            if (!inventory.ContainsKey(Goods.Ration) || inventory[Goods.Ration] < count)
            {
                IsAlive = false;
                return false;
            }
            inventory[Goods.Ration] -= count;
            OnPropertyChanged(nameof(RationsLeft));
            return true;
        }

        public void Restart()
        {
            inventory = new Dictionary<Good, int>(GameParameters.startingGoods);
            Gold = GameParameters.startingGold;
            IsAlive = true;
            OnPropertyChanged(nameof(RationsLeft));
        }
    }
}
