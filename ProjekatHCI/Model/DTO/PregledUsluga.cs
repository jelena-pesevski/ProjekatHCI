using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class PregledUsluga
    {
        public int IdPopravke { get; set; }
        public int IdUsluge { get; set; }
        public  string Naziv { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }

        public PregledUsluga(int idPopravke, int idUsluge, string naziv, double cijena, int kolicina)
        {
            IdPopravke = idPopravke;
            IdUsluge = idUsluge;
            Naziv = naziv;
            Cijena = cijena;
            Kolicina = kolicina;
        }

        public override bool Equals(object obj)
        {
            return obj is PregledUsluga usluga &&
                   IdPopravke == usluga.IdPopravke &&
                   IdUsluge == usluga.IdUsluge;
        }

        public override int GetHashCode()
        {
            int hashCode = 1814186071;
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + IdUsluge.GetHashCode();
            return hashCode;
        }
    }
}
