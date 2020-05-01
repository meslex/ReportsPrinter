using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReportsDAL.Repo;
using ReportsDAL.Models;
using System.Collections.ObjectModel;
using ReportsDAL.Models;

namespace Reports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Executor> _executors;
        private ExecutorRepo executorRepo = new ExecutorRepo();
        private List<string> errors = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            //ConfigureGrid();

            _executors = new ObservableCollection<Executor>(executorRepo.GetAll());
            executorsInventory.DataContext = _executors;
        }

        private void ConfigureGrid()
        {
            using (var repo = new ExecutorRepo())
            {
                // Build a LINQ query that gets back some data from the Inventory table.
                executorsInventory.ItemsSource =
                repo.GetAll().Select(x => new { x.Id, x.LastName, x.FirstName, x.Patronymic, x.TIN,
                x.PassportNumber, x.PassportIssuedBy, x});
            }
        }

        private void ButtonChangeName_Click(object sender, RoutedEventArgs e)
        {
            _executors.First(x => x.Id == ((Executor)executorsInventory.SelectedItem).Id).FirstName = "Джозеф";
        }

        
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            _executors = new ObservableCollection<Executor>(executorRepo.GetAll());
            executorsInventory.DataContext = _executors;
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<Executor> itemsToSave = new List<Executor>();

            //bool errors = false;
            foreach(Executor ex in _executors)
            {
                if (ex._IsChanged && !ex.HasErrors)
                {
                    executorRepo.Save(ex);
                    ex._IsChanged = false;
                }

            }
        }

        
    }
}
