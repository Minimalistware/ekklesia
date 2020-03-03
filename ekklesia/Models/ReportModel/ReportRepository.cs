using ekklesia.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ekklesia.Models.ReportModel
{
    public interface IReportRepository
    {
        Report GetReport(int id);
        IEnumerable<Report> GetReports();
        Report Add(Report report);
        Report Update(Report report);
        IEnumerable<Report> Search(ReportSearchViewModel model);

    }
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationContext applicationContext;

        public ReportRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public Report Add(Report report)
        {
            applicationContext.Reports.Add(report);
            applicationContext.SaveChanges();
            return report;
        }

        public Report GetReport(int id)
        {
            return applicationContext.Reports.Find(id);
        }

        public IEnumerable<Report> GetReports()
        {
            return applicationContext.Reports;
        }

        public IEnumerable<Report> Search(ReportSearchViewModel model)
        {
            throw new NotImplementedException();
        }

        public Report Update(Report alteredReport)
        {
            var report = applicationContext.Reports.Attach(alteredReport);
            report.State = EntityState.Modified;
            applicationContext.SaveChanges();
            return alteredReport;
        }
    }
}
