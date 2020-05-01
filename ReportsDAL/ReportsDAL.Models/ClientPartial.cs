using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
    public partial class Client : IDataErrorInfo
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
                    case nameof(Code):
                        AddErrors(nameof(ExecutorId), GetErrorsFromAnnotations(nameof(ExecutorId), ExecutorId));
                        break;
                }
                return string.Empty;
            }
        }
    }
}
