using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    class StavkePregled
    {
        public string Naziv { set; get; }

        public int Kolicina { set; get; }

        public double Cijena { set; get; }

        public double Ukupno { set; get; }

        public int IdRacuna { set; get; }

        public StavkePregled(string naziv, int kolicina, double cijena, double ukupno, int idRacuna)
        {
            Naziv = naziv;
            Kolicina = kolicina;
            Cijena = cijena;
            Ukupno = ukupno;
            IdRacuna = idRacuna;
        }
    }
}
