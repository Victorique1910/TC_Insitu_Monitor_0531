using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;


namespace TC_Insitu_Monitor.DAL
{
    public class CorrespondentTemperatureConfigs:ConfigFunction
    {
        readonly private string _filePath = string.Empty;
        private CorrespondentTemperature _correspondentTemperature;
        public CorrespondentTemperature CorrespondentTemperature 
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
                return _correspondentTemperature;
            }
            set
            {
                if (File.Exists(_filePath))
                {

                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "CorrespondentTemperatureConfigs.ini"), value);
                }
            }
        }

        public CorrespondentTemperatureConfigs(string filePath):base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _correspondentTemperature = new CorrespondentTemperature() 
            { 
                Channel = "101"
            };
        }

        private void OpenInitial(string filePath)
        {
            _correspondentTemperature = new CorrespondentTemperature()
            {
                Channel = GetKeyValue(filePath, "CorrespondentTemperature", "Channel", "")
            };
        }

        private void SaveInitial(string filePath, CorrespondentTemperature correspondentTemperature)
        {
            SetKeyValue(filePath, "CorrespondentTemperature", "Channel", correspondentTemperature.Channel);
        }
    }
}
