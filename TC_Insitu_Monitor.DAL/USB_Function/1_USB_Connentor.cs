using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.DAL
{
    public class USB_Connentor
    {
        readonly private string    _comport  = "COM7";
        readonly private int       _baud     = 115200;
        readonly private int       _dataBits = 8;
        readonly private StopBits  _stopBits = StopBits.One;
        public SerialPort SerialPort;       
        
        public USB_Connentor(string comport)
        {
            _comport = comport;
            OpenComport();
        }   
        
        public void OpenComport()
        {
            SerialPort = new SerialPort()
            {
                PortName = _comport,
                BaudRate = _baud,
                DataBits = _dataBits,
                StopBits = _stopBits
            };
            try
            {
                if(!SerialPort.IsOpen)
                {
                    try
                    {
                        Console.WriteLine("IsOpen "+ SerialPort.IsOpen);
                        SerialPort.BaseStream.Dispose();
                    }
                    catch 
                    {
                        Console.WriteLine("SerialPort.IsOpen");
                    }
                    SerialPort.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Open Error!!! " + ex.Message);
            }
        }

        public void CloseComport()
        {
            try
            {
                SerialPort.Close();               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
