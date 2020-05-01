using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;
using ReportsDAL.Models.Base;

namespace ReportsDAL.Models
{
    //таблица количества отчетов по грантам напечатаных в рамках договора
    public partial class AmountOfCreatedReports : EntityBase
    {
        public int ContractId { get; set; }

        public int GrantAgreementId { get; set; }

        public int Amount { get; set; }

        [ForeignKey(nameof(ContractId))]

        public Contract Contract { get; set; }

        [ForeignKey(nameof(GrantAgreementId))]
        public GrantAgreement GrantAgreement { get; set; }
    }
}
