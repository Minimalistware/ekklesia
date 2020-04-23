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
        Task Add(Report report);
        Task Update(Report report);
        IEnumerable<Report> Search(ReportSearchViewModel model);

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

        public IEnumerable<Report> Search(ReportSearchViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Report alteredReport)
        {
            var report = applicationContext.Reports.Attach(alteredReport);
            report.State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();
        }
    }
}
