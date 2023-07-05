using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataSummarize
    {
        private readonly DataConfigsStatus _dataConfigsStatus;
        public DataConfigsStatus DataConfigsStatus { get { return _dataConfigsStatus; } }
        private const double coldTemp = 20;
        private readonly bool isWrite = true;

        public DataSummarize(DataConfigsStatus dataConfigsStatus, DataConfigsStatus dataConfigsStatusTemperature, ConterEnum conterEnum, List<SaveFormalLogStruct> saveFormalLog, ConditionTC conditionTC, CorrespondentTemperature correspondentTemperature)
        {
            double TempertureRange = conditionTC.HighTempThreshold - conditionTC.LowTempThreshold;
            SaveConfigFunction saveConfigFunction = new SaveConfigFunction();

            #region DUTName/ID(DataFormatStruct)
            string dutName = dataConfigsStatus.DataFormatStruct.DUTName;
            string id = dataConfigsStatus.DataFormatStruct.ID;
            #endregion
            #region Impedance/Temperature(DataFormatStruct)
            double impedance = dataConfigsStatus.DataFormatStruct.Impedance;
            double temperature = dataConfigsStatusTemperature.DataFormatStruct.Temperature;
            #endregion
            #region VoltageH/VoltageL(DataFormatStruct)
            double voltageH = dataConfigsStatus.DataFormatStruct.VoltageH;
            double voltageL = dataConfigsStatus.DataFormatStruct.VoltageL;
            #endregion
            #region TimeStamp/TimeTicks(JudgementStruct)
            string timeStamp = DateTime.Now.ToString();
            long timeTicks = DateTime.Now.Ticks;
            #endregion
            #region
            int tmperatureCycle = dataConfigsStatus.JudgementStruct.TemperatureCycle;
            double mainTemperature = coldTemp;
            if (conterEnum == ConterEnum.Formal)
            {
                if (id == correspondentTemperature.Channel)
                {
                    mainTemperature = temperature;
                }
            }
            else
            {
                mainTemperature = temperature;
            }
            #endregion
            #region Status(JudgementStruct)
            string status = dataConfigsStatus.JudgementStruct.Status;
            string failCause = dataConfigsStatus.JudgementStruct.FailCause??"";
            double timeStampStart = dataConfigsStatus.TimeStampStart;
            double Time = DateTime.Now.Ticks / 600000000 - dataConfigsStatus.TimeStampStart;            
            //DWEL_L: 溫度低於標準電壓
            if (mainTemperature < conditionTC.LowTempThreshold)
            {
                //DWEL_L: 第一次修改Status為DWEL_L(紀錄Status/TimeStamp)
                if (status == "NA_H2" || status == "")
                {
                    status = "DWEL_L";
                    timeStampStart = timeTicks / 600000000;
                }
            }
            //DWEL_H: 溫度高於標準電壓
            else if (mainTemperature > conditionTC.HighTempThreshold)
            {
                ///DWEL_H: 第一次修改Status為DWEL_H(紀錄Status/TimeStamp)
                if (status == "NA_L2")
                {
                    status = "DWEL_H";
                    timeStampStart = timeTicks / 600000000;
                }
            }
            //RampUP && RampDown
            else if (mainTemperature > conditionTC.LowTempThreshold + 0.1 * TempertureRange
                  && mainTemperature < conditionTC.HighTempThreshold - 0.1 * TempertureRange)
            {
                //****************************************************
                //RampUP
                //RampUP: 第一次修改Status為RampUP(紀錄Status/TimeStamp)
                if (status == "NA_L1")
                {
                    status = "RampUP";
                    timeStampStart = timeTicks / 600000000;
                }
                //*************************************************************
                //RampDown
                //RampDown: 第一次修改Status為RampDown(紀錄Status/TimeStamp)
                else if (status == "NA_H1" || status == "")
                {
                    status = "RampDown";
                    timeStampStart = timeTicks / 600000000;
                }
            }
            else
            {
                //其他
                if (status == "DWEL_L")
                {
                    //實際時間低於設定(紀錄Fail)
                    if (Time < conditionTC.DwellTimeL)
                    {
                        if (timeStampStart > 0)
                        {
                            failCause = "Fail DWEL_L too short " + Time;
                        }
                    }
                    status = "NA_L1";
                    timeStampStart = timeTicks / 600000000;
                }
                else if (status == "RampUP")
                {
                    status = "NA_L2";
                    timeStampStart = timeTicks / 600000000;
                }
                else if (status == "DWEL_H")
                {
                    //實際時間低於設定(紀錄Fail)
                    if (Time < conditionTC.DwellTimeH)
                    {
                        if (timeStampStart > 0)
                        {
                            failCause = "Fail DWEL_H too short " + Time;
                        }
                    }
                    status = "NA_H1";
                    timeStampStart = timeTicks / 600000000;
                }
                else if (status == "RampDown")
                {
                    status = "NA_H2";
                    timeStampStart = timeTicks / 600000000;
                }
            }
            #endregion           
            #region TemperatureCycle(JudgementStruct)            
            //1.計算_temperatureCycle  條件: RampDown/低於溫度初始/isWrite
            if (status == "RampDown" && mainTemperature < coldTemp && isWrite)
            {
                tmperatureCycle++;
                isWrite = false;
            }
            //2.恢復isWrite 條件:高於溫度初始
            if (mainTemperature >= coldTemp)
            {
                isWrite = true;
            }
            #endregion
            #region Judgement/FailCause(JudgementStruct)
            bool isCriteria = dataConfigsStatus.Configs.FailCriteria.IsCriteria;
            string failCondition = dataConfigsStatus.Configs.FailCriteria.FailCondition;
            string method = dataConfigsStatus.Configs.FailCriteria.Method;
            int failureCount = dataConfigsStatus.FailureCount;
            double precentage = dataConfigsStatus.Configs.FailCriteria.Precentage;
           
            if (isCriteria)
            {
                if (failCondition == "Absolute")
                {
                    if (method == "Above")
                    {
                        #region Above
                        if (impedance < dataConfigsStatus.Configs.FailCriteria.ValuesMin)
                        {
                            dataConfigsStatus = Min(dataConfigsStatus, failureCount);
                        }
                        else if (double.IsNaN(impedance))
                        {
                            dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                        #endregion
                    }
                    else if (dataConfigsStatus.Configs.FailCriteria.Method == "Below")
                    {
                        #region Below
                        if (impedance > dataConfigsStatus.Configs.FailCriteria.ValuesMax)
                        {
                            dataConfigsStatus = Max(dataConfigsStatus, failureCount);
                        }
                        else if (double.IsNaN(impedance))
                        {
                            dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                        #endregion
                    }
                    else if (dataConfigsStatus.Configs.FailCriteria.Method == "Between")
                    {
                        #region Between
                        if (impedance > dataConfigsStatus.Configs.FailCriteria.ValuesMax)
                        {
                            dataConfigsStatus = Max(dataConfigsStatus, failureCount);
                        }
                        else if (impedance < dataConfigsStatus.Configs.FailCriteria.ValuesMin)
                        {
                            dataConfigsStatus = Min(dataConfigsStatus, failureCount);
                        }
                        else if (double.IsNaN(impedance))
                        {
                            dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region None
                        dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        #endregion
                    }
                }
                else if (dataConfigsStatus.Configs.FailCriteria.FailCondition == "Relative")
                {
                    if (dataConfigsStatus.Configs.FailCriteria.Base == "Max")
                    {
                        double relativeMax = saveConfigFunction.TempTransImp_Max(saveFormalLog, temperature);
                        if (impedance > relativeMax * (1 + 0.01 * precentage))
                        {
                            dataConfigsStatus = Max(dataConfigsStatus, failureCount);
                        }
                        else if (impedance < relativeMax * (1 - 0.01 * precentage))
                        {
                            dataConfigsStatus = Min(dataConfigsStatus, failureCount);
                        }
                        else if (double.IsNaN(impedance))
                        {
                            dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                    }
                    else if (dataConfigsStatus.Configs.FailCriteria.Base == "Min")
                    {
                        double relativeMin = saveConfigFunction.TempTransImp_Min(saveFormalLog, temperature);
                        if (impedance > relativeMin * (1 + 0.01 * precentage))
                        {
                            dataConfigsStatus = Max(dataConfigsStatus, failureCount);
                        }
                        else if (impedance < relativeMin * (1 - 0.01 * precentage))
                        {
                            dataConfigsStatus = Min(dataConfigsStatus, failureCount);
                        }
                        else if (double.IsNaN(impedance))
                        {
                            dataConfigsStatus = None(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                    }
                    else if (dataConfigsStatus.Configs.FailCriteria.Base == "Average")
                    {
                        double relativeAverage = saveConfigFunction.TempTransImp_Average(saveFormalLog, temperature);
                        if (impedance > relativeAverage * (1 + 0.01 * precentage))
                        {
                            dataConfigsStatus = Max(dataConfigsStatus, failureCount);
                        }
                        else if (impedance < relativeAverage * (1 - 0.01 * precentage))
                        {
                            dataConfigsStatus = Min(dataConfigsStatus, failureCount);
                        }
                        else
                        {
                            if (failureCount < dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
                            {
                                dataConfigsStatus.FailureCount = 0;
                            }
                        }
                    }
                }
            }
            else if (dataConfigsStatus.Configs.FailCriteria.IsDPAT)
            {
                //暫定無
            }
            #endregion
            #region 儲存資料
            DataFormatStruct dataForm = new DataFormatStruct()
            {
                DUTName = dutName,
                ID = id,
                Impedance = impedance,
                Temperature = temperature,
                VoltageH = voltageH,
                VoltageL = voltageL
            };
            JudgementStruct judgement = new JudgementStruct()
            {
                TimeStamp = timeStamp,
                TimeTicks = timeTicks,
                Status = status,
                TemperatureCycle = tmperatureCycle,
                JudgementHighTemp = "",
                JudgementLowTemp = "",
                JudgementRampUp = "",
                JudgementRampDown = "",
                JudgementCycles = 0,
                FailCause = failCause
            };
            dataConfigsStatus.DataFormatStruct = dataForm;
            dataConfigsStatus.JudgementStruct = judgement;
            dataConfigsStatus.TimeStampStart = (long)timeStampStart;
            #endregion            
            #region 儲存狀態
            _dataConfigsStatus = dataConfigsStatus;
            #endregion
        }

        private DataConfigsStatus Max(DataConfigsStatus dataConfigsStatus, int failureCount)
        {
            dataConfigsStatus.FailureCount = failureCount + 1;
            if (failureCount + 1 >= dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
            {
                //FailCause
                //dataConfigsStatus.JudgementStruct.FailCause = beforeFailure + dataConfigsStatus.Configs.DUTName + " ImpedanceLimitHigh " + failureCount;
                dataConfigsStatus.JudgementStruct.FailCause = dataConfigsStatus.Configs.DUTName + " ImpedanceLimitHigh " + failureCount;
                //JudgementCycles
                dataConfigsStatus.JudgementStruct.JudgementCycles = dataConfigsStatus.JudgementStruct.TemperatureCycle;
                //Judgement
                switch (dataConfigsStatus.JudgementStruct.Status)
                {
                    case "DWEL_L":
                        dataConfigsStatus.JudgementStruct.JudgementLowTemp = "Failed";
                        break;
                    case "DWEL_H":
                        dataConfigsStatus.JudgementStruct.JudgementHighTemp = "Failed";
                        break;
                    case "RampUP":
                        dataConfigsStatus.JudgementStruct.JudgementRampUp = "Failed";
                        break;
                    case "RampDown":
                        dataConfigsStatus.JudgementStruct.JudgementRampDown = "Failed";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(dataConfigsStatus.Configs.DUTName + " ImpedanceLimitHigh ");
            }
            return dataConfigsStatus;
        }
        private DataConfigsStatus Min(DataConfigsStatus dataConfigsStatus, int failureCount)
        {
            dataConfigsStatus.FailureCount = failureCount + 1;
            if (failureCount + 1 >= dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
            {
                //FailCause
                //dataConfigsStatus.JudgementStruct.FailCause = beforeFailure + dataConfigsStatus.Configs.DUTName + " ImpedanceLimitLow " + failureCount;
                dataConfigsStatus.JudgementStruct.FailCause = dataConfigsStatus.Configs.DUTName + " ImpedanceLimitLow " + failureCount;
                //JudgementCycles
                dataConfigsStatus.JudgementStruct.JudgementCycles = dataConfigsStatus.JudgementStruct.TemperatureCycle;
                //Judgement
                switch (dataConfigsStatus.JudgementStruct.Status)
                {
                    case "DWEL_L":
                        dataConfigsStatus.JudgementStruct.JudgementLowTemp = "Failed";
                        break;
                    case "DWEL_H":
                        dataConfigsStatus.JudgementStruct.JudgementHighTemp = "Failed";
                        break;
                    case "RampUP":
                        dataConfigsStatus.JudgementStruct.JudgementRampUp = "Failed";
                        break;
                    case "RampDown":
                        dataConfigsStatus.JudgementStruct.JudgementRampDown = "Failed";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(dataConfigsStatus.Configs.DUTName + " ImpedanceLimitLow ");
            }
            return dataConfigsStatus;
        }
        private DataConfigsStatus None(DataConfigsStatus dataConfigsStatus, int failureCount)
        {
            dataConfigsStatus.FailureCount = failureCount + 1;
            if (failureCount + 1 >= dataConfigsStatus.Configs.FailCriteria.ContinuousCount)
            {
                //FailCause
                //dataConfigsStatus.JudgementStruct.FailCause = beforeFailure + dataConfigsStatus.Configs.DUTName + " ImpedanceLimitGG " + failureCount;
                dataConfigsStatus.JudgementStruct.FailCause = dataConfigsStatus.Configs.DUTName + " ImpedanceLimitGG " + failureCount;
                //JudgementCycles
                dataConfigsStatus.JudgementStruct.JudgementCycles = dataConfigsStatus.JudgementStruct.TemperatureCycle;
                //Judgement
                switch (dataConfigsStatus.JudgementStruct.Status)
                {
                    case "DWEL_L":
                        dataConfigsStatus.JudgementStruct.JudgementLowTemp = "Failed";
                        break;
                    case "DWEL_H":
                        dataConfigsStatus.JudgementStruct.JudgementHighTemp = "Failed";
                        break;
                    case "RampUP":
                        dataConfigsStatus.JudgementStruct.JudgementRampUp = "Failed";
                        break;
                    case "RampDown":
                        dataConfigsStatus.JudgementStruct.JudgementRampDown = "Failed";
                        break;
                    default:
                        break;
                }
                Console.WriteLine(dataConfigsStatus.Configs.DUTName + " ImpedanceLimitGG ");
            }
            return dataConfigsStatus;
        }
    }
}
