namespace ReportsDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using ReportsDAL.Models.Base;

    public partial class Client : EntityBase
    {
        [StringLength(50)]
        public string Code{ get; set; }

        //[ForeignKey)]
        public int ExecutorId { get; set; }

        [ForeignKey(nameof(ExecutorId))]
        [JsonIgnoreAttribute]
        public virtual Executor Executor { get; set; }
    }
}
