using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.Services;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    interface ICity
    {
        string Name { get; }
        public Dictionary<GoodType, float> ModCity { get; }
        public Dictionary<GoodType, float> ModPlayer { get; }

        public ObservableCollection<GoodOnSell> GoodsOnSell { get; }

        public Dictionary<GoodType, float>? ModOut { get; set; }
        public Dictionary<GoodType, int>? RefillOut { get; set; }

        public void RefillGoods();
        public bool Sell(GoodOnSell goodOnSell);
        public void AddGood(Good good, int count);
    }

    internal class StandartCity : ICity
    {
        protected readonly string _name;

        protected Dictionary<GoodType, float> _modifierCity;
        protected Dictionary<GoodType, float> _modifierPlayer;

        protected Dictionary<GoodType, int> _refillChanceType;
        protected Dictionary<float, int> _refillChanceCost;


        protected Dictionary<Good, int> _goods;

        public string Name { get => _name; }

        public Dictionary<GoodType, float> ModCity 
        { get
            {
                var ret = new Dictionary<GoodType, float>(_modifierCity);
                if (ModOut != null)
                {
                    foreach (var kvp in ModOut)
                    {
                        if (ret.ContainsKey(kvp.Key))
                            ret[kvp.Key] *= kvp.Value;
                        else
                            ret[kvp.Key] = kvp.Value;
                    }
                }
                return ret;
            }
        }

        public Dictionary<GoodType, float> ModPlayer
        {
            get
            {
                var ret = new Dictionary<GoodType, float>(_modifierPlayer);
                if (ModOut != null)
                {
                    foreach (var kvp in ModOut)
                    {
                        if (ret.ContainsKey(kvp.Key))
                            ret[kvp.Key] *= kvp.Value;
                        else
                            ret[kvp.Key] = kvp.Value;
                    }
                }
                return ret;
            }
        }

        public ObservableCollection<GoodOnSell> GoodsOnSell
        {
            get
            {
                var sell = new ObservableCollection<GoodOnSell> ();
                foreach (var good in _goods)
                {
                    sell.Add(new GoodOnSell(good.Key, good.Value, good.Key.getCost(ModCity)));
                }
                return sell;
            }
        }

        public Dictionary<GoodType, float>? ModOut { get; set; }
        public Dictionary<GoodType, int>? RefillOut { get; set; }

        public StandartCity()
        {
            _name = Namegen.Generate();
            _modifierCity = new Dictionary<GoodType, float> { {GoodType.Any, 1.1f } };
            _modifierPlayer = new Dictionary<GoodType, float> { { GoodType.Any, 0.95f } };
            _refillChanceType = new Dictionary<GoodType, int>
            {
                { GoodType.Food, 650},
                { GoodType.Magical, -10 },
                { GoodType.Metal, 100 },
                { GoodType.Utensils, 50 },
                { GoodType.Illegal, -100 },
                { GoodType.Weapon, -20 },
                { GoodType.Valuables, 50 },
                { GoodType.Animal, 50 },
                { GoodType.Wooden, 50 },
                { GoodType.Armor, -50 },
                { GoodType.Trinkets, 40 },
                { GoodType.Leather, 30 }
            };
            _refillChanceCost = new Dictionary<float, int>
            {
                {5f,  200},
                {50f, 50},
                {500f, -50},
                {1000f, -150},
                {float.MaxValue, -300 }
            };
            _goods = new Dictionary<Good, int>();
            RefillGoods();
        }

        public void RefillGoods()
        {
            _goods.Clear();
            foreach (var item in Goods.AllGoods) 
            {
                int maxchance = 101;
                int count;
                foreach (GoodType goodType in item.tags)
                {
                    if (_refillChanceType.ContainsKey(goodType))
                        maxchance += _refillChanceType[goodType];
                    if (RefillOut != null && RefillOut.ContainsKey(goodType))
                        maxchance += RefillOut[goodType];
                }
                foreach (var pair in _refillChanceCost)
                {
                    if (item.baseCost < pair.Key)
                    {
                        maxchance += pair.Value;
                        break;
                    }
                }
                if (maxchance > 0)
                {
                    count = Random.Shared.Next(0, maxchance) / 100;
                    if (_goods.ContainsKey(item))
                        _goods[item] += count;
                    else if (count > 0)
                        _goods[item] = count;
                }
            }
        }

        public bool Sell(GoodOnSell goodOnSell)
        {
            if (!_goods.ContainsKey(goodOnSell.GooD))
                return false;
            if (_goods[goodOnSell.GooD] < goodOnSell.Count)
                return false;
            if ((_goods[goodOnSell.GooD] -= goodOnSell.Count) == 0)
                _goods.Remove(goodOnSell.GooD);
            return true;
            
        }

        public void AddGood(Good good, int count)
        {
            if (_goods.ContainsKey(good))
                _goods[good] += count;
            else
                _goods[good] = count;
        }
    }

    internal class DesertCity : StandartCity
    {
        public DesertCity()
        {
            _modifierCity = new Dictionary<GoodType, float> { 
                { GoodType.Any, 1.1f }, 
                {GoodType.Food, 1.5f }, 
                {GoodType.Valuables, 0.8f },
                {GoodType.Animal, 1.6f }
            };
            _modifierPlayer = new Dictionary<GoodType, float> { 
                { GoodType.Any, 0.95f }, 
                { GoodType.Food, 1.5f }, 
                { GoodType.Valuables, 0.8f },
                { GoodType.Animal, 1.6f }
            };
            _refillChanceType = new Dictionary<GoodType, int>
            {
                { GoodType.Food, 200},
                { GoodType.Magical, -20 },
                { GoodType.Metal, 150 },
                { GoodType.Utensils, 20 },
                { GoodType.Illegal, -100 },
                { GoodType.Weapon, 30 },
                { GoodType.Valuables, 150 },
                { GoodType.Animal, -50 },
                { GoodType.Wooden, -50 },
                { GoodType.Armor, -50 },
                { GoodType.Trinkets, 40 },
                { GoodType.Leather, 10 }
            };
            _refillChanceCost = new Dictionary<float, int>
            {
                {10f,  100},
                {100f, -10},
                {500f, -40},
                {1000f, -125},
                {float.MaxValue, -300 }
            };
            _goods = new Dictionary<Good, int>();
            RefillGoods();
        }
    }

    internal class MilitaryCity : StandartCity
    {
        public MilitaryCity()
        {
            _modifierCity = new Dictionary<GoodType, float> {
                { GoodType.Any, 1.1f },
                {GoodType.Food, 1.2f },
                {GoodType.Weapon, 0.8f },
                {GoodType.Armor, 0.85f }
            };
            _modifierPlayer = new Dictionary<GoodType, float> {
                { GoodType.Any, 0.95f },
                {GoodType.Food, 1.2f },
                {GoodType.Weapon, 0.8f },
                {GoodType.Armor, 0.85f }
            };
            _refillChanceType = new Dictionary<GoodType, int>
            {
                { GoodType.Food, 550},
                { GoodType.Magical, -100 },
                { GoodType.Metal, 340 },
                { GoodType.Utensils, 30 },
                { GoodType.Illegal, -40 },
                { GoodType.Weapon, 230 },
                { GoodType.Valuables, 40 },
                { GoodType.Animal, 50 },
                { GoodType.Wooden, 250 },
                { GoodType.Armor, 250 },
                { GoodType.Trinkets, 20 },
                { GoodType.Leather, 240 }
            };
            _refillChanceCost = new Dictionary<float, int>
            {
                {10f,  100},
                {100f, -100},
                {500f, -100},
                {1000f, -225},
                {float.MaxValue, -300 }
            };
            _goods = new Dictionary<Good, int>();
            RefillGoods();
        }
    }

    internal class Village : StandartCity
    {
        public Village()
        {
            _modifierCity = new Dictionary<GoodType, float> {
                { GoodType.Any, 1.05f },
                {GoodType.Food, 0.8f },
                {GoodType.Utensils, 0.9f },
            };
            _modifierPlayer = new Dictionary<GoodType, float> {
                { GoodType.Any, 1f },
                {GoodType.Food, 0.8f },
                {GoodType.Utensils, 0.9f },
            };
            _refillChanceType = new Dictionary<GoodType, int>
            {
                { GoodType.Food, 550},
                { GoodType.Magical, -200 },
                { GoodType.Metal, -200 },
                { GoodType.Utensils, 100 },
                { GoodType.Illegal, -70 },
                { GoodType.Weapon, 100 },
                { GoodType.Valuables, 40 },
                { GoodType.Animal, 250 },
                { GoodType.Wooden, 250 },
                { GoodType.Armor, -50 },
                { GoodType.Trinkets, 120 },
                { GoodType.Leather, 240 }
            };
            _refillChanceCost = new Dictionary<float, int>
            {
                {10f,  200},
                {100f, -50},
                {500f, -200},
                {1000f, -400},
                {float.MaxValue, -500 }
            };
            _goods = new Dictionary<Good, int>();
            RefillGoods();
        }
    }
}


