using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using OverviewGraphSpike.Gsp;
using OverviewGraphSpike.ViewModels;

namespace OverviewGraphSpike
{
    public partial class MainPage : UserControl
    {
        AirportOverviewGraphControlViewModel _vm = new AirportOverviewGraphControlViewModel();
        public MainPage()
        {
            InitializeComponent();
            var startDate = new DateTime(2001, 1, 1, 00, 00, 00);
            _vm.SelectedTime = startDate;
            _vm.TimeRange = new DateTimeRange(startDate, startDate.AddMonths(6));
            _vm.TimelineItems =new ObservableCollection<TimelineItem>(DummyData.GetDummyData());
            DataContext = _vm;
        }
    }
}
