using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class DoPreTest : DoFormal
    {
        public delegate void DisplayPreTest(DataTable dataTable);
        public DisplayPreTest displayPreTest;

        protected DataConfigsPreTestDataTable dataConfigsPreTestDataTable = new DataConfigsPreTestDataTable();

        public void DoPreTestPrintData(Statuses statuses, ConditionTC conditionTCConfigs, string folderPath, string newPolderPath, ConterEnum conterEnum)
        {
            Task.Factory.StartNew(() => {
                if (statuses != null)
                {
                    //Compute statuses
                    statuses = Compute(statuses, conditionTCConfigs, folderPath);
                    //statuses轉換成datatable
                    displayPreTest(dataConfigsPreTestDataTable.Update(statuses.DataConfigsStatuses));
                    display(data.Update(statuses.DataConfigsStatuses));
                    CopyFolder(folderPath, newPolderPath);
                    TestEnd.DoTestEndReport(conterEnum);
                }
            });
        }

        //獲取溫度/阻抗 最大/最小/平均
        private Statuses Compute(Statuses statuses, ConditionTC conditionTCConfigs, string folderPath)
        {
            //讀檔            
            SaveConfig saveConfig = new SaveConfig(folderPath);                                                        //讀檔Function     
            List<SaveFormalLogStruct> save = saveConfig.SaveFormalLog;                                                 //整體SaveFormalLogStruct
           
            //分組DUTName
            var temps = save.GroupBy((y) => {
                return y.ID;
            });
            //轉換 saveDictionary
            Dictionary<string, List<SaveFormalLogStruct>> saveDictionarys = new Dictionary<string, List<SaveFormalLogStruct>>(); //以DUTName分組
            foreach (var temp in temps)
            {                
                List<SaveFormalLogStruct> tempConfig = new List<SaveFormalLogStruct>();
                foreach (var value in temp)
                {
                    tempConfig.Add(value);
                }                
                saveDictionarys.Add(temp.Key, tempConfig);
            }
            //輸入saveDictionarys
            foreach (var saveDictionary in saveDictionarys)
            {
                DataConfigsStatus data = statuses.SearchDataConfigsStatus(saveDictionary.Key);

                data.PreTestStruct.TempLimit_Max = saveConfig.SaveConfigFunction.TempLimit_Max(saveDictionary.Value);
                data.PreTestStruct.TempLimit_Min = saveConfig.SaveConfigFunction.TempLimit_Min(saveDictionary.Value);
                data.PreTestStruct.HT_DwellTime_Max = saveConfig.SaveConfigFunction.HT_DwellTime_Max(saveDictionary.Value);
                data.PreTestStruct.HT_DwellTime_Min = saveConfig.SaveConfigFunction.HT_DwellTime_Min(saveDictionary.Value);
                data.PreTestStruct.HT_RampUp_Max = saveConfig.SaveConfigFunction.HT_RampUp_Max(saveDictionary.Value);
                data.PreTestStruct.HT_RampUp_Min = saveConfig.SaveConfigFunction.HT_RampUp_Min(saveDictionary.Value);
                data.PreTestStruct.LT_DwellTime_Max = saveConfig.SaveConfigFunction.LT_DwellTime_Max(saveDictionary.Value);
                data.PreTestStruct.LT_DwellTime_Min = saveConfig.SaveConfigFunction.LT_DwellTime_Min(saveDictionary.Value);
                data.PreTestStruct.LT_RampDown_Max = saveConfig.SaveConfigFunction.LT_RampDown_Max(saveDictionary.Value);
                data.PreTestStruct.LT_RampDown_Min = saveConfig.SaveConfigFunction.LT_RampDown_Min(saveDictionary.Value);
                data.PreTestStruct.TC_TempRange_Max = conditionTCConfigs.DwellTimeH;
                data.PreTestStruct.TC_TempRange_Min = conditionTCConfigs.DwellTimeL;
                data.PreTestStruct.TC_RampRate = conditionTCConfigs.RampUp;
                data.PreTestStruct.TC_DwellTime = conditionTCConfigs.DwellTimeH;

                statuses.ScanDataConfigsStatus(saveDictionary.Key, data);
            }
            return statuses;
        }
    }
}
