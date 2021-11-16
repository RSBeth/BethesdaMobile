using System;
using System.Collections.Generic;
using System.Text;

namespace BethesdaMobileXamarin.Registration.Model
{
   public class NewPatientResults
    {
        public string NoRM { get; set; }
        public string NamaPasien { get; set; }


        public string vc_no_ktp { get; set; }

        public string Alamat { get; set; }

        public string vc_tp_lhr { get; set; }

        public string dtgllahir { get; set; }


        public string vc_email { get; set; }

        public string deskripsiresponse { get; set; }

        public string response { get; set; }

        public string vc_telpon { get; set; }

        public int IN_umurth { get; set; }
        public int IN_umurbl { get; set; }
        public int IN_umurhr { get; set; }
    }
}
