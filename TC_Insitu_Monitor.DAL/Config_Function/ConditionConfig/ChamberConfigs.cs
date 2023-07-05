using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class ChamberConfigs:ConfigFunction
    {
        readonly private string _filePath = string.Empty;
        private Chamber _chamber;       
        public Chamber Chamber
        {
            get
            {
                if (File.Exists(_filePath))
                {
                    OpenInitial(_filePath);
                }
                else
                {
                    Initial();
                }
                return _chamber;
            }
            set
            {
                if (File.Exists(_filePath))
                {

                    SaveInitial(_filePath, value);
                }
                else
                {
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "ChamberConfigs.ini"), value);
                }
            }
        }
        public ChamberConfigs(string filePath):base()
        {
            _filePath = filePath;
        }

        private void Initial()
        {
            _chamber = new Chamber()
            {
                ChamberID = "testChamberID"
            };
        }

        private void OpenInitial(string filePath)
        {
            _chamber = new Chamber()
            {
                ChamberID = GetKeyValue(filePath, "Chamber", "ChamberID", "")
            };
        }

        private void SaveInitial(string filePath, Chamber chamber)
        {
            SetKeyValue(filePath, "Chamber", "ChamberID", chamber.ChamberID);
        }
    }
}
