using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Pattern.Infrastructure
{
    public class EntityModel<TEntity>  where TEntity : class, IObjectState
    {
        TEntity entity;
        public EntityModel(TEntity _entity)
        {
            entity = _entity;
        }
    }
}
