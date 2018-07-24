using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class NewTalkViewModel : INotifyPropertyChanged
    {
        private bool _isPartyInfoInDB = false;
        public NewTalkViewModel()
        {
            _party = new QZ_Party();
            _inquiry = new QZ_Inquiry();
        }

        private QZ_Party _party;

        public QZ_Party Party
        {
            get => _party;
            set
            {
                this.MutateVerbose(ref _party, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private QZ_Inquiry _inquiry;

        public event PropertyChangedEventHandler PropertyChanged;

        public QZ_Inquiry Inquiry
        {
            get => _inquiry;
            set
            {
                this.MutateVerbose(ref _inquiry, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        private string _partyAddress = "shit";

        public string PartyAddress
        {
            get => _partyAddress;

            set
            {
                this.MutateVerbose(ref _partyAddress, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        public async void SavePartyInfo(string deparmentId = "1",string userId = "1")
        {
            ChongGuanSafetySupervisionQZ.DAL.PartyDAL partyDAL = new DAL.PartyDAL();
            ResultData<QZ_Party> result;

            if (_isPartyInfoInDB)
            {
                result = await partyDAL.Update(_party);
            }
            else
            {
                _party.CreateDepartmentId = deparmentId ?? "1";
                _party.CreateUserId = userId ?? "1";
                result = await partyDAL.Add(_party);
            }

            if(result.IsSuccessed)
            {
                Party = result.Data;
            }
        }


        public void GetPartyInfoByIdCard(string idCardNumber)
        {
            ChongGuanSafetySupervisionQZ.DAL.PartyDAL partyDAL = new DAL.PartyDAL();

            var result = partyDAL.Qurey("", "", "", idCardNumber);
            if (result.IsSuccessed && result.Data.Count() > 0)
            {
                Party = result.Data.FirstOrDefault();
                _isPartyInfoInDB = true;
            }
            else
            //test
            {
                //Party = new QZ_Party
                //{
                //    PartyAddress = "12点57分"
                //};

                Party.PartyAddress = "shishishit";
                Party.PartyName = "张晓坤";
                Party.PartyNational = "厕所吃屎";
                Party.PartyCard = "110228198703140101";
                Party.PartyNumber = "119";
                Party.PartyDetentionDate = DateTime.Now.ToString();
                Party.PartyPrison = "北京第一看守所";
                Party.PartyMonitoringArea = "重刑犯";
                Party.PartyMonitoringRoom = "001";
                Party.PartyDetentionReason = "强奸母猪";
                Party.PartySex = "女";
                Party.PartyBirth = "2014-10-10";
                Party.PartyCardImageFilePath = @"C:\Users\PETTO\Desktop\xxx\阿里旺旺图片20180721192221.jpg";
                

                Party = Party;

            };
        }
    }

}
