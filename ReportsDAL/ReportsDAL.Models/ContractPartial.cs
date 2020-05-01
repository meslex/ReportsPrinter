using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
    public partial class Contract : IDataErrorInfo
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
                    case nameof(Number):
                        AddErrors(nameof(Number), GetErrorsFromAnnotations(nameof(Number), Number));
                        break;
                    case nameof(From):
                        AddErrors(nameof(From), GetErrorsFromAnnotations(nameof(From), From));
                        break;
                    case nameof(To):
                        AddErrors(nameof(To), GetErrorsFromAnnotations(nameof(To), To));
                        break;
                }
                return string.Empty;
            }
        }
    }
}
