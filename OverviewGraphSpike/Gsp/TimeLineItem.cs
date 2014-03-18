using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace OverviewGraphSpike.Gsp
{
    public class TimelineItem
    {
        // Main values showed in the chart
        //public TimeRange TimeRange { get; set; }            // The period that the values are valid for, typically one day initially       
        public DateTime Time { get; set; }
        public double StaffingFactor { get; set; }          // Current staffing/optimal % -> 100% == spot on, 50% == we are understaffed, 150% big overstaffing
        public double SchedulingMatch { get; set; }         // How well are we matching the schedules (0-100%)
        // Secondary values showed in sidebar
        public double StaffingNeedMax { get; set; }
        public double StaffingNeedMin { get; set; }
        public double StaffingNeedAvg { get; set; }
    }

    public static class DummyData
    {
        public static IEnumerable<TimelineItem> GetDummyData(DateTime day)
        {
            var timeline = new List<TimelineItem>();
            for (int i = 0; i < 20; i++)
            {
                var staffingFactor = 100;
                var schedulingMatch = 120;

                timeline.Add(new TimelineItem() { Time = day, StaffingFactor = staffingFactor, SchedulingMatch = schedulingMatch, StaffingNeedMin = 0, StaffingNeedMax = 100, StaffingNeedAvg = 50 });
                day = day.AddDays(1);
            }
            return timeline;
        }        
    }

}
