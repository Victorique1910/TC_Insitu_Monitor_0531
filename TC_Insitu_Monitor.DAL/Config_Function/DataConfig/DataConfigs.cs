using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataConfigs:FileFunction
    {        
        private List<ConfigStruct> _configs = new List<ConfigStruct>();
        readonly private string _filePath = string.Empty;

        public List<ConfigStruct> Configs 
        { 
            get {
                if (File.Exists(_filePath))
                {
                    OpenInitial(_filePath);
                }
                else
                {
                    Initial();
                }
                return _configs;
            } 
        }

        public DataConfigs(string filePath)
        {
            _filePath = filePath;           
        }

        private void Initial()
        {
            _configs.Add(new ConfigStruct()
            {
                Board = 0x02,
                COMPort = "COM7",
                Port =1,
                ID = "1-1-Imp-Corner",
                Type = "Impedance",              
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = -0.005458008,
                CompensationADCL = 0.003964355,
                Loss = 0,
                DUTName = "A",
                Chain = "Corner",
                Channel = 1,
                Location = "'1-1",
                TemperatureName = "1-1-Temp",
                TemperatureType = "T-type"
            });

            _configs.Add(new ConfigStruct()
            {
                Board = 0x02,               
                COMPort = "COM7",
                Port = 3,
                ID = "1-2-Imp-Corner",
                Type = "Impedance",               
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",                   
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = 0.000164551,
                CompensationADCL = 0.004570801,
                Loss = 0,
                DUTName = "B",
                Chain = "Corner",
                Channel = 3,
                Location = "'1-2",
                TemperatureName = "1-1-Temp",
                TemperatureType ="T-type"
            });

            _configs.Add(new ConfigStruct()
            {
                Board = 0x02,
                COMPort = "COM7",
                Port = 5,
                ID = "1-3-Imp-Corner",
                Type = "Impedance",                
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = -0.000638672,
                CompensationADCL = 0.00435791,
                Loss = 0,
                DUTName = "C",
                Chain = "Corner",
                Channel = 5,
                Location = "'1-3",
                TemperatureName = "1-1-Temp",
                TemperatureType = "T-type"
            });

            _configs.Add(new ConfigStruct()
            {
                Board = 0x02,
                COMPort = "COM7",
                Port = 7,
                ID = "1-4-Imp-Corner",
                Type = "Impedance",                
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = 0.000967773,
                CompensationADCL = 0.005161133,
                Loss = 0,
                DUTName = "D",
                Chain = "Corner",
                Channel = 7,
                Location = "'1-4",
                TemperatureName = "1-1-Temp",
                TemperatureType = "T-type"
            });

            _configs.Add(new ConfigStruct()
            {
                Board = 0x02,
                COMPort = "COM7",
                Port = 9,
                ID = "1-5-Imp-Corner",
                Type = "Impedance",                
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = -0.028754688,
                CompensationADCL = 0.005780469,
                Loss = 0,
                DUTName = "E",
                Chain = "Corner",
                Channel = 9,
                Location = "'1-5",
                TemperatureName = "1-1-Temp",
                TemperatureType = "T-type"
            }) ;

            _configs.Add(new ConfigStruct()
            {
                Board = 0x03,
                COMPort = "COM8",
                Port = 1,
                ID = "1-1-Temp",
                Type = "Temperature",                
                FailCriteria = new FailCriteria()
                {
                    IsCriteria = true,
                    IsDPAT = false,
                    FailCondition = "Absolute",
                    Method = "Above",
                    Precentage = 10,
                    ValuesMax = 2,
                    ValuesMin = 1,
                    Base = "Max",
                    ContinuousCount = 5
                },
                CompensationADCH = 0,
                CompensationADCL = 0,
                Loss = 0,
                DUTName = "F",
                Chain = "Corner",
                Channel = 1,
                Location = "'2-1",
                TemperatureName = "1-1-Temp",
                TemperatureType = "T-type"
            });
        }

        private void OpenInitial(string filePath)
        {
            _configs = OpenFile(filePath, s=> {
                return new ConfigStruct()
                {
                    Board = Byte.Parse(s[(int)Enum_ColConfig.Board]),
                    COMPort = s[(int)Enum_ColConfig.COMPort],
                    Port = int.Parse(s[(int)Enum_ColConfig.Port]),
                    ID = s[(int)Enum_ColConfig.ID],
                    Type = s[(int)Enum_ColConfig.Type],
                    FailCriteria = new FailCriteria()
                    {
                        IsCriteria = bool.Parse(s[(int)Enum_ColConfig.IsCriteria]),
                        IsDPAT = bool.Parse(s[(int)Enum_ColConfig.IsDPAT]),
                        FailCondition = s[(int)Enum_ColConfig.FailCondition],
                        Method = s[(int)Enum_ColConfig.Method],
                        Precentage = double.Parse(s[(int)Enum_ColConfig.Precentage]),
                        ValuesMax = double.Parse(s[(int)Enum_ColConfig.ValuesMax]),
                        ValuesMin = double.Parse(s[(int)Enum_ColConfig.ValuesMin]),
                        Base = s[(int)Enum_ColConfig.Base],
                        ContinuousCount = int.Parse(s[(int)Enum_ColConfig.ContinuousCount])
                    },
                    CompensationADCH = double.Parse(s[(int)Enum_ColConfig.CompensationADCH]),
                    CompensationADCL = double.Parse(s[(int)Enum_ColConfig.CompensationADCL]),
                    Loss = double.Parse(s[(int)Enum_ColConfig.Loss]),
                    DUTName = s[(int)Enum_ColConfig.DUTName],
                    Chain = s[(int)Enum_ColConfig.Chain],
                    Channel = int.Parse(s[(int)Enum_ColConfig.Channel]),
                    Location = s[(int)Enum_ColConfig.Location],
                    TemperatureName = s[(int)Enum_ColConfig.TemperatureName],
                    TemperatureType = s[(int)Enum_ColConfig.TemperatureType]
                };
            });
        }

        enum Enum_ColConfig
        {
            Board             = 0,
            COMPort           = 1,
            Port              = 2,
            ID                = 3,
            Type              = 4,            
            IsCriteria        = 5,
            IsDPAT            = 6,
            FailCondition     = 7,
            Method            = 8,
            Precentage        = 9,
            ValuesMax         = 10,
            ValuesMin         = 11,
            Base              = 12,
            ContinuousCount   = 13,
            CompensationADCH  = 14,
            CompensationADCL  = 15,
            Loss              = 16,
            DUTName           = 17,
            Chain             = 18,
            Channel           = 19,
            Location          = 20,
            TemperatureName   = 21,
            TemperatureType   = 22,
            END
        }
    }
}
