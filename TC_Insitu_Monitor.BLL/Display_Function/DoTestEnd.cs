using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class DoTestEnd
    {
        public delegate void ProcessTestEnd(ConterEnum conterEnum);
        public ProcessTestEnd processTestEnd;

        public void DoTestEndReport(ConterEnum conterEnum)
        {
            Task.Factory.StartNew(()=> {
                processTestEnd(conterEnum);
            });
        }
    }
}
