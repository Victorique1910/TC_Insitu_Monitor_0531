using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.Model
{   
    public struct DataFormatStruct
    {
        public string ID;
        public string DUTName;       
        public double Impedance;          //阻抗
        public double Temperature;        //溫度
        public double VoltageH;           //電壓高
        public double VoltageL;           //電壓低       
    }

    public struct JudgementStruct
    {
        public string TimeStamp;          //時間 1Hz
        public double TimeTicks;
        public string Status;             //LowTemp HighTemp RampUP RampDown
        public int TemperatureCycle;      //Cycle數
        public string JudgementLowTemp;
        public string JudgementHighTemp;
        public string JudgementRampUp;
        public string JudgementRampDown;
        public int JudgementCycles;
        public string FailCause;          //1.使用者設定cycle數到 2.63%Fail 3.all Fail
    }

    public struct ConfigStruct
    {
        public byte Board;                // 02
        public string COMPort;            // COM11
        public int Port;                  // 1
        public string ID;                 // AAAAA
        public string Type;               // Temperature

        public string DUTName;            // Name
        public string Chain;              // 
        public int Channel;               //
        public string TemperatureName;
        public string TemperatureType;
        public string Location;

        public FailCriteria FailCriteria; //

        public double CompensationADCH;   //ADC量測電壓
        public double CompensationADCL;   //ADC量測電壓
        public double Loss;               //損耗        
    }

    public struct CommendStruct
    {
        public byte Board;
        public double ADC1;
        public double ADC2;
        public double ADC3;
        public double ADC4;
        public double ADC5;
        public double ADC6;
        public double ADC7;
        public double ADC8;
        public double ADC9;
        public double ADC10;
        public double ADC11;
        public double ADC12;
        public double ADC13;
        public double ADC14;
    }

    public struct USBStruct
    {
        public string USBComport;
        public double ADC1;
        public double ADC2;
        public double ADC3;
        public double ADC4;
        public double ADC5;
        public double ADC6;
        public double ADC7;
        public double ADC8;
        public double ADC9;
        public double ADC10;
        public double ADC11;
        public double ADC12;
        public double ADC13;
        public double ADC14;       
    }
}