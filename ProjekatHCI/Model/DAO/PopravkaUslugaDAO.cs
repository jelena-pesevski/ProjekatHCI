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
    public class PopravkaUslugaDAO : GenericDAO<PopravkaUsluga>
    {
        protected override string getTableName()
        {
            throw new NotImplementedException();
        }

        protected override PopravkaUsluga ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(PopravkaUsluga t, MySqlConnection conn)
        {
            string query = "DELETE FROM popravka_usluga WHERE IdPopravke=@IdPopravke AND IdUsluge=@IdUsluge";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PopravkaUsluga t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(PopravkaUsluga t, MySqlConnection conn)
        {
            string query = @"INSERT INTO popravka_usluga (IdPopravke, IdUsluge, Kolicina, Cijena) VALUES (@IdPopravke, @IdUsluge, @Kolicina, @Cijena);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(PopravkaUsluga t, MySqlConnection conn)
        {
            string query = @"UPDATE popravka_usluga SET Cijena=@Cijena, Kolicina=@Kolicina WHERE IdPopravke=@IdPopravke AND IdUsluge=@IdUsluge";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@IdUsluge", t.IdUsluge);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            return command;
        }
    }
}
