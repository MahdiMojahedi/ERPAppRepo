using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class BaseListItemDto<TEntity>  where TEntity : class
    {
        public BaseListItemDto(IEnumerable<TEntity> entities)
        {
            this.Data = entities;
        }
        public IEnumerable<TEntity> Data { get;private set; }
    }
}
