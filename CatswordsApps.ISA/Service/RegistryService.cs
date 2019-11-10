using CatswordsApps.ISA.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace CatswordsApps.ISA.Service
{
    public static class RegistryService
    {
        public static List<BundleModel> GetAllBundles()
        {
            List<BundleModel> items = new List<BundleModel>();

            Dictionary<string, RegistryKey> regKeys = new Dictionary<string, RegistryKey>();
            regKeys.Add("CurrentUser", Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"));
            regKeys.Add("LocalMachine", Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"));
            regKeys.Add("LocalMachineWow64", Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall"));

            foreach (KeyValuePair<string, RegistryKey> rk in regKeys)
            {
                foreach (string skName in rk.Value.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.Value.OpenSubKey(skName))
                    {
                        try
                        {
                            items.Add(new BundleModel
                            {
                                Origin = rk.Key,
                                ResourceName = (string)sk.GetValue("ResourceName"),
                                Default = (string)sk.GetValue(null),
                                InstallDate = (string)sk.GetValue("InstallDate"),
                                InstallLocation = (string)sk.GetValue("InstallLocation"),
                                Publisher = (string)sk.GetValue("Publisher"),
                                DisplayIcon = (string)sk.GetValue("DisplayIcon"),
                                DisplayName = (string)sk.GetValue("DisplayName"),
                                DisplayVersion = (string)sk.GetValue("DisplayVersion"),
                                HelpLink = (string)sk.GetValue("HelpLink"),
                                UninstallString = (string)sk.GetValue("UninstallString")
                            });
                        }
                        catch (Exception)
                        {
                            // pass
                        }
                    }
                }
            }

            return items;
        }

        public static string GetValueByKeyName_HKLM(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public static string GetOSVersion()
        {
            string ProductName = GetValueByKeyName_HKLM(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = GetValueByKeyName_HKLM(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "");
            }
            return "";
        }

        public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }
    }
}
