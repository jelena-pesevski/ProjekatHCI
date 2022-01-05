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
    class PregledRezervniDioDAO : GenericDAO<PregledRezervniDio>
    {
        protected override string getTableName()
        {
            return "pregled_rezervnidio";
        }

        protected override PregledRezervniDio ParseLine(DbDataReader reader)
        {
            return new PregledRezervniDio(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetInt32(4));
        }

        protected override MySqlCommand PrepareDeleteCommand(PregledRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PregledRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(PregledRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(PregledRezervniDio t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PregledRezervniDio>> GetAllForPopravka(Popravka p)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return null;

            List<PregledRezervniDio> list = new List<PregledRezervniDio>();
            try
            {
                string query = "SELECT * FROM pregled_rezervnidio WHERE IdPopravke=@IdPopravke ";
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
