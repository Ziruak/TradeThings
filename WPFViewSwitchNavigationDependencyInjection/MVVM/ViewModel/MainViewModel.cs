using Microsoft.Win32;
using System.Windows.Media;
using System.Windows;
using System;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;
using WPFViewSwitchNavigationDependencyInjection.Services;
using System.Windows.Shapes;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.ViewModel
{
    internal class MainViewModel : Core.ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation; 
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateGridCommand { get; set; }

        public RelayCommand NavigateSettingsCommand { get; set; }

        public RelayCommand NavigateTradeCommand { get; set; }


        private GridService _gridServ;

        public GridService GridServ
        {
            get => _gridServ;
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
            set
            {
                _citiesServ = value;
                OnPropertyChanged();
            }
        }

        private PlayerService _playerServ;

        public PlayerService PlayerServ
        {
            get => _playerServ;
            set
            {
                _playerServ = value;
                OnPropertyChanged();
            }
        }

        private bool _showHelp;

        public bool ShowHelp
        {
            get => _showHelp;
            set { _showHelp = value; OnPropertyChanged(); }
        }

        public RelayCommand ShowHelpComm { get; set; }
        public RelayCommand CloseHelpComm { get; set; }

        private bool _contentFocus;

        public bool ContentFocus
        {
            get => _contentFocus;
            set { _contentFocus = value; OnPropertyChanged(); }
        }

        private string _weekText;

        public string WeekText
        {
            get => _weekText;
            set { _weekText = value; OnPropertyChanged(); }
        }

        private bool _showPopup;

        public bool ShowPopup
        {
            get => _showPopup;
            set { _showPopup = value; OnPropertyChanged(); }
        }

        public RelayCommand ClosePopup { get; set; }

        private bool _restartPopup;

        public bool RestartPopup
        {
            get => _restartPopup;
            set { _restartPopup = value; OnPropertyChanged(); }
        }

        public RelayCommand Restart { get; set; }

        private void makeTurn()
        {
            int curDay = GridServ.Day;
            GridServ.Move(_dx, _dy);
            _dx = 0; _dy = 0;
            if (GridServ.Day - curDay > 0)
            {
                PlayerServ.Eat(GridServ.Day - curDay);
                if (PlayerServ.IsAlive)
                {
                    if (GridServ.Day % 14 == 0)
                    {
                        int weektype = Random.Shared.Next(0, Weeks.WeekText.Count);
                        WeekText = Weeks.WeekText[weektype];
                        CitiesServ.SetOutMod(Weeks.WeekMod[weektype]);
                        CitiesServ.SetOutRefill(Weeks.WeekRefill[weektype]);
                        CitiesServ.RefillCities();
                        ShowPopup = true;
                    }
                }
                else
                {
                    Sounds.GameOverSound.Play();
                    RestartPopup = true;
                }
            }
        }

        public RelayCommand MoveRight { get; set; }

        public RelayCommand MoveLeft { get; set; }

        public RelayCommand MoveUp { get; set; }

        public RelayCommand MoveDown { get; set; }

        private int _dx;

        private int _dy;

        private bool _atGrid;

        public bool AtGrid { get => _atGrid; set { _atGrid = value; OnPropertyChanged(); } }

        public MainViewModel(INavigationService navService, GridService gridService, CitiesService citiesService, PlayerService playerService)
        {
            Navigation = navService;
            GridServ = gridService;
            CitiesServ = citiesService;
            PlayerServ = playerService;
            CitiesServ.InitCities(GridServ.WorldCells);
            NavigateGridCommand = new RelayCommand(o => { AtGrid = true; Navigation.NavigateTo<GridViewModel>(); }, o => true);
            NavigateSettingsCommand = new RelayCommand(o => { AtGrid = false; Navigation.NavigateTo<SettingsViewModel>(); }, o => true);
            NavigateTradeCommand = new RelayCommand(o => {
                CitiesServ.SetCurrentCity(GridServ.PlayerX, GridServ.PlayerY);
                AtGrid = false;
                Navigation.NavigateTo<TradeViewModel>();
            }, o => GridServ.IsPlayerInTown && PlayerServ.IsAlive);
            ClosePopup = new RelayCommand(o => {
                ShowPopup = false;
            }, o => true);
            Restart = new RelayCommand(o =>
            {
                GridServ.Restart();
                PlayerServ.Restart();
                CitiesServ.InitCities(GridServ.WorldCells);
                RestartPopup = false;
            },
            o => RestartPopup
            );
            MoveRight = new RelayCommand(
                obj => { _dx = 1; makeTurn(); },
                o => PlayerServ.IsAlive && !ShowPopup && AtGrid);
            MoveLeft = new RelayCommand(
                obj => { _dx = -1; makeTurn(); },
                o => PlayerServ.IsAlive && !ShowPopup && AtGrid);
            MoveUp = new RelayCommand(
                obj => { _dy = -1; makeTurn(); },
                o => PlayerServ.IsAlive && !ShowPopup && AtGrid);
            MoveDown = new RelayCommand(
                obj => { _dy = 1; makeTurn(); },
                o => PlayerServ.IsAlive && !ShowPopup && AtGrid);
            ShowHelpComm = new RelayCommand(o => { ShowHelp = true; }, o => !ShowHelp);
            CloseHelpComm = new RelayCommand(o => {ShowHelp = false; }, o => ShowHelp);
            Sounds.BackgroundMusic.PlayInLoop();
            AtGrid = true;
            Navigation.NavigateTo<GridViewModel>();
        }
    }
}
