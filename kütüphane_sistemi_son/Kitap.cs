using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class Kitap
    {
        public string isim;
        public string yazar;
        public string yayinevi;
        public List<string> sayfalar;

        public Kitap(string isim, string yazar, string yayinevi, List<string> sayfalar)
        {
            this.isim = isim;
            this.yazar = yazar;
            this.yayinevi = yayinevi;
            this.sayfalar = sayfalar;
        }
    }
}   

