using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class StopCriteriaConfigs:ConfigFunction
    {
       
        private StopCriteria _stopCriteria;
        readonly private string _filePath = string.Empty;

        public StopCriteria StopCriteria
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
                return _stopCriteria;
            }
            set
            {                
                if (File.Exists(_filePath))
                {                    
                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "StopCriteriaConfigs.ini"), value);
                }
            }
        }

        public StopCriteriaConfigs(string filePath) :base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _stopCriteria = new StopCriteria()
            {
                IsCriteria = true,
                IsCycles = false,
                FailDUTQty = 0
            };
        }

        private void OpenInitial(string filePath)
        {
            _stopCriteria = new StopCriteria()
            {
                IsCriteria = bool.Parse(GetKeyValue(filePath, "StopCriteria", "IsCriteria", "")),
                IsCycles   = bool.Parse(GetKeyValue(filePath, "StopCriteria", "IsCycles", "")),
                FailDUTQty = int.Parse(GetKeyValue(filePath, "StopCriteria", "FailDUTQty", ""))
            };
        }

        private void SaveInitial(string filePath, StopCriteria stopCriteria)
        {
            SetKeyValue(filePath, "StopCriteria", "IsCriteria", stopCriteria.IsCriteria.ToString());
            SetKeyValue(filePath, "StopCriteria", "IsCycles", stopCriteria.IsCycles.ToString());
            SetKeyValue(filePath, "StopCriteria", "FailDUTQty", stopCriteria.FailDUTQty.ToString());
        }
    }
}
