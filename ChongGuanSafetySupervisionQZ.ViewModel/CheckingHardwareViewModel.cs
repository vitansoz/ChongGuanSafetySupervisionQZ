using ChongGuanSafetySupervisionQZ.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class CheckingHardwareViewModel : INotifyPropertyChanged
    {
        private bool _isIdentificationGood;
        public bool IsIdentificationGood { get => _isIdentificationGood; set => this.MutateVerbose(ref _isIdentificationGood, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _isFingerprintGood;
        public bool IsFingerprintGood { get => _isFingerprintGood; set => this.MutateVerbose(ref _isFingerprintGood, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _isSignatureGood;
        public bool IsSignatureGood { get => _isSignatureGood; set => this.MutateVerbose(ref _isSignatureGood, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _isSceneCameraGood;
        public bool IsSceneCameraGood { get => _isSceneCameraGood; set => this.MutateVerbose(ref _isSceneCameraGood, value, args => PropertyChanged?.Invoke(this, args)); }

        private int _checkingProgress;
        public int CheckingProgress { get => _checkingProgress; set => this.MutateVerbose(ref _checkingProgress, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _hasIdentificationChecked;
        public bool HasIdentificationChecked { get => _hasIdentificationChecked; set => this.MutateVerbose(ref _hasIdentificationChecked, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _hasFingerprintChecked;
        public bool HasFingerprintChecked { get => _hasFingerprintChecked; set => this.MutateVerbose(ref _hasFingerprintChecked, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _hasSignatureChecked;
        public bool HasSignatureChecked { get => _hasSignatureChecked; set => this.MutateVerbose(ref _hasSignatureChecked, value, args => PropertyChanged?.Invoke(this, args)); }

        private bool _hasSceneCameraChecked;
        public bool HasSceneCameraChecked { get => _hasSceneCameraChecked; set => this.MutateVerbose(ref _hasSceneCameraChecked, value, args => PropertyChanged?.Invoke(this, args)); }

        private string _outputMessage;
        public string OutputMessage { get => _outputMessage; set => this.MutateVerbose(ref _outputMessage, value, args => PropertyChanged?.Invoke(this, args)); }

        private string _errorHardwareMessage;
        public string ErrorHardwareMessage { get => _errorHardwareMessage; set => this.MutateVerbose(ref _errorHardwareMessage, value, args => PropertyChanged?.Invoke(this, args)); }

        public event EventHandler CheckFinished;

        private HwClass.HWDeviceInfo _devInfo;
        public int _disMsg = 0;
        public int _hw_rv_ok = 0;

        private IntPtr _m_hDevice = IntPtr.Zero;
        private IntPtr _mainHwnd;

        public async void InitHardware(IntPtr hwnd)
        {
            ErrorHardwareMessage = string.Empty;
            CheckingProgress = 0;
            HasFingerprintChecked = false;
            HasIdentificationChecked = false;
            HasSceneCameraChecked = false;
            HasSignatureChecked = false;



            _mainHwnd = hwnd;
            await Task.Run(() =>
            {
                OutputMessage = "开始硬件检测……";
                InitDevice(OutMessages.Init_HW_Index);

                Thread.Sleep(1000);
                InitDevice(OutMessages.Init_ZW_Index);

                Thread.Sleep(1000);
                InitDevice(OutMessages.Init_CARD_Index);
                Thread.Sleep(1000);
                InitDevice(OutMessages.Init_Video_CJ_Index);
                Thread.Sleep(300);
                InitDevice(OutMessages.Init_Video_DSR_Index);
                Thread.Sleep(300);
                InitDevice(OutMessages.Init_Video_ZF_Index);
            });
        }

        private void InitDevice(int index)
        {
            switch (index)
            {
                case 1:
                    {
                        OutputMessage = OutMessages.Init_HW_Msg;
                        CheckingProgress += 5;
                        ushort num = HwClass.HWInit(ref this._devInfo, ref this._disMsg, _mainHwnd);
                        if ((int)num != _hw_rv_ok)
                        {
                            OutputMessage = OutMessages.Init_HW_Error_Msg;
                            IsSignatureGood = false;

                            ErrorHardwareMessage += "电子签名、";
                        }
                        else
                        {
                            OutputMessage = OutMessages.Init_HW_OK_Msg;
                            IsSignatureGood = true;
                            OutMessages.IS_HW_OK = true;
                            HwClass.HWClose(_mainHwnd);
                        }

                        HasSignatureChecked = true;
                        CheckingProgress += 5;
                        break;
                    }
                case 2:
                    {
                        CheckingProgress += 5;
                        this._m_hDevice = ZwClass.ZKFPModule_Connect("protocol=USB,vendor-id=6997,product-id=289");
                        bool flag2 = this._m_hDevice == IntPtr.Zero;
                        if (flag2)
                        {
                            OutputMessage = OutMessages.Init_ZW_Error_Msg;
                            IsFingerprintGood = false;

                            ErrorHardwareMessage += "指纹识别、";
                        }
                        else
                        {
                            OutputMessage = OutMessages.Init_ZW_OK_Msg;
                            IsFingerprintGood = true;
                            OutMessages.IS_ZW_OK = true;
                            ZwClass.ZKFPModule_Disconnect(this._m_hDevice);
                            this._m_hDevice = IntPtr.Zero;
                        }

                        HasFingerprintChecked = true;
                        CheckingProgress += 5;

                        break;
                    }
                case 3:
                    {
                        CheckingProgress += 5;
                        int num2 = CardClass.InitCommExt();
                        bool flag3 = num2 > 0;
                        if (!flag3)
                        {
                            OutputMessage = OutMessages.Init_CARD_Error_Msg;
                            IsIdentificationGood = false;

                            ErrorHardwareMessage += "身份识别、";
                        }
                        else
                        {
                            OutputMessage = OutMessages.Init_CARD_OK_Msg;
                            IsIdentificationGood = true;
                            OutMessages.IS_CARD_OK = true;
                        }

                        HasIdentificationChecked = true;
                        CheckingProgress += 5;

                        break;
                    }
                case 4:
                    CheckingProgress += 5;

                    OutputMessage = OutMessages.Init_Video_CJ_OK_Msg;
                    OutMessages.IS_Video_CJ_OK = true;

                    Thread.Sleep(100);
                    CheckingProgress += 5;
                    break;
                case 5:
                    CheckingProgress += 5;

                    OutputMessage = OutMessages.Init_Video_DSR_OK_Msg;
                    OutMessages.IS_Video_DSR_OK = true;
                    Thread.Sleep(100);

                    CheckingProgress += 5;
                    break;
                case 6:
                    CheckingProgress += 5;

                    OutputMessage = OutMessages.Init_Video_ZF_OK_Msg;
                    OutMessages.IS_Video_ZF_OK = true;
                    IsSceneCameraGood = true;
                    Thread.Sleep(100);

                    HasSceneCameraChecked = true;
                    CheckingProgress += 5;

                    ErrorHardwareMessage = ErrorHardwareMessage.TrimEnd('、');
                    if (CheckFinished != null)
                    {
                        CheckFinished(new object(), new EventArgs());
                    }
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
