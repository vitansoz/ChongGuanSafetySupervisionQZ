using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel.BussinessModel
{
    public class SatisticsDataModel : INotifyPropertyChanged
    {
        private double _performance;

        public double Performance
        {
            get => _performance;
            set
            {
                this.MutateVerbose(ref _performance, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                this.MutateVerbose(ref _name, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _showName;
        public string ShowName
        {
            get => _showName;
            set
            {
                this.MutateVerbose(ref _showName, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        private string _remarks;
        public string Remarks
        {
            get => _remarks;
            set
            {
                this.MutateVerbose(ref _remarks, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
