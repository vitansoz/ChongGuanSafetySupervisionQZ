using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Hardware;
using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            _party = new QZ_Party { PartySex = "男", };
            _inquiry = new QZ_Inquiry { InquiryDate = DateTime.Now.Date.ToString() };
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


        public async void SavePartyInfo(string deparmentId = "1", string userId = "1")
        {
            ChongGuanSafetySupervisionQZ.DAL.PartyDAL partyDAL = new DAL.PartyDAL();
            ResultData<QZ_Party> result;

            //var result_t = partyDAL.Qurey("", "", "", _party.PartyCard);
            //if (result_t.IsSuccessed && result_t.Data.Count() > 0)
            //{
            //    result = await partyDAL.Update(_party);

            //}
            //先更新，更是失败添加；

            //try
            //{
            //    result = await partyDAL.UpdateByCard(_party);
            //    if (!result.IsSuccessed)
            //    {
            //        _party.CreateDepartmentId = deparmentId ?? "1";
            //        _party.CreateUserId = userId ?? "1";
            //        result = await partyDAL.Add(_party);
            //    }
            //}
            //catch
            //{
            //    _party.CreateDepartmentId = deparmentId ?? "1";
            //    _party.CreateUserId = userId ?? "1";
            //    result = await partyDAL.Add(_party);

            //}

            _party.CreateDepartmentId = deparmentId ?? "1";
            _party.CreateUserId = userId ?? "1";
            result = await partyDAL.Add(_party);


            if (result.IsSuccessed)
            {
                Party = result.Data;
            }
        }

        public async void SaveInquiryInfo(string deparmentId = "1", string userId = "1")
        {
            ChongGuanSafetySupervisionQZ.DAL.InquiryDAL inquiryDAL = new DAL.InquiryDAL();

            _inquiry.CreateDepartmentId = deparmentId ?? "1";
            _inquiry.CreateUserId = userId ?? "1";

            var result = await inquiryDAL.Add(_inquiry);

            if (result.IsSuccessed)
            {
                Inquiry = result.Data;
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

        public void ReadCDCard()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    if (CardClass.Authenticate() == 1)
                    {
                        LoggerHelper.Log("CardClass.Authenticate() == 1", "身份证");
                        if (CardClass.Read_Content(1) == 1)
                        {
                            LoggerHelper.Log("CardClass.Read_Content(1) == 1", "身份证");

                            InitCardInfo();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void InitCardInfo()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                CardClass.GetCardInfo(i, sb);
                switch (i)
                {
                    case 0:
                        Party.PartyName = sb.ToString();
                        break;
                    case 1:
                        Party.PartySex = sb.ToString();
                        break;
                    case 2:
                        Party.PartyNational = sb.ToString();
                        break;
                    case 3:
                        string text = sb.ToString();
                        Party.PartyBirth = string.Concat(new string[]
                        {
                            text.Substring(0, 4),
                            "-",
                            text.Substring(4, 2),
                            "-",
                            text.Substring(6, 2)
                        });
                        break;
                    case 4:
                        Party.PartyAddress = sb.ToString();
                        break;
                    case 5:
                        Party.PartyCard = sb.ToString();
                        break;
                    case 6:
                        Party.PartyCardIssusOffice = sb.ToString();
                        break;
                    case 7:
                        Party.PartyCardLimitDate = sb.ToString();
                        break;
                    case 8:
                        Party.PartyCardLimitDate = Party.PartyCardLimitDate + sb.ToString();
                        break;
                }                
            }

            CardClass.GetBmpPhotoExt();
            if (CardClass.ExportCardImageF() > 0)
            {
                string cardImageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rescard", Party.PartyCard + ".jpg");
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(cardImageFilePath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(cardImageFilePath));
                    }

                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cardf.jpg"),
                       cardImageFilePath, true);
                }
                catch { }
                Party.PartyCardImageFilePath = cardImageFilePath;
            }

            Party = Party;
        }

    }
}
