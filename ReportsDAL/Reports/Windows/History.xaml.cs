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
using ReportsDAL.Models;
using ReportsDAL.Repo;

namespace Reports.Windows
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        private IList<Report> reports;
        private HistoryRepo historyRepo = MainWindow.historyRepo;

        public History()
        {
            InitializeComponent();

            reports = new ObservableCollection<Report>(historyRepo.GetAll());
            HistoryGrid.DataContext = reports;
        }

        private void HistoryGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(HistoryGrid.SelectedIndex >= 0)
            {
                ReportData reportData = historyRepo.ReadFromHistory(((Report)HistoryGrid.SelectedItem).Id);
                CreateReport createReport = new CreateReport(reportData, 1);
            }
        }

        private void DeleteReport_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryGrid.SelectedIndex >= 0)
            {
                historyRepo.Delete((Report)HistoryGrid.SelectedItem);
                reports.RemoveAt(HistoryGrid.SelectedIndex);
            }
        }


        private void ChangeReport_Click(object sender, RoutedEventArgs e)
        {
            HistoryGrid_MouseDoubleClick(sender, null);
        }
    }
}
