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
    public class DoCalTest : DoFormal
    {        
        public delegate void DisplayCalTest(DataTable dataTable,double calCount);
        public DisplayCalTest displayCalTest;

        protected DataConfigsFormalTestDataTable dataConfigsCalTestDataTable = new DataConfigsFormalTestDataTable();

        public void DoCalPrintData(Statuses statuses, double calCount, ConterEnum conterEnum)
        {
            Task.Factory.StartNew(() => {
                if (statuses != null)
                {                    
                    DataTable temp = dataConfigsCalTestDataTable.Update(statuses.DataConfigsStatuses);                   
                    displayCalTest(temp, calCount);
                    display(data.Update(statuses.DataConfigsStatuses));
                    TestEnd.DoTestEndReport(conterEnum);
                }
            });
        }
    }
}
