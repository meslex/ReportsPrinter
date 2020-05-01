using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReportsDAL.Models.Base;
using System.Data.Entity.Spatial;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ReportsDAL.Models
{
    public partial class GrantAgreement : EntityBase
    {

        [StringLength(12)]
        public string Number { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [JsonIgnoreAttribute]
        public virtual ObservableCollection<Direction> Directions { get; set; } = new ObservableCollection<Direction>();
        [JsonIgnoreAttribute]
        public virtual ObservableCollection<Contract> Contracts { get; set; } = new ObservableCollection<Contract>();

    }
}
