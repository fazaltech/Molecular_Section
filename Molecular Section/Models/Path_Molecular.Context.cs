using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Molecular_Section.Models
{
    public class Path_Molecular:DbContext
    {

        public DbSet<idrl> idrl { get; set; }
        //public DbSet<tbl_roles> tbl_roles { get; set; }
        public DbSet<user_dash> user_dash { get; set; }
        public DbSet<Specimen> specimens { get; set; }
        public DbSet<site> sites { get; set; }

        public Path_Molecular()
                 : base("molesection")
        {
            Database.CreateIfNotExists();
        }

    }
}