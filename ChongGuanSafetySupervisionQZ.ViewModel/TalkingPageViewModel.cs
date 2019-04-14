using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChongGuanSafetySupervisionQZ.ViewModel.Enum;
using ChongGuanSafetySupervisionQZ.Hardware;
using System.Collections.Concurrent;
using System.Threading;
using ChongGuanDotNetUtils.Helpers;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class TalkingPageViewModel : INotifyPropertyChanged
    {
        public TalkingPageViewModel()
        {
            _messageList = new ObservableCollection<TalkingMessageModel>();

        }

        private ObservableCollection<TalkingMessageModel> _messageList;

        public ObservableCollection<TalkingMessageModel> MessageList
        {
            get => _messageList;
            set
            {
                this.MutateVerbose(ref _messageList, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private TalkState _talkState;

        public TalkState TalkState
        {
            get => _talkState;
            set => this.MutateVerbose(ref _talkState, value, args => PropertyChanged?.Invoke(this, args));
        }
        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => this.MutateVerbose(ref _isRunning, value, args => PropertyChanged?.Invoke(this, args));
        }
        private bool _isOpenVoiceConversion;
        public bool IsOpenVoiceConversion
        {
            get => _isOpenVoiceConversion;
            set => this.MutateVerbose(ref _isOpenVoiceConversion, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isPartySpeeking;
        public bool IsPartySpeeking
        {
            get => _isPartySpeeking;
            set => this.MutateVerbose(ref _isPartySpeeking, value, args => PropertyChanged?.Invoke(this, args));
        }

        private TimeSpan _durationTime;
        public TimeSpan DurationTime
        {
            get => _durationTime;
            set => this.MutateVerbose(ref _durationTime, value, args => PropertyChanged?.Invoke(this, args));
        }

        private string _videoStateString;
        public string VideoStateString
        {
            get => _videoStateString;
            set => this.MutateVerbose(ref _videoStateString, value, args => PropertyChanged?.Invoke(this, args));
        }

        private string _messageListStateString;
        public string MessageListStateString
        {
            get => _messageListStateString;
            set => this.MutateVerbose(ref _messageListStateString, value, args => PropertyChanged?.Invoke(this, args));
        }

        private string _voiceStateString;
        public string VoiceStateString
        {
            get => _voiceStateString;
            set => this.MutateVerbose(ref _voiceStateString, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isStartButtonEnable = true;
        public bool IsStartButtonEnable
        {
            get => _isStartButtonEnable;
            set => this.MutateVerbose(ref _isStartButtonEnable, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isPauseButtonEnable = true;
        public bool IsPauseButtonEnable
        {
            get => _isPauseButtonEnable;
            set => this.MutateVerbose(ref _isPauseButtonEnable, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isStopButtonEnable = true;
        public bool IsStopButtonEnable
        {
            get => _isStopButtonEnable;
            set => this.MutateVerbose(ref _isStopButtonEnable, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isCreateLawBookButtonEnable = true;
        public bool IsCreateLawBookButtonEnable
        {
            get => _isCreateLawBookButtonEnable;
            set => this.MutateVerbose(ref _isCreateLawBookButtonEnable, value, args => PropertyChanged?.Invoke(this, args));
        }

        private bool _isShowDurationTime = true;
        public bool IsShowDurationTime
        {
            get => _isShowDurationTime;
            set => this.MutateVerbose(ref _isShowDurationTime, value, args => PropertyChanged?.Invoke(this, args));
        }


        public void AddNewMessage(TalkingMessageModel talkingMessageModel)
        {
            MessageList.Add(talkingMessageModel);
        }

        public void Test()
        {
            MessageList.Add(new TalkingMessageModel { MessageTime = DateTime.Now.ToString(), MessageContent = "哈稍等哈说的话", MessageTypeIsParty = false });
            MessageList.Add(new TalkingMessageModel { MessageTime = DateTime.Now.ToString(), MessageContent = "asd", MessageTypeIsParty = true });
            MessageList.Add(new TalkingMessageModel { MessageTime = DateTime.Now.ToString(), MessageContent = "哈稍asdwajdkjaskdjas;kldjasl;kdjjjjjjjjjjjjjjjjjsa等哈说的话", MessageTypeIsParty = false });
            MessageList.Add(new TalkingMessageModel { MessageTime = DateTime.Now.ToString(), MessageContent = "dsssssss", MessageTypeIsParty = true });
            MessageList.Add(new TalkingMessageModel { MessageTime = DateTime.Now.ToString(), MessageContent = "a", MessageTypeIsParty = false });
        }

        public void SetTalkingState(TalkState talkState)
        {
            TalkState = talkState;

            if (talkState == TalkState.None)
            {
                IsRunning = false;
                IsStartButtonEnable = true;
                IsPauseButtonEnable = false;
                IsStopButtonEnable = false;
                IsCreateLawBookButtonEnable = false;
                IsShowDurationTime = false;


                VideoStateString = "未开始";
                MessageListStateString = "未开始";
                VoiceStateString = "未开始";
            }

            if (talkState == TalkState.Pause)
            {
                IsRunning = false;
                IsStartButtonEnable = true;
                IsPauseButtonEnable = false;
                IsStopButtonEnable = true;
                IsCreateLawBookButtonEnable = false;
                IsShowDurationTime = true;


                VideoStateString = "暂停中";
                MessageListStateString = "暂停中";
                VoiceStateString = "暂停中";
            }

            if (talkState == TalkState.Running)
            {
                IsRunning = true;
                IsStartButtonEnable = false;
                IsPauseButtonEnable = true;
                IsStopButtonEnable = true;
                IsCreateLawBookButtonEnable = false;
                IsShowDurationTime = true;


                VideoStateString = "监控中...";
                MessageListStateString = "已进行";
                VoiceStateString = "录音中";
            }

            if (talkState == TalkState.Stopped)
            {
                IsRunning = false;
                IsStartButtonEnable = false;
                IsPauseButtonEnable = false;
                IsStopButtonEnable = false;
                IsCreateLawBookButtonEnable = true;
                IsShowDurationTime = true;

                VideoStateString = "已结束";
                MessageListStateString = "已结束";
                VoiceStateString = "已结束";
            }

            if (talkState == TalkState.CreateingBook)
            {
                IsRunning = false;
                IsStartButtonEnable = false;
                IsPauseButtonEnable = false;
                IsStopButtonEnable = false;
                IsCreateLawBookButtonEnable = false;
                IsShowDurationTime = true;

                VideoStateString = "已结束";
                MessageListStateString = "已结束";
                VoiceStateString = "已结束";
            }
        }


        public void SaveTalkingMessageList(string fullPath)
        {
            JsonHelper.SerializerToJsonFile(MessageList, fullPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
