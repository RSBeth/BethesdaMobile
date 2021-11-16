using System;
using System.Collections.Generic;
using System.Text;

namespace BethesdaMobileXamarin.Registration.Model
{
   public class NewPatient
    {
        public string vc_no_ktp { get; set; }
        public string NamaPasien { get; set; }
        public string Alamat { get; set; }

        public string VC_jenis_k { get; set; }

        public string vc_tp_lhr { get; set; }

        public string dtgllahir { get; set; }

        public int IN_umurth { get; set; }
        public int IN_umurbl { get; set; }
        public int IN_umurhr { get; set; }
        public string vc_k_agm { get; set; }

        public string vc_k_status { get; set; }

        public string vc_k_negara { get; set; }

        public string vc_turis { get; set; }

        public string vc_k_pek { get; set; }


        public string vc_k_pend { get; set; }

        public string vc_telpon { get; set; }

        public string vc_k_prop { get; set; }

        public string VC_propinsi { get; set; }

        public string vc_k_kota { get; set; }

        public string vc_kota { get; set; }

        public string vc_kode_camat { get; set; }

        public string vc_kecamatan { get; set; }
        
        public string vc_kelurahan { get; set; }
        public string vc_nik { get; set; }
        public string VC_gol_d { get; set; }

        public string vc_BB { get; set; }

        public string vc_TB { get; set; }

        public string vc_kd_cacat { get; set; }


        public string vc_no_peserta_bpjs  { get; set; }

        public string vc_email { get; set; }

        public string filebtye { get; set; }
    }
}
