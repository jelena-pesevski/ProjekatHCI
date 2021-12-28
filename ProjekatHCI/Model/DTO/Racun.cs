using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Racun
    {
        public int IdRacuna { get; set; }
        public int IdPopravke { get; set; }
        public double Cijena { get; set; }

        public Racun(int idRacuna, int idPopravke, double cijena)
        {
            IdRacuna = idRacuna;
            IdPopravke = idPopravke;
            Cijena = cijena;
        }

        public override bool Equals(object obj)
        {
            return obj is Racun racun &&
                   IdRacuna == racun.IdRacuna &&
                   IdPopravke == racun.IdPopravke &&
                   Cijena == racun.Cijena;
        }

        public override int GetHashCode()
        {
            int hashCode = 1724817869;
            hashCode = hashCode * -1521134295 + IdRacuna.GetHashCode();
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            return hashCode;
        }
    }
}
