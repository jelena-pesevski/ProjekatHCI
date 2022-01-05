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
    public class PopravkaRezervniDioDAO : GenericDAO<PopravkaRezervniDio>
    {
        protected override string getTableName()
        {
            return "popravka_rezervnidio";
        }

        protected override PopravkaRezervniDio ParseLine(DbDataReader reader)
        {
            return new PopravkaRezervniDio(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
        }

        protected override MySqlCommand PrepareDeleteCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            string query = "DELETE FROM popravka_rezervnidio WHERE IdPopravke=@IdPopravke AND Sifra=@Sifra";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            string query = "SELECT * FROM popravka_rezervnidio WHERE IdPopravke=@IdPopravke AND Sifra=@Sifra";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            string query = @"INSERT INTO popravka_rezervnidio (IdPopravke, Sifra, Kolicina, Cijena) VALUES (@IdPopravke, @Sifra, @Kolicina, @Cijena);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            string query = @"UPDATE popravka_rezervnidio SET Cijena=@Cijena, Kolicina=@Kolicina WHERE IdPopravke=@IdPopravke AND Sifra=@Sifra";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            return command;
        }
    }
}
