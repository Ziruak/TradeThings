using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;
using WPFViewSwitchNavigationDependencyInjection.Services;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.ViewModel
{
    internal class GridViewModel : Core.ViewModel
    {
        private GridService? _gridServ;


        public GridService GridServ
        {
            get => _gridServ!;
            set
            {
                _gridServ = value;
                OnPropertyChanged();
            }
        }

        private CitiesService _citiesServ;

        public CitiesService CitiesServ
        {
            get => _citiesServ;
            set { _citiesServ = value; OnPropertyChanged(); }
        }

        private PlayerService _playerServ;

        public PlayerService PlayerServ
        {
            get => _playerServ;
            set { _playerServ = value; OnPropertyChanged();}
        }


        public GridViewModel(GridService gridService, CitiesService citiesService, PlayerService playerService) 
        {
            GridServ = gridService;
            CitiesServ = citiesService;
            PlayerServ = playerService;
            
            
        }
    }
}
