using System;
using System.Linq;
using ControlPrinter.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlPrinter.Data
{
    public interface IEfRepository<T>
      where T : class
    {
        IQueryable<T> Table { get; set; }

        void Add(T model);

        void Update(T model);
    }
}
