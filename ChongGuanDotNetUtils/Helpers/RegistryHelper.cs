using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanDotNetUtils.Helpers
{
    public class RegistryHelper
    {
        /// <summary>
        /// 读取指定名称的注册表的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRegistryData(RegistryKey root, string subkey, string name)
        {
            string registData = "";
            RegistryKey myKey = root.OpenSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            if (myKey != null)
            {
                registData = myKey.GetValue(name).ToString();
            }

            return registData;
        }

        /// <summary>
        /// 向注册表中写数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tovalue"></param> 
        public static void SetRegistryData(string subkey, string name, string value)
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey registryKey = localMachine.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            RegistryKey registryKey2 = null;

            if (registryKey != null)
            {
                string[] subKeyNames = registryKey.GetSubKeyNames();
                string[] array = subKeyNames;
                for (int i = 0; i < array.Length; i++)
                {
                    string a = array[i];
                    bool flag = a == subkey;
                    if (flag)
                    {
                        registryKey2 = registryKey.OpenSubKey(subkey, true);
                        break;
                    }
                }
            }

            bool flag2 = registryKey2 == null;
            if (flag2)
            {
                registryKey2 = registryKey.CreateSubKey(subkey);
            }
            registryKey2.SetValue(name, value);
        }

        /// <summary>
        /// 删除注册表中指定的注册表项
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteRegistry(RegistryKey root, string subkey, string name)
        {
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            subkeyNames = myKey.GetSubKeyNames();
            foreach (string aimKey in subkeyNames)
            {
                if (aimKey == name)
                    myKey.DeleteSubKeyTree(name);
            }
        }

        /// <summary>
        /// 判断指定注册表项是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsRegistryExist(RegistryKey root, string subkey, string name)
        {
            //bool exist = false;
            string[] subkeyNames;
            RegistryKey myKey = root.OpenSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl);
            if (myKey != null)
            {
                subkeyNames = myKey.GetSubKeyNames();
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
