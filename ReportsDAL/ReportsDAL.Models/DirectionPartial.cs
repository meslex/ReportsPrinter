using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReportsDAL.Models
{
    public partial class Direction : IDataErrorInfo
    {
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
                    case nameof(Number):
                        AddErrors(nameof(Number), GetErrorsFromAnnotations(nameof(Number), Number));
                        break;
                }
                return string.Empty;
            }
        }
    }
}
