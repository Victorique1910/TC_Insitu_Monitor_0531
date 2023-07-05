using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class DataConfigsDictionary
    {
        private DataConfigs dataConfigs;
        readonly private string _filePath = string.Empty;      
      
        public Dictionary<string, List<ConfigStruct>> ConfigsDictionary 
        { 
            get
            {
                Dictionary<string, List<ConfigStruct>> output;
                dataConfigs = new DataConfigs(_filePath);
                output = ConfigsLinq(dataConfigs.Configs);                
                return output;
            }
        }

        public Dictionary<string, List<ConfigStruct>> BoardDictionary
        {
            get
            {
                Dictionary<string, List<ConfigStruct>> output;
                dataConfigs = new DataConfigs(_filePath);
                output = BoardsLinq(dataConfigs.Configs);
                return output;
            }
        }

        public Dictionary<string,List<ConfigStruct>> ChainDictionary
        {
            get
            {
                Dictionary<string, List<ConfigStruct>> output;
                dataConfigs = new DataConfigs(_filePath);
                output = ChainLinq(dataConfigs.Configs);
                return output;
            }
        }

        public Dictionary<string,List<ConfigStruct>> PortDictionary
        {
            get
            {
                Dictionary<string, List<ConfigStruct>> output;
                dataConfigs = new DataConfigs(_filePath);
                output = PortLinq(dataConfigs.Configs);
                return output;
            }
        }

        public DataConfigsDictionary(string filePath)
        {
            _filePath = filePath;
        }

        private Dictionary<string, List<ConfigStruct>> ConfigsLinq(List<ConfigStruct> configs)
        {
            Dictionary<string, List<ConfigStruct>> ConfigsDictionary = new Dictionary<string, List<ConfigStruct>>();
            Task.Factory.StartNew(() => {
                var temps = configs.GroupBy((a) => {
                    return a.COMPort;
                });

                foreach (var temp in temps)
                {
                    List<ConfigStruct> tempConfig = new List<ConfigStruct>();
                    foreach (var config in temp)
                    {
                        tempConfig.Add(config);
                    }
                    ConfigsDictionary.Add(temp.Key, tempConfig);
                }
            }).Wait();
            return ConfigsDictionary;
        }
        private Dictionary<string, List<ConfigStruct>> BoardsLinq(List<ConfigStruct> configs)
        {
            Dictionary<string, List<ConfigStruct>> ConfigsDictionary = new Dictionary<string, List<ConfigStruct>>();
            Task.Factory.StartNew(() => {
                var temps = configs.GroupBy((a) => {
                    return a.Board;
                });

                foreach (var temp in temps)
                {
                    List<ConfigStruct> tempConfig = new List<ConfigStruct>();
                    foreach (var config in temp)
                    {
                        tempConfig.Add(config);
                    }
                    ConfigsDictionary.Add(temp.Key.ToString(), tempConfig);
                }
            }).Wait();
            return ConfigsDictionary;
        }
        private Dictionary<string, List<ConfigStruct>> ChainLinq(List<ConfigStruct> configs)
        {
            Dictionary<string, List<ConfigStruct>> ConfigsDictionary = new Dictionary<string, List<ConfigStruct>>();
            Task.Factory.StartNew(() => {
                var temps = configs.GroupBy((a) => {
                    return a.Chain;
                });

                foreach (var temp in temps)
                {
                    List<ConfigStruct> tempConfig = new List<ConfigStruct>();
                    foreach (var config in temp)
                    {
                        tempConfig.Add(config);
                    }
                    ConfigsDictionary.Add(temp.Key.ToString(), tempConfig);
                }
            }).Wait();
            return ConfigsDictionary;
        }

        private Dictionary<string, List<ConfigStruct>> PortLinq(List<ConfigStruct> configs)
        {
            Dictionary<string, List<ConfigStruct>> ConfigsDictionary = new Dictionary<string, List<ConfigStruct>>();
            Task.Factory.StartNew(() => {
                var temps = configs.GroupBy((a) => {
                    return a.Port;
                });

                foreach (var temp in temps)
                {
                    List<ConfigStruct> tempConfig = new List<ConfigStruct>();
                    foreach (var config in temp)
                    {
                        tempConfig.Add(config);
                    }
                    ConfigsDictionary.Add(temp.Key.ToString(), tempConfig);
                }
            }).Wait();
            return ConfigsDictionary;
        }
    }
}
