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
using OverviewGraphSpike.Gsp;

namespace OverviewGraphSpike
{
    public static class DummyData
    {
        public static IEnumerable<TimelineItem> GetDummyData(DateTime day, DateTime selectedDay)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);
            var timeline = new List<TimelineItem>();
            for (int i = 0; i < 20; i++)
            {
                var staffingFactor = 1;
                var schedulingMatch = 1.2;

                Brush staffingColor;
                Brush schedulingColor;
                if (day.Year == selectedDay.Year && day.Month == selectedDay.Month && day.Day == selectedDay.Day)
                {
                    staffingColor = GetBrush(BrushTypeEnum.TimelineSelectedStaffingDayBrush);
                    schedulingColor = GetBrush(BrushTypeEnum.TimelineSelectedStaffingDayBrush);
                }
                else
                {
                    staffingColor = GetBrush(BrushTypeEnum.TimelineNormalStaffingDayBrush);
                    schedulingColor = GetBrush(BrushTypeEnum.TimelineNormalSchedulingDayBrush);
                }

                staffingFactor = rnd.Next(0, 100);
                schedulingMatch = rnd.Next(20, 120);
                timeline.Add(new TimelineItem() { Time = day, StaffingFactor = staffingFactor, SchedulingMatch = schedulingMatch, StaffingNeedMin = 0, StaffingNeedMax = 100, StaffingNeedAvg = 50, StaffingColor = staffingColor, SchedulingMatchColor = schedulingColor});
                
                day = day.AddDays(1);
            }
            return timeline;
        }

        public enum BrushTypeEnum
        {
            TimelineSelectedStaffingDayBrush,
            TimelineNormalStaffingDayBrush,
            TimelineSelectedSchedulingDayBrush,
            TimelineNormalSchedulingDayBrush
        }

        public static SolidColorBrush GetBrush(BrushTypeEnum brushType)
        {
            switch (brushType)
            {
                case BrushTypeEnum.TimelineSelectedStaffingDayBrush:
                    return new SolidColorBrush((Color)Application.Current.Resources["GspRed"]);
                case BrushTypeEnum.TimelineNormalStaffingDayBrush:
                    return new SolidColorBrush((Color)Application.Current.Resources["GspLightBlue"]);
                case BrushTypeEnum.TimelineSelectedSchedulingDayBrush:
                    return new SolidColorBrush((Color)Application.Current.Resources["GspRed"]);
                case BrushTypeEnum.TimelineNormalSchedulingDayBrush:
                    return new SolidColorBrush((Color)Application.Current.Resources["SecuritasAccentGreen2"]);
                default:
                    return new SolidColorBrush((Color)Application.Current.Resources["GspBlack"]);
            }            
        }

    }

}
