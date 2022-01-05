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
    class StavkePregledDAO : GenericDAO<StavkePregled>
    {
        protected override string getTableName()
        {
            return "stavke_pregled";
        }

        protected override StavkePregled ParseLine(DbDataReader reader)
        {
            return new StavkePregled(reader.GetString(0), reader.GetInt32(1), reader.GetDouble(2), reader.GetDouble(3), reader.GetInt32(4));
        }

        protected override MySqlCommand PrepareDeleteCommand(StavkePregled t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(StavkePregled t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(StavkePregled t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(StavkePregled t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StavkePregled>> GetAllForRacun(Racun r)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return null;

            List<StavkePregled> list = new List<StavkePregled>();
            try
            {
                string query = "SELECT * FROM stavke_pregled WHERE IdRacuna=@IdRacuna ";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@IdRacuna", r.IdRacuna);
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
