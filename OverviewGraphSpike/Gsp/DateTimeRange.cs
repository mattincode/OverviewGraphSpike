using System;
using System.Collections.Generic;
using Itenso.TimePeriod;

namespace OverviewGraphSpike.Gsp
{

    public struct DateTimeRange : IEquatable<DateTimeRange>, ITimePeriod
    {
        private DateTime _start;
        private DateTime _end;

        public DateTime Start { get { return _start; } }
        public DateTime End { get { return _end; } }

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (end < start) throw new ArgumentException("start date must be less or equal to end date");

            _start = start;
            _end = end;
        }

        public TimeSpan Duration
        {
            get
            {
                return End - Start;
            }
        }

        public IEnumerable<DateTime> GetDays()
        {
            DateTime currentDate = _start.Date;
            DateTime endDate = _end.Date;

            while (currentDate < endDate)
            {
                yield return currentDate;
                currentDate = currentDate.AddDays(1);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Start.ToString(), End.ToString());
        }

        public bool Equals(DateTimeRange other)
        {
            return (this.Start == other.Start) && (this.End == other.End);
        }

        public override bool Equals(object obj)
        {
            return true; // TODO
        }

        public override int GetHashCode()
        {
            return this._start.GetHashCode() ^ this.End.GetHashCode();
        }

        public static bool operator ==(DateTimeRange a, DateTimeRange b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b)) return true;

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
                return false;

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(DateTimeRange a, DateTimeRange b)
        {
            return !(a.Equals(b));
        }


        public string DurationDescription
        {
            get { return new TimeRange(_start, _end).DurationDescription; }
        }

        public string GetDescription(ITimeFormatter formatter)
        {
            return new TimeRange(_start, _end).GetDescription(formatter);
        }

        public PeriodRelation GetRelation(ITimePeriod test)
        {
            return new TimeRange(_start, _end).GetRelation(test);
        }

        public bool HasEnd
        {
            get { return new TimeRange(_start, _end).HasEnd; }
        }

        public bool HasInside(ITimePeriod test)
        {
            return new TimeRange(_start, _end).HasInside(test);
        }

        public bool HasInside(DateTime test)
        {
            var timeRange = new TimeRange(_start, _end);

            //The point of time where the period ends doesn't belong to the period.
            if (test == timeRange.End) return false;

            return timeRange.HasInside(test);
        }

        public bool HasStart
        {
            get { return new TimeRange(_start, _end).HasStart; }
        }

        public bool IntersectsWith(ITimePeriod test)
        {
            return new TimeRange(_start, _end).IntersectsWith(test);
        }

        public bool IsAnytime
        {
            get { return new TimeRange(_start, _end).IsAnytime; }
        }

        public bool IsMoment
        {
            get { return new TimeRange(_start, _end).IsMoment; }
        }

        public bool IsReadOnly
        {
            get { return new TimeRange(_start, _end).IsReadOnly; }
        }

        public bool IsSamePeriod(ITimePeriod test)
        {
            return new TimeRange(_start, _end).IsSamePeriod(test);
        }

        public bool OverlapsWith(ITimePeriod test)
        {
            return new TimeRange(_start, _end).OverlapsWith(test);
        }

        public void Setup(DateTime newStart, DateTime newEnd)
        {
            throw new NotSupportedException();
        }

        public bool IsSameTimeOfDayPeriod(ITimePeriod test)
        {
            return this.Start.TimeOfDay == test.Start.TimeOfDay && this.End.TimeOfDay == test.End.TimeOfDay;
        }
    }
}
