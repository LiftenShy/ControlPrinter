using System;
using System.Linq;
using CP.Data.Models;

namespace CP.Data
{
    public interface IRepository<T> :IDisposable where T : EntityBase
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
    }
}