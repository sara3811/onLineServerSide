using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ActivityTimeDTO
    {

        public int ActivityTimeId { get; set; }
        public int ServiceId { get; set; }
        public int DayInWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int NumOfWorkers { get; set; }
        public int EstimatedDurationOfService { get; set; }
        public double? ActualDurationOfService { get; set; }
        public double? StandardDeviation { get; set; }
        public double? AverageNumOfWaitingPeople { get; set; }

    }
}
