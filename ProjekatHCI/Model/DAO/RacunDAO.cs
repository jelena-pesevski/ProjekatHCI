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
    public class RacunDAO : GenericDAO<Racun>
    {
        protected override string getTableName()
        {
            return "račun";
        }

        protected override Racun ParseLine(DbDataReader reader)
        {
            return new Racun(reader.GetInt32(0), reader.GetInt32(1), reader.GetDouble(2));
        }

        protected override MySqlCommand PrepareDeleteCommand(Racun t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Racun t, MySqlConnection conn)
        {
            string query = "SELECT * FROM račun WHERE IdRacuna=@IdRacuna;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdRacuna", t.IdRacuna);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(Racun t, MySqlConnection conn)
        {
            string query = @"INSERT INTO račun (IdPopravke, Cijena) VALUES (@IdPopravke, @Cijena);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(Racun t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

    }
}
