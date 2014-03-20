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
        private TimelineItem _selectedTimeLineItem;
        private double _testValue = 8.9002;

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

        public TimelineItem SelectedTimeLineItem
        {
            get { return _selectedTimeLineItem; }
            set { _selectedTimeLineItem = value; RaisePropertyChanged(() => SelectedTimeLineItem); }
        }

        public double TestValue
        {
            get { return _testValue; }
            set { _testValue = value; RaisePropertyChanged(() => TestValue); }
        }        
    }
}
