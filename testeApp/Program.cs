using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("I'm alive");

            DBQuery.server          = "10.1.11.15";
            DBQuery.user            = "developer";
            DBQuery.password        = "developer@2020";
            //DBQuery.port            = 3306;
            DBQuery.connectionType  = "sqlserver";
            DBQuery.database        = "Discovery";

            DBQuery.tablesToIndex = new string[] { "tCli" };

            //string testeHihi = DBQuery.Teste();

            //Console.WriteLine(testeHihi);
            Console.ReadLine();
        }
    }
}
