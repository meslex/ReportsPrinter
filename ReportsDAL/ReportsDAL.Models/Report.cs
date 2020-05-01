using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models.Base;

namespace ReportsDAL.Models
{
    public partial class Report : EntityBase
    {
        public DateTime Date { get; set; }
        public int ContractNumber { get; set; }
        public string GrantAgreementNumber { get; set; }
        public string ExecutorsName { get; set; }
        public string JsonObj { get; set; }
    }
}
