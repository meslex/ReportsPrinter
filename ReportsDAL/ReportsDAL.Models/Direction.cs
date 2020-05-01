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
    public partial class Direction : EntityBase
    {

        [StringLength(12)]
        public string Number { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int GrantAgreementId { get; set; }

        [ForeignKey(nameof(GrantAgreementId))]
        [JsonIgnoreAttribute]
        public virtual GrantAgreement GrantAgreement { get; set; }

        public virtual ObservableCollection<Service> Services { get; set; } = new ObservableCollection<Service>();
    }
}
