using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMasterRepository : IRepository<Master>
    {
        List<Master> FindByIDs(List<int> ids); // Extra custom method
    }

    
}
