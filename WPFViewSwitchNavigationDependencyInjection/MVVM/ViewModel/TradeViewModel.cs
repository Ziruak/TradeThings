using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.Services;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.ViewModel
{
    class TradeViewModel : Core.ViewModel
    {
        private CitiesService _citiesServ;

        public CitiesService CitiesServ
        {
            get => _citiesServ;
            set
            {
                _citiesServ = value;
                OnPropertyChanged();
                OnPropertyChanged(WelcomeText);
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

        public ObservableCollection<GoodOnSell> CityGoods { get => CitiesServ.CurrentCity!.GoodsOnSell; }
        public ObservableCollection<GoodOnSell> PlayerGoods { get=>PlayerServ.GainGoodsOnSell(CitiesServ.CurrentCity!.ModPlayer); }

        private string _welcomeText;

        public string WelcomeText
        {
            get => $"Welcome to {CitiesServ.CurrentCity!.Name}";
            set
            {
                _welcomeText = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand BuyFromCity { get; set; }
        public RelayCommand SellFromPlayer { get; set; }

        public TradeViewModel(CitiesService citiesService, PlayerService playerService) 
        { 
            CitiesServ = citiesService;
            PlayerServ = playerService;
            BuyFromCity = new RelayCommand(
                o =>
                {
                    PlayerServ.Buy((o as GoodOnSell)!.GooD, 1, (o as GoodOnSell)!.ActualPrice);
                    CitiesServ.CurrentCity!.Sell(new GoodOnSell(
                        (o as GoodOnSell)!.GooD, 1, (o as GoodOnSell)!.ActualPrice
                        ));
                    OnPropertyChanged(nameof(CityGoods));
                    OnPropertyChanged(nameof(PlayerGoods));
                    OnPropertyChanged("PlayerGoldString");
                },
                o => PlayerServ.Gold >= (o as GoodOnSell)!.ActualPrice);
            SellFromPlayer = new RelayCommand(
                o =>
                {
                    PlayerServ.Sell((o as GoodOnSell)!.GooD, 1, (o as GoodOnSell)!.ActualPrice);
                    CitiesServ.CurrentCity!.AddGood((o as GoodOnSell)!.GooD, 1);
                    OnPropertyChanged(nameof(CityGoods));
                    OnPropertyChanged(nameof(PlayerGoods));
                },
                o => true
                );
        }
    }
}
