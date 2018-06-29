using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public static class AccountInfo
    {
        private const string kAppKey = "appKey";
        private const string kDeveloperKey = "developerKey";
        private const string kCloudUrl = "cloudUrl";
        private const string kCapkey = "capKey";
        private const string kRealTime = "realtime";
        private const string kAddPunc = "addpunc";

        public static string GetAppKeyFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("appKey") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetDeveloperKeyFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("developerKey") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetCloudUrlFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("cloudUrl") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetCapkeyFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("capKey") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetRealTimeFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("realtime") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetkAddPuncFromFile(string file_path)
        {
            StreamReader streamReader = File.OpenText(file_path);
            for (string str1 = streamReader.ReadLine(); str1 != null; str1 = streamReader.ReadLine())
            {
                if (str1.IndexOf('#') != 0 && str1.IndexOf("addpunc") != -1)
                {
                    string str2 = str1.Trim();
                    return str2.Substring(str2.IndexOf("=") + 1);
                }
            }
            return (string)null;
        }

        public static string GetAccountInfoFromFile(string file_path)
        {
            string str1 = "";
            StreamReader streamReader = File.OpenText(file_path);
            for (string str2 = streamReader.ReadLine(); str2 != null; str2 = streamReader.ReadLine())
            {
                if (str2.IndexOf('#') != 0 && (str2.IndexOf("appKey") != -1 || str2.IndexOf("developerKey") != -1 || str2.IndexOf("cloudUrl") != -1))
                {
                    string str3 = str2.Trim();
                    string str4 = str3.Substring(str3.IndexOf("=") + 1);
                    if (str4 == null || str4.Length == 0)
                        return (string)null;
                    str1 = str1 + str3 + ",";
                }
            }
            return str1;
        }
    }
}
