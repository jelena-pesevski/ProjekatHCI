using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Operater :Zaposleni
    {
        public Operater(int idZaposlenog) : base(idZaposlenog)
        {

        }

        public Operater(int idZaposlenog, string ime, string prezime, string korisnickoIme, string lozinka, string tip, int tema, string jezik, string status) : base(idZaposlenog, ime, prezime, korisnickoIme, lozinka, tip, tema, jezik, status)
        {

        }
    }
}
