using Application.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HiracicalReportService : IHiracicalReportService
    {

        private readonly IDataBaseContext _context;

        public HiracicalReportService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<List<HierarchyReportItem>> GetHierarchyReportAsync()
        {
            var masters = await _context.Masters
                .Include(m => m.Subsidiaries.Where(s => !s.IsDeleted && s.ParentSubsidiaryId == null))
                    .ThenInclude(s => s.Children.Where(c => !c.IsDeleted))
                .ToListAsync();

            var result = new List<HierarchyReportItem>();

            foreach (var master in masters)
            {
                var masterReport = new HierarchyReportItem
                {
                    MasterId = master.MasterId,
                    Title = master.Title,
                    Subsidiaries = new List<HierarchyReportItem>()
                };

                foreach (var topLevelSub in master.Subsidiaries)
                {
                    var subReport = BuildSubsidiaryHierarchy(topLevelSub);
                    masterReport.Subsidiaries.Add(subReport);
                }

                // Aggregate all last-level subsidiary amounts
                masterReport.TotalDebit = masterReport.Subsidiaries.Sum(s => s.TotalDebit);
                masterReport.TotalCredit = masterReport.Subsidiaries.Sum(s => s.TotalCredit);
                masterReport.Title = master.Title;

                result.Add(masterReport);
            }

            return result;
        }

        private HierarchyReportItem BuildSubsidiaryHierarchy(Subsidiary node)
        {
            var item = new HierarchyReportItem
            {
                SubsidiaryId = node.SubsidiaryId,
                Title = node.Title,
                Subsidiaries = new List<HierarchyReportItem>(),
                IsLastLevel = node.IsLastLevel
            };

            foreach (var child in node.Children.Where(c => !c.IsDeleted))
            {
                var childItem = BuildSubsidiaryHierarchy(child);
                item.Subsidiaries.Add(childItem);
            }

            if (node.IsLastLevel)
            {
                item.TotalDebit = node.DebitAmount ?? 0;
                item.TotalCredit = node.CreditAmount ?? 0;
            }
            else
            {
                item.TotalDebit = item.Subsidiaries.Sum(x => x.TotalDebit);
                item.TotalCredit = item.Subsidiaries.Sum(x => x.TotalCredit);
                item.Title = node.Title;
                item.IsLastLevel = node.IsLastLevel;
            }

            return item;
        }

    }

    public class HierarchyReportItem
    {
        public int? MasterId { get; set; }
        public int? SubsidiaryId { get; set; }
        public string Title { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public bool IsLastLevel { get; set; }
        public List<HierarchyReportItem> Subsidiaries { get; set; } = new();
    }
}
