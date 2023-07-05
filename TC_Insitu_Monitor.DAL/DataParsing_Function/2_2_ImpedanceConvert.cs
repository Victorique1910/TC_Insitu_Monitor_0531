using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class ImpedanceConvert
    {
        readonly private TableLookUP tableLook = new TableLookUP();
        readonly private Dictionary<double, double> table = new Dictionary<double, double>();
        public ImpedanceConvert()
        {
            Initial();
        }
        public ImpedanceConvert(string filePath)
        {
            if (File.Exists(filePath))
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
            //        KEY     VALUE
            table.Add(0, -0.031);
            table.Add(1.064, 1.0298);
            table.Add(2.074, 2.0366);
            table.Add(9.99, 9.951);
            table.Add(49.507, 49.82);
            table.Add(99.028, 99.75);
            table.Add(493.631, 500.9);
            table.Add(982.068, 1000.1);            
        }

        public double Convert(double input)
        {           
            return tableLook.Convert(table, input);
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
