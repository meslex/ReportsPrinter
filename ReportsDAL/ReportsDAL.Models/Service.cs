namespace ReportsDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;
    using ReportsDAL.Models.Base;

    public partial class Service : EntityBase
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public float Price { get; set; }

        public int DirectionId { get; set; }

        [ForeignKey(nameof(DirectionId))]
        [JsonIgnoreAttribute]
        public Direction Direction { get; set; }
    }
}
