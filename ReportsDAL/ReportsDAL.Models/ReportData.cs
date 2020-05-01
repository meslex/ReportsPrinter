using ReportsDAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReportsDAL.Models
{
    public class ReportData : INotifyDataErrorInfo, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public Executor Executor { get; set; }

        public Contract Contract { get; set; }

        public GrantAgreement GrantAgreement { get; set; }

        public ObservableCollection<Direction> Directions { get; set; }
        

        public ReportData()
        {
            Date = DateTime.Today;
        }

        public bool CheckForErrorsInServices()
        {
            bool hasError = false;

            for (int i = 0; i < Directions.Count; ++i)
            {
                for (int c = 0; c < Directions[i].Services.Count; ++c)
                {
                    if (Directions[i].Services[c].HasErrors)
                    {
                        hasError = true;

                    }
                }
            }

            return hasError;
        }

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
                    case nameof(Number):

                        if (Number < 0)
                        {
                            AddError(nameof(Number), "Номер акту не може бути від'ємним числом.");
                            hasError = true;
                        }

                        if (!hasError)
                        {
                            //ClearErrors(nameof(Number));
                        }

                        AddErrors(nameof(Number), GetErrorsFromAnnotations(nameof(Number), Number));
                        break;
                    case nameof(Date):
                        AddErrors(nameof(Number), GetErrorsFromAnnotations(nameof(Number), Number));
                        break;
                    case nameof(Executor):
                        AddErrors(nameof(Executor), GetErrorsFromAnnotations(nameof(Executor), Executor));
                        break;

                }
                return string.Empty;
            }
        }

        #region someMethods
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Count != 0;

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.Values;
            }
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        protected void AddError(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        protected void AddErrors(string propertyName, IList<string> errors)
        {
            if (errors == null || errors.Count == 0)
            {
                return;
            }

            var changed = false;
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
                changed = true;
            }
            foreach (var err in errors)
            {
                if (_errors[propertyName].Contains(err)) continue;
                _errors[propertyName].Add(err);
                changed = true;
            }
            if (changed)
            {
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName = "")
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                _errors.Clear();
            }
            else
            {
                _errors.Remove(propertyName);
            }
            OnErrorsChanged(propertyName);
        }

        protected string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            var results = new List<ValidationResult>();
            var vc = new ValidationContext(this, null, null) { MemberName = propertyName };
            var isValid = Validator.TryValidateProperty(value, vc, results);
            return (isValid) ? null : Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
