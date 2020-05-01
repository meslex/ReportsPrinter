using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;

namespace ReportsDAL.Repo
{
    public class ReportsRepo : BaseRepo<Report>
    {
        public override List<Report> GetAll() => Context.Reports.OrderBy(x => x.Date).ToList();
    }
}
