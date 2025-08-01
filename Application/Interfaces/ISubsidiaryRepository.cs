using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISubsidiaryRepository : IRepository<Subsidiary>
    {
        List<Subsidiary> FindByIDs(List<int> ids); 
    }

}
