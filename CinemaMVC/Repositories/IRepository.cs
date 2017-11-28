using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaMVC.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Item { get; set; }
        IEnumerable<TEntity> List { get; }
        void Save(TEntity entity);
    }
}
