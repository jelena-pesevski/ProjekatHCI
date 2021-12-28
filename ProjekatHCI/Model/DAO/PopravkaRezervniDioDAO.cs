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
            throw new NotImplementedException();
        }

        protected override PopravkaRezervniDio ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PopravkaRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
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
            string query = @"UPDATE popravka_usluga SET Cijena=@Cijena, Kolicina=@Kolicina WHERE IdPopravke=@IdPopravke AND Sifra=@Sifra";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            command.Parameters.AddWithValue("@Sifra", t.Sifra);
            command.Parameters.AddWithValue("@Cijena", t.Cijena);
            command.Parameters.AddWithValue("@Kolicina", t.Kolicina);
            return command;
        }
    }
}
