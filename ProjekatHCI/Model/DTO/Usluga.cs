using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Usluga
    {
        public int IdUsluge { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }

        public Usluga(int idUsluge, string naziv, double cijena)
        {
            IdUsluge = idUsluge;
            Naziv = naziv;
            Cijena = cijena;
        }

        public override bool Equals(object obj)
        {
            return obj is Usluga usluga &&
                   IdUsluge == usluga.IdUsluge &&
                   Naziv == usluga.Naziv &&
                   Cijena == usluga.Cijena;
        }

        public override int GetHashCode()
        {
            int hashCode = -2084577179;
            hashCode = hashCode * -1521134295 + IdUsluge.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Naziv);
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            return hashCode;
        }
    }
}
