using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace USBscan.DAL
{
    public class WMI_dll
    {        
        private readonly string Commend = "SELECT * FROM Win32_PnPEntity";

        public PnPEntityInfo[] AllPnPEntities
        {
            get { return WhoPnPEntity(); }
        }

        private PnPEntityInfo[] WhoPnPEntity()
        {
            List<PnPEntityInfo> PnPEntities = new List<PnPEntityInfo>();
            // 枚舉即插即用設備實體
            ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher(Commend).Get();
            if (PnPEntityCollection != null)
            {
                foreach (ManagementObject Entity in PnPEntityCollection)
                {
                    String PNPDeviceID = Entity["PNPDeviceID"] as String;
                    String Caption = Entity["Caption"] as String;
                    Match match_ComPort = null;
                    Match match_VidPid = null;
                    Match match_VID = null;
                    Match match_PID = null;

                    if (!String.IsNullOrWhiteSpace(Caption))
                    {
                        match_ComPort = Regex.Match(Caption, @"COM\d*");
                    }
                    if (!String.IsNullOrWhiteSpace(PNPDeviceID))
                    {
                        match_VidPid = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}.?PID_[0-9|A-F]{4}");
                        match_VID = Regex.Match(PNPDeviceID, "VID_[0-9|A-F]{4}");
                        match_PID = Regex.Match(PNPDeviceID, "PID_[0-9|A-F]{4}");
                    }

                    if (match_VidPid.Success)
                    {
                        PnPEntityInfo Element;
                        Element.PNPDeviceID = PNPDeviceID;                          // 設備ID
                        Element.VID = match_VID.Value;                              // 廠商ID
                        Element.PID = match_PID.Value;                              // 產品ID  
                        Element.ComPort = match_ComPort.Value;                      // 設備標題(COM)

                        PnPEntities.Add(Element);
                    }
                }
            }
            if (PnPEntities.Count == 0) return null;
            else return PnPEntities.ToArray();
        }
        public struct PnPEntityInfo
        {
            public String PNPDeviceID;      // 設備ID
            public String VID;              // 廠商ID
            public String PID;              // 產品ID     
            public String ComPort;          // 設備標題(COM)        
        }
    }
}
