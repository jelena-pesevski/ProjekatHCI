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

        public PrijavaKvara(int idPrijave, DateTime datumPrijave, string opis, string status, int operater_IdZaposlenog, int majstor_IdZaposlenog, int idKlijenta)
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
                   IdPrijave == kvara.IdPrijave &&
                   DatumPrijave == kvara.DatumPrijave &&
                   Opis == kvara.Opis &&
                   Status == kvara.Status &&
                   Operater_IdZaposlenog == kvara.Operater_IdZaposlenog &&
                   Majstor_IdZaposlenog == kvara.Majstor_IdZaposlenog &&
                   IdKlijenta == kvara.IdKlijenta;
        }

        public override int GetHashCode()
        {
            int hashCode = -1030244393;
            hashCode = hashCode * -1521134295 + IdPrijave.GetHashCode();
            hashCode = hashCode * -1521134295 + DatumPrijave.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Opis);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Status);
            hashCode = hashCode * -1521134295 + Operater_IdZaposlenog.GetHashCode();
            hashCode = hashCode * -1521134295 + Majstor_IdZaposlenog.GetHashCode();
            hashCode = hashCode * -1521134295 + IdKlijenta.GetHashCode();
            return hashCode;
        }
    }
}
