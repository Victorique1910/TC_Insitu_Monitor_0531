using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TC_Insitu_Monitor.DAL;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.BLL
{
    public class DoFormal
    {
        public DoTestEnd TestEnd = new DoTestEnd();
        public delegate void Display(DataTable dataTable);
        public Display display;

        protected DataConfigsFormalTestDataTable data = new DataConfigsFormalTestDataTable();
        public void DoFormalDisplayData(Statuses statuses)
        {
            Task.Factory.StartNew(()=> {
                if(statuses!=null)
                {                    
                    display(data.Update(statuses.DataConfigsStatuses));                    
                }               
            });
        }
        public void CopyFolder(string sourceFolderPath, string targetFolderPath)
        {
            if (Directory.Exists(sourceFolderPath))
            {
                if (!Directory.Exists(targetFolderPath))
                {
                    Directory.CreateDirectory(targetFolderPath);
                }

                DirectoryInfo di = new DirectoryInfo(sourceFolderPath);
                foreach (FileInfo nextFile in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (Path.GetExtension(nextFile.Name) == ".csv")
                    {
                        File.Copy(nextFile.FullName, Path.Combine(targetFolderPath, nextFile.Name));
                    }
                }
            }
        }

        public void CleanFolder(string sourceFolderPath)
        {
            if (Directory.Exists(sourceFolderPath))
            {
                DirectoryInfo di = new DirectoryInfo(sourceFolderPath);
                foreach (FileInfo nextFile in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (Path.GetExtension(nextFile.Name) == ".csv")
                    {
                        File.Delete(nextFile.FullName);
                    }
                }
            }
        }
    }
}
