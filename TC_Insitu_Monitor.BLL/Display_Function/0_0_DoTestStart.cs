using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class DoTestStart:DoFormal
    {
        public delegate void ProcessTestStart(ConterEnum conterEnum);
        public ProcessTestStart processTestStart;

        public void DoTestStartReport(ConterEnum conterEnum,string filePath)
        {
            Task.Factory.StartNew(() => {
                //processTestStart(conterEnum);
                switch (conterEnum)
                {
                    case ConterEnum.PreTest:
                        CleanFolder(filePath);
                        break;
                    case ConterEnum.NinePoint:
                        CleanFolder(filePath);
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
