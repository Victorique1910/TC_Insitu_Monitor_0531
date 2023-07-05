using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TC_Insitu_Monitor.DAL
{
    public class DetectCom
    {
        /// <summary>
        /// 获取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public string[] GetHardwareInfo()
        {
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        try
                        {
                            if (!hardInfo.Properties["Caption"].IsLocal)
                            {
                                continue;
                            }
                            if (hardInfo.Properties["Caption"].Value.ToString().Contains("COM"))
                            {
                                strs.Add(hardInfo.Properties["Caption"].Value.ToString());
                            }
                        }
                        catch
                        {
                            //忽略错误
                            continue;
                        }
                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return new string[0];
            }
            finally
            { 
                strs = null; 
            }
        }
    }
}
