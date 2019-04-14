using ChongGuanSafetySupervisionQZ.View.WPF.Common;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Helpers
{
    public class AudioHelper
    {
        public WaveIn _waveSource = null;
        public WaveFileWriter _waveFile = null;

        private string _audioFileFullPath;
        private bool _isPause;

        public bool IsPause { get => _isPause; }


        private MMDevice _mMDevice;
        public event EventHandler<AudioDataAvailableEventArgs> AudioDataAvailable;
        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRec(string audioFileFullPath)
        {
            string fullDir = Path.GetDirectoryName(audioFileFullPath);
            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            _audioFileFullPath = audioFileFullPath;

            _waveSource = new WaveIn();
            //_waveSource.DeviceNumber = deviceNumber;
            _waveSource.WaveFormat = new WaveFormat(16000, 16, 1); // 16bit,16KHz,Mono的录音格式

            _waveSource.DataAvailable += _waveSource_DataAvailable;
            _waveSource.RecordingStopped += _waveSource_RecordingStopped;

            _waveFile = new WaveFileWriter(_audioFileFullPath, _waveSource.WaveFormat);

            _waveSource.StartRecording();

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            _mMDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);

        }

        public void Pause()
        {
            _isPause = true;
        }

        public void Resume()
        {
            _isPause = false;
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRec()
        {
            _waveSource.StopRecording();

            // Close Wave(Not needed under synchronous situation)
            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }

            if (_waveFile != null)
            {
                _waveFile.Dispose();
                _waveFile = null;
            }
        }

        private void _waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }

            if (_waveFile != null)
            {
                _waveFile.Dispose();
                _waveFile = null;
            }
        }

        private void _waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_waveFile != null && !IsPause)
            {
                _waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                _waveFile.Flush();

                if (AudioDataAvailable != null)
                {
                    float t = _mMDevice.AudioMeterInformation.MasterPeakValue;
                    AudioDataAvailable(this, new AudioDataAvailableEventArgs { MasterPeakValue = t });
                    Debug.WriteLine(t);
                }
            }
        }
    }
}
