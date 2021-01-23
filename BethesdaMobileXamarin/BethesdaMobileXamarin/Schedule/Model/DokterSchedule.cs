using System;
using System.Collections.Generic;
using System.Text;

namespace BethesdaMobileXamarin.Schedule.Model
{
    class DokterSchedule
    {

        public string NamaDokter { get; set; }

        public string Namaklinik { get; set; }
        public string hari { get; set; }

        public string jam_dari { get; set; }

        public string jam_selesai { get; set; }

        public string jam_full { get; set; }
    }
}
