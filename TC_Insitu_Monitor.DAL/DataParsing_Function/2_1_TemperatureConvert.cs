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
            table.Add(0.192620623, -39.171);
            table.Add(0.2673, -19.492);
            table.Add(0.41767578125, 20.13400);
            table.Add(0.57189453125, 56.499);           
            table.Add(1.130134277, 170.18);
        }
        public double Convert(double input)
        {           
            return tableLook.Convert(table,input);
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
