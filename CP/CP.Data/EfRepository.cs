using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using CP.Data.Models;

namespace CP.Data
{
    public class EfRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _context;
        private IDbSet<T> _entities;
        string _errorMessage = string.Empty;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage +=
                            $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += Environment.NewLine +
                                        $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += Environment.NewLine +
                                        $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table => Entities;

        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
