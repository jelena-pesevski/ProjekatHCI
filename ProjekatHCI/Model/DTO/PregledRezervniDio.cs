using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    class PregledRezervniDio
    {
        public int IdPopravke { get; set; }
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }

        public PregledRezervniDio(int idPopravke, int sifra, string naziv, double cijena, int kolicina)
        {
            IdPopravke = idPopravke;
            Sifra = sifra;
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
        }

        public override bool Equals(object obj)
        {
            return obj is PregledRezervniDio dio &&
                   IdPopravke == dio.IdPopravke &&
                   Sifra == dio.Sifra;
        }

        public override int GetHashCode()
        {
            int hashCode = 1963163652;
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + Sifra.GetHashCode();
            return hashCode;
        }
    }
}
