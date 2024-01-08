using System.Collections.Generic;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    public static class GameParameters
    {
        public static readonly Dictionary<Good, int> startingGoods = new Dictionary<Good, int>
        {
            {Goods.Ration, 15 },
            {Goods.Dagger, 1 },
            {Goods.Diary, 1 },
            { Goods.Snuffbox, 1 }
        };

        public static readonly float startingGold = 50f;

        public static readonly int startX = 5;
        public static readonly int startY = 5;

        public static double cellSize = 32;

        public static readonly int gridCols = 15;
        public static readonly int gridRows = 15;

        public static readonly int worldCols = 30;
        public static readonly int worldRows = 30;

        public static readonly int gridOffsetFromPlayer = gridCols < gridRows ? gridCols/2 : gridRows/2;

        public static int Volume = 50;
    }
}
