using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ProjekatHCI.Util;
using System.Data.Common;

namespace ProjekatHCI.Model.DAO
{
    public abstract class GenericDAO<T>
    {
        private static readonly string SELECT = "SELECT *  FROM ";

        public async Task<List<T>> GetAll()
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn != null)
            {
                List<T> list = new List<T>();

                try
                {
                    string query = SELECT + getTableName() + ";";
                    MySqlCommand command = new MySqlCommand(query, conn);
                    DbDataReader reader =  await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        list.Add(ParseLine(reader));
                    }

                    return list;
                } catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return null;
                }
                finally
                {
                    ConnectionPool.GetInstance().CheckIn(conn);
                }

            }
            return null;
        }

        public async Task<T> GetById(T t)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            if (conn != null)
            {
                try
                {
                    MySqlCommand command = PrepareGetOneByIdCommand(t, conn);
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        return ParseLine(reader);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return default(T);
                }
                finally
                {
                    ConnectionPool.GetInstance().CheckIn(conn);
                }

            }
            return default(T);
        }

        public async Task<int> Insert(T t)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            int result =0;
            if (conn != null)
            {
                try
                {
                    MySqlCommand command = PrepareInsertCommand(t, conn);
                    result = await command.ExecuteNonQueryAsync();

                    if(result>0) PostInsertQuery(t, command.LastInsertedId, conn);
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    result = -1;
                }
                finally
                {
                    ConnectionPool.GetInstance().CheckIn(conn);
                }
            }
            return result;

        }

        protected async virtual Task PostInsertQuery(T t, long lastInsertedId, MySqlConnection conn)
        {
           
        }

        public async Task<int> Update(T t)
        {
            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            int result = 0;
            if (conn != null)
            {
                try
                {
                    MySqlCommand command = PrepareUpdateCommand(t, conn);
                    result = await command.ExecuteNonQueryAsync(); 
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    result = -1;
                }
                finally
                {
                    ConnectionPool.GetInstance().CheckIn(conn);
                }
            }
            return result;
        }

        protected virtual Task<int> PreQuery(T t)
        {
           return Task.FromResult(1); ;
        }

        public async Task<int> Delete(T t)
        {
            int preQueryResult = await PreQuery(t);
            if(preQueryResult!=1) 
                return 0;

            MySqlConnection conn = ConnectionPool.GetInstance().CheckOut();
            int result = 0;
            if (conn != null)
            {
                try
                {
                    MySqlCommand command = PrepareDeleteCommand(t, conn);
                    result = await command.ExecuteNonQueryAsync();
                }catch(Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    result = -1;
                }
                finally
                {
                    ConnectionPool.GetInstance().CheckIn(conn);
                }
            }
            return result;
        }




        protected abstract string getTableName();

        protected abstract T ParseLine(DbDataReader reader);

        protected abstract MySqlCommand PrepareGetOneByIdCommand(T t, MySqlConnection conn);

        protected abstract MySqlCommand PrepareInsertCommand(T t, MySqlConnection conn);

        protected abstract MySqlCommand PrepareUpdateCommand(T t, MySqlConnection conn);

        protected abstract MySqlCommand PrepareDeleteCommand(T t, MySqlConnection conn);
    }
}
