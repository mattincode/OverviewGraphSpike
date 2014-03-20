using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
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

        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(UserControl), new PropertyMetadata(new PropertyChangedCallback(OnSelectedDateChanged)));
        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set
            {
                SetValue(SelectedDateProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedTimeLineItemProperty = DependencyProperty.Register("SelectedTimeLineItem", typeof(TimelineItem), typeof(UserControl), new PropertyMetadata(null));
        public TimelineItem SelectedTimeLineItem
        {
            get { return (TimelineItem)GetValue(SelectedTimeLineItemProperty); }
            set
            {
                SetValue(SelectedTimeLineItemProperty, value);
            }
        }
        #endregion

        public Point PanOffset { get; set; }
        public Size Zoom { get; set; }
        CategoricalDataPoint _latestTrackSelectionItem = null;

        private static void OnSelectedDateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            // Select the value that is 
            var newSelectedDate = (DateTime)dependencyPropertyChangedEventArgs.NewValue;
            var control = sender as AirportOverviewGraphControl;
            if (control != null)
                control.TimeLineItems.UpdateSelectedDay(newSelectedDate, DummyData.GetBrush(DummyData.BrushTypeEnum.TimelineSelectedStaffingDayBrush), DummyData.GetBrush(DummyData.BrushTypeEnum.TimelineNormalStaffingDayBrush), DummyData.GetBrush(DummyData.BrushTypeEnum.TimelineSelectedSchedulingDayBrush), DummyData.GetBrush(DummyData.BrushTypeEnum.TimelineNormalSchedulingDayBrush));            
        }        

        public AirportOverviewGraphControl()
        {
            InitializeComponent();
        }

        private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            var item = e.Context.ClosestDataPoint.DataPoint as CategoricalDataPoint;
            if (item != null)
            {
                // Save tracked item
                _latestTrackSelectionItem = item;
            }
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            if (_latestTrackSelectionItem != null)
            {
                var timelineItem = _latestTrackSelectionItem.DataItem as TimelineItem;
                if (timelineItem == null) return;

                // Set selected date to the clicked element
                SelectedDate = timelineItem.Time;
                SelectedTimeLineItem = timelineItem;
            }
        }
    }
}
