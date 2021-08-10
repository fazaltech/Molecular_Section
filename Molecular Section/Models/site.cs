using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Molecular_Section.Models
{
    [Table("site")]
    public class site
    {
        public int id { get; set; }
        public int site_id { get; set; }
        public string site_name { get; set; }
        public int colflag { get; set; }
    }
}