namespace ChongGuanSafetySupervisionQZ.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelQZ : DbContext
    {
        private static string ConnectStr()
        {
            return $"data source={AppDomain.CurrentDomain.BaseDirectory}database\\QZDatabase.db";
        }

        public ModelQZ()
            : base("name=ModelQZ")
        //: base(ConnectStr())
        {
            //_modelQZ.Database.Initialize(false);
        }

        static ModelQZ()
        {
            _modelQZ = new ModelQZ();
            //_modelQZ.Database.Initialize(false);
            //_modelQZ.Database.Connection.Open();
        }

        private static ModelQZ _modelQZ = null;

        public static ModelQZ DatabaseContext
        {
            get
            {
                if (_modelQZ == null)
                {
                    _modelQZ = new ModelQZ();
                    _modelQZ.Database.Initialize(false);
                }

                return _modelQZ;
            }
        }


        public virtual DbSet<QZ_Areas> QZ_Areas { get; set; }
        public virtual DbSet<QZ_Deparment> QZ_Deparment { get; set; }
        public virtual DbSet<QZ_Deparment_User> QZ_Deparment_User { get; set; }
        public virtual DbSet<QZ_Event> QZ_Event { get; set; }
        public virtual DbSet<QZ_Inquiry> QZ_Inquiry { get; set; }
        public virtual DbSet<QZ_Party> QZ_Party { get; set; }
        public virtual DbSet<QZ_Role> QZ_Role { get; set; }
        public virtual DbSet<QZ_Role_User> QZ_Role_User { get; set; }
        public virtual DbSet<QZ_User> QZ_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
