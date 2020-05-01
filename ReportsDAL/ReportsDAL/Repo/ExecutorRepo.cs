using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;


namespace ReportsDAL.Repo
{
    public class ExecutorRepo : BaseRepo<Executor>
    {
        /*public override List<Executor> GetAll()
        {
            //return Context.Executors.Include("Contracts.GrantAgreement.Directions.Services").ToList();
        }*/

        public override List<Executor> GetAll() => Context.Executors.OrderBy(x => x.LastName).ToList();
    }
}
