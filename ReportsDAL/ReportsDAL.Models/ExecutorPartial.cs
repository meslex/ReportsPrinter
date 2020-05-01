using ReportsDAL.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
    public partial class Executor : EntityBase, IDataErrorInfo
    {
        [NotMapped]
        public string FullName { get { return $"{LastName} {FirstName} {Patronymic}"; } }

        [NotMapped]
        public string ShortName { get { return $"{LastName } {FirstName.Substring(0, 1)}. {Patronymic.Substring(0, 1)}."; } }

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
                    case nameof(Id):
                        //ClearErrors(nameof(Id));
                        AddErrors(nameof(Id), GetErrorsFromAnnotations(nameof(Id), Id));
                        break;
                    case nameof(FirstName):
                        AddErrors(nameof(FirstName), GetErrorsFromAnnotations(nameof(FirstName), FirstName));
                        break;
                    case nameof(LastName):
                        AddErrors(nameof(LastName), GetErrorsFromAnnotations(nameof(LastName), LastName));
                        break;
                    case nameof(Patronymic):
                        AddErrors(nameof(Patronymic), GetErrorsFromAnnotations(nameof(Patronymic), Patronymic));
                        break;
                    case nameof(TIN):
                        hasError = CheckTIN();
                        if (!hasError)
                        {
                            ClearErrors(nameof(TIN));
                        }
                        AddErrors(nameof(TIN), GetErrorsFromAnnotations(nameof(TIN), TIN));
                        break;
                    case nameof(PassportNumber):
                        //ClearErrors(nameof(PassportNumber));
                        AddErrors(nameof(PassportNumber), GetErrorsFromAnnotations(nameof(PassportNumber), PassportNumber));
                        break;
                    case nameof(PassportDateOfIssue):
                        AddErrors(nameof(PassportDateOfIssue), GetErrorsFromAnnotations(nameof(PassportDateOfIssue), PassportDateOfIssue));
                        break;
                    case nameof(PassportIssuedBy):
                        AddErrors(nameof(PassportIssuedBy), GetErrorsFromAnnotations(nameof(PassportIssuedBy), PassportIssuedBy));
                        break;
                }
                return string.Empty;
            }
        }

        private bool CheckTIN()
        {
            if (TIN.Length != 12)
            {
                AddError(nameof(TIN), $"TIN length should be exactly 12");
                return true;
            }

            return false;
        }
    }
}
