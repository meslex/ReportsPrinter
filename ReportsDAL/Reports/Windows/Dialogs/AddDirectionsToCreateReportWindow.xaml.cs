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

namespace Reports.Windows.Dialogs
{
    public partial class AddDirectionsToCreateReportWindow : Window
    {
        public Direction SelectedDirection { get { return direction; } }

        private IList<Direction> _directions;
        private Direction direction;

        public AddDirectionsToCreateReportWindow(int grantAgreementId)
        {
            InitializeComponent();

            _directions = MainWindow.grantAgreementRepo.GetOne(grantAgreementId).Directions;
            DirectionsList.ItemsSource = _directions;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(DirectionsList.SelectedIndex >= 0)
            {
                direction = _directions[DirectionsList.SelectedIndex];
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть напрямок.", "Не вибрано напрямок.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
