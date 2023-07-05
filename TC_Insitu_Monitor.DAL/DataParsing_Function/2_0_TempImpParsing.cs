using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class TempImpParsing: TempImpParsingFunction
    {            
        private USBStruct _tempOutput = new USBStruct();
        private USBStruct _impOutput = new USBStruct();
        private CommendStruct _commendTempBefore = new CommendStruct();
        private string _type;        
        public USBStruct TempOutput { get { return _tempOutput; } }
        public USBStruct ImpOutput { get { return _impOutput; } }
        public CommendStruct CommendTempBefore { get { return _commendTempBefore; } }
        public string Type { get { return _type; } }

        public void Parsing(List<ConfigStruct> configs, CommendStruct commendOut, CommendStruct commendBefore)
        {
            foreach (var config in configs)
            {
                _type = "None";                
                _tempOutput.USBComport = config.COMPort;
                _impOutput.USBComport = config.COMPort;
                if (config.Board.Equals(commendOut.Board)) //
                {
                    if (config.Type.Contains("Temperature"))
                    {
                        _type = "Temperature";                        
                        switch (config.Port)
                        {
                            case 1:                                
                                _tempOutput.ADC1 = Floor1000(ReturnTempOutput(commendOut.ADC1, commendBefore.ADC1, config.CompensationADCH));
                                _commendTempBefore.ADC1 = ReturnCommendTempBefore(commendOut.ADC1, commendBefore.ADC1);                                
                                break;
                            case 2:                                
                                _tempOutput.ADC2 = Floor1000(ReturnTempOutput(commendOut.ADC2, commendBefore.ADC2, config.CompensationADCH));
                                _commendTempBefore.ADC2 = ReturnCommendTempBefore(commendOut.ADC2, commendBefore.ADC2);                                
                                break;
                            case 3:                                
                                _tempOutput.ADC3 = Floor1000(ReturnTempOutput(commendOut.ADC3, commendBefore.ADC3, config.CompensationADCH));
                                _commendTempBefore.ADC3 = ReturnCommendTempBefore(commendOut.ADC3, commendBefore.ADC3);                                
                                break;
                            case 4:
                                _tempOutput.ADC4 =  Floor1000(ReturnTempOutput(commendOut.ADC4, commendBefore.ADC4, config.CompensationADCH));
                                _commendTempBefore.ADC4 = ReturnCommendTempBefore(commendOut.ADC4, commendBefore.ADC4);
                                break;
                            case 5:
                                _tempOutput.ADC5 = Floor1000(ReturnTempOutput(commendOut.ADC5, commendBefore.ADC5, config.CompensationADCH));
                                _commendTempBefore.ADC5 = ReturnCommendTempBefore(commendOut.ADC5, commendBefore.ADC5);
                                break;
                            case 6:
                                _tempOutput.ADC6 = Floor1000(ReturnTempOutput(commendOut.ADC6, commendBefore.ADC6, config.CompensationADCH));
                                _commendTempBefore.ADC6 = ReturnCommendTempBefore(commendOut.ADC6, commendBefore.ADC6);
                                break;
                            case 7:
                                _tempOutput.ADC7 = Floor1000(ReturnTempOutput(commendOut.ADC7, commendBefore.ADC7, config.CompensationADCH));
                                _commendTempBefore.ADC7 = ReturnCommendTempBefore(commendOut.ADC7, commendBefore.ADC7);
                                break;
                            case 8:
                                _tempOutput.ADC8 = Floor1000(ReturnTempOutput(commendOut.ADC8, commendBefore.ADC8, config.CompensationADCH));
                                _commendTempBefore.ADC8 = ReturnCommendTempBefore(commendOut.ADC8, commendBefore.ADC8);
                                break;
                            case 9:
                                _tempOutput.ADC9 = Floor1000(ReturnTempOutput(commendOut.ADC9, commendBefore.ADC9, config.CompensationADCH));
                                _commendTempBefore.ADC9 = ReturnCommendTempBefore(commendOut.ADC9, commendBefore.ADC9);
                                break;
                            case 10:
                                _tempOutput.ADC10 = Floor1000(ReturnTempOutput(commendOut.ADC10, commendBefore.ADC10, config.CompensationADCH));
                                _commendTempBefore.ADC10 = ReturnCommendTempBefore(commendOut.ADC10, commendBefore.ADC10);
                                break;
                            case 11:
                                _tempOutput.ADC11 = Floor1000(ReturnTempOutput(commendOut.ADC11, commendBefore.ADC11, config.CompensationADCH));
                                _commendTempBefore.ADC11 = ReturnCommendTempBefore(commendOut.ADC11, commendBefore.ADC11);
                                break;
                            case 12:
                                _tempOutput.ADC12 = Floor1000(ReturnTempOutput(commendOut.ADC12, commendBefore.ADC12, config.CompensationADCH)) ;
                                _commendTempBefore.ADC12 = ReturnCommendTempBefore(commendOut.ADC12, commendBefore.ADC12);
                                break;
                            case 13:
                                _tempOutput.ADC13 = Floor1000(ReturnTempOutput(commendOut.ADC13, commendBefore.ADC13, config.CompensationADCH));
                                _commendTempBefore.ADC13 = ReturnCommendTempBefore(commendOut.ADC13, commendBefore.ADC13);
                                break;
                            case 14:
                                _tempOutput.ADC14 = Floor1000(ReturnTempOutput(commendOut.ADC14, commendBefore.ADC14, config.CompensationADCH));
                                _commendTempBefore.ADC14 = ReturnCommendTempBefore(commendOut.ADC14, commendBefore.ADC14);
                                break;
                            default:
                                break;
                        }
                    }
                    else if(config.Type.Contains("Impedance"))
                    {                        
                        _type = "Impedance";
                        switch (config.Port)
                        {
                            case 1:
                                _impOutput.ADC1 = ReturnImpedanceConvert(commendOut.ADC1, commendOut.ADC2, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 2:
                                _impOutput.ADC2 = ReturnImpedanceConvert(commendOut.ADC1, commendOut.ADC2, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 3:
                                _impOutput.ADC3 = ReturnImpedanceConvert(commendOut.ADC3, commendOut.ADC4, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 4:
                                _impOutput.ADC4 = ReturnImpedanceConvert(commendOut.ADC3, commendOut.ADC4, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 5:
                                _impOutput.ADC5 = ReturnImpedanceConvert(commendOut.ADC5, commendOut.ADC6, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 6:
                                _impOutput.ADC6 = ReturnImpedanceConvert(commendOut.ADC5, commendOut.ADC6, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 7:
                                _impOutput.ADC7 = ReturnImpedanceConvert(commendOut.ADC7, commendOut.ADC8, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 8:
                                _impOutput.ADC8 = ReturnImpedanceConvert(commendOut.ADC7, commendOut.ADC8, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 9:
                                _impOutput.ADC9 = ReturnImpedanceConvert(commendOut.ADC9, commendOut.ADC10, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 10:
                                _impOutput.ADC10 = ReturnImpedanceConvert(commendOut.ADC9, commendOut.ADC10, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 11:
                                _impOutput.ADC11 = ReturnImpedanceConvert(commendOut.ADC11, commendOut.ADC12, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 12:
                                _impOutput.ADC12 = ReturnImpedanceConvert(commendOut.ADC11, commendOut.ADC12, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 13:
                                _impOutput.ADC13 = ReturnImpedanceConvert(commendOut.ADC13, commendOut.ADC14, config.CompensationADCH, config.CompensationADCL);
                                break;
                            case 14:
                                _impOutput.ADC14 = ReturnImpedanceConvert(commendOut.ADC13, commendOut.ADC14, config.CompensationADCH, config.CompensationADCL);
                                break;
                            default:                                
                                break;
                        }
                    }
                }
            }
        }
    }

    public class TempImpParsingFunction
    {
        readonly private TemperatureConvert temperatureConvert = new TemperatureConvert();
        readonly private ImpedanceConvert impedanceConvert = new ImpedanceConvert();
        private const double tempTH = 0.005;
        protected double ReturnCommendTempBefore(double commendOut, double commendBefore)
        {
            double _commendTempBefore;
            commendOut = Floor1000(commendOut);            
            Console.WriteLine("commendOut "+ commendOut);
            if (Math.Abs(commendOut - commendBefore) < tempTH)
            {
                _commendTempBefore = commendBefore;
            }
            else
            {
                _commendTempBefore = commendOut;
            }
            return _commendTempBefore;
        }
        protected double ReturnTempOutput(double commendOut, double commendBefore, double CompensationADCH)
        {
            double _tempOutput;
            commendOut = Floor1000(commendOut);
            if (Math.Abs(commendOut - commendBefore) < tempTH)
            {                
                _tempOutput = temperatureConvert.Convert(commendBefore - CompensationADCH);
            }
            else
            {
                _tempOutput = temperatureConvert.Convert(commendOut - CompensationADCH);
            }           
            return _tempOutput;
        }

        protected double ReturnImpedanceConvert(double ADCL, double ADCH, double CompensationADCH, double CompensationADCL)
        {
            double _impOutput;
            ADCL -= CompensationADCH;
            ADCH -= CompensationADCL;
            _impOutput = impedanceConvert.Convert(Math.Round((ADCH / ((ADCL - ADCH) / 100)), 3, MidpointRounding.AwayFromZero));
            return _impOutput;
        }

        protected double Floor1000(double input)
        {
            double output = input * 1000;
            output = output > 0 ? Math.Floor(output) : Math.Ceiling(output);
            return output / 1000;
        }
    }
}
