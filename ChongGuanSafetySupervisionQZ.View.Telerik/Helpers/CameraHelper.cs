using AForge.Controls;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video.FFMPEG;
using System.Diagnostics;

namespace ChongGuanSafetySupervisionQZ.View.WPF.Helpers
{
    public class CameraHelper
    {
        private FilterInfoCollection _cameraDevices;
        private VideoCaptureDevice _div = null;
        private VideoSourcePlayer _sourcePlayer = new VideoSourcePlayer();
        private VideoFileWriter _videoWriter = null;
        private bool _isDisplay = false;

        private RotateFlipType _rotateFlipType = RotateFlipType.RotateNoneFlipNone;

        //指示_isDisplay设置为true后，是否设置了其他的sourcePlayer，若未设置则_isDisplay重设为false
        private bool isSet = false;

        private bool _isRec;
        private bool _isStartRec;

        private bool _isPicture;
        private bool _isStartPicture;

        private string _videoSaveFullPath;
        private string _mainPictureSaveFullPath;
        /// <summary>
        /// 获取或设置摄像头设备，无设备为null
        /// </summary>
        public FilterInfoCollection CameraDevices
        {
            get
            {
                return _cameraDevices;
            }
            set
            {
                _cameraDevices = value;
            }
        }

        /// <summary>
        /// 指示是否显示摄像头视频画面
        /// 默认false
        /// </summary>
        public bool IsDisplay
        {
            get { return _isDisplay; }
            set { _isDisplay = value; }
        }

        /// <summary>
        /// 获取或设置VideoSourcePlayer控件，
        /// 只有当IsDisplay设置为true时，该属性才可以设置成功
        /// </summary>
        public VideoSourcePlayer SourcePlayer
        {
            get { return _sourcePlayer; }
            set
            {
                if (_isDisplay)
                {
                    _sourcePlayer = value;
                    isSet = true;
                }

            }
        }

        public bool IsStartRec { get => _isStartRec; set => _isStartRec = value; }
        public bool IsStartPicture { get => _isStartPicture; set => _isStartPicture = value; }

        /// <summary>
        /// 更新摄像头设备信息
        /// </summary>
        public void UpdateCameraDevices()
        {
            _cameraDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        /// <summary>
        /// 设置使用的摄像头设备
        /// </summary>
        /// <param name="index">设备在CameraDevices中的索引</param>
        /// <returns><see cref="bool"/></returns>
        public bool SetCameraDevice(int index, string videoSaveFullPath, string mainPictureSaveFullPath, bool isRec, bool isPicture, RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone)
        {
            if (!isSet) _isDisplay = false;
            if (_cameraDevices.Count <= 0 || index > _cameraDevices.Count - 1)
            {
                return false;
            }

            if (isRec)
            {
                string fullDir = Path.GetDirectoryName(videoSaveFullPath);
                if (!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }
            }


            if (isPicture)
            {
                string fullDir = Path.GetDirectoryName(mainPictureSaveFullPath);
                if (!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }
            }

            _videoSaveFullPath = videoSaveFullPath;
            _isRec = isRec;
            _isPicture = isPicture;
            _mainPictureSaveFullPath = mainPictureSaveFullPath;
            _rotateFlipType = rotateFlipType;

            // 设定初始视频设备
            _div = new VideoCaptureDevice(_cameraDevices[index].MonikerString);
            _div.NewFrame += _div_NewFrame;

            _sourcePlayer.VideoSource = _div;
            _sourcePlayer.NewFrame += _sourcePlayer_NewFrame;


            _div.Start();
            _sourcePlayer.Start();

            return true;
        }

        private void _sourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            if (image != null)
            {
                image.RotateFlip(_rotateFlipType);
            }
        }

        private void _div_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bmp_t = _sourcePlayer.GetCurrentVideoFrame();

            if (bmp_t != null)
            {
                if (_isRec && _isStartRec)
                {
                    if (_videoWriter == null)
                    {
                        _videoWriter = new VideoFileWriter();

                        Debug.WriteLine($"_div.VideoCapabilities:{_div.VideoCapabilities.Count()}");
                        foreach (var item in _div.VideoCapabilities)
                        {
                            Debug.WriteLine($"AverageFrameRate:{item.AverageFrameRate}");
                            Debug.WriteLine($"MaximumFrameRate:{item.MaximumFrameRate}");
                        }

                        _videoWriter.Open(
                            _videoSaveFullPath,
                            _div.VideoCapabilities[0].FrameSize.Width,
                            _div.VideoCapabilities[0].FrameSize.Height,
                            20,
                            //28,
                            VideoCodec.MPEG4);

                        if (_isPicture && _isStartPicture)
                        {
                            bmp_t.Save(_mainPictureSaveFullPath);
                        }
                    }

                    //Debug.WriteLine(bmp_t.Width + ":" + bmp_t.Height);
                    _videoWriter.WriteVideoFrame(bmp_t);
                }
                bmp_t.Dispose();
            }
        }

        /// <summary>
        /// 截取一帧图像并保存
        /// </summary>
        /// <param name="filePath">图像保存路径</param>
        /// <param name="fileName">保存的图像文件名</param>
        public void CaptureImage(string fileFullPath = "")
        {
            if (_sourcePlayer.VideoSource == null) return;

            if (!string.IsNullOrWhiteSpace(fileFullPath))
            {
                string fullDir = Path.GetDirectoryName(fileFullPath);
                if (!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }
            }

            try
            {
                System.Drawing.Image bitmap = _sourcePlayer.GetCurrentVideoFrame();
                bitmap.Save(string.IsNullOrWhiteSpace(fileFullPath) ? _mainPictureSaveFullPath : fileFullPath, ImageFormat.Jpeg);
                bitmap.Dispose();
            }
            catch (Exception e)
            {

            }

        }
        /// <summary>
        /// 关闭摄像头设备
        /// </summary>
        public void CloseDevice()
        {
            if (_div != null && _div.IsRunning)
            {
                _div.NewFrame -= _div_NewFrame;
                _sourcePlayer.NewFrame -= _sourcePlayer_NewFrame;
                _sourcePlayer.Stop();

                _div.SignalToStop();
                _div.WaitForStop();
                _videoWriter.Close();

                _div = null;
                _cameraDevices = null;
                _videoWriter = null;
            }
        }
    }
}
