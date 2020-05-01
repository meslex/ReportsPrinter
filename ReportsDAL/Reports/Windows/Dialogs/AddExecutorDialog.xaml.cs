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
using Reports;
using ReportsDAL.Models;

namespace Reports.Windows.Dialogs
{
    /// <summary>
    /// Interaction logic for AddExecutorDialog.xaml
    /// </summary>
    public partial class AddExecutorDialog : Window
    {
        public Executor executor;

        public AddExecutorDialog()
        {
            InitializeComponent();
            DataContext = new Executor();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Executor ex = (Executor)DataContext;
            Console.WriteLine(ex.ToString());
        }
    }
}
