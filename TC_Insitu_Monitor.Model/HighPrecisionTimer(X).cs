using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.Model
{
    public sealed class MillisecondTimer
    {
        //字段*******************************************************************
        private static TimerCaps caps;
        private int interval;
        private bool isRunning;
        readonly private int resolution;
        readonly private TimerCallback timerCallback;
        private int timerID;
        //属性*******************************************************************        
        public int Interval
        {
            get
            {
                return this.interval;
            }
            set
            {
                if ((value < caps.periodMin) || (value > caps.periodMax))
                {
                    throw new Exception("超出範圍！");
                }
                this.interval = value;
            }
        }       
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }
        //委託*********************************************************************
        private delegate void TimerCallback(int id, int msg, int user, int param1, int param2); // timeSetEvent回調函數
        //事件*********************************************************************
        public event EventHandler Disposed;  // 这个事件实现了IComponet接口
        public event EventHandler Tick;
        //回調*********************************************************************
        private void TimerEventCallback(int id, int msg, int user, int param1, int param2)
        {
            Tick?.Invoke(this, null);  // 引發事件
        }
        //構造函式***************************************************************
        static MillisecondTimer()
        {
            timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
        }
        public MillisecondTimer()
        {
            this.interval = caps.periodMin;    // 
            this.resolution = caps.periodMin;  //
            this.isRunning = false;
            this.timerCallback = new TimerCallback(this.TimerEventCallback);
        }
        ~MillisecondTimer()
        {
            timeKillEvent(this.timerID);
        }
        //方法*******************************************************************        
        public void Start()
        {
            if (!isRunning)
            {
                timerID = timeSetEvent(this.interval, this.resolution, this.timerCallback, 0, 1); // 间隔性地运行
                if (timerID == 0)
                {
                    throw new Exception("無法啟動計時器");
                }
                isRunning = true;
            }
        }
        public void Stop()
        {
            if (isRunning)
            {
                timeKillEvent(this.timerID);
                isRunning = false;
            }
        }         
        /// <summary>
        /// 實現IDisposable接口
        /// </summary>
        public void Dispose()
        {
            timeKillEvent(this.timerID);
            GC.SuppressFinalize(this);
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        //内部函数******************************************************************
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerCallback callback, int user, int mode);
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);
        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);        
        //内部類型******************************************************************       
        /// <summary>
        /// 定時器的分辨率(resolution)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct TimerCaps
        {
            public int periodMin;
            public int periodMax;
        }
    }
}
