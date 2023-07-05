using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.Model
{
    public struct ConditionTC
    {
        public double LowTempThreshold;
        public double DwellTimeL;
        public double RampDown;
        public double HighTempThreshold;
        public double DwellTimeH;
        public double RampUp;
        public int OffsetCycle;
        public int CalculateCycle;
        public int TestCycle;
    }
    
    public struct FailCriteria
    {
        public bool IsCriteria;         //Setting Criteria
        public bool IsDPAT;             //DPAT
        public string FailCondition;    //Absolute / Relative
        public string Method;           //Above / below / between
        public double Precentage;       //10%
        public double ValuesMax;        //12
        public double ValuesMin;        //6
        public string Base;             //Max
        public int ContinuousCount;     //5
    }

    public struct SampleRate
    {
        public double InitialSampleRate; //0.02
        public double FailSampleRate;    //1
        public int SaveLogBeforeFail;    //6
        public int SaveLogAfterFail;     //24
    }

    public struct Chamber
    {
        public string ChamberID;         //xxx.xx.xx
    }

    public struct StopCriteria
    {
        public bool IsCriteria;         //Fail DUT Qty
        public bool IsCycles;           //Cycles
        public int FailDUTQty;          //45      
    }

    public struct CorrespondentTemperature
    {
        public string Channel;
    }

    public struct EmailConfigStruct
    {
        public string NotifyCondition;
        public string SMTPServer;
        public int PortNumber;
        public string ID;
        public string EmailFrom;    
        public string Password;
        public string Sender;
        public bool UsingTLS;
        public string ProjectName;
        public string Body;
        public List<string> MailList;
        public string FileName;
    }

    public struct CalConfigsStruct
    {
        public double IdelImpedance;       
        public double IdelTemperature;
    }
}
