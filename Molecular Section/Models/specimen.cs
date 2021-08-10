using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Molecular_Section.Models
{
    [Table("specimen")]
    public class Specimen
    {
         public int id { get; set; }
        public int site_id { get; set; }
        public string specimen { get; set; }
        public int colflag { get; set; }
    }
}