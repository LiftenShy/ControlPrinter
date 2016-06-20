using System.Data.Entity;
using CP.Data.Models;

namespace CP.Data
{
    internal class ControlPrinterDbInitializer : CreateDatabaseIfNotExists<ControlPrinterDbContext>
    {
        protected override void Seed(ControlPrinterDbContext context)
        {
            base.Seed(context);

            context.Roles.Add(new Role {NameRole = "admin"});
            context.Roles.Add(new Role {NameRole = "user"});
            context.SaveChanges();
        }
    }
}
