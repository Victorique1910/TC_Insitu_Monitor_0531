using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class Interrupt
    {
        private int second = 0;
        readonly private MillisecondTimer thread_Interrupt = new MillisecondTimer();
        public delegate void ComputeInterrupt(int second);
        public ComputeInterrupt computeInterrupt;
        public Interrupt()
        {
            thread_Interrupt.Interval = 1000;
            thread_Interrupt.Tick += Compute;
        }

        private void Compute(object sender, EventArgs e)
        {
            second++;
            computeInterrupt(second);
        }

        public void Start()
        {
            thread_Interrupt.Start();
        }

        public void Close()
        {
            thread_Interrupt.Stop();            
        }
    }
}
