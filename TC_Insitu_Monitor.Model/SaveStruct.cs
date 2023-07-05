using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.Model
{
    public struct SaveFormalLogStruct
    {
        public string ID;
        public string DUTName;
        public double Impedance;
        public double Temperature;
        public int Cycle;
        public string Status;
        public string TimeStamp;
        public double TimeTicks;
    }
}