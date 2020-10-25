using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class unusual
    {
        public unusual(int activityTimeId,double average,bool kind,DateTime startTime,TimeSpan endTime,double standardDeviation)
        {

            this.activityTimeId = activityTimeId;
            this.average = average;
            this.isActive = true;
            this.kindOfUnusual = kind;
            this.startTime = startTime;
            this.endTime = endTime;
            this.standardDeviation = standardDeviation;
        }
    }
}
