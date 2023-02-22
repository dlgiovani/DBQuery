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

            DBQuery.server          = "localhost";
            DBQuery.user            = "root";
            DBQuery.password        = "makina";
            DBQuery.port            = 3306;
            DBQuery.connectionType  = "mysql";
            DBQuery.database        = "vinicola_exemplo";

            DBQuery.tablesToIndex = new string[] {  "vinho" };

            string testeHihi = DBQuery.Teste();

            Console.WriteLine(testeHihi);
            Console.ReadLine();
        }
    }
}
