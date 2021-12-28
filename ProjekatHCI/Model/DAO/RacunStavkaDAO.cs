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
    public class RacunStavkaDAO : GenericDAO<RacunStavka>
    {
        protected override string getTableName()
        {
            throw new NotImplementedException();
        }

        protected override RacunStavka ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(RacunStavka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(RacunStavka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(RacunStavka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(RacunStavka t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public async Task InsertItemsIntoBill(Racun r)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            try
            {
                MySqlCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "unesi_rezDijelove_u_racun";
                command.Parameters.AddWithValue("@pIdRacuna", r.IdRacuna);
                command.Parameters["@pIdRacuna"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters.AddWithValue("@pIdPopravke", r.IdPopravke);
                command.Parameters["@pIdPopravke"].Direction = System.Data.ParameterDirection.Input;

                command.ExecuteNonQuery();

                command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "unesi_usluge_u_racun";
                command.Parameters.AddWithValue("@pIdRacuna", r.IdRacuna);
                command.Parameters["@pIdRacuna"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters.AddWithValue("@pIdPopravke", r.IdPopravke);
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
    }
}
