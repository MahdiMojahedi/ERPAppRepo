using Application.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class MasterRepository : Repository<Master>, IMasterRepository
    {
        private readonly IDataBaseContext _context;

        public MasterRepository(IDataBaseContext context) : base(context)
        {
            _context = context;
        }

        public List<Master> FindByIDs(List<int> ids)
        {
            return _context.Masters.Where(m => ids.Contains(m.MasterId)).ToList();
        }
    }
}
