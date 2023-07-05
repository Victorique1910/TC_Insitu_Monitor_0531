using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class Counter
    {
        private int _calCount = 0;
        //******************************************************************
        private bool isFirst = true;
        private bool isFirstStart;
        private bool isMeetFail;
        //private Chamber chamberConfigs;
        private ConditionTC conditionTCConfigs;
        private CorrespondentTemperature correspondentTemperatureConfigs;
        private SampleRate sampleRateConfigs;
        private StopCriteria stopCriteriaConfigs;
        private EmailConfigStruct emailConfigStruct;
        private CalConfigsStruct calConfigsStruct;
        private Dictionary<string, List<ConfigStruct>> configsDictionary;
        private List<SaveFormalLogStruct> saveFormalLog;
       
        private Statuses statuses;        
        private ConterEnum _conterState = new ConterEnum();

        private readonly List<USB_Interface> usbInterfaces;
        private readonly List<DataConfigsStatus> totalStruct = new List<DataConfigsStatus>();

        public DoTestStart doTestStart = new DoTestStart();
        public DoFormal doFormal = new DoFormal();
        public DoPreCheck doPreCheck = new DoPreCheck();
        public DoPreTest doPreTest = new DoPreTest();
        public DoNinePoint doNinePoint = new DoNinePoint();        
        public DoCalTest doCalTest = new DoCalTest();
        public DoLossTest doLossTest = new DoLossTest();

        private FileStream fs;
        private StreamWriter sw;
        private MailModel mailModel;       

        private readonly System.Timers.Timer _timerSend;
        private readonly System.Timers.Timer _timerUpdateUI;
        private readonly System.Timers.Timer _timerSaveLog;
        private readonly static object lockObject = new object();

        private event EventHandler Saved;
        private event EventHandler SavePreTested;
        private int Count = 0;

        public Counter()
        {
            usbInterfaces = new List<USB_Interface>();
            _timerSend = new System.Timers.Timer()
            {
                Interval = 1000,
                AutoReset = true
            };
            _timerSend.Elapsed += Send;
            _timerUpdateUI = new System.Timers.Timer()
            {
                Interval = 1100,
                AutoReset = true
            };
            _timerUpdateUI.Elapsed += UpdateUI;
            _timerSaveLog = new System.Timers.Timer()
            {
                Interval = 1000000,
                AutoReset = true
            };
            _timerSaveLog.Elapsed += SaveLog;
            Saved += SaveLog;
        }       

        private void SerialPortDataReceivedProcess(object sender, byte[] arrDataReceived)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine(HexConverter.ByteArrayToHex(arrDataReceived));                             
                //取得USB對象sender
                USB_Interface uSB_Interface = (USB_Interface)sender;
                if (configsDictionary != null)
                {
                    foreach (var configs in configsDictionary)
                    {
                        //從configsDictionary中獲取Comport內容
                        if (configs.Key == uSB_Interface.Comport)
                        {
                            //1.轉換指令
                            CommendStruct commendOut = CommendParsing.ParsingByteToDataFormat(arrDataReceived);
                            CommendStruct commendOut1 = CommendParsing.ParsingByteToDataFormat(arrDataReceived,1);

                            //2.指令轉換數值
                            TempImpParsing tempImpParsing = new TempImpParsing();
                            if(_conterState == ConterEnum.Formal)
                                tempImpParsing.Parsing(configs.Value, statuses, commendOut,commendOut1,true);
                            else
                                tempImpParsing.Parsing(configs.Value, statuses, commendOut, commendOut1,false);

                            USBStruct newImpStruct = tempImpParsing.ImpOutput;
                            USBStruct newTempStruct = tempImpParsing.TempOutput;
                            CommendStruct commendTempBefore = tempImpParsing.CommendTempBefore;

                            //3.解析數值放進空間(Temperature/Impedance)
                            if (tempImpParsing.Type.Contains("Temperature"))
                            {
                                //*************************************************************************
                                //傳入資料設定黨(configs.Value)/解析後數值(newTempStruct)/狀態(statuses))/電壓(commendOut)
                                //回傳狀態(statuses)
                                DataTempParsing datatemp = new DataTempParsing(configs.Value, newTempStruct, statuses, commendOut, commendOut1, commendTempBefore);
                                statuses = datatemp.Statuses;                                
                            }
                            else if (tempImpParsing.Type.Contains("Impedance"))
                            {
                                //傳入資料設定黨(configs.Value)/解析後數值(newTempStruct)/狀態(statuses)/電壓(commendOut)
                                //回傳狀態(statuses)
                                DataImpParsing dataimp = new DataImpParsing(configs.Value, newImpStruct, statuses, commendOut,commendOut1);
                                statuses = dataimp.Statuses;                               
                            }

                            //4.判斷是否為最後一筆
                            if (configs.Key == statuses.LastCOMport)
                            {
                                statuses = DataSummarize(statuses);
                                string Failure = statuses.Failure;
                                switch (_conterState)
                                {
                                    case ConterEnum.Formal:
                                        if (statuses.isFailed)
                                        {
                                            Saved?.Invoke(this, new EventArgs());
                                            mailModel.SendEmail(Failure, _conterState);                                            
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.PreCheck:
                                        if (statuses.isFailed)
                                        {
                                            Saved?.Invoke(this, new EventArgs());
                                            //mailModel.SendEmail(Failure, _conterState);
                                            doPreCheck.DoPreCheckPrintData(statuses, _conterState);
                                            //顯示
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.PreTest:
                                        if (statuses.isFailed)
                                        {
                                            SavePreTested?.Invoke(this, new EventArgs());
                                            doPreTest.DoPreTestPrintData(statuses, conditionTCConfigs, Path.Combine(System.Environment.CurrentDirectory, "Pre-Test"), Path.Combine(System.Environment.CurrentDirectory, "Pre-temp"), _conterState);
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.NinePoint:
                                        if (statuses.isFailed)
                                        {
                                            SavePreTested?.Invoke(this, new EventArgs());
                                            doNinePoint.DoNinePointPrintData(statuses, conditionTCConfigs, Path.Combine(System.Environment.CurrentDirectory, "Pre-Test"), Path.Combine(System.Environment.CurrentDirectory, "Pre-temp"), _conterState);                                            
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.Cal:
                                        if (statuses.isFailed)
                                        {
                                            ///回傳
                                            CalParsing calParsing = new CalParsing();
                                            Saved?.Invoke(this, new EventArgs());
                                            //mailModel.SendEmDoCalPrintDataail(Failure, _conterState);
                                            doCalTest.DoCalPrintData(calParsing.Parsing(statuses, calConfigsStruct, _calCount), _calCount, _conterState);
                                            //doTestEnd.DoTestEndReport(_conterState);
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.Loss:
                                        if (statuses.isFailed)
                                        {
                                            Saved?.Invoke(this, new EventArgs());
                                            //mailModel.SendEmail(Failure, _conterState);
                                            doLossTest.DoLossPrintData(statuses, _conterState);
                                            Stop();
                                        }
                                        break;
                                    case ConterEnum.None:
                                        break;
                                    default:
                                        break;
                                }
                                
                                if (!statuses.isFailed && isMeetFail)
                                {
                                    if (Failure.Length > 0)
                                    {
                                        Console.WriteLine("isMeetFail");
                                        Saved?.Invoke(this, new EventArgs());
                                        //mailModel.SendEmail(Failure, _conterState);
                                        isMeetFail = false;
                                    }
                                }
                            }
                        }
                    }
                }
            });
        }

        private Statuses DataSummarize(Statuses statuses)
        {           
            Count++;
            bool isFailed = false;
            int FailDUTNumber = 0;
            List<string> DUTName = new List<string>();
            foreach (var dataConfigsStatuse in statuses.DataConfigsStatuses)
            {
                DataConfigsStatus dataConfigsStatus = statuses.SearchDataConfigsStatus(dataConfigsStatuse.Configs.ID);
                DataConfigsStatus dataConfigsStatusTemperature = statuses.SearchDataConfigsStatus(dataConfigsStatuse.Configs.TemperatureName);
                DataSummarize dataSummarize = new DataSummarize(dataConfigsStatus, dataConfigsStatusTemperature, _conterState, saveFormalLog, conditionTCConfigs, correspondentTemperatureConfigs);
                dataConfigsStatus = dataSummarize.DataConfigsStatus;
                
                totalStruct.Add(dataConfigsStatus);
                statuses.ScanDataConfigsStatus(dataConfigsStatuse.Configs.ID,dataConfigsStatus);

                if(!DUTName.Contains(dataConfigsStatus.Configs.DUTName))
                {                   
                    if (dataConfigsStatus.JudgementStruct.FailCause.Length > 0)
                    {
                        FailDUTNumber++;
                        DUTName.Add(dataConfigsStatus.Configs.DUTName);
                    }
                }

                //5.執行UI
                int TemperatureCycle = dataConfigsStatus.JudgementStruct.TemperatureCycle;                
                switch (_conterState)
                {
                    case ConterEnum.Formal:
                        #region Formal                          
                        string Failure = dataConfigsStatus.JudgementStruct.FailCause;
                        if (TemperatureCycle > conditionTCConfigs.OffsetCycle)
                        {
                            if (stopCriteriaConfigs.IsCycles)
                            {
                                if (TemperatureCycle > conditionTCConfigs.TestCycle)
                                {
                                    isFailed = true;
                                }
                            }
                            else if (stopCriteriaConfigs.IsCriteria)
                            {
                                if (FailDUTNumber >= stopCriteriaConfigs.FailDUTQty)
                                {
                                    isFailed = true;
                                }
                            }
                        }                        
                        #endregion
                        break;
                    case ConterEnum.PreCheck:
                        #region PreCheck
                        if (Count > 0)
                        {
                            isFailed = true;
                        }                        
                        #endregion
                        break;
                    case ConterEnum.PreTest:
                        #region PreTest                        
                        if (TemperatureCycle > conditionTCConfigs.OffsetCycle)
                        {
                            if (TemperatureCycle > conditionTCConfigs.CalculateCycle)
                            {
                                isFailed = true;
                            }
                            //*****************************************************************
                            if (Count > 0)
                            {
                                isFailed = true;
                            }
                            //******************************************************************                            
                        }
                        #endregion
                        break;
                    case ConterEnum.NinePoint:
                        #region NinePoint                      
                        if (TemperatureCycle > conditionTCConfigs.OffsetCycle)
                        {
                            if (TemperatureCycle > conditionTCConfigs.CalculateCycle)
                            {
                                isFailed = true;
                            }                            
                        }
                        #endregion
                        break;
                    case ConterEnum.Cal:
                        #region Cal
                        if (Count > 0)
                        {
                            isFailed = true;
                        }
                        #endregion
                        break;
                    case ConterEnum.Loss:
                        #region Loss
                        if (Count > 0)
                        {
                            isFailed = true;
                        }                       
                        #endregion
                        break;
                    case ConterEnum.None:
                        break;
                    default:
                        break;
                }
            }
            statuses.isFailed = isFailed;
            return statuses;
        }

        private void UpdateUI(object sender, EventArgs e)
        { 
            if(_conterState == ConterEnum.Formal)
            {
                doFormal.DoFormalDisplayData(statuses);
            }           
        }

        private void Send(object sender, EventArgs e)
        {                          
            try
            {
                foreach (var usbInterface in usbInterfaces)
                {
                    foreach (var configs in configsDictionary)
                    {
                        if (usbInterface.Comport == configs.Key)
                        {
                            if (isFirstStart)
                            {
                                usbInterface.WriteBytes(new byte[] { 0xA1, 0x02, 0x00, Convert.ToByte(configs.Value[0].Board), 0x0A });
                                usbInterface.WriteBytes(new byte[] { 0xA4, 0x00, 0x0A });
                            }
                            else
                            {
                                usbInterface.WriteBytes(new byte[] { 0xA4, 0x00, 0x0A });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _timerSend.Stop();
                Console.WriteLine("WriteBytes Wrong " + ex);
                foreach (var usbInterface in usbInterfaces)
                {
                    usbInterface.Close();
                    usbInterface.Open();
                }                   
                _timerSend.Start();
            }            
        }

        private void SaveLog(object sender, EventArgs e)
        {
            switch(_conterState)
            {
                case ConterEnum.Formal:
                    #region Formal
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Log"));                  
                    #endregion
                    break;
                case ConterEnum.PreCheck:
                    #region PreCheck
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Log"));                    
                    #endregion
                    break;
                case ConterEnum.PreTest:
                    #region PreTest
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Pre-Test"));
                    #endregion
                    break;
                case ConterEnum.NinePoint:
                    #region NinePoint
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Pre-Test"));
                    #endregion
                    break;
                case ConterEnum.Cal:
                    #region Cal
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Log"));
                    #endregion
                    break;
                case ConterEnum.Loss:
                    #region Loss
                    Save(Path.Combine(System.Environment.CurrentDirectory, "Log"));
                    #endregion
                    break;
                case ConterEnum.None:
                    break;
                default:
                    break;
            }           
        }

        private void Save(string folderName)
        {                   
            Task.Factory.StartNew(() => {
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
               
                if(totalStruct.Count>0)
                {
                    DataConfigsStatus[] totalFormatStruct;
                    lock (lockObject)
                    {
                        totalFormatStruct = totalStruct.ToArray();
                    }
                   
                    using (fs = new FileStream(Path.Combine(folderName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + "_Formallog" + ".csv"), System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                    {
                        using (sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine("ID,DUTName,Impedance,Temperature,Cycle,Status,TimeStamp,TimeTicks");
                            foreach (var DataFormatStruct in totalFormatStruct)
                            {
                                sw.WriteLine(DataFormatStruct.Configs.ID + ","
                                           + DataFormatStruct.Configs.DUTName + ","
                                           + DataFormatStruct.DataFormatStruct.Impedance + ","
                                           + DataFormatStruct.DataFormatStruct.Temperature + ","
                                           + DataFormatStruct.JudgementStruct.TemperatureCycle + ","
                                           + DataFormatStruct.JudgementStruct.Status + ","
                                           + DataFormatStruct.JudgementStruct.TimeStamp + ","
                                           + DataFormatStruct.JudgementStruct.TimeTicks);
                            }                            
                        }
                    }
                    totalStruct.Clear();
                    Console.WriteLine("SaveLog");
                }                
            });
        }

        private void Initialtest(Initial initial)
        {
            #region Configs
            calConfigsStruct = initial.CalConfigsStruct;
            //chamberConfigs = initial.Chamber;
            conditionTCConfigs = initial.ConditionTC;
            correspondentTemperatureConfigs = initial.CorrespondentTemperature;            
            sampleRateConfigs = initial.SampleRate;
            stopCriteriaConfigs = initial.StopCriteria;
            statuses = initial.Statuses;            
            configsDictionary = initial.ConfigsDictionary;
            emailConfigStruct = initial.EmailConfigStruct;
            saveFormalLog = initial.SaveFormalLog;
            mailModel = new MailModel(emailConfigStruct);
            #endregion
            #region isMeetFail
            isMeetFail = false;
            foreach (var temp in emailConfigStruct.NotifyCondition.Split(','))
            {
                if (temp.Contains("MeetFail"))
                {
                    isMeetFail = true;
                }
            }
            #endregion
            isFirstStart = true;            
        }

       
        public void Start(Initial initial, ConterEnum conterEnum, List<string> comports, int calCount)
        {
            if (isFirst)
            {
                foreach (string comport in comports)
                {
                    USB_Interface usbInterface = new USB_Interface(comport);
                    usbInterface.DataReceived += SerialPortDataReceivedProcess;
                    usbInterfaces.Add(usbInterface);                   
                }
                isFirst = false;
            }

            Initialtest(initial);
            //************************************
            _calCount = calCount;
            _timerSend.Interval = 1000;
            //***********************************
            _timerSend.Start();
            _timerUpdateUI.Start();
            _timerSaveLog.Start();
            _conterState = conterEnum;
            Console.WriteLine("Start");
        }

        public void Start(Initial initial, ConterEnum conterEnum, List<string> comports)
        {
            if(isFirst)
            {
                foreach (string comport in comports)
                {
                    USB_Interface usbInterface = new USB_Interface(comport);
                    usbInterface.DataReceived += SerialPortDataReceivedProcess;
                    usbInterfaces.Add(usbInterface);
                }
                isFirst = false;
            }
            
            Initialtest(initial);
            _timerSend.Interval = sampleRateConfigs.InitialSampleRate;
            _timerSend.Start();
            _timerUpdateUI.Start();
            _timerSaveLog.Start();
            _conterState = conterEnum;
            doTestStart.DoTestStartReport(_conterState, Path.Combine(System.Environment.CurrentDirectory, "Pre-Test"));
            Console.WriteLine("Start");
        }     
        
        public void Stop()
        {
            _timerSend.Stop();
            _timerUpdateUI.Stop();
            _timerSaveLog.Stop();            
        }
      
        public void Close()
        {
            _timerUpdateUI.Stop();            
            foreach (var usbInterface in usbInterfaces)
            {
                usbInterface.Close();
            }
        }
    }
}