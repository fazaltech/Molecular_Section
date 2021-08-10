using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Molecular_Section.Models
{
    [Table("user_dash")]
    public class user_dash
    {

        public long Id { get; set; }
        public string user_name { get; set; }
        public string email_id { get; set; }
        public string password { get; set; }
        public string role { get; set; }

    }
}