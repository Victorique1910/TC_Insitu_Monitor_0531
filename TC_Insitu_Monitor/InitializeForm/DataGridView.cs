using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC_Insitu_Monitor
{
    public partial class TC_Insitu_Monitor
    {
        private void InitializeFormalTestDataTable()
        {
            DataGridView_ImpedanceFornt.DataSource = initial.FormalTestDataTable;
            DataGridView_TemperatureFornt.DataSource = initial.FormalTestDataTable;
        }

        private void InitializedataGridView(DataGridView dataGridView)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView.DataSource];
            cm.SuspendBinding();
            for (int count=0;count< dataGridView.Columns.Count;count++)
            {
                dataGridView.Columns[count].Visible = false;
            }
            cm.ResumeBinding();
        }

        private void ShowdataGridView(DataGridView dataGridView, string colName, string name)
        {            
            for (int rowCow = 0; rowCow < dataGridView.Rows.Count - 1; rowCow++)
            {                
                if (dataGridView.Rows[rowCow].Cells[colName].Value.ToString() == name)
                {                    
                    dataGridView.Rows[rowCow].Visible = true;
                }
            }           
        }

        private void HideAlldataGridView(DataGridView dataGridView)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView.DataSource];
            cm.SuspendBinding();
            for (int rowCow = 0; rowCow < dataGridView.Rows.Count - 1; rowCow++)
            {
                dataGridView.Rows[rowCow].Visible = false;
            }
            cm.ResumeBinding();
        }
    }
}
