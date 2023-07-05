using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class DoLossTest:DoFormal
    {
        public delegate void DisplayLossTest(DataTable dataTable);
        public DisplayLossTest displayLossTest;

        protected DataConfigsFormalTestDataTable dataConfigsLossTestDataTable = new DataConfigsFormalTestDataTable();

        public void DoLossPrintData(Statuses statuses, ConterEnum conterEnum)
        {
            Task.Factory.StartNew(() => {
                if (statuses != null)
                {
                    statuses = Loss(statuses);                    
                    displayLossTest(dataConfigsLossTestDataTable.Update(statuses.DataConfigsStatuses));
                    display(data.Update(statuses.DataConfigsStatuses));
                    TestEnd.DoTestEndReport(conterEnum);
                }
            });
        }

        private Statuses Loss(Statuses statuses)
        {
            //輸入saveDictionarys
            foreach (var dataConfigsStatus in statuses.DataConfigsStatuses)
            {
                DataConfigsStatus data = statuses.SearchDataConfigsStatus(dataConfigsStatus.DataFormatStruct.ID);
                if(data != null)
                {
                    if (data.Configs.Type.Equals("Impedance"))
                    {
                        data.Configs.Loss = data.DataFormatStruct.VoltageH;
                    }
                    statuses.ScanDataConfigsStatus(dataConfigsStatus.DataFormatStruct.DUTName, data);
                }                
            }
            return statuses;
        }
    }
}
