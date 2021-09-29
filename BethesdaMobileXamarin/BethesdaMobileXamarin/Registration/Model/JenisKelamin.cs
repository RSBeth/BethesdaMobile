using System;
using System.Collections.Generic;
using System.Text;

namespace BethesdaMobileXamarin.Registration.Model
{
    class JenisKelamin
    {
        public JenisKelamin(string v1, string v2)
        {
            this.vc_kodeKelamin = v1;
            this.vc_jenisKelamin = v2;
        }

        public string vc_kodeKelamin { get; set; }
        public string vc_jenisKelamin { get; set; }
    }
}
