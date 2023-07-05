using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class ConditionTCConfigs:ConfigFunction
    {
        private readonly string _filePath = string.Empty;      
        private ConditionTC _conditionTC;

        public ConditionTC ConditionTC 
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
                return _conditionTC;
            }
            set
            {
                if (File.Exists(_filePath))
                {

                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "ConditionTCConfigs.ini"), value);
                }
            }
        }
        public ConditionTCConfigs(string filePath):base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _conditionTC = new ConditionTC()
            {
                LowTempThreshold = 30,
                DwellTimeL = 20,
                RampDown = 20,
                HighTempThreshold = 40,
                DwellTimeH = 20,
                RampUp =20,
                OffsetCycle =0,
                CalculateCycle =0,
                TestCycle =10
            };
        }

        private void OpenInitial(string filePath)
        {
            _conditionTC = new ConditionTC()
            {
                LowTempThreshold = double.Parse(GetKeyValue(filePath, "ConditionTC", "LowTempThreshold", "")),
                DwellTimeL = double.Parse(GetKeyValue(filePath, "ConditionTC", "DwellTimeL", "")),
                RampDown = double.Parse(GetKeyValue(filePath, "ConditionTC", "RampDown", "")),
                HighTempThreshold = double.Parse(GetKeyValue(filePath, "ConditionTC", "HighTempThreshold", "")),
                DwellTimeH = double.Parse(GetKeyValue(filePath, "ConditionTC", "DwellTimeH", "")),
                RampUp = double.Parse(GetKeyValue(filePath, "ConditionTC", "RampUp", "")),
                OffsetCycle = int.Parse(GetKeyValue(filePath, "ConditionTC", "OffsetCycle", "")),
                CalculateCycle = int.Parse(GetKeyValue(filePath, "ConditionTC", "CalculateCycle", "")),
                TestCycle = int.Parse(GetKeyValue(filePath, "ConditionTC", "TestCycle", ""))
            };
        }

        private void SaveInitial(string filePath, ConditionTC conditionTC)
        {
            SetKeyValue(filePath, "ConditionTC", "LowTempThreshold", conditionTC.LowTempThreshold.ToString());
            SetKeyValue(filePath, "ConditionTC", "DwellTimeL", conditionTC.DwellTimeL.ToString());
            SetKeyValue(filePath, "ConditionTC", "RampDown", conditionTC.RampDown.ToString());
            SetKeyValue(filePath, "ConditionTC", "HighTempThreshold", conditionTC.HighTempThreshold.ToString());
            SetKeyValue(filePath, "ConditionTC", "DwellTimeH", conditionTC.DwellTimeH.ToString());
            SetKeyValue(filePath, "ConditionTC", "RampUp", conditionTC.RampUp.ToString());
            SetKeyValue(filePath, "ConditionTC", "OffsetCycle", conditionTC.OffsetCycle.ToString());
            SetKeyValue(filePath, "ConditionTC", "CalculateCycle", conditionTC.CalculateCycle.ToString());
            SetKeyValue(filePath, "ConditionTC", "TestCycle", conditionTC.TestCycle.ToString());
        }
    }
}
