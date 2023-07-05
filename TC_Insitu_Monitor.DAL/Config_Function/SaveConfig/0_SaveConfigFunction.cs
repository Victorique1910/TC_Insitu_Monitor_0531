using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class SaveConfigFunction
    {
        readonly private TableLookUP tableLook = new TableLookUP();
        public double TempTransImp_Max(List<SaveFormalLogStruct> sources, double temperature)
        {
            List<double> outputs = new List<double>();
            var temps = sources.GroupBy((c)=>c.Cycle);
            foreach(var temp in temps)
            {
                Dictionary<double, double> table = new Dictionary<double, double>();
                foreach (var value in temp)
                {
                    //                   KEY       VALUE        
                    table.Add(value.Temperature,value.Impedance);
                }
                outputs.Add(tableLook.Convert(table, temperature));
            }
            return outputs.Max();
        }

        public double TempTransImp_Min(List<SaveFormalLogStruct> sources, double temperature)
        {
            List<double> outputs = new List<double>();
            var temps = sources.GroupBy((c) => c.Cycle);
            foreach (var temp in temps)
            {
                Dictionary<double, double> table = new Dictionary<double, double>();
                foreach (var value in temp)
                {
                    //                   KEY       VALUE        
                    table.Add(value.Temperature, value.Impedance);
                }
                outputs.Add(tableLook.Convert(table, temperature));
            }
            return outputs.Min();
        }

        public double TempTransImp_Average(List<SaveFormalLogStruct> sources, double temperature)
        {
            List<double> outputs = new List<double>();
            var temps = sources.GroupBy((c) => c.Cycle);
            foreach (var temp in temps)
            {
                Dictionary<double, double> table = new Dictionary<double, double>();
                foreach (var value in temp)
                {
                    //                   KEY       VALUE        
                    table.Add(value.Temperature, value.Impedance);
                }
                outputs.Add(tableLook.Convert(table, temperature));
            }
            return outputs.Average();
        }

        public double TempLimit_Max(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if(outputs.Count>0)
            {
                return outputs.Max();
            }
            else
            {
                return double.NaN;
            }
        }

        public double TempLimit_Min(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if (outputs.Count > 0)
            {
                return outputs.Min();
            }
            else
            {
                return double.NaN;
            }            
        }

        public double HT_DwellTime_Max(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "DWEL_H";
            });
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if (outputs.Count > 0)
            {
                return outputs.Max();
            }
            else
            {
                return double.NaN;
            }
        }

        public double HT_DwellTime_Min(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "DWEL_H";
            });
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if (outputs.Count > 0)
            {
                return outputs.Min();
            }
            else
            {
                return double.NaN;
            }           
        }

        public double HT_RampUp_Max(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "RampUp";
            }).GroupBy((c)=> c.Cycle);

            foreach(var temp in temps)
            {
                List<double> outputTemp = new List<double>();
                List<double> outputTime = new List<double>();
                foreach(var value in temp)
                {
                    outputTemp.Add(value.Temperature);
                    outputTime.Add(double.Parse(value.TimeStamp));
                }
                outputs.Add((outputTemp.Max() - outputTemp.Min()) / (outputTime.Max() - outputTime.Min()));                
            }
            if (outputs.Count > 0)
            {
                return outputs.Max();
            }
            else
            {
                return double.NaN;
            }           
        }

        public double HT_RampUp_Min(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "RampUp";
            }).GroupBy((c) => c.Cycle);

            foreach (var temp in temps)
            {
                List<double> outputTemp = new List<double>();
                List<double> outputTime = new List<double>();
                foreach (var value in temp)
                {
                    outputTemp.Add(value.Temperature);
                    outputTime.Add(double.Parse(value.TimeStamp));
                }
                outputs.Add((outputTemp.Max() - outputTemp.Min()) / (outputTime.Max() - outputTime.Min()));
            }
            if (outputs.Count > 0)
            {
                return outputs.Min();
            }
            else
            {
                return double.NaN;
            }            
        }

        public double LT_DwellTime_Max(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "DWEL_L";
            });
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if (outputs.Count > 0)
            {
                return outputs.Max();
            }
            else
            {
                return double.NaN;
            }           
        }

        public double LT_DwellTime_Min(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "DWEL_L";
            });
            foreach (var temp in sources)
            {
                outputs.Add(temp.Temperature);
            }
            if (outputs.Count > 0)
            {
                return outputs.Min();
            }
            else
            {
                return double.NaN;
            }            
        }

        public double LT_RampDown_Max(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "RampDown";
            }).GroupBy((c) => c.Cycle);

            foreach (var temp in temps)
            {
                List<double> outputTemp = new List<double>();
                List<double> outputTime = new List<double>();
                foreach (var value in temp)
                {
                    outputTemp.Add(value.Temperature);
                    outputTime.Add(value.TimeTicks / 600000000);
                }
                outputs.Add((outputTemp.Max() - outputTemp.Min()) / (outputTime.Max() - outputTime.Min()));
            }
            if (outputs.Count > 0)
            {
                return outputs.Max();
            }
            else
            {
                return double.NaN;
            }            
        }

        public double LT_RampDown_Min(List<SaveFormalLogStruct> sources)
        {
            List<double> outputs = new List<double>();
            var temps = sources.Where(b =>
            {
                return b.Status == "RampDown";
            }).GroupBy((c) => c.Cycle);

            foreach (var temp in temps)
            {
                List<double> outputTemp = new List<double>();
                List<double> outputTime = new List<double>();
                foreach (var value in temp)
                {
                    outputTemp.Add(value.Temperature);
                    outputTime.Add(value.TimeTicks/ 600000000);
                }
                outputs.Add((outputTemp.Max() - outputTemp.Min()) / (outputTime.Max() - outputTime.Min()));
            }
            if (outputs.Count > 0)
            {
                return outputs.Min();
            }
            else
            {
                return double.NaN;
            }            
        }
    }
}
