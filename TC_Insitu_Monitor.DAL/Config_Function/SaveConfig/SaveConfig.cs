using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class SaveConfig:FileFunction
    {
        private List<SaveFormalLogStruct> _saveFormalLogs = new List<SaveFormalLogStruct>();
        private readonly string _folderPath = string.Empty;
        public SaveConfigFunction SaveConfigFunction = new SaveConfigFunction();

        public List<SaveFormalLogStruct> SaveFormalLog {
            get {                
                if (Directory.Exists(_folderPath))
                {
                    OpenInitial(_folderPath);
                }
                else
                {
                    Initial();
                }
                return _saveFormalLogs; 
            }
        }

        public SaveConfig(string folderPath)
        {            
            _folderPath = folderPath;
        }

        private void Initial()
        {
            SaveFormalLogStruct saveFormalLog = new SaveFormalLogStruct()
            {
                DUTName ="",
                Impedance =0,
                Temperature =0,
                Cycle =0,
                Status ="",
                TimeStamp =""
            };
            _saveFormalLogs.Add(saveFormalLog);
        }

        private void OpenInitial(string folderPath)
        {            
            //讀取Log值
            _saveFormalLogs = OpenFolder<SaveFormalLogStruct>(folderPath, s => {
                return new SaveFormalLogStruct()
                {
                    ID = s[(int)SaveFormalLogEnum.ID],
                    DUTName = s[(int)SaveFormalLogEnum.DUTName],
                    Impedance = double.Parse(s[(int)SaveFormalLogEnum.Impedance]),
                    Temperature = double.Parse(s[(int)SaveFormalLogEnum.Temperature]),
                    Cycle = int.Parse(s[(int)SaveFormalLogEnum.Cycle]),
                    Status = s[(int)SaveFormalLogEnum.Status],
                    TimeStamp = s[(int)SaveFormalLogEnum.TimeStamp],
                    TimeTicks = double.Parse(s[(int)SaveFormalLogEnum.TimeTicks])
                };
            });
        }

        public enum SaveFormalLogEnum
        {
            ID = 0,
            DUTName = 1,
            Impedance = 2,
            Temperature = 3,
            Cycle = 4,
            Status = 5,
            TimeStamp = 6,
            TimeTicks = 7,
            END = 8
        }
    }
}
