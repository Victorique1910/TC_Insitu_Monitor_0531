using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class SampleRateConfigs:ConfigFunction
    {        
        readonly private string _filePath = string.Empty;
        private SampleRate _sampleRate;

        public SampleRate SampleRate 
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
                return _sampleRate;
            }
            set
            {
                if (File.Exists(_filePath))
                {

                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "SampleRateConfigs.ini"), value);
                }
            }
        }

        public SampleRateConfigs(string filePath) : base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _sampleRate = new SampleRate()
            {
                InitialSampleRate = 1000,
                FailSampleRate =1000,
                SaveLogBeforeFail =1000,
                SaveLogAfterFail =1000
            };
        }

        private void OpenInitial(string filePath)
        {
            _sampleRate = new SampleRate()
            {
                InitialSampleRate = 1000/double.Parse(GetKeyValue(filePath, "SampleRate", "InitialSampleRate", "")),
                FailSampleRate = 1000/ double.Parse(GetKeyValue(filePath, "SampleRate", "FailSampleRate", "")),
                SaveLogBeforeFail = int.Parse(GetKeyValue(filePath, "SampleRate", "SaveLogBeforeFail", "")),
                SaveLogAfterFail = int.Parse(GetKeyValue(filePath, "SampleRate", "SaveLogAfterFail", "")),
            };
        }

        private void SaveInitial(string filePath, SampleRate sampleRate)
        {
            SetKeyValue(filePath, "SampleRate", "InitialSampleRate", sampleRate.InitialSampleRate.ToString());
            SetKeyValue(filePath, "SampleRate", "FailSampleRate", sampleRate.FailSampleRate.ToString());
            SetKeyValue(filePath, "SampleRate", "SaveLogBeforeFail", sampleRate.SaveLogBeforeFail.ToString());
            SetKeyValue(filePath, "SampleRate", "SaveLogAfterFail", sampleRate.SaveLogAfterFail.ToString());
        }
    }
}
