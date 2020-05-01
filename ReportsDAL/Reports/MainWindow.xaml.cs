using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Newtonsoft.Json;
using Reports.Windows;
using Reports.Windows.Dialogs;
using ReportsDAL.Models;
using ReportsDAL.Repo;

namespace Reports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ExecutorRepo executorRepo;
        public static DirectionsRepo directionsRepo;
        public static ServiceRepo serviceRepo;
        public static GrantAgreementRepo grantAgreementRepo;
        //public static ReportsRepo reportsRepo;
        public static HistoryRepo historyRepo;

        public MainWindow()
        {
            InitializeComponent();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            executorRepo = new ExecutorRepo();
            directionsRepo = new DirectionsRepo();
            serviceRepo = new ServiceRepo();
            grantAgreementRepo = new GrantAgreementRepo();
            historyRepo = new HistoryRepo();

            stopWatch.Stop();

            TimeSpan ts1 = stopWatch.Elapsed;
            string elapsedTime= String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts1.Hours, ts1.Minutes, ts1.Seconds, ts1.Milliseconds);
            Console.WriteLine(elapsedTime);

            SetCulture();
        }


        private void SetCulture()
        {
            // Change the current culture if the language is not French.
            CultureInfo current = Thread.CurrentThread.CurrentUICulture;
            if (current.TwoLetterISOLanguageName != "fr")
            {
                CultureInfo newCulture = CultureInfo.CreateSpecificCulture("uk-UA");
                Thread.CurrentThread.CurrentUICulture = newCulture;
                // Make current UI culture consistent with current culture.
                Thread.CurrentThread.CurrentCulture = newCulture;
            }
            Console.WriteLine("The current UI culture is {0} [{1}]",
                              Thread.CurrentThread.CurrentUICulture.NativeName,
                              Thread.CurrentThread.CurrentUICulture.Name);
            Console.WriteLine("The current culture is {0} [{1}]",
                              Thread.CurrentThread.CurrentUICulture.NativeName,
                              Thread.CurrentThread.CurrentUICulture.Name);
        }

        private void ButtonDatabase_Click(object sender, RoutedEventArgs e)
        {
            DatabaseAccess databaseAccess = new DatabaseAccess();

            databaseAccess.Show();

        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            history.Show();
        
        }

        private void ButtonExecutorsList_Click(object sender, RoutedEventArgs e)
        {
            ExecutorsList executorsList = new ExecutorsList();

            executorsList.Show();

        }

        private void ButtonSerialization_Click(object sender, RoutedEventArgs e)
        {
            Executor executor = executorRepo.GetOne(1);
            ReportData reportData = new ReportData()
            {
                Number = 69,
                Date = DateTime.Today,
                Executor = executor,
                Contract = executor.Contracts[0],
                GrantAgreement = executor.Contracts[0].GrantAgreements[0],
                Directions = executor.Contracts[0].GrantAgreements[0].Directions
            };

            string json = JsonConvert.SerializeObject(reportData);
        }
    }
}
