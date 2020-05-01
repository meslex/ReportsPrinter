using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Repo;
using ReportsDAL.Models;


namespace ReportsTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Printing all Services");
            using (var repo = new ServiceRepo())
            {
                repo.Delete(repo.GetOne(2));

                foreach (Service c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }
            Console.ReadLine();
        }
    }
}
