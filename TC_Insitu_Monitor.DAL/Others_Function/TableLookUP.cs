using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.DAL
{
    public class TableLookUP
    {
        public double Convert(Dictionary<double,double> table,double input)
        {
            int rowCount = 0;
            double previousKey = table.Keys.Min();
            double nextKey = table.Keys.Min();
            double previousValue = table.Values.Min();
            double nextValue = table.Values.Min();            
            double KeyDivValue;
            foreach (var row in table)
            {
                nextKey = row.Key;
                nextValue = row.Value;
                if (input <= row.Key)
                {
                    break;
                }
                previousKey = row.Key;
                previousValue = row.Value;
                rowCount++;
            }
            KeyDivValue = (nextKey - previousKey) /(nextValue - previousValue);            
            if(nextKey - previousKey == 0)
            {
                return previousValue;
            }
            else
            {
                return ((input - previousKey) / KeyDivValue) + previousValue;
            }            
        }

        public double ReConverter(Dictionary<double, double> table,double input)
        {
            int rowCount = 0;
            double previousKey = table.Keys.Min();
            double nextKey = table.Keys.Min();
            double previousValue = table.Values.Min();
            double nextValue = table.Values.Min();
            double ValueDivValue;             //Value 
            foreach (var row in table)
            {
                nextKey = row.Key;
                nextValue = row.Value;
                if (input <= row.Value)     //Value
                {
                    break;
                }
                previousKey = row.Key;
                previousValue = row.Value;
                rowCount++;
            }
            ValueDivValue = (nextKey - previousKey) / (nextValue - previousValue);   // key/value
            return ((input - previousValue) * ValueDivValue) + previousKey;
        }
    }
}
