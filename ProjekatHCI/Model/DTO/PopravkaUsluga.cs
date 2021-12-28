using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class PopravkaUsluga
    {
        public int IdPopravke { get; set; }
        public int IdUsluge { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }

        public PopravkaUsluga(int idPopravke, int idUsluge, int kolicina, double cijena)
        {
            IdPopravke = idPopravke;
            IdUsluge = idUsluge;
            Kolicina = kolicina;
            Cijena = cijena;
        }

        public override bool Equals(object obj)
        {
            return obj is PopravkaUsluga usluga &&
                   IdPopravke == usluga.IdPopravke &&
                   IdUsluge == usluga.IdUsluge &&
                   Kolicina == usluga.Kolicina &&
                   Cijena == usluga.Cijena;
        }

        public override int GetHashCode()
        {
            int hashCode = 1198515557;
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + IdUsluge.GetHashCode();
            hashCode = hashCode * -1521134295 + Kolicina.GetHashCode();
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            return hashCode;
        }
    }
}
