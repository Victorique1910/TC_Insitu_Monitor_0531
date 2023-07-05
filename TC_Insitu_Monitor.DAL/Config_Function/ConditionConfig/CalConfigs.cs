using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class CalConfigs: ConfigFunction
    {
        private readonly string _filePath = string.Empty;
        private CalConfigsStruct _calConfigsStruct;

        public CalConfigsStruct CalConfigsStruct
        {
            get
            {
                if (File.Exists(_filePath))
                {
                    OpenInitial(_filePath);
                }
                else
                {
                    Initial();
                }
                return _calConfigsStruct;
            }
            set
            {
                if (File.Exists(_filePath))
                {
                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "CalConfigs.ini"), value);
                }
            }
        }

        public CalConfigs(string filePath):base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _calConfigsStruct = new CalConfigsStruct()
            {
                IdelImpedance = 0,
                IdelTemperature = 0
            };
        }
        private void OpenInitial(string filePath)
        {
            _calConfigsStruct = new CalConfigsStruct()
            {                
                IdelImpedance = double.Parse(GetKeyValue(filePath, "CalConfigs", "IdelImpedance", "")),
                IdelTemperature = double.Parse(GetKeyValue(filePath, "CalConfigs", "IdelTemperature", ""))
            };
        }
        private void SaveInitial(string filePath, CalConfigsStruct calConfigsStruct)
        {
            SetKeyValue(filePath, "CalConfigs", "IdelImpedance", calConfigsStruct.IdelImpedance.ToString());
            SetKeyValue(filePath, "CalConfigs", "IdelTemperature", calConfigsStruct.IdelTemperature.ToString());
        }
    }
}
