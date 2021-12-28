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
    public class PrijavaKvaraDAO : GenericDAO<PrijavaKvara>
    {
        protected override string getTableName()
        {
            return "prijavakvara";
        }

        protected override PrijavaKvara ParseLine(DbDataReader reader)
        {
            return new PrijavaKvara(reader.GetInt32(0), reader.GetDateTime(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6));
        }

        protected override MySqlCommand PrepareDeleteCommand(PrijavaKvara t, MySqlConnection conn)
        {
            string query = "DELETE FROM prijavakvara WHERE IdPrijave=@IdPrijave";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPrijave", t.IdPrijave);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(PrijavaKvara t, MySqlConnection conn)
        {
            string query = "SELECT * FROM prijavakvara WHERE IdPrijave=@IdPrijave";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdPrijave", t.IdPrijave);
            return command;
        }

        protected override MySqlCommand PrepareInsertCommand(PrijavaKvara t, MySqlConnection conn)
        {
            string query = @"INSERT INTO prijavakvara (Opis, Operater_IdZaposlenog, IdKlijenta) VALUES (@Opis, @Operater_IdZaposlenog, @IdKlijenta)";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Opis", t.Opis);
            command.Parameters.AddWithValue("@Operater_IdZaposlenog", t.Operater_IdZaposlenog);
            command.Parameters.AddWithValue("@IdKlijenta", t.IdKlijenta);
            return command;
        }

        protected override MySqlCommand PrepareUpdateCommand(PrijavaKvara t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected async override Task PostInsertQuery(PrijavaKvara t, long lastInsertedId, MySqlConnection conn)
        {
            try
            {
                t.IdPrijave = (int)lastInsertedId;
                MySqlCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "dodijeli_zaduzenje_majstoru";
                command.Parameters.AddWithValue("@pIdPrijave", t.IdPrijave);
                command.Parameters["@pIdPrijave"].Direction = System.Data.ParameterDirection.Input;

                await command.ExecuteNonQueryAsync();

            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public async Task<List<PrijavaKvara>> GetPrijaveKvaraForMajstor(Majstor m)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn == null) return null;
            string query = "SELECT * FROM prijavakvara WHERE Status!='zavrseno' and Majstor_IdZaposlenog=@MajstorId;";
            List<PrijavaKvara> list = new List<PrijavaKvara>();
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@MajstorId", m.IdZaposlenog);
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
