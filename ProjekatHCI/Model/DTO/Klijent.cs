using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Klijent
    {
        public int IdKlijenta { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public Klijent(int idKlijenta, string ime, string prezime, string adresa, string telefon)
        {
            IdKlijenta = idKlijenta;
            Ime = ime;
            Prezime = prezime;
            Adresa = adresa;
            Telefon = telefon;
        }

        public override bool Equals(object obj)
        {
            return obj is Klijent klijent &&
                   IdKlijenta == klijent.IdKlijenta &&
                   Ime == klijent.Ime &&
                   Prezime == klijent.Prezime &&
                   Adresa == klijent.Adresa &&
                   Telefon == klijent.Telefon;
        }

        public override int GetHashCode()
        {
            int hashCode = -232831552;
            hashCode = hashCode * -1521134295 + IdKlijenta.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Ime);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Prezime);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Adresa);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Telefon);
            return hashCode;
        }
    }
}
