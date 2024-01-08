using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.Model
{
    public enum Enviroment
    {
        Grasslands,
        Desert,
        Water,
        EnumEnd
    }

    public enum GridContent
    {
        Empty,
        Town,
        EnumEnd,
        TownsStart,
        StandartTown,
        DesertTown,
        MilitaryTown,
        Village,
        TownsEnd
    }

    public class GameGridCell
    {
        private Enviroment _cellEnviroment;
        private GridContent _cellContent;
        private bool _playerInCell;
        private Image _cellImage;

        public Enviroment CellEnviroment { get=>_cellEnviroment; 
            set
            {
                if (value != _cellEnviroment)
                {
                    _cellEnviroment = value;
                    renewImage();
                }
            }
        }
        public GridContent CellContent { get=>_cellContent;
            set 
            { 
                if (value != _cellContent) 
                {
                    _cellContent = value;
                    renewImage();
                }    
            }
        }
        public bool PlayerInCell
        {
            get => _playerInCell;
            set
            {
                if (value != _playerInCell)
                {
                    _playerInCell = value;
                    renewImage();
                }
            }
        }
        public Image CellImage
        {
            get => _cellImage; 
            private set
            {
                _cellImage = value;
            }
        }
        
        public GameGridCell(Enviroment enviroment, GridContent gridContent, bool playerInCell) 
        {
            setValues(enviroment, gridContent, playerInCell);
        }

        public void setValues(Enviroment enviroment, GridContent gridContent, bool playerInCell)
        {
            _cellEnviroment = enviroment;
            _cellContent = gridContent;
            _playerInCell = playerInCell;
            renewImage();
        }

        private void renewImage()
        {
            List<BitmapImage> imgs = new List<BitmapImage>();
            switch (_cellEnviroment)
            {
                case Enviroment.Grasslands:
                    //img = new Image { Source = Images.RandomGrasslands, Width = GameParameters.cellSize, Height = GameParameters.cellSize };
                    imgs.Add(Images.RandomGrasslands as BitmapImage);
                    break;
                case Enviroment.Desert:
                    //img = new Image { Source = Images.RandomDesert, Width = GameParameters.cellSize, Height = GameParameters.cellSize };
                    imgs.Add(Images.RandomDesert as BitmapImage);
                    break;
                case Enviroment.Water:
                    //img = new Image { Source = Images.RandomWater, Width = GameParameters.cellSize, Height = GameParameters.cellSize };
                    imgs.Add(Images.RandomWater as BitmapImage);
                    break;
                default:
                    throw new Exception("Invalid Enviroment enum");
            }
            switch (_cellContent)
            {
                case GridContent.Empty: break;
                case GridContent.StandartTown:
                    imgs.Add(Images.castle2 as BitmapImage); 
                    break;
                case GridContent.MilitaryTown:
                    imgs.Add(Images.castle as BitmapImage);
                    break;
                case GridContent.DesertTown:
                    imgs.Add(Images.desertCity as BitmapImage);
                    break;
                case GridContent.Village:
                    imgs.Add(Images.village as BitmapImage);
                    break;
                default:
                    throw new Exception("Invalid GridContent enum");
            }
            if (PlayerInCell)
            {
                imgs.Add(Images.player as BitmapImage);
            }
            Image img = new Image
            {
                Source = Images.MultiMerge(imgs),
                Width = GameParameters.cellSize,
                Height = GameParameters.cellSize
            };

            CellImage = img;
        }
    }
}
