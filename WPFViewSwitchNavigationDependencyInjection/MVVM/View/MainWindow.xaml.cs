using System.Windows;
using System.Windows.Media;
using WPFViewSwitchNavigationDependencyInjection.Core;
using WPFViewSwitchNavigationDependencyInjection.MVVM.Model;

namespace WPFViewSwitchNavigationDependencyInjection.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double sWidth;
        private double sHeight;

        public MainWindow()
        {
            InitializeComponent();
            MainGameWindow.MinWidth = GameParameters.gridCols * GameParameters.cellSize + 200;
            MainGameWindow.MinHeight = GameParameters.gridRows * GameParameters.cellSize + 50;
            sWidth = MainGameWindow.Width;
            sHeight = MainGameWindow.Height;
            UpdateGridPadding();
        }

        protected void OnWindowSizeChange(object sender, SizeChangedEventArgs e)
        {
            double nHeight = e.NewSize.Height;
            double nWidth = e.NewSize.Width;
            sWidth = nWidth; sHeight = nHeight;
            UpdateGridPadding();
        }

        private void UpdateGridPadding()
        {
            LeftGridPadding.Width = new GridLength((sWidth - ((GameParameters.gridCols + 0.5) * GameParameters.cellSize)) / 2);
            RightGridPadding.Width = new GridLength((sWidth - ((GameParameters.gridCols + 0.5 )* GameParameters.cellSize)) / 2);
            ContentView.Width = (GameParameters.gridCols + 0.5) * GameParameters.cellSize;
            ContentView.Height = (GameParameters.gridRows) * GameParameters.cellSize;
        }
    }
}
