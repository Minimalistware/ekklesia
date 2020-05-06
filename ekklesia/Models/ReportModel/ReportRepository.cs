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
        Task<Report> GetReport(int id);
        Task<IEnumerable<Report>> GetReports();
        IQueryable<Report> Reports();
        Task Add(Report report);
        Task Update(Report report);
        IQueryable<Report> Search(ReportSearchViewModel model);

    }
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationContext applicationContext;

        public ReportRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task Add(Report report)
        {
            await applicationContext.Reports.AddAsync(report);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<Report> GetReport(int id)
        {
            return await applicationContext.Reports
                .Include(r => r.Preacher)
                .Include(r => r.Coordinator)
                .SingleOrDefaultAsync(r => r.Id == id);

        }

        public async Task<IEnumerable<Report>> GetReports()
        {
            return await applicationContext.Reports
                .Include(r => r.Preacher)
                .ToListAsync();
        }

        public IQueryable<Report> Reports()
        {
            return applicationContext.Reports.Include(r => r.Preacher);
        }

        public IQueryable<Report> Search(ReportSearchViewModel model)
        {
            var query = "SELECT * FROM dbo.Reports WHERE ";
            if (model.ReportType != null)
            {
                query += "ReportType = @p0 AND ";
            }
            if (model.Months != null)
            {
                query += "MONTH([Date]) < @p1 AND ";
            }
            query += "1 = 1";
            return applicationContext.Reports.FromSql(query, model.ReportType, model.Months).Include(r => r.Preacher);
        }

        public async Task Update(Report alteredReport)
        {
            var report = applicationContext.Reports.Attach(alteredReport);
            report.State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();
        }
    }
}
