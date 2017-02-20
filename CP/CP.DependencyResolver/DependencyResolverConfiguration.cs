using System.Data.Entity;
using CP.Business;
using CP.Data;
using CP.Storage;
using Microsoft.Practices.Unity;

namespace CP.DependencyResolver
{
    public class DependencyResolverConfiguration
    {
        public DependencyResolverConfiguration()
        {
            this.Container = new UnityContainer();
            this.RegisterTypes();
        }

        public IUnityContainer Container { get; private set; }

        protected virtual void RegisterTypes()
        {
            this.Container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
            this.Container.RegisterType<DbContext, ControlPrinterDbContext>();
            this.Container.RegisterType<IRoleService, RoleService>();
            this.Container.RegisterType<IUserService, UserService>();
            this.Container.RegisterType<IFileManager, CloudinaryFileManager>();
            this.Container.RegisterType<IImageService, ImageService>();
        }
    }
}
