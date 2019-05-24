using System;
using System.Linq;
using ControlPrinter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ControlPrinter.Data
{
    public class EfRepository<T> : IEfRepository<T>
     where T : class 
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entities;

        public IQueryable<T> Table { get; set; }

        public EfRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<T>();
            Table = _entities.AsQueryable();
        }

        public void Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentException($"Model is null: {model}");
            }

            _entities.Update(model);
            _dbContext.SaveChanges();
        }
    }
}
