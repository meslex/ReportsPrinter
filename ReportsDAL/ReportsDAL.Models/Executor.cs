namespace ReportsDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using ReportsDAL.Models.Base;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;


    public partial class Executor : EntityBase
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(12)]
        public string TIN { get; set; }
        [StringLength(8)]
        public string PassportNumber { get; set; }
        public DateTime PassportDateOfIssue { get; set; }
        public bool Entrepreneur { get; set; }
        [StringLength(100)]
        public string PassportIssuedBy { get; set; }

        [JsonIgnoreAttribute]
        public virtual ObservableCollection<Contract> Contracts { get; set; } = new ObservableCollection<Contract>();
       
        public virtual ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();



    }
}
