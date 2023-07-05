using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataTempParsing
    {     
        private readonly Statuses _statuses;
        public Statuses Statuses { get { return _statuses; } }

        public DataTempParsing(List<ConfigStruct> configs, USBStruct newUSBStruct, Statuses statuses, CommendStruct commendOut, CommendStruct commendOut1, CommendStruct beforeCommend)
        {
            foreach (var config in configs)
            {
                DataConfigsStatus dataConfigsStatus = statuses.SearchDataConfigsStatus(config.ID);
                #region 獲取溫度
                double temp = 0;
                double voltageH = 0;
                double voltageL = 0;
                double layerH = 0;
                double layerL = 0;
                double beforeLayer = 0;
                switch (dataConfigsStatus.Configs.Port)
                {
                    case 1:
                        temp = newUSBStruct.ADC1 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC1;  
                        voltageL = 0;
                        layerH = commendOut1.ADC1;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC1;
                        break;
                    case 2:
                        temp = newUSBStruct.ADC2 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC2;
                        voltageL = 0;
                        layerH = commendOut1.ADC2;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC2;
                        break;
                    case 3:
                        temp = newUSBStruct.ADC3 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC3;
                        voltageL = 0;
                        layerH = commendOut1.ADC3;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC3;
                        break;
                    case 4:
                        temp = newUSBStruct.ADC4 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC4;
                        voltageL = 0;
                        layerH = commendOut1.ADC4;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC4;
                        break;
                    case 5:
                        temp = newUSBStruct.ADC5 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC5;
                        voltageL = 0;
                        layerH = commendOut1.ADC5;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC5;
                        break;
                    case 6:
                        temp = newUSBStruct.ADC6 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC6;
                        voltageL = 0;
                        layerH = commendOut1.ADC6;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC6;
                        break;
                    case 7:
                        temp = newUSBStruct.ADC7 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC7;
                        voltageL = 0;
                        layerH = commendOut1.ADC7;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC7;
                        break;
                    case 8:
                        temp = newUSBStruct.ADC8 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC8;
                        voltageL = 0;
                        layerH = commendOut1.ADC8;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC8;
                        break;
                    case 9:
                        temp = newUSBStruct.ADC9 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC9;
                        voltageL = 0;
                        layerH = commendOut1.ADC9;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC9;
                        break;
                    case 10:
                        temp = newUSBStruct.ADC10 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC10;
                        voltageL = 0;
                        layerH = commendOut1.ADC10;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC10;
                        break;
                    case 11:
                        temp = newUSBStruct.ADC11 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC11;
                        voltageL = 0;
                        layerH = commendOut1.ADC11;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC11;
                        break;
                    case 12:
                        temp = newUSBStruct.ADC12 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC12;
                        voltageL = 0;
                        layerH = commendOut1.ADC12;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC12;
                        break;
                    case 13:
                        temp = newUSBStruct.ADC13 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC13;
                        voltageL = 0;
                        layerH = commendOut1.ADC13;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC13;
                        break;
                    case 14:
                        temp = newUSBStruct.ADC14 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC14;
                        voltageL = 0;
                        layerH = commendOut1.ADC14;
                        layerL = 0;
                        beforeLayer = beforeCommend.ADC14;
                        break;
                    default:
                        break;
                }
                #endregion                 
                #region 儲存資料
                DataFormatStruct data = new DataFormatStruct()
                {
                    ID               = config.ID,
                    DUTName          = config.DUTName,
                    Impedance        = 0,
                    VoltageH         = voltageH,
                    VoltageL         = voltageL,
                    LayerH           = layerH,
                    LayerL           = layerL,
                    BeforeLayer      = beforeLayer,
                    Temperature      = temp
                };               
                dataConfigsStatus.DataFormatStruct = data;                
                #endregion
                #region 儲存溫度
                statuses.ScanDataConfigsStatus(dataConfigsStatus.Configs.ID, dataConfigsStatus);               
                #endregion
                #region 儲存狀態
                _statuses = statuses;
                #endregion
            }
        }
    }
}
