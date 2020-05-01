using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReportsDAL.Repo
{
    public class DirectionsRepo : BaseRepo<Direction>
    {
        public override List<Direction> GetAll() => Context.Directions.OrderBy(x => x.Number).ToList();
    }
}
