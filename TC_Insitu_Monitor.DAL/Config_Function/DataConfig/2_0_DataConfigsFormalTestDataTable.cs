using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataConfigsFormalTestDataTable:FileFunction
    {
        private readonly DataTable _dataTable = new DataTable();
        private readonly List<string> headLine = new List<string>() { "Board", "COMPort", "Port", "ID", "Type", "IsCriteria", "IsDPAT", "FailCondition", "Method", "Precentage", "ValuesMax", "ValuesMin", "Base", "ContinuousCount", "CompensationADCH", "CompensationADCL", "Loss", "DUTName", "Chain", "ImpedanceChannel", "Location", "TemperatureName", "TemperatureType",  "InitialImpLowTemp", "InitialImpHighTemp", "LowLimitLowTemp", "LowLimitHighTemp", "HighLimitLowTemp", "HighLimitHighTemp", "InitialImpedance", "CurrentImpedance", "CurrentTemperature", "VoltageH", "VoltageL", "TimeStamp", "Status", "TemperatureCycle", "ImpedanceDelta", "JudgementLowTemp", "JudgementHighTemp", "JudgementRampUp", "JudgementRampDown", "JudgementCycles", "FailCriteria" };
        private readonly string _filePath = string.Empty;
        private DataConfigs _dataConfigs;

        public DataTable DataTable
        {
            get
            {
                _dataConfigs = new DataConfigs(_filePath);
                OpenInitial(_dataConfigs.Configs);
                return _dataTable;
            }
            set
            {
                if (File.Exists(_filePath))
                {
                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "DataConfigs.csv"), value);
                }
            }
        }
        //顯示用
        public DataConfigsFormalTestDataTable()
        {

        }
        //存檔讀取用
        public DataConfigsFormalTestDataTable(string filePath)
        {
            _filePath = filePath;
        }

        private void OpenInitial(List<ConfigStruct> configs)
        {            
            for (int i = 0; i < headLine.Count; i++)
            {
                DataColumn dc = new DataColumn(headLine[i]);
                _dataTable.Columns.Add(dc);
            }
            
            foreach (var config in configs)
            {                
                DataRow dr = _dataTable.NewRow();
                dr["Board"]              = config.Board.ToString();
                dr["COMPort"]            = config.COMPort;
                dr["Port"]               = config.Port;
                dr["ID"]                 = config.ID;
                dr["Type"]               = config.Type;
                dr["IsCriteria"]         = config.FailCriteria.IsCriteria;
                dr["IsDPAT"]             = config.FailCriteria.IsDPAT;
                dr["FailCondition"]      = config.FailCriteria.FailCondition;
                dr["Method"]             = config.FailCriteria.Method;
                dr["Precentage"]         = config.FailCriteria.Precentage;
                dr["ValuesMax"]          = config.FailCriteria.ValuesMax;
                dr["ValuesMin"]          = config.FailCriteria.ValuesMin;
                dr["Base"]               = config.FailCriteria.Base;
                dr["ContinuousCount"]    = config.FailCriteria.ContinuousCount;
                dr["CompensationADCH"]   = config.CompensationADCH;
                dr["CompensationADCL"]   = config.CompensationADCL;
                dr["Loss"]               = config.Loss;

                dr["DUTName"]            = config.DUTName;
                dr["Chain"]              = config.Chain;
                dr["ImpedanceChannel"]   = config.Channel;
                dr["Location"]           = config.Location;
                dr["TemperatureName"]    = config.TemperatureName;
                dr["TemperatureType"]    = config.TemperatureType;

                dr["InitialImpLowTemp"]  = "";
                dr["InitialImpHighTemp"] = "";
                dr["LowLimitLowTemp"]    = "";
                dr["LowLimitHighTemp"]   = "";
                dr["HighLimitLowTemp"]   = "";
                dr["HighLimitHighTemp"]  = "";
                dr["InitialImpedance"]   = "";

                dr["CurrentImpedance"]   = "";
                dr["CurrentTemperature"] = "";
                dr["VoltageH"]           = "";
                dr["VoltageL"]           = "";
                dr["TimeStamp"]          = "";
                dr["Status"]             = "";
                dr["TemperatureCycle"]   = "";
                dr["ImpedanceDelta"]     = "";
                dr["JudgementLowTemp"]   = "";
                dr["JudgementHighTemp"]  = "";
                dr["JudgementRampUp"]    = "";
                dr["JudgementRampDown"]  = "";
                dr["JudgementCycles"]    = "";
                dr["FailCriteria"]       = "";

                _dataTable.Rows.Add(dr);
            }
        }

        public DataTable Update(List<DataConfigsStatus> dataConfigs)
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < headLine.Count; i++)
            {
                DataColumn dc = new DataColumn(headLine[i]);
                dataTable.Columns.Add(dc);
            }

            foreach (var dataConfig in dataConfigs)
            {
                DataRow dr = dataTable.NewRow();
                dr["Board"]              = dataConfig.Configs.Board.ToString();
                dr["COMPort"]            = dataConfig.Configs.COMPort;
                dr["Port"]               = dataConfig.Configs.Port;
                dr["ID"]                 = dataConfig.Configs.ID;
                dr["Type"]               = dataConfig.Configs.Type;
                dr["IsCriteria"]         = dataConfig.Configs.FailCriteria.IsCriteria;
                dr["IsDPAT"]             = dataConfig.Configs.FailCriteria.IsDPAT;
                dr["FailCondition"]      = dataConfig.Configs.FailCriteria.FailCondition;
                dr["Method"]             = dataConfig.Configs.FailCriteria.Method;
                dr["Precentage"]         = dataConfig.Configs.FailCriteria.Precentage;
                dr["ValuesMax"]          = dataConfig.Configs.FailCriteria.ValuesMax;
                dr["ValuesMin"]          = dataConfig.Configs.FailCriteria.ValuesMin;
                dr["Base"]               = dataConfig.Configs.FailCriteria.Base;
                dr["ContinuousCount"]    = dataConfig.Configs.FailCriteria.ContinuousCount;
                dr["CompensationADCH"]   = dataConfig.Configs.CompensationADCH;
                dr["CompensationADCL"]   = dataConfig.Configs.CompensationADCL;
                dr["Loss"]               = dataConfig.Configs.Loss;

                dr["DUTName"]            = dataConfig.Configs.DUTName;
                dr["Chain"]              = dataConfig.Configs.Chain;
                dr["ImpedanceChannel"]   = dataConfig.Configs.Channel;
                dr["Location"]           = dataConfig.Configs.Location;
                dr["TemperatureName"]    = dataConfig.Configs.TemperatureName;
                dr["TemperatureType"]    = dataConfig.Configs.TemperatureType;

                dr["InitialImpLowTemp"]  = dataConfig.FormalTestStruct.InitialImpLowTemp;
                dr["InitialImpHighTemp"] = dataConfig.FormalTestStruct.InitialImpHighTemp;
                dr["LowLimitLowTemp"]    = dataConfig.FormalTestStruct.LowLimitLowTemp;
                dr["LowLimitHighTemp"]   = dataConfig.FormalTestStruct.LowLimitHighTemp;
                dr["HighLimitLowTemp"]   = dataConfig.FormalTestStruct.HighLimitLowTemp;
                dr["HighLimitHighTemp"]  = dataConfig.FormalTestStruct.HighLimitHighTemp;
                dr["InitialImpedance"]   = dataConfig.FormalTestStruct.InitialImpedance;

                dr["CurrentImpedance"]   = dataConfig.DataFormatStruct.Impedance;
                dr["CurrentTemperature"] = dataConfig.DataFormatStruct.Temperature;
                dr["VoltageH"]           = dataConfig.DataFormatStruct.VoltageH;
                dr["VoltageL"]           = dataConfig.DataFormatStruct.VoltageL;
                dr["TimeStamp"]          = dataConfig.JudgementStruct.TimeStamp;
                dr["Status"]             = dataConfig.JudgementStruct.Status;
                dr["TemperatureCycle"]   = dataConfig.JudgementStruct.TemperatureCycle;
                dr["ImpedanceDelta"]     = dataConfig.DataFormatStruct.Impedance - dataConfig.FormalTestStruct.InitialImpedance;
                dr["JudgementLowTemp"]   = dataConfig.JudgementStruct.JudgementLowTemp;
                dr["JudgementHighTemp"]  = dataConfig.JudgementStruct.JudgementHighTemp;
                dr["JudgementRampUp"]    = dataConfig.JudgementStruct.JudgementRampUp;
                dr["JudgementRampDown"]  = dataConfig.JudgementStruct.JudgementRampDown;
                dr["JudgementCycles"]    = dataConfig.JudgementStruct.JudgementCycles;
                dr["FailCriteria"]       = dataConfig.JudgementStruct.FailCause;

                dataTable.Rows.Add(dr);
            }
            return dataTable;
        }

        private void SaveInitial(string filePath, DataTable dataTable)
        {
            SaveFile(filePath, dataTable);
        }
    }
}
