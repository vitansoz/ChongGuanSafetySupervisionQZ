using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Model
{

    public class Rootobject
    {
        public int error { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Banner banner { get; set; }
        public Notice[] notice { get; set; }
        public Messages messages { get; set; }
        public Box_Status box_status { get; set; }
        public string mall_url { get; set; }
        public string game_url { get; set; }
        public Userinfo userinfo { get; set; }
        public Version_Update version_update { get; set; }
    }

    public class Banner
    {
        public float interval { get; set; }
        public int count { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string title { get; set; }
        public string desc { get; set; }
        public string img_url { get; set; }
        public string url { get; set; }
    }

    public class Messages
    {
        public int total { get; set; }
        public int unread { get; set; }
        public Datum1[] data { get; set; }
    }

    public class Datum1
    {
        public Type1[] type1 { get; set; }
        public Type2[] type2 { get; set; }
        public Type3[] type3 { get; set; }
        public Type4[] type4 { get; set; }
        public Type5[] type5 { get; set; }
    }

    public class Type1
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Type2
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Type3
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Type4
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Type5
    {
        public int id { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public int status { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class Box_Status
    {
        public object box { get; set; }
        public bool is_admin { get; set; }
    }

    public class Userinfo
    {
        public string name { get; set; }
        public string nickname { get; set; }
        public string headimg { get; set; }
        public string phone { get; set; }
        public Province province { get; set; }
        public City city { get; set; }
        public School school { get; set; }
        public Building building { get; set; }
        public Room room { get; set; }
        public bool is_box_admin { get; set; }
        public bool is_subscribe_wechat { get; set; }
        public int box_id { get; set; }
        public string my_qrcode { get; set; }
        public string student_id_card { get; set; }
        public string last_use_box_id { get; set; }
        public int balance { get; set; }
        public string aboutus { get; set; }
        public string matriculate { get; set; }
        public string[] address { get; set; }
    }

    public class Province
    {
        public string name { get; set; }
        public int province_id { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public int city_id { get; set; }
    }

    public class School
    {
        public string name { get; set; }
        public int school_id { get; set; }
    }

    public class Building
    {
        public string name { get; set; }
        public int building_id { get; set; }
    }

    public class Room
    {
        public string name { get; set; }
        public int room_id { get; set; }
    }

    public class Version_Update
    {
        public bool need_update { get; set; }
        public bool need_force_update { get; set; }
        public string latest_version_url { get; set; }
    }

    public class Notice
    {
        public string content { get; set; }
    }

}
