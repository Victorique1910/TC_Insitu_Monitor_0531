using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class FailCriteriaConfigs:ConfigFunction
    {
        readonly private string _filePath = string.Empty;
        private FailCriteria _failCriteria;

        public FailCriteria FailCriteria
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
                return _failCriteria;
            }
            set
            {
                if (File.Exists(_filePath))
                {

                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "FailCriteriaConfigs.ini"), value);
                }
            }
        }

        public FailCriteriaConfigs(string filePath) : base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _failCriteria = new FailCriteria()
            {
                IsCriteria = true,
                IsDPAT = false,
                FailCondition = "Absolute",
                Method = "Above",
                Precentage = 0,
                ValuesMax = 10,
                ValuesMin = 0,
                Base = "Max",
                ContinuousCount = 5
            };
        }

        private void OpenInitial(string filePath)
        {
            _failCriteria = new FailCriteria()
            {
                IsCriteria =     bool.Parse(GetKeyValue(filePath, "FailCriteria", "IsCriteria", "")),
                IsDPAT =         bool.Parse(GetKeyValue(filePath, "FailCriteria", "IsDPAT", "")),
                FailCondition =             GetKeyValue(filePath, "FailCriteria", "FailCondition", ""),               
                Method =                    GetKeyValue(filePath, "FailCriteria", "Method", ""),
                Precentage =   double.Parse(GetKeyValue(filePath, "FailCriteria", "Precentage", "")),
                ValuesMax =    double.Parse(GetKeyValue(filePath, "FailCriteria", "ValuesMax", "")),
                ValuesMin =    double.Parse(GetKeyValue(filePath, "FailCriteria", "ValuesMin", "")),
                Base =                      GetKeyValue(filePath, "FailCriteria", "Base", ""),
                ContinuousCount = int.Parse(GetKeyValue(filePath, "FailCriteria", "ContinuousCount", ""))
            };
        }

        private void SaveInitial(string filePath, FailCriteria failCriteria)
        {
            SetKeyValue(filePath, "FailCriteria", "IsCriteria", failCriteria.IsCriteria.ToString());
            SetKeyValue(filePath, "FailCriteria", "IsDPAT", failCriteria.IsDPAT.ToString());
            SetKeyValue(filePath, "FailCriteria", "FailCondition", failCriteria.FailCondition);
            SetKeyValue(filePath, "FailCriteria", "Method", failCriteria.Method);
            SetKeyValue(filePath, "FailCriteria", "ValuesMax", failCriteria.ValuesMax.ToString());
            SetKeyValue(filePath, "FailCriteria", "ValuesMin", failCriteria.ValuesMin.ToString());
            SetKeyValue(filePath, "FailCriteria", "Base", failCriteria.Base);
            SetKeyValue(filePath, "FailCriteria", "ContinuousCount", failCriteria.ContinuousCount.ToString());
        }
    }
}
