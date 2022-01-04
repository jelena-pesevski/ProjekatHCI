using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Majstor :Zaposleni
    {
        public int BrojZaduzenja { get; set; }

        public Majstor(int idZaposlenog) : base(idZaposlenog)
        {

        }

        public Majstor(int idZaposlenog, int brZaduzenja) : base(idZaposlenog)
        {
            BrojZaduzenja = brZaduzenja;
        }

        public Majstor(int idZaposlenog, string ime, string prezime, string korisnickoIme, string lozinka, string tip, int tema, string jezik, string status) : base(idZaposlenog, ime, prezime, korisnickoIme, lozinka, tip, tema, jezik, status)
        {

        }
    }
}
