using System;
using System.Collections.Generic;
using System.Linq;
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

        //Get By Id
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        //Insert
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        //Update
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); //Update Locally
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
