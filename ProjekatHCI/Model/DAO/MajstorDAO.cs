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
    public class MajstorDAO : GenericDAO<Majstor>
    {
        protected override string getTableName()
        {
            throw new NotImplementedException();
        }

        protected override Majstor ParseLine(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareDeleteCommand(Majstor t, MySqlConnection conn)
        {
            string query = "DELETE FROM majstor WHERE IdZaposlenog=@IdZaposlenog";
            MySqlCommand command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@IdZaposlenog", t.IdZaposlenog);
            return command;
        }

        protected override MySqlCommand PrepareGetOneByIdCommand(Majstor t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareInsertCommand(Majstor t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }

        protected override MySqlCommand PrepareUpdateCommand(Majstor t, MySqlConnection conn)
        {
            throw new NotImplementedException();
        }
    }
}
