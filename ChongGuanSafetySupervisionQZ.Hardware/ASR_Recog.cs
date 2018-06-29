using SinoVoice.HciCloud.Api;
using SinoVoice.HciCloud.Api.Asr;
using SinoVoice.HciCloud.Common;
using SinoVoice.HciCloud.Common.Asr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class ASR_Recog
    {
        private static int vad_check_audiodata_len = 3200000;
        private static int cur_len = 0;
        private static string capkey_;
        private static int session_id_;
        private static int session_id_novad;
        private static string realtime_;
        private static string addpunc_;
        private static byte[] vad_check_audiodata;

        public static int Init()
        {
            string str1 = "conf/AccountInfo.txt";
            string capkeyFromFile = AccountInfo.GetCapkeyFromFile(str1);
            if (capkeyFromFile == null || capkeyFromFile.Length == 0)
            {
                Console.WriteLine("是不是忘记了在" + str1 + "中填写capkey了？");
                return -1;
            }
            else
            {
                string config1 = ASR_Recog.GetConfig(str1);
                if (config1 == null || config1.Length == 0)
                {
                    Console.WriteLine("获取配置失败");
                    return -1;
                }
                else
                {
                    HciErrorCode hciErrorCode1 = HciCloudSys.HciInit(config1);
                    if (hciErrorCode1 == HciErrorCode.HCI_ERR_NONE)
                    {
                        Console.WriteLine("HciCloudSys HciInit Success");
                        if (!ASR_Recog.CheckAndUpdataAuth())
                        {
                            int num = (int)HciCloudSys.HciRelease();
                            return -1;
                        }
                        else if (!ASR_Recog.IsCapkeyEnable(capkeyFromFile))
                        {
                            Console.WriteLine("capkey " + capkeyFromFile + " is not enable\n");
                            int num = (int)HciCloudSys.HciRelease();
                            return -1;
                        }
                        else
                        {
                            HciErrorCode hciErrorCode2 = HciCloudAsr.HciAsrInit("dataPath=data" + ",initCapkeys=" + capkeyFromFile);
                            if ((uint)hciErrorCode2 > 0U)
                            {
                                Console.WriteLine("HciAsrInit failed return " + (object)hciErrorCode2);
                                int num = (int)HciCloudSys.HciRelease();
                                return -1;
                            }
                            else
                            {
                                Console.WriteLine("HciAsrInit success");
                                ASR_Recog.addpunc_ = AccountInfo.GetkAddPuncFromFile(str1);
                                ASR_Recog.capkey_ = capkeyFromFile;
                                string str2 = "capkey=" + capkeyFromFile + ",realtime=";
                                ASR_Recog.realtime_ = AccountInfo.GetRealTimeFromFile(str1);
                                if (ASR_Recog.realtime_ != "no")
                                {
                                    string config2 = str2 + ASR_Recog.realtime_ + ",audioformat=pcm16k16bit,vadThreshold=10";
                                    if (ASR_Recog.realtime_ == "yes")
                                        config2 = config2 + ",encode=speex,vadHead=0,vadTail=500,vadSeg=500";
                                    HciErrorCode hciErrorCode3 = HciCloudAsr.HciAsrSessionStart(config2, ref ASR_Recog.session_id_);
                                    if (hciErrorCode3 == HciErrorCode.HCI_ERR_NONE)
                                    {
                                        Console.WriteLine("HciCloudAsr HciAsrSessionStart Success");
                                    }
                                    else
                                    {
                                        Console.WriteLine("HciCloudAsr HciAsrSessionStart 出错: " + (object)hciErrorCode3);
                                        return -1;
                                    }
                                }
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("HciCloudSys HciInit 出错: " + (object)hciErrorCode1);
                        return -1;
                    }
                }
            }
        }

        public static string GetRealtime()
        {
            return ASR_Recog.realtime_;
        }

        public static void Release()
        {
            HciErrorCode hciErrorCode1 = HciCloudAsr.HciAsrSessionStop(ASR_Recog.session_id_);
            if (hciErrorCode1 == HciErrorCode.HCI_ERR_NONE)
                Console.WriteLine("HciCloudAsr HciAsrSessionStop Success");
            else
                Console.WriteLine("HciCloudAsr HciAsrSessionStop 出错: " + (object)hciErrorCode1);
            HciErrorCode hciErrorCode2 = HciCloudAsr.HciAsrSessionStop(ASR_Recog.session_id_novad);
            if (hciErrorCode2 == HciErrorCode.HCI_ERR_NONE)
                Console.WriteLine("HciCloudAsr HciAsrSessionStop Success");
            else
                Console.WriteLine("HciCloudAsr HciAsrSessionStop 出错: " + (object)hciErrorCode2);
            HciErrorCode hciErrorCode3 = HciCloudAsr.HciAsrRelease();
            hciErrorCode3 = HciCloudSys.HciRelease();
        }

        public static bool CheckAndUpdataAuth()
        {
            long num = (DateTime.Now.ToFileTimeUtc() - new DateTime(1970, 1, 1, 0, 0, 0).ToFileTimeUtc()) * 100L / 1000000000L;
            long expire_time = 0L;
            HciErrorCode authExpireTime = HciCloudSys.HciGetAuthExpireTime(ref expire_time);
            switch (authExpireTime)
            {
                case HciErrorCode.HCI_ERR_NONE:
                    if (expire_time < num)
                    {
                        HciErrorCode hciErrorCode = HciCloudSys.HciCheckAuth();
                        if (hciErrorCode == HciErrorCode.HCI_ERR_NONE)
                        {
                            Console.WriteLine("check auth success");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("check auth failed return" + (object)hciErrorCode);
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("check auth success");
                        break;
                    }
                case HciErrorCode.HCI_ERR_SYS_AUTHFILE_INVALID:
                    HciErrorCode hciErrorCode1 = HciCloudSys.HciCheckAuth();
                    if (hciErrorCode1 == HciErrorCode.HCI_ERR_NONE)
                    {
                        Console.WriteLine("check auth success");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("check auth failed return" + (object)hciErrorCode1);
                        return false;
                    }
                default:
                    Console.WriteLine("check auth failed return" + (object)authExpireTime);
                    return false;
            }
            return true;
        }

        public static bool IsCapkeyEnable(string capkey)
        {
            CapabilityList capability_result = new CapabilityList();
            HciErrorCode capabilityList = HciCloudSys.HciGetCapabilityList((string)null, ref capability_result);
            if ((uint)capabilityList > 0U)
            {
                Console.WriteLine("hci_get_capability_list failed return " + (object)capabilityList);
                return false;
            }
            else
            {
                bool flag = false;
                foreach (CapabilityItem capabilityItem in (IEnumerable<CapabilityItem>)capability_result.CapabilityItemList)
                {
                    if (capkey == capabilityItem.CapKey)
                    {
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
        }

        private static string GetConfig(string account_info_path)
        {
            InitParam initParam = new InitParam();
            string appKeyFromFile = AccountInfo.GetAppKeyFromFile(account_info_path);
            if (appKeyFromFile == null || appKeyFromFile.Length == 0)
            {
                Console.WriteLine("获取appkey失败,是不是忘了在" + account_info_path + "填写appkey了");
                return (string)null;
            }
            else
            {
                string developerKeyFromFile = AccountInfo.GetDeveloperKeyFromFile(account_info_path);
                if (developerKeyFromFile == null || developerKeyFromFile.Length == 0)
                {
                    Console.WriteLine("获取developerKey失败,是不是忘在" + account_info_path + "填写developerKey了");
                    return (string)null;
                }
                else
                {
                    string cloudUrlFromFile = AccountInfo.GetCloudUrlFromFile(account_info_path);
                    if (cloudUrlFromFile == null || cloudUrlFromFile.Length == 0)
                    {
                        Console.WriteLine("获取cloudUrl失败,是不是忘在" + account_info_path + "填写cloudUrl了");
                        return (string)null;
                    }
                    else
                    {
                        initParam.AddParam(InitParam.PARAM_KEY_APP_KEY.ToLower(), appKeyFromFile);
                        initParam.AddParam(InitParam.PARAM_KEY_DEVELOPER_KEY.ToLower(), developerKeyFromFile);
                        initParam.AddParam(InitParam.PARAM_KEY_CLOUD_URL.ToLower(), cloudUrlFromFile);
                        initParam.AddParam(InitParam.PARAM_KEY_AUTH_PATH.ToLower(), "data");
                        initParam.AddParam(InitParam.PARAM_KEY_LOG_FILE_COUNT.ToLower(), "5");
                        initParam.AddParam(InitParam.PARAM_KEY_LOG_FILE_PATH.ToLower(), "log");
                        initParam.AddParam(InitParam.PARAM_KEY_LOG_FILE_SIZE.ToLower(), "10240");
                        initParam.AddParam(InitParam.PARAM_KEY_LOG_LEVEL.ToLower(), "5");
                        Console.WriteLine("HciCloudSys HciInitConfig: " + initParam.GetConfig());
                        return initParam.GetConfig();
                    }
                }
            }
        }

        private static void VoiceCheck(int nSessionId, byte[] data, int data_len, ref AsrVoiceCheckResult check_result, string config)
        {
            AsrVoiceCheckFlag flag = AsrVoiceCheckFlag.CHECK_FLAG_BEGIN;
            if (ASR_Recog.cur_len + data_len > ASR_Recog.vad_check_audiodata_len)
            {
                check_result.eStatus = AsrVoiceCheckStatus.VAD_VOICE_END;
            }
            else
            {
                data.CopyTo((Array)ASR_Recog.vad_check_audiodata, ASR_Recog.cur_len);
                ASR_Recog.cur_len += data_len;
                byte[] voice_data = new byte[ASR_Recog.cur_len];
                Array.Copy((Array)ASR_Recog.vad_check_audiodata, (Array)voice_data, ASR_Recog.cur_len);
                AsrVoiceCheckStatus voiceCheckStatus = AsrVoiceCheckStatus.VAD_DETECTING;
                HciCloudAsr.HciAsrVoiceCheck(nSessionId, flag, voice_data, config, ref check_result);
                if (voiceCheckStatus == check_result.eStatus)
                    return;
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_DETECTING)
                    Console.WriteLine("检测中");
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_USER_STOP)
                    Console.WriteLine("用户终止音频输入");
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_BUFF_FULL)
                    Console.WriteLine("缓冲区满");
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_VOICE_BEGIN)
                    Console.WriteLine("检测到开始端点");
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_VOICE_END)
                    Console.WriteLine("检测到结束端点");
                if (check_result.eStatus == AsrVoiceCheckStatus.VAD_NO_VOICE_INPUT)
                    Console.WriteLine("没有发现音频输入（开始静音时间太长）");
            }
        }

        public static string CloudRecogFreeTalk(byte[] data, int data_len)
        {
            byte[] data1 = new byte[data_len];
            Array.Copy((Array)data, (Array)data1, data_len);
            string str = "";
            AsrVoiceCheckResult check_result = new AsrVoiceCheckResult();
            string config1 = "vadhead=10000,vadtail=500,audioFormat=pcm16k16bit";
            ASR_Recog.VoiceCheck(ASR_Recog.session_id_novad, data1, data_len, ref check_result, config1);
            if (check_result.eStatus != AsrVoiceCheckStatus.VAD_VOICE_END)
                return str;
            byte[] voice_data1 = new byte[ASR_Recog.cur_len];
            Array.Copy((Array)ASR_Recog.vad_check_audiodata, (Array)voice_data1, ASR_Recog.cur_len);
            ASR_Recog.cur_len = 0;
            string config2 = "addPunc=yes,realtime=yes,vadhead=20000,vadtail=600,";
            AsrRecogResult result = new AsrRecogResult();
            HciErrorCode hciErrorCode1 = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_novad, ref voice_data1, config2, (string)null, ref result);
            if (hciErrorCode1 == HciErrorCode.HCI_ERR_NONE)
            {
                Console.WriteLine("HciCloudAsr HciAsrRecog Success");
                Console.WriteLine("psResultItemList count " + (object)result.psResultItemList.Count);
                foreach (AsrRecogResultItem asrRecogResultItem in (IEnumerable<AsrRecogResultItem>)result.psResultItemList)
                {
                    Console.WriteLine("pszResult: " + asrRecogResultItem.pszResult);
                    str = str + asrRecogResultItem.pszResult;
                }
            }
            else
                Console.WriteLine("HciCloudAsr HciAsrRecog 出错: " + (object)hciErrorCode1);
            byte[] voice_data2 = (byte[])null;
            HciErrorCode hciErrorCode2 = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_novad, ref voice_data2, config2, (string)null, ref result);
            if (hciErrorCode2 == HciErrorCode.HCI_ERR_NONE)
            {
                Console.WriteLine("HciCloudAsr HciAsrRecog Success");
                Console.WriteLine("psResultItemList count " + (object)result.psResultItemList.Count);
                foreach (AsrRecogResultItem asrRecogResultItem in (IEnumerable<AsrRecogResultItem>)result.psResultItemList)
                {
                    Console.WriteLine("pszResult: " + asrRecogResultItem.pszResult);
                    str = str + asrRecogResultItem.pszResult;
                }
                return str;
            }
            else
            {
                Console.WriteLine("HciCloudAsr HciAsrRecog 出错: " + (object)hciErrorCode2);
                return str;
            }
        }

        public static void CloudRealtimeRecogFreeTalk(string audio_file_path)
        {
            HciErrorCode hciErrorCode = HciErrorCode.HCI_ERR_UNKNOWN;
            FileStream fileStream = File.OpenRead(audio_file_path);
            long length1 = 1024L;
            long num1 = 0L;
            long length2 = fileStream.Length;
            byte[] voice_data = new byte[length1];
            string config = (string)null;
            AsrRecogResult result = new AsrRecogResult();
            while (num1 + length1 < length2)
            {
                long num2 = length2 - num1 < length1 ? length2 - num1 : length1;
                fileStream.Read(voice_data, 0, (int)num2);
                hciErrorCode = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_, ref voice_data, config, (string)null, ref result);
                switch (hciErrorCode)
                {
                    case HciErrorCode.HCI_ERR_ASR_REALTIME_WAITING:
                        num1 += num2;
                        continue;
                    case HciErrorCode.HCI_ERR_ASR_REALTIME_END:
                        goto label_5;
                    default:
                        Console.WriteLine("hci_asr_recog failed with " + (object)hciErrorCode);
                        goto label_5;
                }
            }
            label_5:
            if (hciErrorCode == HciErrorCode.HCI_ERR_ASR_REALTIME_WAITING || hciErrorCode == HciErrorCode.HCI_ERR_ASR_REALTIME_END)
            {
                voice_data = (byte[])null;
                hciErrorCode = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_, ref voice_data, config, (string)null, ref result);
            }
            if (hciErrorCode == HciErrorCode.HCI_ERR_NONE)
            {
                Console.WriteLine("HciCloudAsr HciAsrRecog Success");
                Console.WriteLine("psResultItemList count " + (object)result.psResultItemList.Count);
                foreach (AsrRecogResultItem asrRecogResultItem in (IEnumerable<AsrRecogResultItem>)result.psResultItemList)
                    Console.WriteLine("pszResult: " + asrRecogResultItem.pszResult);
            }
            else
                Console.WriteLine("HciCloudAsr HciAsrRecog 出错: " + (object)hciErrorCode);
        }

        public static string RealtimeRecog(byte[] data, int data_len, out int ASRRealTimeEnd)
        {
            HciErrorCode hciErrorCode = HciErrorCode.HCI_ERR_UNKNOWN;
            string text = "";
            byte[] destinationArray = new byte[data_len];
            Array.Copy(data, destinationArray, data_len);
            string config = "addPunc=" + ASR_Recog.addpunc_;
            AsrRecogResult asrRecogResult = default(AsrRecogResult);
            hciErrorCode = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_, ref destinationArray, config, null, ref asrRecogResult);
            bool flag = hciErrorCode != HciErrorCode.HCI_ERR_ASR_REALTIME_WAITING;
            if (flag)
            {
                bool flag2 = hciErrorCode == HciErrorCode.HCI_ERR_ASR_REALTIME_END;
                if (flag2)
                {
                    Console.WriteLine("HciCloudAsr HciAsrRecog Success");
                }
                else
                {
                    Console.WriteLine("hci_asr_recog failed with " + hciErrorCode);
                }
            }
            bool flag3 = asrRecogResult.psResultItemList != null && ASR_Recog.realtime_ == "rt";
            if (flag3)
            {
                Console.WriteLine("--- psResultItemList count " + asrRecogResult.psResultItemList.Count);
                foreach (AsrRecogResultItem current in asrRecogResult.psResultItemList)
                {
                    Console.WriteLine("pszResult: " + current.pszResult);
                    text += current.pszResult;
                }
            }
            bool flag4 = hciErrorCode == HciErrorCode.HCI_ERR_ASR_REALTIME_END;
            string result;
            if (flag4)
            {
                Console.WriteLine("test---");
                destinationArray = null;
                ASRRealTimeEnd = 1;
                hciErrorCode = HciCloudAsr.HciAsrRecog(ASR_Recog.session_id_, ref destinationArray, config, null, ref asrRecogResult);
                bool flag5 = hciErrorCode == HciErrorCode.HCI_ERR_NONE;
                if (flag5)
                {
                    text = "";
                    Console.WriteLine("HciCloudAsr HciAsrRecog Success");
                    bool flag6 = asrRecogResult.psResultItemList != null;
                    if (flag6)
                    {
                        Console.WriteLine("psResultItemList count " + asrRecogResult.psResultItemList.Count);
                        foreach (AsrRecogResultItem current2 in asrRecogResult.psResultItemList)
                        {
                            Console.WriteLine("pszResult: " + current2.pszResult);
                            text += current2.pszResult;
                        }
                    }
                    result = text;
                    return result;
                }
                Console.WriteLine("HciCloudAsr HciAsrRecog 出错: " + hciErrorCode);
            }
            ASRRealTimeEnd = 0;
            result = text;
            return result;
        }
    }
}
