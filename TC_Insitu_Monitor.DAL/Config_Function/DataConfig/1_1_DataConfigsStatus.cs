using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class Statuses
    {       
        readonly private DataConfigs dataConfigs;
        readonly private List<DataConfigsStatus> _dataConfigsStatuses = new List<DataConfigsStatus>();     
        private DataConfigsStatus[] dataConfigsStatuses;        
        private readonly object balanceLockScan = new object();
        private readonly object balanceLockSearch = new object();     
        private string _failure="";
        public Statuses(string filePath)
        {            
            dataConfigs = new DataConfigs(filePath);           
            foreach (var config in dataConfigs.Configs)
            {
                DataConfigsStatus dataConfigsStatus = new DataConfigsStatus(config);
                _dataConfigsStatuses.Add(dataConfigsStatus);
            }
            dataConfigsStatuses = _dataConfigsStatuses.ToArray();
        }
        //Temp
        public bool isFailed;
        //Only輸出
        public string LastCOMport{ get {
            return dataConfigsStatuses[dataConfigsStatuses.Length - 1].Configs.COMPort; } }        
        public string Failure { get {
                UpdateFailure();
                return _failure; } }      
        public List<DataConfigsStatus> DataConfigsStatuses { get { 
                return _dataConfigsStatuses; } }
        //公用方法
        public DataConfigsStatus SearchDataConfigsStatus(string source)
        {
            DataConfigsStatus output = default;
            lock(balanceLockSearch)
            {
                dataConfigsStatuses = _dataConfigsStatuses.ToArray();
                for (int count = 0; count < _dataConfigsStatuses.Count; count++)
                {
                    if (dataConfigsStatuses[count].Configs.ID == source)
                    {
                        output = dataConfigsStatuses[count];
                        break;
                    }
                }
            }           
            return output;
        }
        public void ScanDataConfigsStatus(string source, DataConfigsStatus input)
        {
            lock(balanceLockScan)
            {
                dataConfigsStatuses = _dataConfigsStatuses.ToArray();
                for (int count = 0; count < _dataConfigsStatuses.Count; count++)
                {
                    if (dataConfigsStatuses[count].Configs.ID == source)
                    {
                        dataConfigsStatuses[count] = input;
                        break;
                    }
                }
            }           
        }
        //私用方法 
        private void UpdateFailure()
        {
            _failure = "";
            //1.收尋dataConfigsStatuses 累積Failure
            foreach (var _dataConfigsStatus in _dataConfigsStatuses)
            {
                if(_dataConfigsStatus.JudgementStruct.FailCause!=null)
                {
                    _failure += _dataConfigsStatus.JudgementStruct.FailCause + Environment.NewLine;
                }               
            }            
        }        
    }

    public class DataConfigsStatus
    {
        public ConfigStruct Configs = new ConfigStruct();        
        public long TimeStampStart = 0;
        public int FailureCount = 0;
        //DUTName,Impedance,Temperature,TimeStamp,Status,Cycle,FailCause
        public DataFormatStruct DataFormatStruct = new DataFormatStruct();
        public JudgementStruct JudgementStruct = new JudgementStruct();
        public FormalTestStruct FormalTestStruct = new FormalTestStruct();
        public PreTestStruct PreTestStruct = new PreTestStruct();

        public DataConfigsStatus(ConfigStruct configs)
        {
            Configs = configs;
        }
    }
}
