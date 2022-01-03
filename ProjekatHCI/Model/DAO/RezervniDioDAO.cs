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
    public class RezervniDioDAO : GenericDAO<RezervniDio>
    {
        protected override string getTableName()
        {
            return "rezervnidio";
        }

        protected override RezervniDio ParseLine(DbDataReader reader)
        {
            return new RezervniDio(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2), reader.GetInt32(3));
        }

        protected override MySqlCommand PrepareDeleteCommand(RezervniDio t, MySqlConnection conn)
        {
            string query = "DELETE FROM rezervnidio WHERE Sifra=@Sifra;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(RezervniDio t, MySqlConnection conn)
        {
            string query = "SELECT * FROM rezervnidio WHERE Sifra=@Sifra;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(RezervniDio t, MySqlConnection conn)
        {
            string query = @"INSERT INTO rezervnidio (Naziv, Cijena, Kolicina) VALUES (@Naziv, @Cijena, @Kolicina);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Naziv", t.Naziv);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(RezervniDio t, MySqlConnection conn)
        {
            string query = @"UPDATE rezervnidio SET Naziv=@Naziv, Cijena=@Cijena, Kolicina=@Kolicina WHERE Sifra=@Sifra;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Naziv", t.Naziv);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            return command;
        }
    }
}
