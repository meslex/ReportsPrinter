using ReportsDAL.Models;
using ReportsDAL.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Reports.Windows
{
    /// <summary>
    /// Interaction logic for ExecutorsList.xaml
    /// </summary>
    public partial class ExecutorsList : Window
    {

        private IList<Executor> _executors;
        private ExecutorRepo executorRepo = MainWindow.executorRepo;


        public ExecutorsList()
        {
            InitializeComponent();

            _executors = new ObservableCollection<Executor>(executorRepo.GetAll());
            executorsInventory.ItemsSource = _executors;
        }

        

        private void CreateReport_OnClick(object sender, RoutedEventArgs e)
        {
            if(executorsInventory.SelectedIndex >= 0)
            {
                Executor ex = (Executor)executorsInventory.SelectedItem;
                if(ex.Contracts.Count > 0)
                {
                    CreateReport createReport = new CreateReport(ex);
                }
                else
                {
                    MessageBox.Show("Вибраний виконавець не має жодного дійсного договору", "Помилка",
    MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //this.Hide();
            }
        }
    }
}
