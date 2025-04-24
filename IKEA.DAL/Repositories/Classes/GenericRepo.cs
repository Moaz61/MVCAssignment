using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Data.Contexts;
using IKEA.DAL.Models.DepartmentModel;
using IKEA.DAL.Models.Shared;
using IKEA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.Repositories.Classes
{
    public class GenericRepo<TEntity>(ApplicationDBContext _dbContext) : IGenericRepo<TEntity> where TEntity : BaseEntity
    {
        //Get All
        public IEnumerable<TEntity> GetAll(bool WithTracker = false)
        {
            if (WithTracker)
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
                             .Where(predicate)
                             .ToList();
        }

        //Get By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        //Insert
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        //Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); //Update Locally
        }

        //Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
