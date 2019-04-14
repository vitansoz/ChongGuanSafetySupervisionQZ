using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;
using ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class TalkingRecordManageViewModel : INotifyPropertyChanged
    {
        public TalkingRecordManageViewModel()
        {
            AllInquiryAndPartyList = new ObservableCollection<InquiryAndPartyModel>();
            CurrentInquiryAndPartyList = new ObservableCollection<InquiryAndPartyModel>();

            AllInquiryAndPartyListBySearch = new ObservableCollection<InquiryAndPartyModel>();
            CurrentInquiryAndPartyListBySearch = new ObservableCollection<InquiryAndPartyModel>();

            MessageList = new ObservableCollection<TalkingMessageModel>();
        }

        public ObservableCollection<InquiryAndPartyModel> AllInquiryAndPartyList { get; set; }
        public ObservableCollection<InquiryAndPartyModel> CurrentInquiryAndPartyList { get; set; }

        public ObservableCollection<InquiryAndPartyModel> AllInquiryAndPartyListBySearch { get; set; }
        public ObservableCollection<InquiryAndPartyModel> CurrentInquiryAndPartyListBySearch { get; set; }

        public ObservableCollection<TalkingMessageModel> MessageList { get; set; }

        private InquiryAndPartyModel _currentInquiryAndParty;

        public InquiryAndPartyModel CurrentInquiryAndParty
        {
            get => _currentInquiryAndParty;
            set => this.MutateVerbose(ref _currentInquiryAndParty, value, args => PropertyChanged?.Invoke(this, args));
        }


        private int _recordCountBySearch;
        public int RecordCountBySearch
        {
            get => _recordCountBySearch;
            set => this.MutateVerbose(ref _recordCountBySearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        private int _pageCountBySearch;
        public int PageCountBySearch
        {
            get => _pageCountBySearch;
            set => this.MutateVerbose(ref _pageCountBySearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        private int _currentPageNumberBySearch = 1;
        public int CurrentPageNumberBySearch
        {
            get => _currentPageNumberBySearch;
            set => this.MutateVerbose(ref _currentPageNumberBySearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        private int _recordCount;
        public int RecordCount
        {
            get => _recordCount;
            set => this.MutateVerbose(ref _recordCount, value, args => PropertyChanged?.Invoke(this, args));
        }

        private int _pageCount;
        public int PageCount
        {
            get => _pageCount;
            set => this.MutateVerbose(ref _pageCount, value, args => PropertyChanged?.Invoke(this, args));
        }

        private int _currentPageNumber = 1;
        public int CurrentPageNumber
        {
            get => _currentPageNumber;
            set => this.MutateVerbose(ref _currentPageNumber, value, args => PropertyChanged?.Invoke(this, args));
        }

        public void ClearAll()
        {
            AllInquiryAndPartyList.Clear();
            CurrentInquiryAndPartyList.Clear();

            RecordCount = 0;
            CurrentPageNumber = 1;
            PageCount = 0;
        }


        public void SearchAll(double itemWidth = 0, double itemHeight = 0)
        {
            ClearAll();

            ChongGuanSafetySupervisionQZ.DAL.InquiryDAL inquiryDAL = new DAL.InquiryDAL();
            var t = inquiryDAL.QueryByCondition();
            if (t.IsSuccessed)
            {

                int i = 0;
                foreach (var item in t.Data)
                {
                    InquiryAndPartyModel qz_i = new InquiryAndPartyModel
                    {
                        ItemHeight = itemWidth,
                        ItemWidth = itemWidth,
                        CreateDepartmentId = item.Inquiry.CreateDepartmentId,
                        CreateTime = item.Inquiry.CreateTime,
                        CreateUserId = item.Inquiry.CreateUserId,
                        EventId = item.Inquiry.EventId,
                        InquiryAddress = item.Inquiry.InquiryAddress,
                        InquiryAudioFilePath = item.Inquiry.InquiryAudioFilePath,
                        InquiryBeginTime = item.Inquiry.InquiryBeginTime,
                        InquiryChatFilePath = item.Inquiry.InquiryChatFilePath,
                        InquiryCode = item.Inquiry.InquiryCode,
                        InquiryDate = item.Inquiry.InquiryDate.ToString(),
                        InquiryEndTime = item.Inquiry.InquiryEndTime,
                        InquiryId = item.Inquiry.InquiryId,
                        InquiryLawBookFilePath = item.Inquiry.InquiryLawBookFilePath,
                        InquiryPictureFilePath = item.Inquiry.InquiryPictureFilePath,
                        InquiryPoliceName = item.Inquiry.InquiryPoliceName,
                        InquiryPoliceNumber = item.Inquiry.InquiryPoliceNumber,
                        InquiryRemarks = item.Inquiry.InquiryRemarks,
                        InquiryTalkType = item.Inquiry.InquiryTalkType,
                        InquiryVideo1FilePath = item.Inquiry.InquiryVideo1FilePath,
                        InquiryVideo2FilePath = item.Inquiry.InquiryVideo2FilePath,
                        InquiryVideo3FilePath = item.Inquiry.InquiryVideo3FilePath,
                        InquiryVideo4FilePath = item.Inquiry.InquiryVideo4FilePath,
                        IsDeleteId = item.Inquiry.IsDeleteId,
                        ModifyTime = item.Inquiry.ModifyTime,
                        Users = item.Inquiry.Users,

                        PartyAddress = item.Party.PartyAddress,
                        PartyBirth = item.Party.PartyBirth,
                        PartyCard = item.Party.PartyCard,
                        PartyCardImageFilePath = item.Party.PartyCardImageFilePath,
                        PartyCardIssusOffice = item.Party.PartyCardIssusOffice,
                        PartyCardLimitDate = item.Party.PartyCardLimitDate,
                        PartyDetentionDate = item.Party.PartyDetentionDate,
                        PartyDetentionReason = item.Party.PartyDetentionReason,
                        PartyFingerImageFilePath = item.Party.PartyFingerImageFilePath,
                        PartyId = item.Party.PartyId,
                        PartyMonitoringArea = item.Party.PartyMonitoringArea,
                        PartyMonitoringRoom = item.Party.PartyMonitoringRoom,
                        PartyName = item.Party.PartyName,
                        PartyNational = item.Party.PartyNational,
                        PartyNumber = item.Party.PartyNumber,
                        PartyPhone = item.Party.PartyPhone,
                        PartyPosition = item.Party.PartyPosition,
                        PartyPrison = item.Party.PartyPrison,
                        PartySex = item.Party.PartySex,
                        PartyUnit = item.Party.PartyUnit,
                    };

                    AllInquiryAndPartyList.Add(qz_i);
                    if (i++ <= 7)
                    {
                        CurrentInquiryAndPartyList.Add(qz_i);
                    }
                }

                RecordCount = AllInquiryAndPartyList.Count;
                PageCount = AllInquiryAndPartyList.Count / 8 + (AllInquiryAndPartyList.Count % 8 > 0 ? 1 : 0);
            }

        }

        public void NextPage()
        {
            if (CurrentPageNumber < PageCount)
            {
                CurrentPageNumber = CurrentPageNumber + 1;
                CurrentInquiryAndPartyList.Clear();

                for (int i = (CurrentPageNumber - 1) * 8; i < Math.Min(CurrentPageNumber * 8, AllInquiryAndPartyList.Count); i++)
                {
                    CurrentInquiryAndPartyList.Add(AllInquiryAndPartyList[i]);
                }
            }
        }

        public void PrePage()
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber = CurrentPageNumber - 1;
                CurrentInquiryAndPartyList.Clear();

                for (int i = (CurrentPageNumber - 1) * 8; i < Math.Min(CurrentPageNumber * 8, AllInquiryAndPartyList.Count); i++)
                {
                    CurrentInquiryAndPartyList.Add(AllInquiryAndPartyList[i]);
                }
            }
        }

        public void ClearBySearch()
        {
            AllInquiryAndPartyListBySearch.Clear();
            CurrentInquiryAndPartyListBySearch.Clear();

            RecordCountBySearch = 0;
            CurrentPageNumberBySearch = 1;
            PageCountBySearch = 0;
        }


        public void Search(double itemWidth = 0, double itemHeight = 0, string partyName = "", string partyNumber = "", string talkingType = "")
        {
            ClearBySearch();

            ChongGuanSafetySupervisionQZ.DAL.InquiryDAL inquiryDAL = new DAL.InquiryDAL();
            var t = inquiryDAL.QueryByCondition(partyName, partyNumber, talkingType);
            if (t.IsSuccessed)
            {

                int i = 0;
                foreach (var item in t.Data)
                {
                    InquiryAndPartyModel qz_i = new InquiryAndPartyModel
                    {
                        ItemHeight = itemWidth,
                        ItemWidth = itemWidth,
                        CreateDepartmentId = item.Inquiry.CreateDepartmentId,
                        CreateTime = item.Inquiry.CreateTime,
                        CreateUserId = item.Inquiry.CreateUserId,
                        EventId = item.Inquiry.EventId,
                        InquiryAddress = item.Inquiry.InquiryAddress,
                        InquiryAudioFilePath = item.Inquiry.InquiryAudioFilePath,
                        InquiryBeginTime = item.Inquiry.InquiryBeginTime,
                        InquiryChatFilePath = item.Inquiry.InquiryChatFilePath,
                        InquiryCode = item.Inquiry.InquiryCode,
                        InquiryDate = item.Inquiry.InquiryDate.ToString(),
                        InquiryEndTime = item.Inquiry.InquiryEndTime,
                        InquiryId = item.Inquiry.InquiryId,
                        InquiryLawBookFilePath = item.Inquiry.InquiryLawBookFilePath,
                        InquiryPictureFilePath = item.Inquiry.InquiryPictureFilePath,
                        InquiryPoliceName = item.Inquiry.InquiryPoliceName,
                        InquiryPoliceNumber = item.Inquiry.InquiryPoliceNumber,
                        InquiryRemarks = item.Inquiry.InquiryRemarks,
                        InquiryTalkType = item.Inquiry.InquiryTalkType,
                        InquiryVideo1FilePath = item.Inquiry.InquiryVideo1FilePath,
                        InquiryVideo2FilePath = item.Inquiry.InquiryVideo2FilePath,
                        InquiryVideo3FilePath = item.Inquiry.InquiryVideo3FilePath,
                        InquiryVideo4FilePath = item.Inquiry.InquiryVideo4FilePath,
                        IsDeleteId = item.Inquiry.IsDeleteId,
                        ModifyTime = item.Inquiry.ModifyTime,
                        Users = item.Inquiry.Users,

                        PartyAddress = item.Party.PartyAddress,
                        PartyBirth = item.Party.PartyBirth,
                        PartyCard = item.Party.PartyCard,
                        PartyCardImageFilePath = item.Party.PartyCardImageFilePath,
                        PartyCardIssusOffice = item.Party.PartyCardIssusOffice,
                        PartyCardLimitDate = item.Party.PartyCardLimitDate,
                        PartyDetentionDate = item.Party.PartyDetentionDate,
                        PartyDetentionReason = item.Party.PartyDetentionReason,
                        PartyFingerImageFilePath = item.Party.PartyFingerImageFilePath,
                        PartyId = item.Party.PartyId,
                        PartyMonitoringArea = item.Party.PartyMonitoringArea,
                        PartyMonitoringRoom = item.Party.PartyMonitoringRoom,
                        PartyName = item.Party.PartyName,
                        PartyNational = item.Party.PartyNational,
                        PartyNumber = item.Party.PartyNumber,
                        PartyPhone = item.Party.PartyPhone,
                        PartyPosition = item.Party.PartyPosition,
                        PartyPrison = item.Party.PartyPrison,
                        PartySex = item.Party.PartySex,
                        PartyUnit = item.Party.PartyUnit,
                    };

                    AllInquiryAndPartyListBySearch.Add(qz_i);
                    if (i++ <= 7)
                    {
                        CurrentInquiryAndPartyListBySearch.Add(qz_i);
                    }
                }

                RecordCountBySearch = AllInquiryAndPartyListBySearch.Count;
                PageCountBySearch = AllInquiryAndPartyListBySearch.Count / 8 + (AllInquiryAndPartyListBySearch.Count % 8 > 0 ? 1 : 0);
            }

        }

        public void NextPageBySearch()
        {
            if (CurrentPageNumberBySearch < PageCountBySearch)
            {
                CurrentPageNumberBySearch = CurrentPageNumberBySearch + 1;
                CurrentInquiryAndPartyListBySearch.Clear();

                for (int i = (CurrentPageNumberBySearch - 1) * 8;
                    i < Math.Min(CurrentPageNumberBySearch * 8, AllInquiryAndPartyListBySearch.Count); i++)
                {
                    CurrentInquiryAndPartyListBySearch.Add(AllInquiryAndPartyListBySearch[i]);
                }
            }
        }

        public void PrePageBySearch()
        {
            if (CurrentPageNumberBySearch > 1)
            {
                CurrentPageNumberBySearch = CurrentPageNumberBySearch - 1;
                CurrentInquiryAndPartyListBySearch.Clear();

                for (int i = (CurrentPageNumberBySearch - 1) * 8;
                    i < Math.Min(CurrentPageNumberBySearch * 8, AllInquiryAndPartyListBySearch.Count); i++)
                {
                    CurrentInquiryAndPartyListBySearch.Add(AllInquiryAndPartyListBySearch[i]);
                }
            }
        }

        public void LoadMessageList()
        {
            MessageList.Clear();
            //var t = JsonHelper.DeserializeObjectFromJsonFile<ObservableCollection<TalkingMessageModel>>(@"E:\Work\公司相关\项目\执法取证-公安\Project\ChongGuanSafetySupervisionQZ\ChongGuanSafetySupervisionQZ.View.Telerik\bin\Debug\InquiryData\5bcedec1-1598-4960-a809-b4e399c1696c\talkingMessageList.json");
            var t = JsonHelper.DeserializeObjectFromJsonFile<ObservableCollection<TalkingMessageModel>>(CurrentInquiryAndParty.InquiryChatFilePath);

            foreach (var item in t)
            {
                MessageList.Add(item);
            }
        }

        private string _partyNameForSearch;
        public string PartyNameForSearch
        {
            get => _partyNameForSearch;
            set => this.MutateVerbose(ref _partyNameForSearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        private string _partyNumberForSearch;
        public string PartyNumberForSearch
        {
            get => _partyNumberForSearch;
            set => this.MutateVerbose(ref _partyNumberForSearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        private string _inquiryTalkTypeForSearch;
        public string InquiryTalkTypeForSearch
        {
            get => _inquiryTalkTypeForSearch;
            set => this.MutateVerbose(ref _inquiryTalkTypeForSearch, value, args => PropertyChanged?.Invoke(this, args));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
