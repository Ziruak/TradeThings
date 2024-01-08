using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    internal static class Namegen
    {
        private static readonly List<string> _prefix = new List<string>
        {
            "Sre", "Kha", "Wat", "Urg", "Lo", "P", "Cri", "Zlo", "Van", "Dre", "Ok", "Ore"
        };

        private static readonly List<string> _ending = new List<string>
        {
            "ing", "ton", "a", "tin", "ol", "am", "ell", "ugh", "on", "is", "ens"
        };

        private static readonly List<string> _middle = new List<string>
        {
            "rod", "rel", "im", "en", "wo", "eg", "dar", "ker", "st", "op", "el", "si"
        };

        public static string Generate()
        {
            string name = _prefix[Random.Shared.Next(0, _prefix.Count)];
            for (int i = Random.Shared.Next(0, 3); i > 0; i--)
            {
                name += _middle[Random.Shared.Next(0, _middle.Count)];
            }
            name += _ending[Random.Shared.Next(0, _ending.Count)];
            return name;
        }
    }
}
