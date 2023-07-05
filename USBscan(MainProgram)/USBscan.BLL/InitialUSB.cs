using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.BLL;

namespace USBscan.BLL
{    
    public class InitialUSB
    {
        public InitialUSB(string folderPath)
        {
            initial = new Initial(folderPath);
        }
       
        private readonly Initial initial;
        private readonly List<string> _data = new List<string>();
        public List<string> Data { get {
                GetData();
                return _data; } }

        public void Clean()
        {
            DataTable dataTable = initial.DataTable;
            for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
            {
                dataTable.Rows[rowCount]["COMPort"] = "";
            }
            initial.Save(dataTable);
        }

        public void Save(string board,string comport)
        {
            DataTable dataTable = initial.DataTable;
            for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
            {
                if (dataTable.Rows[rowCount]["Board"].ToString() == board)
                {
                    dataTable.Rows[rowCount]["COMPort"] = comport;
                }
            }
            initial.Save(dataTable);                    
        }
       
        private void GetData()
        {
            foreach(var board in initial.BoardDictionary)
            {
                _data.Add(board.Key);
            }
        }
    }
}
