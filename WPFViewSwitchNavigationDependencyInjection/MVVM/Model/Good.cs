using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    public enum GoodType
    {
        Any,
        Food,
        Weapon, //Оружие
        Armor, //Доспехи
        Wooden,//Деревянный
        Leather,//Кожа
        Metal,
        Animal,
        Magical,
        Utensils, //Домашняя утварь, предмет быта
        Valuables, //Ценности
        Trinkets, //Безделушки
        Illegal
    }

    public class Good
    {
        public string Name { get; private set; }
        public readonly float baseCost;
        public readonly List<GoodType> tags;
        
        //Неопределенный товар, не исп
        public Good()
        {
            baseCost = 1f;
            Name = "Undefined";
            tags = new List<GoodType> { GoodType.Any };
        }

       
        public Good(string Name, float baseCost, List<GoodType>? tags)
        {
            this.Name = Name;
            this.baseCost = baseCost;
            if (tags == null)
            {
                this.tags = new List<GoodType>();
                return;
            }
            this.tags = tags;
        }

        public float getCost(Dictionary<GoodType,float>? modifiers)
        {
            float cost = baseCost;
            if (modifiers != null)
            {
                foreach (var mod in modifiers)
                {
                    if (mod.Key == GoodType.Any || tags.Contains(mod.Key))
                    {
                        cost *= mod.Value;
                    }
                }
            }
            return cost;
        }
    }
}
