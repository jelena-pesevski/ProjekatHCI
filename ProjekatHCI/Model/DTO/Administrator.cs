using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Administrator : Zaposleni
    {
        public Administrator(int idZaposlenog) : base(idZaposlenog)
        {

        }

        public Administrator(int idZaposlenog, string ime, string prezime, string korisnickoIme, string lozinka, string tip, int tema, string jezik, string status): base(idZaposlenog, ime, prezime, korisnickoIme, lozinka, tip, tema, jezik, status)
        {
         
        }
    }
}
