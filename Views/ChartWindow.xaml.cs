using OxyPlot.Wpf;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class ChartWindow : Window
    {
        public ChartWindow(OxyPlot.PlotModel plotModel)
        {
            InitializeComponent();
            PlotView.Model = plotModel;
        }
    }
}
