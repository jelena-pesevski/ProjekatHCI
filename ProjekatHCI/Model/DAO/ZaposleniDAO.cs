using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Model.DAO
{
    public class ZaposleniDAO : GenericDAO<Zaposleni>
    {

    
        protected override string getTableName()
        {
            return "zaposleni";
        }

        protected override Zaposleni ParseLine(DbDataReader reader)
        {
            return new Zaposleni(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetString(7), reader.GetString(8));
        }

        protected override MySqlCommand PrepareDeleteCommand(Zaposleni z, MySqlConnection conn)
        {
            string query= "DELETE FROM zaposleni WHERE IdZaposlenog=@IdZaposlenog";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", z.IdZaposlenog);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Zaposleni z, MySqlConnection conn)
        {
            string query = "SELECT * FROM zaposleni WHERE IdZaposlenog=@IdZaposlenog";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", z.IdZaposlenog);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(Zaposleni t, MySqlConnection conn)
        {
            string query = @"INSERT INTO zaposleni (Ime, Prezime, KorisničkoIme, Lozinka, Tip) VALUES (@Ime, @Prezime, @KorisničkoIme, @Lozinka, @Tip)";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Ime", t.Ime);
            command.Parameters.AddWithValue("@Prezime", t.Prezime);
            command.Parameters.AddWithValue("@KorisničkoIme", t.KorisnickoIme);
            command.Parameters.AddWithValue("@Lozinka", t.Lozinka);
            command.Parameters.AddWithValue("@Tip", t.Tip);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(Zaposleni t, MySqlConnection conn)
        {
            string query = @"UPDATE zaposleni SET Ime=@Ime, Prezime=@Prezime, KorisničkoIme=@KorisničkoIme, Lozinka=@Lozinka, Tip=@Tip, Tema=@Tema,  Jezik=@Jezik, Status=@Status WHERE IdZaposlenog=@IdZaposlenog;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Ime", t.Ime);
            command.Parameters.AddWithValue("@Prezime", t.Prezime);
            command.Parameters.AddWithValue("@KorisničkoIme", t.KorisnickoIme);
            command.Parameters.AddWithValue("@Lozinka", t.Lozinka);
            command.Parameters.AddWithValue("@Tip", t.Tip);
            command.Parameters.AddWithValue("@Tema", t.Tema);
            command.Parameters.AddWithValue("@Jezik", t.Jezik);
            command.Parameters.AddWithValue("@Status", t.Status);
            command.Parameters.AddWithValue("@IdZaposlenog", t.IdZaposlenog);
            return command;
        }


        override async protected Task<int> PreQuery(Zaposleni z)
        {
            if ("O".Equals(z.Tip))
            {
                OperaterDAO servis = new OperaterDAO();
                return await servis.Delete(new Operater(z.IdZaposlenog));
            }else if ("A".Equals(z.Tip))
            {
                AdministratorDAO servis = new AdministratorDAO();
                return await servis.Delete(new Administrator(z.IdZaposlenog));
            }
            else if("M".Equals(z.Tip))
            {
                MajstorDAO servis = new MajstorDAO();
                return await servis.Delete(new Majstor(z.IdZaposlenog));
            }
            else
            {
                return 0;
            }
        }
    }
}
