using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;

namespace ReportsDAL.Repo
{
    public class ServiceRepo : BaseRepo<Service>
    {
        public override List<Service> GetAll() => Context.Services.OrderBy(x => x.Price).ToList();
    }
}
