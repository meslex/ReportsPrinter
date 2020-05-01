using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
   public partial class Service : IDataErrorInfo
    {
        [NotMapped]
        public int Amount { get; set; }

        [NotMapped]
        public float Total { get { return Price * Amount; } }

        private string _error;

        public string Error => _error;

        public string this[string columnName]
        {
            get
            {
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(Id):
                        AddErrors(nameof(Id), GetErrorsFromAnnotations(nameof(Id), Id));
                        break;
                    case nameof(Name):
                        AddErrors(nameof(Name), GetErrorsFromAnnotations(nameof(Name), Name));
                        break;
                    case nameof(Type):
                        AddErrors(nameof(Type), GetErrorsFromAnnotations(nameof(Type), Type));
                        break;
                    case nameof(Price):
                        AddErrors(nameof(Price), GetErrorsFromAnnotations(nameof(Price), Price));
                        break;
                    case nameof(Amount):
                        if(Amount <= 0)
                        {
                            AddError(nameof(Amount), "Кількість виконаних послуг не може дорівнювати нулю");
                            hasError = true;
                        }
                        if (!hasError)
                        {
                            ClearErrors(nameof(Amount));
                        }

                        AddErrors(nameof(Amount), GetErrorsFromAnnotations(nameof(Amount), Amount));
                        break;
                }
                return string.Empty;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(propertyName == "Amount")
            {
                Console.WriteLine("Amount was changed");
                //Total = Amount * Price;
            }

            base.OnPropertyChanged(propertyName);
        }
    }
}
