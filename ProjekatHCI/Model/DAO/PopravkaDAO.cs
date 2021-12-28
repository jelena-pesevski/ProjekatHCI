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
    public class PopravkaDAO : GenericDAO<Popravka>
    {
        protected override string getTableName()
        {
            return "popravka";
        }

        protected override Popravka ParseLine(DbDataReader reader)
        {
            return new Popravka(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetBoolean(5));
        }

        protected override MySqlCommand PrepareDeleteCommand(Popravka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Popravka t, MySqlConnection conn)
        {
            string query = "SELECT * FROM popravka WHERE IdPopravke=@IdPopravke;";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPopravke", t.IdPopravke);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(Popravka t, MySqlConnection conn)
        {
            string query = @"INSERT INTO popravka (IdZaposlenog, IdPrijave) VALUES (@IdZaposlenog, @IdPrijave);";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", t.IdZaposlenog);
            command.Parameters.AddWithValue("@IdPrijave", t.IdPrijave);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(Popravka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public async Task finishRepairment(Popravka t)
        {

            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return;
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "zavrsi_popravku";
                command.Parameters.AddWithValue("@pIdPopravke", t.IdPopravke);
                command.Parameters["@pIdPopravke"].Direction = System.Data.ParameterDirection.Input;

                await command.ExecuteNonQueryAsync();
            }
          
              catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            
            }
            finally
            {
                ConnectionPool.GetInstance().CheckIn(conn);
            }
        }

        public async Task<List<Popravka>> GetAllUnfinishedForRepairman(Majstor m)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return null;

            List<Popravka> list = new List<Popravka>();
            try
            {
                string query = "SELECT * FROM popravka WHERE Zavrseno=0 and IdZaposlenog=@IdZaposlenog;";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@IdZaposlenog", m.IdZaposlenog);
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
