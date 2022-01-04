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
    public class KlijentDAO : GenericDAO<Klijent>
    {
        protected override string getTableName()
        {
            return "klijent";
        }

        protected override Klijent ParseLine(DbDataReader reader)
        {
            return new Klijent(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
        }

        protected override MySqlCommand PrepareDeleteCommand(Klijent t, MySqlConnection conn)
        {
            string query = "DELETE FROM klijent WHERE IdKlijenta=@IdKlijenta";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdKlijenta",t.IdKlijenta);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Klijent t, MySqlConnection conn)
        {
            string query = "SELECT * FROM klijent WHERE IdKlijenta=@IdKlijenta";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdKlijenta", t.IdKlijenta);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(Klijent t, MySqlConnection conn)
        {
            string query = @"INSERT INTO klijent (Ime, Prezime, Adresa, Telefon) VALUES (@Ime, @Prezime, @Adresa, @Telefon);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Ime", t.Ime);
            command.Parameters.AddWithValue("@Prezime", t.Prezime);
            command.Parameters.AddWithValue("@Adresa", t.Adresa);
            command.Parameters.AddWithValue("@Telefon", t.Telefon);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(Klijent t, MySqlConnection conn)
        {
            string query = @"UPDATE klijent SET Ime=@Ime, Prezime=@Prezime, Adresa=@Adresa, Telefon=@Telefon WHERE IdKlijenta=@IdKlijenta;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Ime", t.Ime);
            command.Parameters.AddWithValue("@Prezime", t.Prezime);
            command.Parameters.AddWithValue("@Adresa", t.Adresa);
            command.Parameters.AddWithValue("@Telefon", t.Telefon);
            command.Parameters.AddWithValue("@IdKlijenta", t.IdKlijenta);
            return command;
        }

        protected async override Task PostInsertQuery(Klijent t, long lastInsertedId, MySqlConnection conn)
        {
            t.IdKlijenta = (int)lastInsertedId;
        }

    }
}
