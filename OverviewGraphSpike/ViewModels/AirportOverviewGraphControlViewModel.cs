using System;
using System.Collections.ObjectModel;
using OverviewGraphSpike.Gsp;

namespace OverviewGraphSpike.ViewModels
{
    public class AirportOverviewGraphControlViewModel : GspBaseViewModel
    {
        private DateTimeRange _timeRange;
        private DateTime _selectedTime;
        private ObservableCollection<TimelineItem> _timelineItems;

        public DateTimeRange TimeRange
        {
            get { return _timeRange; }
            set { _timeRange = value; RaisePropertyChanged(() => TimeRange); }
        }

        public DateTime SelectedTime  
        {
            get { return _selectedTime; }
            set { _selectedTime = value; RaisePropertyChanged(() => SelectedTime);}
        }

        public ObservableCollection<TimelineItem> TimelineItems
        {
            get { return _timelineItems; }
            set { _timelineItems = value; RaisePropertyChanged(() => TimelineItems);}
        }

        //public IActionCommand SelectTimeCommand { get; private set; }

        //public AirportOverviewGraphControlViewModel()
        //{
        //    NavigateBackCommand = new ActionCommand<object>(OnNavigateBack, null);

        //}
    }
}
