using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using OverviewGraphSpike.ViewModels;

namespace OverviewGraphSpike.Gsp
{
    public class TimelineItem :GspBaseViewModel
    {
        private Brush _staffingColor;
        private Brush _schedulingMatchColor;
        private bool _isSelected;
        private string _dateString;
        // Main values showed in the chart
        public DateTime Time { get; set; }
        public double StaffingFactor { get; set; }          // Current staffing/optimal % -> 100% == spot on, 50% == we are understaffed, 150% big overstaffing
        public double SchedulingMatch { get; set; } // How well are we matching the schedules (0-100%)

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value; 
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public string DateStringStaffing
        {
            get
            {
                return String.Format("({0}%) {1}", (StaffingFactor*100).ToString("0.#"), Time.ToShortDateString()) ;
            }
        }

        public string DateStringScheduling
        {
            get
            {
                return String.Format("({0}%) {1}", SchedulingMatch*100, Time.ToShortDateString());
            }
        }

        public Brush StaffingColor
        {
            get { return _staffingColor; }
            set
            {
                _staffingColor = value; 
                RaisePropertyChanged(() => StaffingColor);
            }
        }

        public Brush SchedulingMatchColor
        {
            get { return _schedulingMatchColor; }
            set
            {
                _schedulingMatchColor = value;
                RaisePropertyChanged(() => SchedulingMatchColor);
            }
        }


        // Secondary values showed in sidebar
        public double StaffingNeedMax { get; set; }
        public double StaffingNeedMin { get; set; }
        public double StaffingNeedAvg { get; set; }
    }

    public static class TimelineItemExtensions
    {
        public static void UpdateSelectedDay(this IEnumerable<TimelineItem> items, DateTime selectedDay, Brush selectedBrushStaffing, Brush normalBrushStaffing, Brush selectedBrushScheduling, Brush normalBrushScheduling)
        {           
            if (items == null) return;
            
            // Clear all selected
            foreach (var item in items)
            {
                if (item.IsSelected)
                {
                    item.StaffingColor = normalBrushStaffing;
                    item.SchedulingMatchColor = normalBrushScheduling;
                    item.IsSelected = false;
                }                    
            }
            // Set selected
            var selectedItem = items.FirstOrDefault(x => x.Time.Year == selectedDay.Year && x.Time.Month == selectedDay.Month && x.Time.Day == selectedDay.Day);
            if (selectedItem != null)
            {
                selectedItem.IsSelected = true;
                selectedItem.StaffingColor = selectedBrushStaffing;
                selectedItem.SchedulingMatchColor = selectedBrushScheduling;
            }
        }
    }
}
