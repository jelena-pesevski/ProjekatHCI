using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProjekatHCI.Model.DTO;
using ProjekatHCI.Util;

namespace ProjekatHCI.Model.DAO
{
    class PregledUslugaDAO : GenericDAO<PregledUsluga>
    {
        protected override string getTableName()
        {
            return "pregled_usluge";
        }

        protected override PregledUsluga ParseLine(DbDataReader reader)
        {
            return new PregledUsluga(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetInt32(4));
        }

        protected override MySqlCommand PrepareDeleteCommand(PregledUsluga t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PregledUsluga t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(PregledUsluga t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(PregledUsluga t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PregledUsluga>> GetAllForPopravka(Popravka p)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return null;

            List<PregledUsluga> list = new List<PregledUsluga>();
            try
            {
                string query = "SELECT * FROM pregled_usluge WHERE IdPopravke=@IdPopravke ";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@IdPopravke", p.IdPopravke);
                DbDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    list.Add(ParseLine(reader));
                }

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            finally
            {
                ConnectionPool.GetInstance().CheckIn(conn);
            }
        }

    }
}
