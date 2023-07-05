using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class USB_Interface
    {
        #region 常數        
        public const Int32 SERIAL_RECEIVED_TIME_INTERVAL = 10;  //接收数据间隔
        public const Int32 SERIAL_RECEIVED_LENGTH_MAX = 128;    //接收帧最大长度       
        #endregion
        #region 內部變數
        private readonly USB_Connentor usbConnentor;
        private readonly string _comport;

        private Queue queueSerialCacheReceived;
        private Queue queueSerialDataReceived;
        private Thread threadSeriaCacheReceived;
        private Thread threadSerialDataReceived;
        #endregion
        #region 外部變數
        public string Comport { get { return _comport; } }
        #endregion
        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="arrDataReceived">接收到的数据帧</param>
        public delegate void DataReceivedHandeler(object sender, byte[] arrDataReceived);
        /// <summary>
        /// 定义事件
        /// </summary>
        public event DataReceivedHandeler DataReceived;

        #region 初始化
        /// <summary>
        /// 初始化队列（线程安全）
        /// </summary>
        private void InitQueue()
        {
            //初始化接收缓存队列
            queueSerialCacheReceived = Queue.Synchronized(new Queue());

            //初始化接收数据队列
            queueSerialDataReceived = Queue.Synchronized(new Queue());
        }
        #endregion
        #region 建構子
        public USB_Interface(string comport)
        {
            _comport = comport;            
            usbConnentor = new USB_Connentor(_comport);        
            InitQueue();
            Initial();          
        }
        /// <summary>
        /// 接收錯誤接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            usbConnentor.OpenComport();
            Console.WriteLine("Received Error!!!");             
        }
        /// <summary>
        /// 接收stream資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] readBuffer = new byte[usbConnentor.SerialPort.BytesToRead];
                int count = usbConnentor.SerialPort.Read(readBuffer, 0, readBuffer.Length);
                lock (queueSerialCacheReceived.SyncRoot)
                {
                    for (int i = 0; i < count; i++)
                    {
                        queueSerialCacheReceived.Enqueue(readBuffer[i]);
                    }
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
            }
        }      
        /// <summary>
        /// 接收缓存线程函数
        /// </summary>
        private void ThreadSerialCacheReceivedFunction()
        {
            List<byte> listData = new List<byte>();
            while (usbConnentor.SerialPort.IsOpen)
            {
                int cacheLength = queueSerialCacheReceived.Count;
                if (cacheLength > 0)
                {
                    for (int i = 0; i < cacheLength; i++)
                    {
                        listData.Add((byte)queueSerialCacheReceived.Dequeue());
                        if (listData.Count >= SERIAL_RECEIVED_LENGTH_MAX)
                        {
                            lock (queueSerialCacheReceived.SyncRoot)
                            {
                                queueSerialDataReceived.Enqueue(listData.ToArray()); //转为帧队列
                            }
                            listData.Clear();
                        }
                    }
                }
                else if (listData.Count > 0)
                {
                    lock (queueSerialCacheReceived.SyncRoot)
                    {
                        queueSerialDataReceived.Enqueue(listData.ToArray()); //转为帧队列
                    }
                    listData.Clear();
                }
                Thread.Sleep(SERIAL_RECEIVED_TIME_INTERVAL);
            }           
            queueSerialCacheReceived.Clear();
        }
        /// <summary>
        /// 接收数据线程函数
        /// </summary>
        private void ThreadSerialDataReceivedFunction()
        {
            while (usbConnentor.SerialPort.IsOpen)
            {
                lock (queueSerialDataReceived.SyncRoot)
                {
                    if (queueSerialDataReceived.Count > 0)
                    {
                        byte[] byteData = (byte[])queueSerialDataReceived.Dequeue();
                        DataReceived?.Invoke(this, byteData);            //触发事件
                        continue;
                    }
                }
                Thread.Sleep(SERIAL_RECEIVED_TIME_INTERVAL);
            }
            lock (queueSerialDataReceived.SyncRoot)
            {
                queueSerialDataReceived.Clear();
            }
        }
        private void Initial()
        {
            usbConnentor.SerialPort.DataReceived += SerialPort_DataReceived;
            usbConnentor.SerialPort.ErrorReceived += SerialPort_ErrorReceived;
            threadSeriaCacheReceived = new Thread(new ThreadStart(ThreadSerialCacheReceivedFunction));
            threadSerialDataReceived = new Thread(new ThreadStart(ThreadSerialDataReceivedFunction));
            threadSeriaCacheReceived.Start();
            threadSerialDataReceived.Start();
        }
        #endregion
        #region 動態函式
        /// <summary>
        /// 寫入Byte
        /// </summary>
        public void WriteBytes(byte[] bytes)
        {
            usbConnentor.SerialPort.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        /// 開啟
        /// </summary>
        public void Open()
        {
            usbConnentor.OpenComport();
            Initial();
        }
        /// <summary>
		/// 關閉
		/// </summary>
		public void Close()
        {
            var t1 = Task.Factory.StartNew(() => {
                usbConnentor.CloseComport();
            });
            Task.WaitAll(new Task[] { t1 }, 1000);        
        }
        #endregion       
    }
}
