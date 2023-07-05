using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class Mail
    {
        private Initial _initial;
        private readonly Timer _timerMail;
        private MailModel _mailModel;
        private ConterEnum _conterEnum = ConterEnum.Formal;        
        string state = "初";

        public Mail()
        {            
            _timerMail = new Timer() {
                Interval = 1000
            };
            _timerMail.Elapsed += UpdateMail;
        }
        private void UpdateMail(object sender, EventArgs e)
        {
            bool isPeriodic = false;           
            foreach (var temp in _initial.EmailConfigStruct.NotifyCondition.Split(','))
            {
                if(temp.Contains("Periodic"))
                {
                    isPeriodic = true;
                }
            }
            if(isPeriodic)
            {               
                if (DateTime.Now.Hour== 6 && (state.Contains("早") || state.Contains("初")))
                {
                    _mailModel.SendEmail("", _conterEnum);
                    state = "中";
                }
                else if(DateTime.Now.Hour == 12 && (state.Contains("中") || state.Contains("初")))
                {                    
                    _mailModel.SendEmail("", _conterEnum);
                    state = "晚";
                }
                else if(DateTime.Now.Hour == 18 && (state.Contains("晚") || state.Contains("初")))
                {
                    _mailModel.SendEmail("", _conterEnum);
                    state = "早";
                }
            }
        }
        public void Start(Initial initial, ConterEnum conterEnum)
        {
            _initial = initial;
            _conterEnum = conterEnum;
            _mailModel = new MailModel(_initial.EmailConfigStruct);
            _timerMail.Start();
        }
        public void Stop()
        {
            _timerMail.Stop();
        }
    }
}
