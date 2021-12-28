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
    public class AdministratorDAO : GenericDAO<Administrator>
    {
        protected override string getTableName()
        {
            throw new NotImplementedException();
        }

        protected override Administrator ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(Administrator t, MySqlConnection conn)
        {
            string query = "DELETE FROM administrator WHERE IdZaposlenog=@IdZaposlenog";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", t.IdZaposlenog);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Administrator t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(Administrator t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(Administrator t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
