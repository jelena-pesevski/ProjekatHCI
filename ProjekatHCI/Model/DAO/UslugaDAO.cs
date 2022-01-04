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
    public class UslugaDAO : GenericDAO<Usluga>
    {
        protected override string getTableName()
        {
            return "usluga";
        }

        protected override Usluga ParseLine(DbDataReader reader)
        {
            return new Usluga(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2));
        }

        protected override MySqlCommand PrepareDeleteCommand(Usluga t, MySqlConnection conn)
        {
            string query = "DELETE FROM usluga WHERE IdUsluge=@IdUsluge;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Usluga t, MySqlConnection conn)
        {
            string query = "SELECT * FROM usluga WHERE IdUsluge=@IdUsluge;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(Usluga t, MySqlConnection conn)
        {
            string query = @"INSERT INTO usluga (Naziv, Cijena) VALUES (@Naziv, @Cijena);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Naziv", t.Naziv);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(Usluga t, MySqlConnection conn)
        {
            string query = @"UPDATE usluga SET Naziv=@Naziv, Cijena=@Cijena WHERE IdUsluge=@IdUsluge;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Naziv", t.Naziv);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            return command;
        }

        protected async override Task PostInsertQuery(Usluga t, long lastInsertedId, MySqlConnection conn)
        {
            t.IdUsluge = (int)lastInsertedId;
        }
    }


}
