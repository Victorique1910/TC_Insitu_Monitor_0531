using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using USBscan.DAL;

namespace USBscan.BLL
{
    public class USBwatcher
    {
        private readonly Timer timers;
        private readonly WMI_dll wmi = new WMI_dll();
        public delegate void USBEventHandle(object sender, USBEventArgs e);
        public event USBEventHandle USBed;
        private readonly List<string> comports = new List<string>();
        private string _board;

        //建構子
        public USBwatcher(USBEventHandle usbHandle)
        {
            foreach (var port in wmi.AllPnPEntities)
            {                
                if (port.ComPort.Length > 0)
                {
                    comports.Add(port.ComPort);
                }
            }
            USBed += usbHandle;

            timers = new Timer() 
            {
                Interval = 1000,
                AutoReset = true
            };           
            timers.Elapsed += USBEventHandler;
        }        
        private void USBEventHandler(object sender, ElapsedEventArgs e)
        {
            string ComPort = "";
            string[] comport = comports.ToArray();
            foreach (var port in wmi.AllPnPEntities)
            {
                bool isExist = false;
                if (port.ComPort.Length > 0)
                {
                    for (int count = 0; count < comport.Length; count++)
                    {
                        if (port.ComPort.Equals(comport[count]))
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        //事件觸發
                        ComPort = port.ComPort;
                        comports.Add(port.ComPort);
                    }
                }
            }

            if(ComPort.Length>0)
            {
                Console.WriteLine(ComPort);
                USBed?.Invoke(this, new USBEventArgs(ComPort,_board));
            }
        }
        public void Start(string board)
        {
            _board = board;
            timers.Start();
        }
      
        public void Stop()
        {
            timers.Stop();
        }
    }

    public class USBEventArgs : EventArgs
    {
        public USBEventArgs(string data,string board)
        {
            Data = data;
            Board = board;
        }
        public string Data { get; }
        public string Board { get; }
    }
}
