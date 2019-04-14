using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class TalkingMessageModel : INotifyPropertyChanged
    {
        private string _messageTime;

        public string MessageTime
        {
            get => _messageTime;
            set
            {
                this.MutateVerbose(ref _messageTime, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _messageContent;

        public string MessageContent
        {
            get => _messageContent;
            set
            {
                this.MutateVerbose(ref _messageContent, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private bool _messageTypeIsParty;

        public bool MessageTypeIsParty
        {
            get => _messageTypeIsParty;
            set
            {
                this.MutateVerbose(ref _messageTypeIsParty, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
