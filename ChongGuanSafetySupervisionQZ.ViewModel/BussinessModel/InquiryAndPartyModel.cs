using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel
{
    public class InquiryAndPartyModel
    {
        public double ItemWidth
        {
            get;
            set;
        }

        public double ItemHeight
        {
            get;
            set;
        }

        public string InquiryId { get; set; }

        public string InquiryCode { get; set; }

        public string InquiryDate { get; set; }

        public string InquiryAddress { get; set; }

        public string EventId { get; set; }

        public string PartyId { get; set; }

        public string Users { get; set; }

        public string InquiryChatFilePath { get; set; }

        public string InquiryAudioFilePath { get; set; }

        public string InquiryVideo1FilePath { get; set; }

        public string InquiryVideo2FilePath { get; set; }

        public string InquiryVideo3FilePath { get; set; }

        public string InquiryVideo4FilePath { get; set; }

        public string InquiryPictureFilePath { get; set; }

        public string InquiryLawBookFilePath { get; set; }

        public string InquiryBeginTime { get; set; }

        public string InquiryEndTime { get; set; }

        public string InquiryPoliceName { get; set; }

        public string InquiryPoliceNumber { get; set; }

        public string InquiryTalkType { get; set; }

        public string InquiryRemarks { get; set; }

        public string CreateUserId { get; set; }

        public string CreateDepartmentId { get; set; }

        public int IsDeleteId { get; set; }

        public string CreateTime { get; set; }

        public string ModifyTime { get; set; }

        public string PartyNumber { get; set; }

        public string PartyDetentionDate { get; set; }

        public string PartyPrison { get; set; }

        public string PartyMonitoringArea { get; set; }

        public string PartyMonitoringRoom { get; set; }

        public string PartyDetentionReason { get; set; }

        public string PartyName
        {
            get;
            set;
        }

        public string PartySex { get; set; }

        public string PartyBirth { get; set; }

        public string PartyNational { get; set; }

        public string PartyCard { get; set; }

        public string PartyAddress
        {
            get;
            set;
        }

        public string PartyUnit { get; set; }

        public string PartyPhone { get; set; }

        public string PartyPosition { get; set; }

        public string PartyCardImageFilePath { get; set; }

        public string PartyFingerImageFilePath { get; set; }

        public string PartyCardIssusOffice { get; set; }

            public string PartyCardLimitDate { get; set; }
    }
}
