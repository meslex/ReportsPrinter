using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Office.Tools.Excel;
using Reports.Windows.Dialogs;
using ReportsDAL.Models;
using ReportsDAL.Repo;
using Excel = Microsoft.Office.Interop.Excel;

namespace Reports
{
    /// <summary>
    /// Interaction logic for CreateReport.xaml
    /// </summary>
    public partial class CreateReport : Window
    {
        private ReportData reportData = new ReportData();
        private Executor Executor { get; set; }

        private ObservableCollection<Direction> _directions;
        private int id = -1;
        private bool reportWasPrinted;
        private bool historyMode;


        public CreateReport(Executor ex = null)
        {
            InitializeComponent();
            this.Show();
            historyMode = false;
            this.Executor = ex;
            reportData.Executor = this.Executor;
            if (Executor == null)
            {
                MessageBox.Show("Щось пішло не так, спробуйте знову", "Помилка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }

            SetSources();

            SetBinding();


        }

        public CreateReport(ReportData reportData, int id)
        {
            InitializeComponent();
            this.Show();
            historyMode = true;
            this.reportData = reportData;
            this.Executor = reportData.Executor;
            this.id = id;

            InitialiseFromHistory();

            SetBinding();


        }

        private void InitialiseFromHistory()
        {
            this.Title = "Акт завантаженно з історії";

            ContractCombobox.ToolTip = "Цей акт завантажено з історії ви не может змінити це поле.";
            GrantAgreementCombobox.ToolTip = "Цей акт завантажено з історії ви не может змінити це поле.";

            ExecutorNameTextBox.Text = Executor.FullName;
            ContractCombobox.ItemsSource = new ObservableCollection<Contract>() { reportData.Contract };
            DirectionsList.ItemsSource = _directions;//reportData.Directions;

            ReportDatePicker.Focusable = false;
            ReportDatePicker.IsHitTestVisible = false;
            ReportDatePicker.ToolTip = "Цей акт завантажено з історії ви не может змінити це поле.";
            //ContractCombobox.IsEnabled = false;
            //GrantAgreementCombobox.IsEnabled = false;
        }


        private void SetSources()
        {
            ExecutorNameTextBox.Text = Executor.FullName;
            if(Executor.Contracts.Count == 0)
            {
                MessageBox.Show("Вибраний виконавець не має жодного дійсного договору", "Помилка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            ContractCombobox.ItemsSource = Executor.Contracts;
            DirectionsList.ItemsSource =_directions;
        }


        private void SetBinding()
        {
            Binding myBinding = new Binding();
            myBinding.Source = reportData;
            myBinding.Path = new PropertyPath("Number");
            myBinding.Mode = BindingMode.TwoWay;
            myBinding.ValidatesOnDataErrors = true;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(NumberTextBox, TextBox.TextProperty, myBinding);

            myBinding = new Binding();
            myBinding.Source = reportData;
            myBinding.Path = new PropertyPath("Date");
            myBinding.Mode = BindingMode.TwoWay;
            myBinding.ValidatesOnDataErrors = true;
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(ReportDatePicker, DatePicker.SelectedDateProperty, myBinding);
            //ReportDatePicker.SelectedDate = reportData.Date;
        }

        #region AddDelete

        private void ServicesContextMenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0 && Services.SelectedIndex >= 0)
            {
                _directions[DirectionsList.SelectedIndex].Services.RemoveAt(Services.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть послугу яку ви хочете видалити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DirectionsContextMenuDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0)
            {
                _directions.RemoveAt(DirectionsList.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть напрямок який ви хочете видалити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void ServicesContextMenuAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0)
            {
                AddServiceToCreateReportWindow addService = new AddServiceToCreateReportWindow(((Direction)DirectionsList.SelectedItem).Id);

                if ((bool)addService.ShowDialog())
                {
                    ((Direction)DirectionsList.SelectedItem).Services.Add(addService.SelectedService);
                }
            }
            else
            {
                MessageBox.Show("Будь ласка спочатку виберіть напрямок в до якого ви хочете додати послугу", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DirectionsContextMenuAdd_OnClick(object sender, RoutedEventArgs e)
        {
            AddDirectionsToCreateReportWindow addDirections = 
                new AddDirectionsToCreateReportWindow(((GrantAgreement)GrantAgreementCombobox.SelectedItem).Id);

            if ((bool)addDirections.ShowDialog())
            {
                _directions.Add(addDirections.SelectedDirection);
                //Console.WriteLine($"OK Number: {addDirections.SelectedDirection.Number} Name: {addDirections.SelectedDirection.Name}");
            }
        }

        #endregion

        #region UpDown
        private void ServicesContextMenuUp_OnClick(object sender, RoutedEventArgs e)
        {
            if (DirectionsList.SelectedIndex >= 0 && Services.SelectedIndex >= 0)
            {
                if(Services.SelectedIndex > 0)
                {
                    int i = Services.SelectedIndex;
                    //Services.SelectedIndex = Services.SelectedIndex - 1;
                    Service temp = _directions[DirectionsList.SelectedIndex].Services[i - 1];
                    _directions[DirectionsList.SelectedIndex].Services[i - 1] =  _directions[DirectionsList.SelectedIndex].Services[i];
                    _directions[DirectionsList.SelectedIndex].Services[i] = temp;
                    Services.SelectedIndex = i - 1;
                }
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть послугу положення якої ви хочете змінити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ServicesContextMenuDown_OnClick(object sender, RoutedEventArgs e)
        {
            if (DirectionsList.SelectedIndex >= 0 && Services.SelectedIndex >= 0)
            {
                if (Services.SelectedIndex < _directions[DirectionsList.SelectedIndex].Services.Count-1)
                {
                    int i = Services.SelectedIndex;
                    //Services.SelectedIndex = Services.SelectedIndex - 1;
                    Service temp = _directions[DirectionsList.SelectedIndex].Services[i + 1];
                    _directions[DirectionsList.SelectedIndex].Services[i + 1] = _directions[DirectionsList.SelectedIndex].Services[i];
                    _directions[DirectionsList.SelectedIndex].Services[i] = temp;
                    Services.SelectedIndex = i + 1;
                }
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть послугу положення якої ви хочете змінити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DirectionsContextMenuUp_OnClick(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0)
            {
                if (DirectionsList.SelectedIndex > 0)
                {
                    int i = DirectionsList.SelectedIndex;
                    //Services.SelectedIndex = Services.SelectedIndex - 1;
                    Direction temp = _directions[i - 1];
                    _directions[i - 1] = _directions[i];
                    _directions[i] = temp;
                    DirectionsList.SelectedIndex = i - 1;
                }
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть напрямок положення якого ви хочете змінити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DirectionsContextMenuDown_OnClick(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0)
            {
                if (DirectionsList.SelectedIndex < _directions.Count - 1)
                {
                    int i = DirectionsList.SelectedIndex;
                    //Services.SelectedIndex = Services.SelectedIndex - 1;
                    Direction temp = _directions[i + 1];
                    _directions[i + 1] = _directions[i];
                    _directions[i] = temp;
                    DirectionsList.SelectedIndex = i + 1;
                }
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть напрямок положення якого ви хочете змінити", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #endregion

        #region SelectionChanged

        private void ContractCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!historyMode)
            {
                GrantAgreementCombobox.ItemsSource = ((Contract)ContractCombobox.SelectedItem).GrantAgreements;
            }
            else
            {
                GrantAgreementCombobox.ItemsSource = new ObservableCollection<GrantAgreement>() { reportData.GrantAgreement};
            }

        }

        private void GrantAgreementCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!historyMode)
            {
                _directions = ((GrantAgreement)GrantAgreementCombobox.SelectedItem).Directions;
            }
            else
            {
                _directions = reportData.Directions;
            }

        }

        private void DirectionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DirectionsList.Items.Count == 0)
            {
                Services.ItemsSource = null;
            }
            else if (DirectionsList.SelectedIndex >= 0)
            {
                Services.ItemsSource = _directions[DirectionsList.SelectedIndex].Services;

                /*if (!historyMode)
                {
                    Services.ItemsSource = _directions[DirectionsList.SelectedIndex].Services;
                }
                else
                {
                    Services.ItemsSource = reportData.Directions[DirectionsList.SelectedIndex].Services;
                }*/

            }

        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormReport.Example();
        }

        private bool CheckForErrors()
        {

            return false;
        }

        private void PopulateDataToReportData()
        {
            reportData.Contract = (Contract)ContractCombobox.SelectedItem;
            reportData.GrantAgreement = (GrantAgreement)GrantAgreementCombobox.SelectedItem;
            reportData.Directions = _directions;
        }

        private void ButtonCheckInputClick(object sender, RoutedEventArgs e)
        {
            PopulateDataToReportData();

            //bool temp = reportData.CheckForErrorsInServices();

            if (reportData.HasErrors)
            {
                MessageBox.Show("Будь ласка перевірте введені данні");
            }
            else if (reportData.CheckForErrorsInServices())
            {
                MessageBox.Show("Знайденна помилка у списку послуг," +
                    " Будь ласка виправте її", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                 FormReport.Form(reportData);
                 reportWasPrinted = true;
            }
        }

        private void ButtonSaveClick(object sender, RoutedEventArgs e)
        {
            SaveToHistory();
        }
        private void ButtonDebugClick(object sender, RoutedEventArgs e)
        {
            reportData.ToString();
        }

        

        private void SaveToHistory()
        {
            PopulateDataToReportData();
            if (reportData.HasErrors)
            {
                MessageBox.Show("Будь ласка перевірте введені данні");
            }
            else if (reportData.CheckForErrorsInServices())
            {
                MessageBox.Show("Знайденна помилка у списку послуг," +
                    " Будь ласка виправте її", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (historyMode)
                {
                    MainWindow.historyRepo.UpdateHistory(reportData, id);
                }
                else
                {
                    MainWindow.historyRepo.AddIntoHistory(reportData);
                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (reportWasPrinted)
            {
                MainWindow.historyRepo.AddIntoHistory(reportData);
            }
        }
    }
}
