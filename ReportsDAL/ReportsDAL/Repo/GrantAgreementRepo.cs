using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReportsDAL.Repo
{
    public class GrantAgreementRepo : BaseRepo<GrantAgreement>
    {
        public override List<GrantAgreement> GetAll() => Context.GrantAgreements.OrderBy(x => x.Number).ToList();
    }
}
