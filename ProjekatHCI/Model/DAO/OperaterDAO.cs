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
    public class OperaterDAO : GenericDAO<Operater>
    {
        protected override string getTableName()
        {
            throw new NotImplementedException();
        }

        protected override Operater ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(Operater t, MySqlConnection conn)
        {
            string query = "DELETE FROM operater WHERE IdZaposlenog=@IdZaposlenog";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", t.IdZaposlenog);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Operater t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(Operater t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(Operater t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
