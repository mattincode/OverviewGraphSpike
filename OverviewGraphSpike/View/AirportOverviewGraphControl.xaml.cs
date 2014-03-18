using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OverviewGraphSpike.Gsp;
using OverviewGraphSpike.ViewModels;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;

namespace OverviewGraphSpike.View
{
    public partial class AirportOverviewGraphControl : UserControl
    {
        #region Dependency properties
        public static readonly DependencyProperty TimeLineItemsProperty = DependencyProperty.Register("TimeLineItems", typeof(ObservableCollection<TimelineItem>), typeof(UserControl), new PropertyMetadata(null));
        public ObservableCollection<TimelineItem> TimeLineItems
        {
            get { return (ObservableCollection<TimelineItem>)GetValue(TimeLineItemsProperty); }
            set
            {
                SetValue(TimeLineItemsProperty, value);
            }
        }

        public static readonly DependencyProperty TimeRangeProperty = DependencyProperty.Register("TimeRange", typeof(DateTimeRange), typeof(UserControl), new PropertyMetadata(null));
        public DateTimeRange TimeRange
        {
            get { return (DateTimeRange)GetValue(TimeRangeProperty); }
            set
            {
                SetValue(TimeRangeProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(UserControl), new PropertyMetadata(null));
        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }
        #endregion

        TimelineItem _latestTrackSelectionItem = null; 
        public AirportOverviewGraphControl()
        {
            InitializeComponent();
        }

        private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            var item = e.Context.ClosestDataPoint.DataPoint as CategoricalDataPoint;
            if (item != null)
            {
                _latestTrackSelectionItem = item.DataItem as TimelineItem;
            }
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_latestTrackSelectionItem != null)
            {
                var viewmodel = DataContext as AirportOverviewGraphControlViewModel;
                if (viewmodel == null) return;

                viewmodel.SelectedTime =_latestTrackSelectionItem.Time;

            }
        }

    }
}
