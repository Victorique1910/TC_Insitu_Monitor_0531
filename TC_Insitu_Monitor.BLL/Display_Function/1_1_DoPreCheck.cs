using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;
using TC_Insitu_Monitor.DAL;

namespace TC_Insitu_Monitor.BLL
{
    public class DoPreCheck : DoFormal
    {
        public delegate void DisplayPreCheck(DataTable dataTable);
        public DisplayPreCheck displayPreCheck;

        protected DataConfigsFormalTestDataTable dataConfigsPreCheckDataTable = new DataConfigsFormalTestDataTable();

        public void DoPreCheckPrintData(Statuses statuses,ConterEnum conterEnum)
        {
            Task.Factory.StartNew(() => {
                if (statuses != null)
                {
                    statuses = PreCheck(statuses);
                    displayPreCheck(dataConfigsPreCheckDataTable.Update(statuses.DataConfigsStatuses));
                    display(data.Update(statuses.DataConfigsStatuses));
                    TestEnd.DoTestEndReport(conterEnum);
                }
            });
        }

        private Statuses PreCheck(Statuses statuses)
        {
            //輸入saveDictionarys
            foreach (var dataConfigsStatus in statuses.DataConfigsStatuses)
            {
                DataConfigsStatus data = statuses.SearchDataConfigsStatus(dataConfigsStatus.DataFormatStruct.ID);

                if (data != null)
                {
                    if (data.Configs.Type.Equals("Impedance"))
                    {
                        data.FormalTestStruct.InitialImpHighTemp = 0;
                    }                    
                    statuses.ScanDataConfigsStatus(dataConfigsStatus.DataFormatStruct.DUTName, data);
                }
            }
            return statuses;
        }
    }
}
