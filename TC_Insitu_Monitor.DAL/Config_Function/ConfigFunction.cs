﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TC_Insitu_Monitor.Model;

namespace TC_Insitu_Monitor.DAL
{
    public class ConfigFunction : IDisposable
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private bool bDisposed = false;

        /// <summary>
        /// 解構子。
        /// </summary>
        ~ConfigFunction()
        {
            Dispose(false);
        }

        /// <summary>
        /// 釋放資源(程式設計師呼叫)。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        /// <summary>
        /// 釋放資源(給系統呼叫的)。
        /// </summary>        
        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
            {
                return;
            }
            if (IsDisposing)
            {
                //補充：
                //這裡釋放具有實做 IDisposable 的物件(資源關閉或是 Dispose 等..)
                //ex: DataSet DS = new DataSet();
                //可在這邊 使用 DS.Dispose();
                //或是 DS = null;
                //或是釋放 自訂的物件。
                //因為我沒有這類的物件，故意留下這段 code ;若繼承這個類別，
                //可覆寫這個函式。
            }

            bDisposed = true;
        }

        /// <summary>
        /// 設定 KeyValue 值。
        /// </summary>
        /// <param name="IN_Section"></param>
        /// <param name="IN_Key"></param>
        /// <param name="IN_Value"></param>
        /// <param name="FilePath"></param>
        public void SetKeyValue(string FilePath, string IN_Section, string IN_Key, string IN_Value)
        {
            WritePrivateProfileString(IN_Section, IN_Key, IN_Value, FilePath);
        }

        /// <summary>
        /// 取得 Key 相對的 Value 值，若沒有則使用預設值(DefaultValue)。
        /// </summary>
        /// <param name="Section">Section。</param>
        /// <param name="Key">Key。</param>
        /// <param name="DefaultValue">DefaultValue。</param>        
        public string GetKeyValue(string FilePath,string Section, string Key, string DefaultValue)
        {
            StringBuilder sbResult;
            try
            {
                sbResult = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, "", sbResult, 255, FilePath);
                return (sbResult.Length > 0) ? sbResult.ToString() : DefaultValue;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
