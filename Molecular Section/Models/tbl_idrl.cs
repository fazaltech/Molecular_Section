using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Molecular_Section.Models
{
    [Table("idrl")]
    public class idrl
    {

        public long id { get; set; }

        public int site_id { get; set; }
        public string specimen { get; set; }
        public string datesample { get; set; }
        public string timesample { get; set; }
        public string receivedby { get; set; }
        public string temloggerinclude { get; set; }
        public string tempcoleman { get; set; }
        public string samplereceipt { get; set; }
        public string laboratoryremark { get; set; }
        public string rnaextractiondate { get; set; }
        public string rnaremark { get; set; }
        public string realtimepcrn1ddmmyy { get; set; }
        public string realtimepcrn1gen { get; set; }
        public string ctvaluep1 { get; set; }
        public string n1gneremark { get; set; }
        public string realtimepcrn2ddmmyy { get; set; }
        public string realtimepcrn2gen { get; set; }
        public string ctvaluep2 { get; set; }
        public string n2gneremark { get; set; }
        public string realtimepcrnEgen { get; set; }
        public string realtimepcrn3Egen { get; set; }
        public string ctvaluep3 { get; set; }
        public string n_e_gneremark { get; set; }
        public string target1gcl { get; set; }
        public string target2gcl { get; set; }
        public string target1limitgcl { get; set; }
        public string target2limitgcl { get; set; }
        public string target1pre_absen { get; set; }
        public string target2pre_absen { get; set; }
        public string flowm3day { get; set; }
        public string passqaqc { get; set; }
        public string waterquality { get; set; }
        public string watervalue { get; set; }
        public string collectmethod { get; set; }
        public string note { get; set; }
        public string entry_date { get; set; }
        public string user_name { get; set; }




    }
}