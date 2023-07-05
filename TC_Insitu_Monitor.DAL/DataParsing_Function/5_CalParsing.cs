using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class CalParsing : TempImpParsingFunction
    { 
        public Statuses Parsing(Statuses statuses, CalConfigsStruct calConfigsStruct, int point)
        {            
            double temperature = 0;
            double impedance = 0;
            List<string> COMPort = new List<string>();
            foreach (var dataConfigsStatus in statuses.DataConfigsStatuses)
            {
                DataConfigsStatus temp = statuses.SearchDataConfigsStatus(dataConfigsStatus.Configs.ID);
                if (dataConfigsStatus.Configs.Type.Contains("Temperature"))
                {
                    if (!COMPort.Contains(dataConfigsStatus.Configs.COMPort))
                    {
                        Console.WriteLine($"CompensationADCH {dataConfigsStatus.Configs.Port} , {dataConfigsStatus.Configs.CompensationADCH}");
                        temperature = ComputeTemperature(temp.DataFormatStruct.VoltageH, dataConfigsStatus.Configs.CompensationADCH, calConfigsStruct, point);
                        COMPort.Add(dataConfigsStatus.Configs.COMPort);
                    }
                    temp.Configs.CompensationADCH = temperature;
                    temp.Configs.CompensationADCL = 0;
                }
                else if (dataConfigsStatus.Configs.Type.Contains("Impedance"))
                {
                    if (!COMPort.Contains(dataConfigsStatus.Configs.COMPort))
                    {
                        impedance = ComputeImpedance(temp.DataFormatStruct.VoltageH, temp.DataFormatStruct.VoltageL, dataConfigsStatus.Configs.CompensationADCL, calConfigsStruct, point);
                        COMPort.Add(dataConfigsStatus.Configs.COMPort);
                    }                    
                    temp.Configs.CompensationADCH = 0;
                    temp.Configs.CompensationADCL = impedance;
                }
                statuses.ScanDataConfigsStatus(dataConfigsStatus.Configs.ID,temp);
            }
            Console.WriteLine($"point {point}");
            return statuses;
        }
        /// <summary>
        /// 輸入commendOut(實際電壓),CompensationADCH(補償電壓),calConfigsStruct(使用者輸入值),point(第幾次)
        /// 利用commendOut實際電壓和CompensationADCH(補償電壓)-(+9~-9)*10^-point計算實際阻抗
        /// 比較離calConfigsStruct(使用者輸入值)最接近的值
        /// 回傳其補償值CompensationADCH(補償電壓)-(+9~-9)*10^-point計算實際阻抗 
        /// </summary>
        /// <param name="commendOut"></param>
        /// <param name="CompensationADCH"></param>
        /// <param name="calConfigsStruct"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private double ComputeTemperature(double commendOut, double CompensationADCH, CalConfigsStruct calConfigsStruct, int point)
        {
            double[,] temp = new double[2, 19];
            bool isFirst = true;
            double min = double.MaxValue;
            double output = double.MaxValue;            
            temp[0, 0] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 9 * Math.Pow(10, -point)));
            temp[0, 1] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 8 * Math.Pow(10, -point)));
            temp[0, 2] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 7 * Math.Pow(10, -point)));
            temp[0, 3] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 6 * Math.Pow(10, -point)));
            temp[0, 4] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 5 * Math.Pow(10, -point)));
            temp[0, 5] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 4 * Math.Pow(10, -point)));
            temp[0, 6] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 3 * Math.Pow(10, -point)));
            temp[0, 7] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 2 * Math.Pow(10, -point)));
            temp[0, 8] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 1 * Math.Pow(10, -point)));
            temp[0, 9] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH - 0 * Math.Pow(10, -point)));
            temp[0, 10] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 1 * Math.Pow(10, -point)));
            temp[0, 11] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 2 * Math.Pow(10, -point)));
            temp[0, 12] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 3 * Math.Pow(10, -point)));
            temp[0, 13] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 4 * Math.Pow(10, -point)));
            temp[0, 14] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 5 * Math.Pow(10, -point)));
            temp[0, 15] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 6 * Math.Pow(10, -point)));
            temp[0, 16] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 7 * Math.Pow(10, -point)));
            temp[0, 17] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 8 * Math.Pow(10, -point)));
            temp[0, 18] = Math.Abs(calConfigsStruct.IdelTemperature - ReturnTempOutput(commendOut, commendOut, CompensationADCH + 9 * Math.Pow(10, -point)));
            temp[1, 0] = CompensationADCH - 9 * Math.Pow(10, -point);
            temp[1, 1] = CompensationADCH - 8 * Math.Pow(10, -point);
            temp[1, 2] = CompensationADCH - 7 * Math.Pow(10, -point);
            temp[1, 3] = CompensationADCH - 6 * Math.Pow(10, -point);
            temp[1, 4] = CompensationADCH - 5 * Math.Pow(10, -point);
            temp[1, 5] = CompensationADCH - 4 * Math.Pow(10, -point);
            temp[1, 6] = CompensationADCH - 3 * Math.Pow(10, -point);
            temp[1, 7] = CompensationADCH - 2 * Math.Pow(10, -point);
            temp[1, 8] = CompensationADCH - 1 * Math.Pow(10, -point);
            temp[1, 9] = CompensationADCH - 0 * Math.Pow(10, -point);
            temp[1, 10] = CompensationADCH + 1 * Math.Pow(10, -point);
            temp[1, 11] = CompensationADCH + 2 * Math.Pow(10, -point);
            temp[1, 12] = CompensationADCH + 3 * Math.Pow(10, -point);
            temp[1, 13] = CompensationADCH + 4 * Math.Pow(10, -point);
            temp[1, 14] = CompensationADCH + 5 * Math.Pow(10, -point);
            temp[1, 15] = CompensationADCH + 6 * Math.Pow(10, -point);
            temp[1, 16] = CompensationADCH + 7 * Math.Pow(10, -point);
            temp[1, 17] = CompensationADCH + 8 * Math.Pow(10, -point);
            temp[1, 18] = CompensationADCH + 9 * Math.Pow(10, -point);
            for (int count=0;count<19;count++)
            {                
                if (isFirst)
                {                   
                    min = temp[0, count];
                    output = temp[1, count];
                    isFirst = false;
                }
                else
                {                   
                    if (min > temp[0, count])
                    {
                        min = temp[0, count];
                        output = temp[1, count];                       
                    }                    
                }
                Console.WriteLine($"CompensationADCH + output {CompensationADCH},{temp[1, count]}");
            }
            Console.WriteLine($"output min IdelTemperature commendOut {output},{min},{calConfigsStruct.IdelTemperature},{commendOut}");
            return output;
        }

        /// <summary>
        /// 輸入ADCL,ADCH(實際電壓),CompensationADCL(補償電壓),calConfigsStruct(使用者輸入值),point(第幾次)
        /// 利用ADCL,ADCH實際電壓和CompensationADCL(補償電壓)-(+9~-9)*10^-point計算實際阻抗
        /// 比較離calConfigsStruct(使用者輸入值)最接近的值
        /// 回傳其補償值CompensationADCL(補償電壓)-(+9~-9)*10^-point計算實際阻抗
        /// </summary>
        /// <param name="ADCL"></param>
        /// <param name="ADCH"></param>
        /// <param name="CompensationADCL"></param>
        /// <param name="calConfigsStruct"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private double ComputeImpedance(double ADCL, double ADCH, double CompensationADCL, CalConfigsStruct calConfigsStruct, int point)
        {
            double[,] temp = new double[2, 19];
            bool isFirst = true;
            double min = double.MaxValue;
            double output = double.MaxValue;
            temp[0, 0] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 9 * Math.Pow(10, -point));
            temp[0, 1] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 8 * Math.Pow(10, -point));
            temp[0, 2] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 7 * Math.Pow(10, -point));
            temp[0, 3] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 6 * Math.Pow(10, -point));
            temp[0, 4] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 5 * Math.Pow(10, -point));
            temp[0, 5] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 4 * Math.Pow(10, -point));
            temp[0, 6] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 3 * Math.Pow(10, -point));
            temp[0, 7] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 2 * Math.Pow(10, -point));
            temp[0, 8] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 1 * Math.Pow(10, -point));
            temp[0, 9] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL - 0 * Math.Pow(10, -point));
            temp[0, 10] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 1 * Math.Pow(10, -point));
            temp[0, 11] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 2 * Math.Pow(10, -point));
            temp[0, 12] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 3 * Math.Pow(10, -point));
            temp[0, 13] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 4 * Math.Pow(10, -point));
            temp[0, 14] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 5 * Math.Pow(10, -point));
            temp[0, 15] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 6 * Math.Pow(10, -point));
            temp[0, 16] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 7 * Math.Pow(10, -point));
            temp[0, 17] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 8 * Math.Pow(10, -point));
            temp[0, 18] = calConfigsStruct.IdelTemperature - ReturnImpedanceConvert(ADCL, ADCH, 0, CompensationADCL + 9 * Math.Pow(10, -point));
            temp[1, 0] = CompensationADCL - 9 * Math.Pow(10, -point);
            temp[1, 1] = CompensationADCL - 8 * Math.Pow(10, -point);
            temp[1, 2] = CompensationADCL - 7 * Math.Pow(10, -point);
            temp[1, 3] = CompensationADCL - 6 * Math.Pow(10, -point);
            temp[1, 4] = CompensationADCL - 5 * Math.Pow(10, -point);
            temp[1, 5] = CompensationADCL - 4 * Math.Pow(10, -point);
            temp[1, 6] = CompensationADCL - 3 * Math.Pow(10, -point);
            temp[1, 7] = CompensationADCL - 2 * Math.Pow(10, -point);
            temp[1, 8] = CompensationADCL - 1 * Math.Pow(10, -point);
            temp[1, 9] = CompensationADCL - 0 * Math.Pow(10, -point);
            temp[1, 10] = CompensationADCL + 1 * Math.Pow(10, -point);
            temp[1, 11] = CompensationADCL + 2 * Math.Pow(10, -point);
            temp[1, 12] = CompensationADCL + 3 * Math.Pow(10, -point);
            temp[1, 13] = CompensationADCL + 4 * Math.Pow(10, -point);
            temp[1, 14] = CompensationADCL + 5 * Math.Pow(10, -point);
            temp[1, 15] = CompensationADCL + 6 * Math.Pow(10, -point);
            temp[1, 16] = CompensationADCL + 7 * Math.Pow(10, -point);
            temp[1, 17] = CompensationADCL + 8 * Math.Pow(10, -point);
            temp[1, 18] = CompensationADCL + 9 * Math.Pow(10, -point);
            for (int count = 0; count < 19; count++)
            {
                if (isFirst)
                {
                    min = temp[0, count];
                    output = temp[1, count];
                    isFirst = false;
                }
                else
                {
                    if (min > temp[0, count])
                    {
                        min = temp[0, count];
                        output = temp[1, count];
                    }
                }
            }          
            return output;
        }
    }
}
