using System.Data.Entity;
using CP.Business;
using CP.Business.Abstract;
using CP.Data;
using CP.Storage;
using Unity;

namespace CP.DependencyResolver
{
    public class DependencyResolverConfiguration
    {
        public DependencyResolverConfiguration()
        {
            Container = new UnityContainer();
            RegisterTypes();
        }

        public IUnityContainer Container { get;}

        protected virtual void RegisterTypes()
        {
            Container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
            Container.RegisterType<DbContext, ControlPrinterDbContext>();
            Container.RegisterType<IRoleService, RoleService>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType<IFileManager, CloudinaryFileManager>();
            Container.RegisterType<IImageService, ImageService>();
            Container.RegisterType<IQueueService,QueueService>();
        }
    }
}
