using ReportsDAL.Models;
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
using System.Windows.Shapes;

namespace Reports.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for AddServiceToCreateReportWindow.xaml
    /// </summary>
    public partial class AddServiceToCreateReportWindow : Window
    {
        public Service SelectedService;

        private IList<Service> services;

        public AddServiceToCreateReportWindow(int directionId)
        {
            InitializeComponent();

            services = MainWindow.directionsRepo.GetOne(directionId).Services;
            Services.ItemsSource = services;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (Services.SelectedIndex >= 0)
            {
                SelectedService = services[Services.SelectedIndex];
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть послугу.", "Не вибрано послугу.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

    }
}
