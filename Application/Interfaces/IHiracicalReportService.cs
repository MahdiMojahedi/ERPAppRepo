using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHiracicalReportService
    {
        public  Task<List<HierarchyReportItem>> GetHierarchyReportAsync();
    }
}
