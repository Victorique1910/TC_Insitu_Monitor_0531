using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using USBscan.BLL;

namespace USBscan
{
    public partial class TC_PreCheck_COMport : Form
    {
        private int count = 0;
        private readonly string exeFolderName = Path.Combine(Environment.CurrentDirectory, "TC_Insitu_Monitor.exe");
        private readonly string folderPath = Path.Combine(Environment.CurrentDirectory, "Configs");       
        private readonly AutoResetEvent mre = new AutoResetEvent(false);
        private readonly List<string> comports = new List<string>();
        private InitialUSB initial;
        private USBwatcher watcher;

        public TC_PreCheck_COMport()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            watcher = new USBwatcher(USBHandle);
            initial = new InitialUSB(folderPath);
            initial.Clean();
            Main();           
        }
       
        private void Main()
        {
            Task.Factory.StartNew(() =>
            {
                //匯入Initial**********************
                List<string> data = initial.Data;
                //********************************
                while (count < data.Count) //判斷資料最大數
                {
                    try
                    {
                        Console.WriteLine(count);
                        
                        this.Invoke((Action)(() =>
                        {
                            //顯示板子資料
                            Label_ShowInformation.Text = "Board"+ data[count];
                            //等待按鈕或USB事件觸發
                            Label_ShowStatus.Text = "請輸入SN";
                        }));
                        watcher.Start(data[count]);
                        
                        while (!mre.WaitOne()) { }
                        count++;
                        watcher.Stop();
                        Thread.Sleep(500);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        Finish();
                    }
                }                
                Finish();                
            });            
        }

        private void TextBox_SN_TextChanged(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
            {
                Label_ShowStatus.Text = "請插入USB";
            }));
        }

        private void USBHandle(object sender, USBEventArgs e)
        {
            bool isSet = true;
            mre.Set();
            this.Invoke((Action)(() =>
            {
                if(TextBox_SN.Text == e.Board)
                {
                    Label_ShowInformation.Text = e.Data;
                    comports.Add(e.Data);
                    Label_ShowStatus.Text = "等待中.....";
                }
                else
                {
                    Label_ShowInformation.Text = "SN與設定不符";
                    isSet = false;
                }               
            }));
            if(isSet)
            {
                //存檔
                initial.Save(e.Board, e.Data);
            }
            else
            {
                Thread.Sleep(1000);
                Environment.Exit(0);
            }            
            mre.Reset();           
        }

        private void Button_Jump_Click(object sender, EventArgs e)
        {
            Button_Jump.Enabled = false;
            mre.Set();
            mre.Reset();
            Button_Jump.Enabled = true;
        }

        private void Button_End_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Finish()
        {
            //執行TC************************
            string commend = "";
            for(int comportsCount=0; comportsCount< comports.Count; comportsCount++)
            {
                commend += comports[comportsCount]+" ";               
            }
            
            if (File.Exists(exeFolderName))
            {
                Process p = new Process();
                p.StartInfo.FileName = exeFolderName;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;  //接受來自呼叫程式的輸入資訊
                p.StartInfo.RedirectStandardOutput = true; //由呼叫程式獲取輸出資訊
                p.StartInfo.RedirectStandardError = true;  //重定向標準錯誤輸出
                p.StartInfo.CreateNoWindow = false;        //不跳出cmd視窗
                p.StartInfo.Arguments = commend;
                p.Start();                                 // 啟動程式
            }
            else
            {
                MessageBox.Show("沒有此資料");
                Environment.Exit(0);
            }
            Environment.Exit(0);
        }
    }
}
