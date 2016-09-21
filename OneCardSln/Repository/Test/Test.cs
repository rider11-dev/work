using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

using OneCardSln.Model;
using DapperExtensions;
using DapperExtensions.Sql;

namespace OneCardSln.Repository.Test
{
    public class Test
    {
        static void Main(string[] args)
        {
            test1();


            Console.ReadKey();
        }

        static void test1()
        {
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { typeof(Test).Assembly });
            DapperExtensions.DapperExtensions.SqlDialect = DbUtils.GetSqlDialect();

            var conn = DbUtils.CreateDbConnection();
            try
            {
                conn.Open();
                Console.WriteLine("connection state:" + conn.State.ToString());

                //var where = Predicates.Field<User>(u => u.Name, Operator.Eq, "admin");
                //var obj = conn.GetList<User>(where).FirstOrDefault();

                var where = Predicates.Field<CardInfo>(u => u.idcard, Operator.Eq, "372924198708265138");
                var obj = conn.GetList<CardInfo>(where).FirstOrDefault();


                if (obj != null)
                {
                    Console.WriteLine(obj.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                Console.WriteLine("connection state:" + conn.State.ToString());
                conn.Dispose();
            }
        }

    }
}
