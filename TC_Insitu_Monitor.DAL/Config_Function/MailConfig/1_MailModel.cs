using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class MailModel
    {
        private MailMessage myMailMessage;
        private SmtpClient mySmtpClient;
        private EmailConfigStruct _emailConfigs;
        public MailModel(EmailConfigStruct emailConfigs)
        {
            _emailConfigs = emailConfigs;
        }

        public void SendEmail(string newBody,ConterEnum conterEnum)
        {
            string sourcePath = "";
            if (conterEnum == ConterEnum.PreTest)
            {
                sourcePath = Path.Combine(System.Environment.CurrentDirectory, "PreTest");
            }
            else if(conterEnum == ConterEnum.Formal)
            {
                sourcePath = Path.Combine(System.Environment.CurrentDirectory, "Log");
            }

            Task.Factory.StartNew(()=> {
                using (myMailMessage = new MailMessage())
                {
                    myMailMessage.From = new MailAddress(_emailConfigs.EmailFrom);
                    foreach (string EmailToUser in _emailConfigs.MailList)
                    {
                        myMailMessage.To.Add(EmailToUser);
                    }
                    myMailMessage.Subject = _emailConfigs.Sender;
                    myMailMessage.Body = _emailConfigs.Body+Environment.NewLine+ newBody;
                    // 若你的內容是HTML格式，則為True
                    myMailMessage.IsBodyHtml = false;
                    
                    //夾帶檔案        
                    if (Directory.Exists(sourcePath))
                    {
                        //獲取目錄下所有檔案
                        List<string> files = new List<string>(Directory.GetFiles(sourcePath));

                        foreach (string file in files)
                        {
                            FileInfo f = new FileInfo(file);
                            if(Convert.ToInt32(DateTime.Now.Ticks/10000000 - f.LastWriteTime.Ticks/10000000) < 60)
                            {
                                myMailMessage.Attachments.Add(new Attachment(f.FullName));
                            }
                        }
                    }

                    using (mySmtpClient = new SmtpClient(_emailConfigs.SMTPServer, _emailConfigs.PortNumber))
                    {
                        mySmtpClient.Credentials = new NetworkCredential(_emailConfigs.ID, _emailConfigs.Password);
                        mySmtpClient.EnableSsl = _emailConfigs.UsingTLS;
                        mySmtpClient.Send(myMailMessage);
                    }
                }
            });
        }

        public void SendEmailWithOutFile(string newBody)
        {
            Task.Factory.StartNew(() => {
                using (myMailMessage = new MailMessage())
                {
                    myMailMessage.From = new MailAddress(_emailConfigs.EmailFrom);
                    foreach (string EmailToUser in _emailConfigs.MailList)
                    {
                        myMailMessage.To.Add(EmailToUser);
                    }
                    myMailMessage.Subject = _emailConfigs.Sender;
                    myMailMessage.Body = _emailConfigs.Body + Environment.NewLine + newBody;
                    // 若你的內容是HTML格式，則為True
                    myMailMessage.IsBodyHtml = false;
                   
                    using (mySmtpClient = new SmtpClient(_emailConfigs.SMTPServer, _emailConfigs.PortNumber))
                    {
                        mySmtpClient.Credentials = new NetworkCredential(_emailConfigs.ID, _emailConfigs.Password);
                        mySmtpClient.EnableSsl = _emailConfigs.UsingTLS;
                        mySmtpClient.Send(myMailMessage);
                    }
                }
            });
        }
    }
}
