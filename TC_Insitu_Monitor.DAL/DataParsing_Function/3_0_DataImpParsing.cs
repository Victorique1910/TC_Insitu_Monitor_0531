using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataImpParsing
    {               
        readonly private Statuses _statuses;        
        public Statuses Statuses { get { return _statuses; } }

        public DataImpParsing(List<ConfigStruct> configs, USBStruct newUSBStruct, Statuses statuses,CommendStruct commendOut)
        {           
            foreach (var config in configs)
            {
                DataConfigsStatus dataConfigsStatus = statuses.SearchDataConfigsStatus(config.ID);               
                #region 獲取阻抗
                double imp = 0;
                double voltageH = 0;
                double voltageL = 0;
                switch (dataConfigsStatus.Configs.Port)
                {
                    case 1:
                        imp = newUSBStruct.ADC1 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC1;
                        voltageL = commendOut.ADC2;
                        break;
                    case 2:
                        imp = newUSBStruct.ADC2 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC1;
                        voltageL = commendOut.ADC2;
                        break;
                    case 3:
                        imp = newUSBStruct.ADC3 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC3;
                        voltageL = commendOut.ADC4;
                        break;
                    case 4:
                        imp = newUSBStruct.ADC4 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC3;
                        voltageL = commendOut.ADC4;
                        break;
                    case 5:
                        imp = newUSBStruct.ADC5 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC5;
                        voltageL = commendOut.ADC6;
                        break;
                    case 6:
                        imp = newUSBStruct.ADC6 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC5;
                        voltageL = commendOut.ADC6;
                        break;
                    case 7:
                        imp = newUSBStruct.ADC7 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC7;
                        voltageL = commendOut.ADC8;
                        break;
                    case 8:
                        imp = newUSBStruct.ADC8 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC7;
                        voltageL = commendOut.ADC8;
                        break;
                    case 9:
                        imp = newUSBStruct.ADC9 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC9;
                        voltageL = commendOut.ADC10;
                        break;
                    case 10:
                        imp = newUSBStruct.ADC10 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC9;
                        voltageL = commendOut.ADC10;
                        break;
                    case 11:
                        imp = newUSBStruct.ADC11 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC11;
                        voltageL = commendOut.ADC12;
                        break;
                    case 12:
                        imp = newUSBStruct.ADC12 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC11;
                        voltageL = commendOut.ADC12;
                        break;
                    case 13:
                        imp = newUSBStruct.ADC13 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC13;
                        voltageL = commendOut.ADC14;
                        break;
                    case 14:
                        imp = newUSBStruct.ADC14 - dataConfigsStatus.Configs.Loss;
                        voltageH = commendOut.ADC13;
                        voltageL = commendOut.ADC14;
                        break;
                    default:
                        break;
                }
                #endregion
                #region 儲存資料
                DataFormatStruct data = new DataFormatStruct()
                {
                    ID = config.ID,
                    DUTName = config.DUTName,
                    VoltageH = voltageH,
                    VoltageL = voltageL,
                    Impedance = imp,
                    Temperature = 0
                };
                dataConfigsStatus.DataFormatStruct = data;
                #endregion
                #region 儲存阻抗
                statuses.ScanDataConfigsStatus(dataConfigsStatus.Configs.ID, dataConfigsStatus);
                #endregion           
                #region 儲存狀態
                _statuses = statuses;               
                #endregion
            }
        }
    }
}
