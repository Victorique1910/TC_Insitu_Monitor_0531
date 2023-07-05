using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.DAL
{
    public class FileFunction
    {
        public List<Tout> OpenFolder<Tout>(string folderPath, Func<string[], Tout> func)
        {
            List<Tout> outputStructs = new List<Tout>();
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                foreach (FileInfo nextFile in di.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (Path.GetExtension(nextFile.Name) == ".csv") //Log
                    {
                        using (var fs = new FileStream(nextFile.FullName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
                        {
                            using (var sr = new StreamReader(fs, System.Text.Encoding.Default))
                            {
                                string temps = "";
                                bool isFirst = true;
                                while ((temps = sr.ReadLine()) != null)
                                {
                                    if (isFirst)
                                    {
                                        isFirst = false;
                                    }
                                    else
                                    {
                                        string[] temp = temps.Split(',');
                                        Tout outputStruct = func.Invoke(temp);                                       
                                        outputStructs.Add(outputStruct);                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return outputStructs;
        }

        public List<Tout> OpenFile<Tout>(string filePath, Func<string[], Tout> func)
        {
            List<Tout> outputStructs = new List<Tout>();

            if(File.Exists(filePath))
            {
                using (var fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
                {
                    using (var sr = new StreamReader(fs, System.Text.Encoding.Default))
                    {
                        string temps = "";
                        bool isFirst = true;
                        while ((temps = sr.ReadLine()) != null)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                            }
                            else
                            {
                                string[] temp = temps.Split(',');
                                Tout outputStruct = func.Invoke(temp);                                
                                outputStructs.Add(outputStruct);
                            }
                        }
                    }
                }
            }

            return outputStructs;
        }

        public void SaveFile(string filePath, DataTable dataTable)
        {
            using (var fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    string data = "";
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        data += dataTable.Columns[i].ColumnName.ToString();
                        if (i < dataTable.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        data = "";
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            string str = dataTable.Rows[i][j].ToString();
                            str = str.Replace("\"", "\"\"");
                            if (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"))
                            {
                                str = string.Format("\"{0}\"", str);
                            }
                            data += str;

                            if (j < dataTable.Columns.Count - 1)
                            {
                                data += ",";
                            }
                        }                        
                        sw.WriteLine(data);
                    }
                }
            }
        }        
    }
}
