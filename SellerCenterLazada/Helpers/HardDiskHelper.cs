using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SellerCenterLazada.Helpers
{
    public class HardDiskHelper
    {
        
        public static string GetHardDiskSerials()
        {
            List<string> hdCollection = new List<string>();
            var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                if("IDE".Equals(wmi_HD["InterfaceType"]?.ToString().ToUpper())) 
                    hdCollection.Add(wmi_HD["SerialNumber"].ToString()?.Trim());
            }
            return string.Join("-", hdCollection);
        }
        public static string GenerateKey()
        {
            return CryptoHelper.Encrypt(GetHardDiskSerials());
        }
    }
}
