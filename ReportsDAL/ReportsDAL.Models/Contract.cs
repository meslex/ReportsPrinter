using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using ReportsDAL.Models.Base;

namespace ReportsDAL.Models
{
    public partial class Contract : EntityBase
    {
        public int Number { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int ExecutorId { get; set; }
        [ForeignKey(nameof(ExecutorId))]
        [JsonIgnoreAttribute]
        public Executor Executor { get; set; }
        [JsonIgnoreAttribute]
        public virtual ObservableCollection<GrantAgreement> GrantAgreements { get; set; } = new ObservableCollection<GrantAgreement>();
        [JsonIgnoreAttribute]
        public virtual ObservableCollection<AmountOfCreatedReports> AmountOfContracts { get; set; } = new ObservableCollection<AmountOfCreatedReports>();

    }
}
