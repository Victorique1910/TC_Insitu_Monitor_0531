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

        public void Parsing(List<ConfigStruct> configs, Statuses statuses, CommendStruct commendOut,CommendStruct commendOut1, bool isFormal)
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
                        DataConfigsStatus dataConfigsStatus = statuses.SearchDataConfigsStatus(config.ID);
                        double beforeLayer = dataConfigsStatus.DataFormatStruct.BeforeLayer;

                        switch (config.Port)
                        {
                            case 1:                  
                                if(!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC1;
                                }
                                _tempOutput.ADC1 = Floor1000(ReturnTempOutput(commendOut1.ADC1, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC1 = ReturnCommendTempBefore(commendOut1.ADC1, beforeLayer);                                
                                break;
                            case 2:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC2;
                                }
                                _tempOutput.ADC2 = Floor1000(ReturnTempOutput(commendOut1.ADC2, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC2 = ReturnCommendTempBefore(commendOut1.ADC2, beforeLayer);                                
                                break;
                            case 3:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC3;
                                }
                                _tempOutput.ADC3 = Floor1000(ReturnTempOutput(commendOut1.ADC3, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC3 = ReturnCommendTempBefore(commendOut1.ADC3, beforeLayer);                                
                                break;
                            case 4:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC4;
                                }
                                _tempOutput.ADC4 =  Floor1000(ReturnTempOutput(commendOut1.ADC4, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC4 = ReturnCommendTempBefore(commendOut1.ADC4, beforeLayer);
                                break;
                            case 5:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC5;
                                }
                                _tempOutput.ADC5 = Floor1000(ReturnTempOutput(commendOut1.ADC5, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC5 = ReturnCommendTempBefore(commendOut1.ADC5, beforeLayer);
                                break;
                            case 6:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC6;
                                }
                                _tempOutput.ADC6 = Floor1000(ReturnTempOutput(commendOut1.ADC6, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC6 = ReturnCommendTempBefore(commendOut1.ADC6, beforeLayer);
                                break;
                            case 7:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC7;
                                }
                                _tempOutput.ADC7 = Floor1000(ReturnTempOutput(commendOut1.ADC7, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC7 = ReturnCommendTempBefore(commendOut1.ADC7, beforeLayer);
                                break;
                            case 8:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC8;
                                }
                                _tempOutput.ADC8 = Floor1000(ReturnTempOutput(commendOut1.ADC8, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC8 = ReturnCommendTempBefore(commendOut1.ADC8, beforeLayer);
                                break;
                            case 9:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC9;
                                }
                                _tempOutput.ADC9 = Floor1000(ReturnTempOutput(commendOut1.ADC9, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC9 = ReturnCommendTempBefore(commendOut1.ADC9, beforeLayer);
                                break;
                            case 10:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC10;
                                }
                                _tempOutput.ADC10 = Floor1000(ReturnTempOutput(commendOut1.ADC10, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC10 = ReturnCommendTempBefore(commendOut1.ADC10, beforeLayer);
                                break;
                            case 11:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC11;
                                }
                                _tempOutput.ADC11 = Floor1000(ReturnTempOutput(commendOut1.ADC11, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC11 = ReturnCommendTempBefore(commendOut1.ADC11, beforeLayer);
                                break;
                            case 12:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC12;
                                }
                                _tempOutput.ADC12 = Floor1000(ReturnTempOutput(commendOut1.ADC12, beforeLayer, config.CompensationADCH)) ;
                                _commendTempBefore.ADC12 = ReturnCommendTempBefore(commendOut1.ADC12, beforeLayer);
                                break;
                            case 13:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC13;
                                }
                                _tempOutput.ADC13 = Floor1000(ReturnTempOutput(commendOut1.ADC13, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC13 = ReturnCommendTempBefore(commendOut1.ADC13, beforeLayer);
                                break;
                            case 14:
                                if (!isFormal)
                                {
                                    beforeLayer = commendOut1.ADC14;
                                }
                                _tempOutput.ADC14 = Floor1000(ReturnTempOutput(commendOut1.ADC14, beforeLayer, config.CompensationADCH));
                                _commendTempBefore.ADC14 = ReturnCommendTempBefore(commendOut1.ADC14, beforeLayer);
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
        private const double tempTH = 5;
        protected double ReturnCommendTempBefore(double commendOut, double commendBefore)
        {
            double _commendTempBefore;
            //commendOut = Floor1000(commendOut);            
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
            //commendOut = Floor1000(commendOut);
            //Console.WriteLine($"commendBefore-CompensationADCH {commendBefore} {CompensationADCH}");
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
