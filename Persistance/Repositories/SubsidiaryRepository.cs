using Application.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class SubsidiaryRepository : Repository<Subsidiary>, ISubsidiaryRepository
    {
        private readonly IDataBaseContext _context;

        public SubsidiaryRepository(IDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public List<Subsidiary> FindByIDs(List<int> ids)
        {
            return _context.Subsidiaries.Where(m => ids.Contains(m.SubsidiaryId)).ToList();
        }

       
    }
}
