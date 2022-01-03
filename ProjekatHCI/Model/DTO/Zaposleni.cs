using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class Zaposleni
    {
        public int IdZaposlenog { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Tip { get; set; }
        public int Tema { get; set; }
        public string Jezik { get; set; }
        public string Status { get; set; }

        public string LozinkaZaPrikaz { get; set; }

        public Zaposleni(int idZaposlenog)
        {
            IdZaposlenog = idZaposlenog;
        }

        public Zaposleni(int idZaposlenog, string ime, string prezime, string korisnickoIme, string lozinka, string tip, int tema, string jezik, string status)
        {
            IdZaposlenog = idZaposlenog;
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Tip = tip;
            Tema = tema;
            Jezik = jezik;
            Status = status;
        }

        public override bool Equals(object obj)
        {
            return obj is Zaposleni zaposleni &&
                   IdZaposlenog == zaposleni.IdZaposlenog;
        }

        public override int GetHashCode()
        {
            return -742774186 + IdZaposlenog.GetHashCode();
        }
    }
}
