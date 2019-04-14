using ChongGuanSafetySupervisionQZ.Model;
using ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class SatisticsPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SatisticsDataModel> _satisticsDataByMonth;
        private ObservableCollection<SatisticsDataModel> _satisticsDataByTalkingType;

        public void Test()
        {
            _satisticsDataByMonth = new ObservableCollection<SatisticsDataModel>
            {
                //new SatisticsDataModel{Name = "1月",Performance = 8,Remarks="fuck"},
                //new SatisticsDataModel{Name = "2月",Performance = 2,Remarks="fuck"},
                //new SatisticsDataModel{Name = "3月",Performance = 18,Remarks="fuck"},
                //new SatisticsDataModel{Name = "4月",Performance = 28,Remarks="fuck"},
                //new SatisticsDataModel{Name = "5月",Performance = 38,Remarks="fuck"},
                //new SatisticsDataModel{Name = "6月",Performance = 11,Remarks="fuck"},
                //new SatisticsDataModel{Name = "7月",Performance =23,Remarks="fuck"},
                //new SatisticsDataModel{Name = "8月",Performance = 98,Remarks="fuck"},
                //new SatisticsDataModel{Name = "9月",Performance = 102,Remarks="fuck"},
                //new SatisticsDataModel{Name = "10月",Performance = 38,Remarks="fuck"},
                //new SatisticsDataModel{Name = "11月",Performance = 78,Remarks="fuck"},
                //new SatisticsDataModel{Name = "asdjaskldjalksjdlaks陆军空军",Performance = 18,Remarks="fuck"},\\

                new SatisticsDataModel{Name = "对新收押\n人员首次\n谈话",Performance = 8,Remarks="fuck"},
                new SatisticsDataModel{Name = "在押人员\n诉讼环节\n发生变化\n谈话",Performance = 2,Remarks="fuck"},
                new SatisticsDataModel{Name = "会见律师后\n思想不稳定、\n表现异常的\n谈话",Performance = 18,Remarks="fuck"},
                new SatisticsDataModel{Name = "在押人员家庭\n发生变故\n谈话",Performance = 28,Remarks="fuck"},
                new SatisticsDataModel{Name = "被加戴械具、\n处罚前后\n谈话",Performance = 38,Remarks="fuck"},
                new SatisticsDataModel{Name = "调换监室的\n谈话",Performance = 11,Remarks="fuck"},
                new SatisticsDataModel{Name = "要求反映\n监室动态\n的谈话",Performance =23,Remarks="fuck"},
                new SatisticsDataModel{Name = "出所前\n谈话",Performance = 58,Remarks="fuck"},
            };

            _axisStepByMonth = 0;
            _axisMaxValueByMonth = 0;

            _researchStartTime = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            _researchEndTime = DateTime.Now.ToShortDateString();
        }

        public SatisticsPageViewModel()
        {
            _satisticsDataByMonth = new ObservableCollection<SatisticsDataModel>();
            _satisticsDataByTalkingType = new ObservableCollection<SatisticsDataModel>();

            TalkingType = new List<string>();

            _axisStepByMonth = 0;
            _axisMaxValueByMonth = 0;

            _axisStepByTalkingType = 0;
            _axisMaxValueByTalkingType = 0;

            _researchStartTime = new DateTime(DateTime.Now.Year, 1, 1).ToShortDateString();
            _researchEndTime = DateTime.Now.ToShortDateString();
        }

        public ObservableCollection<SatisticsDataModel> SatisticsDataByMonth
        {
            get => _satisticsDataByMonth;
            set
            {
                this.MutateVerbose(ref _satisticsDataByMonth, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        public ObservableCollection<SatisticsDataModel> SatisticsDataByTalkingType
        {
            get => _satisticsDataByTalkingType;
            set
            {
                this.MutateVerbose(ref _satisticsDataByTalkingType, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private bool _showLabels = true;
        public bool ShowLabels
        {
            get => _showLabels;
            set
            {
                this.MutateVerbose(ref _showLabels, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private double _gapLengthByMonth = 0.2d;

        public double GapLengthByMonth
        {
            get => _gapLengthByMonth;
            set
            {
                this.MutateVerbose(ref _gapLengthByMonth, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private double _axisMaxValueByMonth = 0;

        public double AxisMaxValueByMonth
        {
            get => _axisMaxValueByMonth;
            set
            {
                this.MutateVerbose(ref _axisMaxValueByMonth, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private double _axisStepByMonth = 0;
        public double AxisStepByMonth
        {
            get => _axisStepByMonth;
            set
            {
                this.MutateVerbose(ref _axisStepByMonth, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private string _axisTitleByMonth = "谈话次数";
        public string AxisTitleByMonth
        {
            get => _axisTitleByMonth;
            set
            {
                this.MutateVerbose(ref _axisTitleByMonth, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private string _researchStartTime;
        public string ResearchStartTime
        {
            get => _researchStartTime;
            set
            {
                this.MutateVerbose(ref _researchStartTime, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        private double _gapLengthByTalkingType = 0.2d;

        public double GapLengthByTalkingType
        {
            get => _gapLengthByTalkingType;
            set
            {
                this.MutateVerbose(ref _gapLengthByTalkingType, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private double _axisMaxValueByTalkingType = 0;

        public double AxisMaxValueByTalkingType
        {
            get => _axisMaxValueByTalkingType;
            set
            {
                this.MutateVerbose(ref _axisMaxValueByTalkingType, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private double _axisStepByTalkingType = 0;
        public double AxisStepByTalkingType
        {
            get => _axisStepByTalkingType;
            set
            {
                this.MutateVerbose(ref _axisStepByTalkingType, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private string _axisTitleByTalkingType = "谈话次数";
        public string AxisTitleByTalkingType
        {
            get => _axisTitleByTalkingType;
            set
            {
                this.MutateVerbose(ref _axisTitleByTalkingType, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        private string _researchEndTime;
        public string ResearchEndTime
        {
            get => _researchEndTime;
            set
            {
                this.MutateVerbose(ref _researchEndTime, value, args => PropertyChanged?.Invoke(this, args));

            }
        }

        public List<string> TalkingType;

        public void Search()
        {

            SatisticsDataByMonth.Clear();
            SatisticsDataByTalkingType.Clear();

            //ChongGuanSafetySupervisionQZ.DAL.TalkTypeDAL talkTypeDAL = new DAL.TalkTypeDAL();

            //var result_t = talkTypeDAL.Query();

            //if (result_t.IsSuccessed)
            //{
            //    TalkingType.Clear();

            //    List<QZ_TalkType> talkTypes = result_t.Data.ToList();

            //    foreach (var item in talkTypes)
            //    {
            //        TalkingType.Add(item.TalkTypeName);
            //        SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = item.TalkTypeName, Performance = 0 });
            //    }
            //}
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "对新收押人员首次谈话", ShowName = "对新收押\n人员首次\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "在押人员诉讼环节发生变化谈话", ShowName = "在押人员\n诉讼环节\n发生变化\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "会见律师后思想不稳定、表现异常的谈话", ShowName = "会见律师后\n思想不稳定、\n表现异常的\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "在押人员家庭发生变故谈话", ShowName = "在押人员家庭\n发生变故\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "被加戴械具、处罚前后谈话", ShowName = "被加戴械具、\n处罚前后\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "调换监室的谈话", ShowName = "调换监室的\n谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "要求反映监室动态的谈话", ShowName = "要求反映\n监室动态\n的谈话", Performance = 0 });
            SatisticsDataByTalkingType.Add(new SatisticsDataModel { Name = "出所前谈话", ShowName = "出所前\n谈话", Performance = 0 });

            DateTime startDateTime = DateTime.Parse(_researchStartTime);
            DateTime endDateTime = DateTime.Parse(_researchEndTime);

            ChongGuanSafetySupervisionQZ.DAL.InquiryDAL inquiryDAL = new DAL.InquiryDAL();
            var t = inquiryDAL.QureyByDate(startDateTime, endDateTime);

            if (t.IsSuccessed)
            {
                DateTime dateTime_t = new DateTime(1900, 1, 1);
                foreach (var item in t.Data)
                {
                    DateTime dateTime_Item = DateTime.Parse(item.InquiryDate.ToString());

                    if (dateTime_Item.Year > dateTime_t.Year || (dateTime_Item.Year == dateTime_t.Year && dateTime_Item.Month > dateTime_t.Month))
                    {
                        Debug.WriteLine(dateTime_Item);
                        dateTime_t = dateTime_Item;
                        SatisticsDataByMonth.Add(new SatisticsDataModel
                        {
                            //Name = GetDateStringByDateTime(dateTime_t),
                            Name = dateTime_t.ToString("yyyy-MM"),
                            Performance = 1,
                            Remarks = item.InquiryId
                        });

                    }
                    else if (dateTime_Item.Year == dateTime_t.Year && dateTime_Item.Month == dateTime_t.Month)
                    {
                        SatisticsDataByMonth[SatisticsDataByMonth.Count - 1].Performance++;
                    }

                    foreach (var s in SatisticsDataByTalkingType)
                    {
                        if (item.InquiryTalkType == s.Name)
                        {
                            s.Performance++;
                            s.Remarks = item.InquiryId;
                        }
                    }
                }
            }
        }

        public string GetDateStringByDateTime(DateTime dateTime)
        {
            StringBuilder stringBuilder = new StringBuilder();

            switch (dateTime.Year - DateTime.Now.Year)
            {
                case 0:
                    stringBuilder.Append("今年");
                    break;

                case -1:
                    stringBuilder.Append("去年");
                    break;

                case 2:
                    stringBuilder.Append("前年");
                    break;

                default:
                    stringBuilder.Append("今年");
                    break;
            }

            switch (dateTime.Month)
            {
                case 1:
                    stringBuilder.Append("一月");
                    break;
                case 2:
                    stringBuilder.Append("二月");
                    break;
                case 3:
                    stringBuilder.Append("三月");
                    break;
                case 4:
                    stringBuilder.Append("四月");
                    break;
                case 5:
                    stringBuilder.Append("五月");
                    break;
                case 6:
                    stringBuilder.Append("六月");
                    break;
                case 7:
                    stringBuilder.Append("七月");
                    break;
                case 8:
                    stringBuilder.Append("八月");
                    break;
                case 9:
                    stringBuilder.Append("九月");
                    break;
                case 10:
                    stringBuilder.Append("十月");
                    break;
                case 11:
                    stringBuilder.Append("十一月");
                    break;
                case 12:
                    stringBuilder.Append("十二月");
                    break;
            }

            return stringBuilder.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
