using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.Model
{
    public struct FormalTestStruct
    {
        public double InitialImpLowTemp;
        public double InitialImpHighTemp;
        public double LowLimitLowTemp;
        public double LowLimitHighTemp;
        public double HighLimitLowTemp;
        public double HighLimitHighTemp;
        public double InitialImpedance;
    }

    public struct PreTestStruct
    {
        public double TC_TempRange_Max;
        public double TC_TempRange_Min;
        public double TC_RampRate;
        public double TC_DwellTime;
        public double TempLimit_Max;
        public double TempLimit_Min;
        public double HT_RampUp_Max;
        public double HT_RampUp_Min;
        public double HT_DwellTime_Max;
        public double HT_DwellTime_Min;
        public double LT_RampDown_Max;
        public double LT_RampDown_Min;
        public double LT_DwellTime_Max;
        public double LT_DwellTime_Min;
    }
}
