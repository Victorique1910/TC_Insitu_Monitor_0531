using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;
using TC_Insitu_Monitor.DAL;
using System.IO;
using System.Data;

namespace TC_Insitu_Monitor.BLL
{
    public class Initial
    {
        public readonly string _folderName = Path.Combine(System.Environment.CurrentDirectory, "Configs");
        public readonly string _preTestFolderName = Path.Combine(System.Environment.CurrentDirectory, "Pre-Test");
        public Initial()
        {

        }
        public Initial(string folderName, string preTestFolderName)
        {
            _folderName = folderName;
            _preTestFolderName = preTestFolderName;
        }     
        public List<SaveFormalLogStruct> SaveFormalLog
        {
            get
            {
                SaveConfig saveConfig = new SaveConfig(_preTestFolderName);
                return saveConfig.SaveFormalLog;
            }
        }
        public Dictionary<string, List<ConfigStruct>> ConfigsDictionary
        {
            get
            {
                DataConfigsDictionary dataConfigsDictionary = new DataConfigsDictionary(Path.Combine(_folderName, "DataConfigs.csv"));
                return dataConfigsDictionary.ConfigsDictionary;
            }
        }
        public Dictionary<string, List<ConfigStruct>> BoardDictionary
        {
            get
            {
                DataConfigsDictionary dataConfigsDictionary = new DataConfigsDictionary(Path.Combine(_folderName, "DataConfigs.csv"));
                return dataConfigsDictionary.BoardDictionary;
            }
        }
        public Dictionary<string, List<ConfigStruct>> ChainDictionary
        {
            get
            {
                DataConfigsDictionary dataConfigsDictionary = new DataConfigsDictionary(Path.Combine(_folderName, "DataConfigs.csv"));
                return dataConfigsDictionary.ChainDictionary;
            }
        }
        public Dictionary<string, List<ConfigStruct>> PortDictionary
        {
            get
            {
                DataConfigsDictionary dataConfigsDictionary = new DataConfigsDictionary(Path.Combine(_folderName, "DataConfigs.csv"));
                return dataConfigsDictionary.PortDictionary;
            }
        }
        public Statuses Statuses
        {
            get
            {
                Statuses statuses = new Statuses(Path.Combine(_folderName, "DataConfigs.csv"));
                Console.WriteLine("Read"+ statuses.DataConfigsStatuses[0].Configs.CompensationADCH);
                return statuses;
            }
        }      
        public CalConfigsStruct CalConfigsStruct
        {
            get
            {
                CalConfigs calConfigs = new CalConfigs(Path.Combine(_folderName, "CalConfigs.ini"));
                return calConfigs.CalConfigsStruct;
            }
        }
        public EmailConfigStruct EmailConfigStruct
        {
            get
            {
                EmailConfigs emailConfigs = new EmailConfigs(Path.Combine(_folderName, "EmailConfigs.ini"));
                return emailConfigs.EmailConfigStruct;
            }
        }
        public StopCriteria StopCriteria
        {
            get 
            {
                StopCriteriaConfigs stopCriteriaConfigs = new StopCriteriaConfigs(Path.Combine(_folderName, "StopCriteriaConfigs.ini"));
                return stopCriteriaConfigs.StopCriteria;
            }
        }
        public SampleRate SampleRate
        {
            get 
            {
                SampleRateConfigs sampleRateConfigs = new SampleRateConfigs(Path.Combine(_folderName, "SampleRateConfigs.ini"));
                return sampleRateConfigs.SampleRate;
            }
        }
        public CorrespondentTemperature CorrespondentTemperature
        {
            get
            {
                CorrespondentTemperatureConfigs correspondentTemperatureConfigs = new CorrespondentTemperatureConfigs(Path.Combine(_folderName, "CorrespondentTemperatureConfigs.ini"));
                return correspondentTemperatureConfigs.CorrespondentTemperature;
            }
        }
        public ConditionTC ConditionTC
        {
            get
            {
                ConditionTCConfigs conditionTCConfigs = new ConditionTCConfigs(Path.Combine(_folderName, "ConditionTCConfigs.ini"));
                return conditionTCConfigs.ConditionTC;
            }
        }
        public Chamber Chamber
        {
            get
            {
                ChamberConfigs chamberConfigs = new ChamberConfigs(Path.Combine(_folderName, "ChamberConfigs.ini"));
                return chamberConfigs.Chamber;
            }
        }
        public FailCriteria FailCriteria
        {
            get
            {
                FailCriteriaConfigs failCriteriaConfigs = new FailCriteriaConfigs(Path.Combine(_folderName, "FailCriteriaConfigs.ini"));
                return failCriteriaConfigs.FailCriteria;
            }
        }
        #region 匯入之前的圖
        public DataTable FormalTestDataTable
        {
            get
            {
                DataConfigsFormalTestDataTable dataConfigsDataTable = new DataConfigsFormalTestDataTable(Path.Combine(_folderName, "DataConfigs.csv"));
                return dataConfigsDataTable.DataTable;
            }
        }      
        public DataTable PreTestDataTable
        {
            get
            {
                DataConfigsPreTestDataTable dataConfigsPreTestDataTable = new DataConfigsPreTestDataTable(Path.Combine(_folderName, "DataConfigsPreTest.csv"));
                return dataConfigsPreTestDataTable.DataTable;
            }
        }
        #endregion
        public void Save(EmailConfigStruct emailConfigStruct)
        {
            new EmailConfigs(Path.Combine(_folderName, "EmailConfigs.ini")) 
            {
                EmailConfigStruct = emailConfigStruct
            };           
        }        
        public void Save(StopCriteria stopCriteria)
        {
            new StopCriteriaConfigs(Path.Combine(_folderName, "StopCriteriaConfigs.ini")) 
            {
                StopCriteria = stopCriteria
            };            
        }
        public void Save(SampleRate sampleRate)
        {
            new SampleRateConfigs(Path.Combine(_folderName, "SampleRateConfigs.ini"))
            {
                SampleRate = sampleRate
            };           
        }
        public void Save(CorrespondentTemperature correspondentTemperature)
        {
            new CorrespondentTemperatureConfigs(Path.Combine(_folderName, "CorrespondentTemperatureConfigs.ini"))
            {
                CorrespondentTemperature = correspondentTemperature
            };            
        }
        public void Save(ConditionTC conditionTC)
        {
            new ConditionTCConfigs(Path.Combine(_folderName, "ConditionTCConfigs.ini"))
            {
                ConditionTC = conditionTC
            };
        }
        public void Save(Chamber chamber) 
        {
            new ChamberConfigs(Path.Combine(_folderName, "ChamberConfigs.ini")) 
            {
                Chamber = chamber
            };           
        }
        public void Save(CalConfigsStruct calConfigsStruct)
        {
            new CalConfigs(Path.Combine(_folderName, "CalConfigs.ini"))
            {
                CalConfigsStruct = calConfigsStruct
            };
        }
        public void Save(FailCriteria failCriteria)
        {
            new FailCriteriaConfigs(Path.Combine(_folderName, "FailCriteriaConfigs.ini"))
            {
                FailCriteria = failCriteria
            };
        }
        #region 存入之前的圖
        public void SaveFormalTest(DataTable dataTable)
        {
            Console.WriteLine("Save "+dataTable.Rows[0][14].ToString());
            new DataConfigsFormalTestDataTable(Path.Combine(_folderName, "DataConfigs.csv"))
            {
                DataTable = dataTable
            };            
        }       
        public void SavePreTest(DataTable dataTable)
        {
            new DataConfigsPreTestDataTable(Path.Combine(_folderName, "DataConfigsPreTest.csv"))
            {
                DataTable = dataTable
            };
        }
        #endregion
    }
}
