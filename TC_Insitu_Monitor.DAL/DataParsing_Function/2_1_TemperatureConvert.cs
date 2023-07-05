using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class TemperatureConvert
    {
        readonly private TableLookUP tableLook = new TableLookUP();
        readonly private Dictionary<double, double> table = new Dictionary<double, double>();
        public TemperatureConvert()
        {
            Initial();
        }
        public TemperatureConvert(string filePath)
        {
            if(File.Exists(filePath))
            {
                OpenInitial(filePath);
            }
            else
            {
                Initial();
            }
           
        }
        private void Initial()
        {
            //        KEY       VALUE        
            table.Add(0, -77.25);
            table.Add(696, 60);
            table.Add(1204, 160);
            table.Add(4096, 729.662);          
        }
        public double Convert(double input)
        {
            return tableLook.Convert(table,input);
            //return input * 0.197 - -77.25;
        }

        public double ReConverter(double input)
        {
            return tableLook.ReConverter(table, input);
        }

        public void OpenInitial(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SaveInitial(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
