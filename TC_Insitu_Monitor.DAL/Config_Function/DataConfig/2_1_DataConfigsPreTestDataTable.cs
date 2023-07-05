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
    public class DataConfigsPreTestDataTable : FileFunction
    {
        private readonly DataTable _dataTable = new DataTable();
        private readonly List<string> headLine = new List<string>() { "DUTName", "TC_TempRange", "TC_RampRate", "TC_DwellTime", "TempLimit", "HT_RampUp", "HT_DwellTime", "LT_RampUp", "LT_DwellTime" };
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
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "DataConfigsPreTest.csv"), value);
                }
            }
        }

        public DataConfigsPreTestDataTable()
        {

        }

        public DataConfigsPreTestDataTable(string filePath)
        {
            _filePath = filePath;
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
                dr["DUTName"]      = dataConfig.Configs.DUTName;
                dr["TC_TempRange"] = dataConfig.PreTestStruct.TC_TempRange_Max + "~" + dataConfig.PreTestStruct.TC_TempRange_Min;
                dr["TC_RampRate"]  = dataConfig.PreTestStruct.TC_RampRate;
                dr["TC_DwellTime"] = dataConfig.PreTestStruct.TC_DwellTime;
                dr["TempLimit"]    = dataConfig.PreTestStruct.TempLimit_Max + "~" + dataConfig.PreTestStruct.TempLimit_Min;
                dr["HT_RampUp"]    = dataConfig.PreTestStruct.HT_RampUp_Max + "~" + dataConfig.PreTestStruct.HT_RampUp_Min;
                dr["HT_DwellTime"] = dataConfig.PreTestStruct.HT_DwellTime_Max + "~" + dataConfig.PreTestStruct.HT_DwellTime_Min;
                dr["LT_RampUp"]    = dataConfig.PreTestStruct.LT_RampDown_Max + "~" + dataConfig.PreTestStruct.LT_RampDown_Min;
                dr["LT_DwellTime"] = dataConfig.PreTestStruct.LT_DwellTime_Max + "~" + dataConfig.PreTestStruct.LT_DwellTime_Min;

                dataTable.Rows.Add(dr);
            }
            return dataTable;
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
                dr["DUTName"]      = config.DUTName;
                dr["TC_TempRange"] = "";
                dr["TC_RampRate"]  = "";
                dr["TC_DwellTime"] = "";
                dr["TempLimit"]    = "";
                dr["HT_RampUp"]    = "";
                dr["HT_DwellTime"] = "";
                dr["LT_RampUp"]    = "";
                dr["LT_DwellTime"] = "";

                _dataTable.Rows.Add(dr);
            }
        }

        private void SaveInitial(string filePath, DataTable dataTable)
        {
            SaveFile(filePath, dataTable);
        }
    }
}
