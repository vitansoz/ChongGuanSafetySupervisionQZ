using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Hardware
{
    public class OutMessages
    {
        public static string Init_HW_Msg = "正在初始化电子签名设备...";
        public static string Init_ZW_Msg = "正在初始化指纹识别器...";
        public static string Init_CARD_Msg = "正在初始化身份证读卡器...";
        public static string Init_Video_CJ_Msg = "正在初始化场景图像信息...";
        public static string Init_Video_DSR_Msg = "正在初始化当事人图像信息...";
        public static string Init_Video_ZF_Msg = "正在初始化执法人员图像信息...";
        public static string Init_HW_Error_Msg = "初始化电子签名设备失败";
        public static string Init_ZW_Error_Msg = "初始化指纹识别器失败";
        public static string Init_CARD_Error_Msg = "初始化身份证读卡器失败";
        public static string Init_Video_CJ_Error_Msg = "初始化场景图像信息失败";
        public static string Init_Video_DSR_Error_Msg = "初始化当事人图像信息失败";
        public static string Init_Video_ZF_Error_Msg = "初始化执法人员图像信息失败";
        public static string Init_HW_OK_Msg = "初始化电子签名设备成功";
        public static string Init_ZW_OK_Msg = "初始化指纹识别器成功";
        public static string Init_CARD_OK_Msg = "初始化身份证读卡器成功";
        public static string Init_Video_CJ_OK_Msg = "初始化场景图像信息成功";
        public static string Init_Video_DSR_OK_Msg = "初始化当事人图像信息成功";
        public static string Init_Video_ZF_OK_Msg = "初始化执法人员图像信息成功";

        public static string Init_OK_Msg = "系统初始化完成，欢迎使用";
        public static int Init_HW_Index = 1;
        public static int Init_ZW_Index = 2;
        public static int Init_CARD_Index = 3;
        public static int Init_Video_CJ_Index = 4;
        public static int Init_Video_DSR_Index = 5;
        public static int Init_Video_ZF_Index = 6;
        public static bool IS_HW_OK = false;
        public static bool IS_ZW_OK = false;
        public static bool IS_CARD_OK = false;
        public static bool IS_Video_CJ_OK = false;
        public static bool IS_Video_DSR_OK = false;
        public static bool IS_Video_ZF_OK = false;
        public static bool IS_AUDIO_OK = false;
        public static bool IS_JKYQ_OK = true;
    }
}
