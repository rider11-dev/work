using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using OneCardSln.Model;
using DapperExtensions.Sql;

namespace OneCardSln.Repository.Test
{
    public class Test
    {
        static void Main(string[] args)
        {
            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[] { typeof(Test).Assembly });
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
            //test1();
            //test2();
            //test3();
            test4();



            Console.ReadKey();
        }

        static void test1()
        {
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

        static IBaseRepository<CardInfo> rep = new CardInfoRepository(new DbSession());
        static void test2()
        {
            var where = Predicates.Field<CardInfo>(u => u.idcard, Operator.Eq, "372924198708265138");
            long total = 0;
            IList<ISort> sort = new List<ISort> { new Sort { PropertyName = "username", Ascending = true } };

            var rst = rep.GetPageList(1, 2, out total, sort);
            Console.WriteLine("data count:" + rst.Count());
        }

        static void test3()
        {
            var obj = new CardInfo { id = "0", number = "a", username = "b", idcard = "c", govmoney = 0, mymoney = 0, phone = "d", state = "正常" };
            var rst = rep.Insert(obj);
        }

        static void test4()
        {
            var entity = new CardInfo { id = "0274C52CD28F979B2308D185F0EA4470", number = "aaa" };
            var entities = new List<CardInfo> { entity, new CardInfo { id = "32d4861d64f5ab0417a82fe44bde5454", govmoney = 100 } };


            var rst = rep.UpdateBatch(entities);
        }

        static void test5()
        {

        }
    }
}
