using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
    public partial class AmountOfCreatedReports : IDataErrorInfo
    {

        private string _error;

        public string Error => _error;

        public string this[string columnName]
        {
            get
            {
                ClearErrors();
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(Amount):
                        AddErrors(nameof(Amount), GetErrorsFromAnnotations(nameof(Amount), Amount));
                        break;
                }
                return string.Empty;
            }
        }

    }
}
