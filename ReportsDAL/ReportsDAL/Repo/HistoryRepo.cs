using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsDAL.Models;
using Newtonsoft.Json;

namespace ReportsDAL.Repo
{
    public class HistoryRepo:BaseRepo<Report>
    {
        public override List<Report> GetAll() => Context.Reports.OrderBy(x => x.Date).ToList();

        public void AddIntoHistory(ReportData reportData)
        {
            Add(new Report
            {
                Date = reportData.Date,
                ContractNumber = reportData.Contract.Number,
                GrantAgreementNumber = reportData.GrantAgreement.Number,
                ExecutorsName = reportData.Executor.FullName,
                JsonObj = JsonConvert.SerializeObject(reportData)
            });

        }

        public void UpdateHistory(ReportData reportData, int id)
        {
            Report report = GetOne(id);
            report.JsonObj = JsonConvert.SerializeObject(reportData);
            Save(report);
        }

        public ReportData ReadFromHistory(int id)
        {
            string jsonString = GetOne(id).JsonObj;
            ReportData reportData = JsonConvert.DeserializeObject<ReportData>(jsonString);
            return reportData;
        }
    }
}
