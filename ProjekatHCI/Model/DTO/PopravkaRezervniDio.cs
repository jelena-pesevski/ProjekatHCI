using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class PopravkaRezervniDio
    {
        public int IdPopravke { get; set; }
        public int Sifra { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }

        public PopravkaRezervniDio(int idPopravke, int sifra, int kolicina, double cijena)
        {
            IdPopravke = idPopravke;
            Sifra = sifra;
            Kolicina = kolicina;
            Cijena = cijena;
        }

        public override bool Equals(object obj)
        {
            return obj is PopravkaRezervniDio dio &&
                   IdPopravke == dio.IdPopravke &&
                   Sifra == dio.Sifra &&
                   Kolicina == dio.Kolicina &&
                   Cijena == dio.Cijena;
        }

        public override int GetHashCode()
        {
            int hashCode = 98676322;
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + Sifra.GetHashCode();
            hashCode = hashCode * -1521134295 + Kolicina.GetHashCode();
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            return hashCode;
        }
    }
}
