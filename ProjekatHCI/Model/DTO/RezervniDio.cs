using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class RezervniDio
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }

        public RezervniDio(int sifra, string naziv, double cijena, int kolicina)
        {
            Sifra = sifra;
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
        }

        public override bool Equals(object obj)
        {
            return obj is RezervniDio dio &&
                   Sifra == dio.Sifra &&
                   Naziv == dio.Naziv &&
                   Cijena == dio.Cijena &&
                   Kolicina == dio.Kolicina;
        }

        public override int GetHashCode()
        {
            int hashCode = 1516421651;
            hashCode = hashCode * -1521134295 + Sifra.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Naziv);
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            hashCode = hashCode * -1521134295 + Kolicina.GetHashCode();
            return hashCode;
        }
    }
}
