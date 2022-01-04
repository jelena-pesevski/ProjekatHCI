using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatHCI.Model.DTO
{
    public class PrijavaKvara
    {
        public int IdPrijave { get; set; }
        public DateTime DatumPrijave { get; set; }
        public string Opis { get; set; }
        public string Status { get; set; }

        public int Operater_IdZaposlenog { get; set; }

        public int Majstor_IdZaposlenog { get; set; }
        public int IdKlijenta { get; set; }

        public PrijavaKvara(string opis, int operater_IdZaposlenog, int idKlijenta)
        {
            IdPrijave = 0;
            Opis = opis;
            Operater_IdZaposlenog = operater_IdZaposlenog;
            IdKlijenta = idKlijenta;
        }

        public PrijavaKvara(int idPrijave, DateTime datumPrijave, string opis, int operater_IdZaposlenog, int idKlijenta, int majstor_IdZaposlenog, string status)
        {
            IdPrijave = idPrijave;
            DatumPrijave = datumPrijave;
            Opis = opis;
            Status = status;
            Operater_IdZaposlenog = operater_IdZaposlenog;
            Majstor_IdZaposlenog = majstor_IdZaposlenog;
            IdKlijenta = idKlijenta;
        }

        public override bool Equals(object obj)
        {
            return obj is PrijavaKvara kvara &&
                   IdPrijave == kvara.IdPrijave;
        }

        public override int GetHashCode()
        {
            return 1872032777 + IdPrijave.GetHashCode();
        }
    }
}
