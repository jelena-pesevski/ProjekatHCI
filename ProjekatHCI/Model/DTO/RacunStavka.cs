using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class RacunStavka

        //TODO dodati naziv?
    {
        public int BrojStavke { get; set; }
        public int IdRacuna { get; set; }
        public double Cijena { get; set; }
        public int Kolicina { get; set; }
        public int IdUsluge { get; set; }
        public int RezervniDio_Sifra { get; set; }

        public RacunStavka(int brojStavke, int idRacuna, double cijena, int kolicina, int idUsluge, int rezervniDio_Sifra)
        {
            BrojStavke = brojStavke;
            IdRacuna = idRacuna;
            Cijena = cijena;
            Kolicina = kolicina;
            IdUsluge = idUsluge;
            RezervniDio_Sifra = rezervniDio_Sifra;
        }

        public override bool Equals(object obj)
        {
            return obj is RacunStavka stavka &&
                   BrojStavke == stavka.BrojStavke &&
                   IdRacuna == stavka.IdRacuna &&
                   Cijena == stavka.Cijena &&
                   Kolicina == stavka.Kolicina &&
                   IdUsluge == stavka.IdUsluge &&
                   RezervniDio_Sifra == stavka.RezervniDio_Sifra;
        }

        public override int GetHashCode()
        {
            int hashCode = -1312060579;
            hashCode = hashCode * -1521134295 + BrojStavke.GetHashCode();
            hashCode = hashCode * -1521134295 + IdRacuna.GetHashCode();
            hashCode = hashCode * -1521134295 + Cijena.GetHashCode();
            hashCode = hashCode * -1521134295 + Kolicina.GetHashCode();
            hashCode = hashCode * -1521134295 + IdUsluge.GetHashCode();
            hashCode = hashCode * -1521134295 + RezervniDio_Sifra.GetHashCode();
            return hashCode;
        }
    }
}
