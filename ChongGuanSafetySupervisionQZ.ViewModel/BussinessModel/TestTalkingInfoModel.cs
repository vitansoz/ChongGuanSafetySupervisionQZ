using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel
{
    public class TestTalkingInfoModel : INotifyPropertyChanged
    {
        private string _inquiryPictureFilePath;

        public string InquiryPictureFilePath
        {
            get => _inquiryPictureFilePath;
            set
            {
                this.MutateVerbose(ref _inquiryPictureFilePath, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _partyName;

        public string PartyName
        {
            get => _partyName;
            set
            {
                this.MutateVerbose(ref _partyName, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _inquiryDate;

        public string InquiryDate
        {
            get => _inquiryDate;
            set
            {
                this.MutateVerbose(ref _inquiryDate, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _inquiryTalkType;

        public string InquiryTalkType
        {
            get => _inquiryTalkType;
            set
            {
                this.MutateVerbose(ref _inquiryTalkType, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private double _itemWidth;

        public double ItemWidth
        {
            get => _itemWidth;
            set
            {
                this.MutateVerbose(ref _itemWidth, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        private double _itemheight;

        public double Itemheight
        {
            get => _itemheight;
            set
            {
                this.MutateVerbose(ref _itemheight, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
