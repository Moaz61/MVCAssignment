using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Shared;

namespace IKEA.DAL.Repositories.Interfaces
{
    public interface IGenericRepo<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool WithTracker = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity? GetById(int id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
