using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

namespace WPFViewSwitchNavigationDependencyInjection.Services
{
    internal class CitiesService
    {
        public Dictionary<(int,int),ICity> cities { get; private set; }

        public void SetCurrentCity(int x, int y)
        {
            if (cities.ContainsKey((x, y)))
                CurrentCity = cities[(x, y)];
            else
                CurrentCity = null;
        }

        public CitiesService()
        {
            cities = new Dictionary<(int, int), ICity> ();
        }

        private ICity? _currentCity;

        public ICity? CurrentCity { get; private set; }

        public void RefillCities()
        {
            foreach (var c in cities.Values) 
            {
                c.RefillGoods();
            }
        }

        public void SetOutMod(Dictionary<GoodType,float>? mod)
        {
            foreach (var c in cities.Values)
                c.ModOut = mod;
        }

        public void SetOutRefill(Dictionary<GoodType,int>? refill)
        {
            foreach (var c in cities.Values)
                c.RefillOut = refill;
        }

        public void InitCities(List<List<GameGridCell>> worldMap)
        {
            for (int y = 0; y < worldMap.Count; y++) 
            {
                for (int x = 0; x < worldMap[y].Count; x++)
                {
                    if (worldMap[y][x].CellContent >= GridContent.TownsStart && worldMap[y][x].CellContent <= GridContent.TownsEnd)
                    {
                        switch (worldMap[y][x].CellContent)
                        {
                            case GridContent.StandartTown:
                                cities[(x, y)] = new StandartCity(); break;
                            case GridContent.DesertTown:
                                cities[(x, y)]= new DesertCity(); break;
                            case GridContent.MilitaryTown:
                                cities[(x, y)] = new MilitaryCity(); break;
                            case GridContent.Village:
                                cities[(x, y)] = new Village(); break;
                            default: throw new NotImplementedException("Such Town doesn't exist");
                        }
                    }
                }
            }
        }
    }
}
