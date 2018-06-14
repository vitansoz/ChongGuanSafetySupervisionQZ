using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.ViewModel
{
    public class ChinaCitiesViewModel : INotifyPropertyChanged
    {
        private Location _selectedItem;

        public Location SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                this.MutateVerbose(ref _selectedItem, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        public ObservableCollection<Povince> Povinces { get; }

        public ObservableCollection<Location> Locations { get; }

        public ChinaCitiesViewModel(IList<QZ_Areas> qZ_AreasList)
        {
/*
            var p = from q in qZ_AreasList
                    where q.AreaLevel == 1
                    select new Povince
                    {
                        AreaId = q.AreaId,
                        AreaLevel = q.AreaLevel,
                        AreaName = q.AreaName,
                        AreaPid = q.AreaPid,
                        //Cities = new ObservableCollection<City>()
                    };

            Povinces = new ObservableCollection<Povince>(p.ToList());

            var c = from q in qZ_AreasList
                    where q.AreaLevel == 2
                    select new City
                    {
                        AreaId = q.AreaId,
                        AreaLevel = q.AreaLevel,
                        AreaName = q.AreaName,
                        AreaPid = q.AreaPid,
                        //Districts = new ObservableCollection<District>()
                    };

            List<City> cities = new List<City>(c.ToList());

            var d = from q in qZ_AreasList
                    where q.AreaLevel == 2
                    select new District
                    {
                        AreaId = q.AreaId,
                        AreaLevel = q.AreaLevel,
                        AreaName = q.AreaName,
                        AreaPid = q.AreaPid
                    };
            List<District> districts = new List<District>(d.ToList());


            foreach (var c_t in cities)
            {
                var dd = from ddd in districts
                         where ddd.AreaPid == c_t.AreaId
                         select ddd;

                c_t.Districts = new ObservableCollection<District>(dd.ToList());
            }

            foreach (var p_t in Povinces)
            {
                var cc = from ccc in cities
                         where ccc.AreaPid == p_t.AreaId
                         select ccc;

                p_t.Cities = new ObservableCollection<City>(cc.ToList());
            }
            */

            var p2 = from q in qZ_AreasList
                     where q.AreaLevel == 1
                     select new Location
                     {
                         AreaId = q.AreaId,
                         AreaLevel = q.AreaLevel,
                         AreaName = q.AreaName,
                         AreaPid = q.AreaPid,
                         //Cities = new ObservableCollection<City>()
                     };

            Locations = new ObservableCollection<Location>(p2.ToList());

            foreach (var p2t in Locations)
            {
                var p3 = from q in qZ_AreasList
                         where q.AreaPid == p2t.AreaId
                         select new Location
                         {
                             AreaId = q.AreaId,
                             AreaLevel = q.AreaLevel,
                             AreaName = q.AreaName,
                             AreaPid = q.AreaPid                          
                         };

                p2t.Children = new ObservableCollection<Location>(p3.ToList());

                foreach (var p3t in p2t.Children)
                {
                    var p4 = from q in qZ_AreasList
                             where q.AreaPid == p3t.AreaId
                             select new Location
                             {
                                 AreaId = q.AreaId,
                                 AreaLevel = q.AreaLevel,
                                 AreaName = q.AreaName,
                                 AreaPid = q.AreaPid
                             };

                    p3t.Children = new ObservableCollection<Location>(p4.ToList());
                }
            }

            _selectedItem = new Location { AreaId = "0", AreaName = "请选择所在地区" };
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Povince
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public int AreaLevel { get; set; }
        public string AreaPid { get; set; }

        public ObservableCollection<City> Cities { get; set; }
    }

    public class City
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public int AreaLevel { get; set; }
        public string AreaPid { get; set; }

        public ObservableCollection<District> Districts { get; set; }
    }

    public class District
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public int AreaLevel { get; set; }
        public string AreaPid { get; set; }
    }

    public class Location
    {
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public int AreaLevel { get; set; }
        public string AreaPid { get; set; }

        public ObservableCollection<Location> Children { get; set; }
    }



}
