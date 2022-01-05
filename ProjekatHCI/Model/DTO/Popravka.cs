using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Popravka
    {
        public int IdPopravke { get; set; }
        public int IdZaposlenog { get; set; }
        public int IdPrijave { get; set; }
        public DateTime Pocetak { get; set; }
        public Nullable<DateTime> Zavrsetak { get; set; }
        public bool Zavrseno { get; set; }

        public Popravka(int idZaposlenog, int idPrijave)
        {
            IdZaposlenog = idZaposlenog;
            IdPrijave = idPrijave;
        }

        public Popravka(int idPopravke, int idZaposlenog, int idPrijave, DateTime pocetak, DateTime zavrsetak, bool zavrseno)
        {
            IdPopravke = idPopravke;
            IdZaposlenog = idZaposlenog;
            IdPrijave = idPrijave;
            Pocetak = pocetak;
            Zavrsetak = zavrsetak;
            Zavrseno = zavrseno;
        }

        public Popravka(int idPopravke, int idZaposlenog, int idPrijave, DateTime pocetak, bool zavrseno)
        {
            IdPopravke = idPopravke;
            IdZaposlenog = idZaposlenog;
            IdPrijave = idPrijave;
            Pocetak = pocetak;
            Zavrseno = zavrseno;
        }

        public override bool Equals(object obj)
        {
            return obj is Popravka popravka &&
                   IdPopravke == popravka.IdPopravke &&
                   IdZaposlenog == popravka.IdZaposlenog &&
                   IdPrijave == popravka.IdPrijave &&
                   Pocetak == popravka.Pocetak &&
                   Zavrsetak == popravka.Zavrsetak &&
                   Zavrseno == popravka.Zavrseno;
        }

        public override int GetHashCode()
        {
            int hashCode = -2061120302;
            hashCode = hashCode * -1521134295 + IdPopravke.GetHashCode();
            hashCode = hashCode * -1521134295 + IdZaposlenog.GetHashCode();
            hashCode = hashCode * -1521134295 + IdPrijave.GetHashCode();
            hashCode = hashCode * -1521134295 + Pocetak.GetHashCode();
            hashCode = hashCode * -1521134295 + Zavrsetak.GetHashCode();
            hashCode = hashCode * -1521134295 + Zavrseno.GetHashCode();
            return hashCode;
        }
    }
}
