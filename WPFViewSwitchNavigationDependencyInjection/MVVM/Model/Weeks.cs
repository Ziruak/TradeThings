using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    static class Weeks
    {
        public static readonly List<string> WeekText = new List<string>
        {
            "War has started in our lands. Weapons and armor appears more often and costs more!",
            "Stars favour to people who are calm and rational.",
            "Animals are breeding rapidly. All animal goods appear more often and cheaper for now.",
            "Woods are growing recklessly. Wooden googs are going to be much more common.",
            "There is nothing special about this period of time.",
            "Sky seems more blue than ever in this time of year.",
            "Hunters worked too hard to their own bad. Animals and food are few... and expensive.",
            "Drugs are legalized now at affordable prices! Something tells you not for long..."
        };

        public static readonly List<Dictionary<GoodType, float>?> WeekMod = new List<Dictionary<GoodType, float>?>
        {
            new Dictionary<GoodType, float> { {GoodType.Metal, 1.5f },{GoodType.Armor, 1.3f}, { GoodType.Weapon, 1.3f} },
            null,
            new Dictionary<GoodType, float> {{GoodType.Animal, 0.8f}, { GoodType.Food, 0.9f} },
            new Dictionary<GoodType, float> {{GoodType.Wooden, 0.85f}},
            null,
            null,
            new Dictionary<GoodType, float> {{GoodType.Animal, 1.6f}, {GoodType.Food, 1.6f} },
            new Dictionary<GoodType, float> {{GoodType.Illegal, 0.6f}}
        };

        public static readonly List<Dictionary<GoodType, int>?> WeekRefill = new List<Dictionary<GoodType, int>?>
        {
            new Dictionary<GoodType, int> {{GoodType.Metal, -30}, { GoodType.Armor, 200}, { GoodType.Weapon, 200 } },
            null,
            new Dictionary<GoodType, int> {{GoodType.Animal, 200}, { GoodType.Food, 100} },
            new Dictionary<GoodType, int> {{GoodType.Wooden, 200}},
            null,
            null,
            new Dictionary<GoodType, int> {{GoodType.Animal, -100}, { GoodType.Food, -200} },
            new Dictionary<GoodType, int> {{GoodType.Illegal, 200}}
        };
    }
}
