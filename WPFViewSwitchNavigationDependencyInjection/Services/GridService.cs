using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

namespace WPFViewSwitchNavigationDependencyInjection.Services
{

    internal class GridService : ObservableObject
    {
        public int Rows { get; private set; }
        public int WorldRows { get; private set; }
        public int Cols { get; private set; }
        public int WorldCols { get; private set; }

        public int Left { get; private set; }
        public int Right { get; private set; }
        public int Top { get; private set; }
        public int Bottom { get; private set; }

        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }
        public int OffsetFromPlayer { get; private set; }
   
        private int _day;
        public int Day
        {
            get => _day;
            set
            {
                _day = value;
                OnPropertyChanged();
            }
        }

        private List<List<GameGridCell>> _worldGridCells;

        public GridService()
        {
            _day = 1;
            Rows = GameParameters.gridRows;
            Cols = GameParameters.gridCols;
            WorldRows = GameParameters.worldRows;
            WorldCols = GameParameters.worldCols;
            OffsetFromPlayer = GameParameters.gridOffsetFromPlayer;

            Restart();
        }

        private List<List<GameGridCell>> GenerateWorld()
        {
            var _newgrid = new List<List<GameGridCell>>();
            for (int y = 0; y < WorldRows; y++)
            {
                _newgrid.Add(new List<GameGridCell>());
                for (int x = 0; x < WorldCols; x++)
                {
                    Enviroment env = (Enviroment)Random.Shared.Next(0, (int)Enviroment.EnumEnd);
                    GridContent cont = GridContent.Empty;
                    int townchance = 10;
                    bool istown = Random.Shared.Next(0, 101) < townchance && env != Enviroment.Water;
                    if (istown)
                    {
                        if (Random.Shared.Next(0, 101) <= 20)
                            cont = GridContent.MilitaryTown;
                        else if (env == Enviroment.Desert)
                            cont = GridContent.DesertTown;
                        else if (Random.Shared.Next(0,101)<=33)
                            cont = GridContent.Village;
                        else
                            cont = GridContent.StandartTown;
                    }
                    _newgrid[y].Add(new GameGridCell(env, cont, false));
                }
            }
            _newgrid[PlayerY][PlayerX].CellEnviroment = Enviroment.Grasslands;
            _newgrid[PlayerY][PlayerX].PlayerInCell = true;
            return _newgrid;
        }

        public List<List<GameGridCell>> WorldCells { get=>_worldGridCells; }

        public ObservableCollection<ObservableCollection<Image>> GameGridCells
        {
            get 
            {
                var ret = new ObservableCollection<ObservableCollection<Image>>();
                for (int y = Top; y <= Bottom; y++)
                {
                    ret.Add(new ObservableCollection<Image>());
                    for (int x = Left; x <= Right; x++)
                    {
                        ret[y-Top].Add(_worldGridCells[y][x].CellImage);
                    }
                }
                return ret;
            } 
        }

        public bool IsPlayerInTown {
            get => _worldGridCells[PlayerY][PlayerX].CellContent > GridContent.TownsStart &&
                _worldGridCells[PlayerY][PlayerX].CellContent < GridContent.TownsEnd;
        }

        public bool Move(int dx, int dy)
        {
            bool gridMoved = false;

            if (MovePlayer(dx, dy)) // Can player move?
            {
                Day++;
                if (MoveGrid(dx, dy)) // Can grid move?
                {
                    gridMoved = true;
                }
            }
            OnPropertyChanged(nameof(GameGridCells));

            return gridMoved;
        }

        private bool MoveGrid(int dx, int dy, bool checkOnly = false)
        {
            // CAN move grid
            if (Right + dx >= WorldCols) return false;
            if (Left + dx < 0) return false;
            if (Bottom + dy >= WorldRows) return false;
            if (Top + dy < 0) return false;
            // SHOULD move grid
            if (dx > 0 && Left + OffsetFromPlayer > PlayerX) return false;
            if (dx < 0 && Right - OffsetFromPlayer < PlayerX) return false;
            if (dy > 0 && Top + OffsetFromPlayer > PlayerY) return false;
            if (dy < 0 && Bottom - OffsetFromPlayer < PlayerY) return false;

            if (!checkOnly)
            {
                Left += dx; Right += dx;
                Top += dy; Bottom += dy;
            }

            return true;
        }

        private bool MovePlayer(int dx, int dy, bool checkOnly = false)
        {
            // TODO: Optimize checks?
            if (PlayerX + dx > Right) return false;
            if (PlayerX + dx < Left) return false;
            if (PlayerY + dy > Bottom) return false;
            if (PlayerY + dy < Top) return false;
            if (_worldGridCells[PlayerY + dy][PlayerX + dx].CellEnviroment == Enviroment.Water) return false;

            if (!checkOnly)
            {
                _worldGridCells[PlayerY][PlayerX].PlayerInCell = false;
                PlayerX += dx;
                PlayerY += dy;
                _worldGridCells[PlayerY][PlayerX].PlayerInCell = true;
            }

            return true;
        }

        public void Restart()
        {
            Day = 0;

            Left = 0;
            Right = GameParameters.gridCols - 1;
            Top = 0;
            Bottom = GameParameters.gridRows - 1;

            PlayerX = GameParameters.startX;
            PlayerY = GameParameters.startY;

            _worldGridCells = GenerateWorld();
            OnPropertyChanged(nameof(GameGridCells));
        }
    }
}
