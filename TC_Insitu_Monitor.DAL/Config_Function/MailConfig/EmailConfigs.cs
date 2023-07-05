using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class EmailConfigs: ConfigFunction
    {
        private EmailConfigStruct _emailConfigStruct;
        readonly private string _filePath = string.Empty;
        public EmailConfigStruct EmailConfigStruct
        {
            get
            {
                if (File.Exists(_filePath))
                {
                    OpenInitial(_filePath);
                }
                else
                {
                    Initial();
                }
                return _emailConfigStruct;
            }
            set
            {
                if (File.Exists(_filePath))
                {
                    
                    SaveInitial(_filePath,value);
                }
                else
                {                  
                    SaveInitial(Path.Combine(System.Environment.CurrentDirectory, "EmailConfigs.ini"), value);
                }               
            }
        }

        public EmailConfigs(string filePath): base ()
        {
            _filePath = filePath;
        }

        readonly private int _portNumber = 587;
        readonly private string _projectName = "Default_test";
        readonly private string _body = "Dear: This is In-Situ Monitor Report.";

        private void Initial()
        {
            _emailConfigStruct = new EmailConfigStruct()
            {
                NotifyCondition = "",
                SMTPServer = "smtp.office365.com",
                PortNumber = _portNumber,
                ID = "tw.sys.dqa1_in-situ@usiglobal.com",
                EmailFrom = "tw.sys.dqa1_in-situ@usiglobal.com",                
                Password = "Usidq@a123",
                Sender = "In-Situ Monitor Report",
                UsingTLS = true,
                ProjectName = _projectName,
                Body = _body,
                MailList = new List<string>() { "george_kao@usiglobal.com" },
                FileName = Path.Combine(System.Environment.CurrentDirectory, "Log")
            };
        }

        private void OpenInitial(string filePath)
        {
            List<string> mailLists = new List<string>();
            foreach (var mailList in GetKeyValue(filePath, "EmailConfigStruct", "MailList", "george_kao@usiglobal.com").Split('+'))
            {
                mailLists.Add(mailList);
            }

            _emailConfigStruct = new EmailConfigStruct()
            {
                NotifyCondition =            GetKeyValue(filePath, "EmailConfigStruct", "NotifyCondition", ""),
                SMTPServer      =            GetKeyValue(filePath, "EmailConfigStruct", "SMTPServer"     , "smtp.office365.com"),
                PortNumber      = _portNumber,
                ID              =            GetKeyValue(filePath, "EmailConfigStruct", "ID"             , "tw.sys.dqa1_in-situ@usiglobal.com"),
                EmailFrom       =            GetKeyValue(filePath, "EmailConfigStruct", "ID"             , "tw.sys.dqa1_in-situ@usiglobal.com"),
                Password        =            GetKeyValue(filePath, "EmailConfigStruct", "Password"       , "Usidq@a123"),
                Sender          =            GetKeyValue(filePath, "EmailConfigStruct", "Sender"         , "In-Situ Monitor Report"),
                UsingTLS        = bool.Parse(GetKeyValue(filePath, "EmailConfigStruct", "UsingTLS"       , "UsingTLS")),
                ProjectName     = _projectName,
                Body            = _body,
                MailList        = mailLists,
                FileName        =            GetKeyValue(filePath, "EmailConfigStruct", "FileName"       , Path.Combine(System.Environment.CurrentDirectory, "Log"))
            };
        }

        private void SaveInitial(string filePath, EmailConfigStruct emailConfigStruct)
        {
            string mailLists = "";
            for(int count=0; count< emailConfigStruct.MailList.Count; count++)
            {
                mailLists += emailConfigStruct.MailList[count];

                if(count < emailConfigStruct.MailList.Count-1)
                {
                    mailLists += "+";
                }
            }

            SetKeyValue(filePath, "EmailConfigStruct", "NotifyCondition", emailConfigStruct.NotifyCondition);
            SetKeyValue(filePath, "EmailConfigStruct", "SMTPServer"     , emailConfigStruct.SMTPServer);
            SetKeyValue(filePath, "EmailConfigStruct", "ID"             , emailConfigStruct.ID);
            SetKeyValue(filePath, "EmailConfigStruct", "Password"       , emailConfigStruct.Password);
            SetKeyValue(filePath, "EmailConfigStruct", "Sender"         , emailConfigStruct.Sender);
            SetKeyValue(filePath, "EmailConfigStruct", "UsingTLS"       , emailConfigStruct.UsingTLS.ToString());
            SetKeyValue(filePath, "EmailConfigStruct", "MailList"       , mailLists);
            SetKeyValue(filePath, "EmailConfigStruct", "FileName"       , emailConfigStruct.FileName);
        }
    }
}
