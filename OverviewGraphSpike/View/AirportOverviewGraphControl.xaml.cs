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

        public Point PanOffset
        {
            get { return _panOffset; }
            set { _panOffset = value;}
        }

        public Size Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        CategoricalDataPoint _latestTrackSelectionItem = null;
        private Point _panOffset;
        private Size _zoom;

        public AirportOverviewGraphControl()
        {
            InitializeComponent();
        }

        private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            var item = e.Context.ClosestDataPoint.DataPoint as CategoricalDataPoint;
            if (item != null)
            {
                _latestTrackSelectionItem = item; //.DataItem as TimelineItem;
            }
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_latestTrackSelectionItem != null)
            {
                var viewmodel = DataContext as AirportOverviewGraphControlViewModel;
                var timelineItem = _latestTrackSelectionItem.DataItem as TimelineItem;
                if (viewmodel == null || timelineItem == null) return;

                viewmodel.SelectedTime = timelineItem.Time;
                DrawLine(_latestTrackSelectionItem);
            }
        }

        private void DrawLine(CategoricalDataPoint point)
        {
            if (CustomSelection == null || CustomSelection.Children == null) return;
            CustomSelection.Children.Clear();
            var x = _latestTrackSelectionItem.LayoutSlot.X;
            var y = _latestTrackSelectionItem.LayoutSlot.Y;
            //var parentY = _latestTrackSelectionItem.Parent.LayoutSlot.Bottom;
            var circle = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = new SolidColorBrush(Colors.Blue)
            };
            circle.SetValue(Canvas.TopProperty,y-5);
            circle.SetValue(Canvas.LeftProperty, x-5);
            var line = new Line
            {
                X1 = x,
                Y1 = y-5,
                X2 = x,
                Y2 = y+5,
                StrokeThickness = 5,
                Stroke = new SolidColorBrush(Colors.LightGray)
            };
            CustomSelection.Children.Add(circle);
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_latestTrackSelectionItem == null) return;
            DrawLine(_latestTrackSelectionItem);
        }
    }
}
