using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Repo;
using ReportsDAL.Models;
using Newtonsoft.Json;

namespace Reports
{
    /*public static class HistoryController
    {
        public static void AddIntoHistory(ReportData reportData)
        {
            MainWindow.reportsRepo.Add(new Report
            {
                Date = reportData.Date,
                ContractNumber = reportData.Contract.Number,
                GrantAgreementNumber = reportData.GrantAgreement.Number,
                ExecutorsName = reportData.Executor.FullName,
                JsonObj = JsonConvert.SerializeObject(reportData)
            });

        }

        public static void UpdateHistory(ReportData reportData, int id)
        {
            Report report = MainWindow.reportsRepo.GetOne(id);
            report.JsonObj = JsonConvert.SerializeObject(reportData);
            MainWindow.reportsRepo.Save(report);
        }

        public static ReportData ReadFromHistory(int id)
        {
            string jsonString = MainWindow.reportsRepo.GetOne(id).JsonObj;
            ReportData reportData = JsonConvert.DeserializeObject<ReportData>(jsonString);
            return reportData;
        }
    }*/
}
