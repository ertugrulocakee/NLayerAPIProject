using Module = Autofac.Module;
using NLayerAPI.Repository.Concrete;
using NLayerAPI.Service.Mapping;
using System.Reflection;
using Autofac;
using NLayerAPI.Repository.Repository;
using NLayerAPI.Core.Repository;
using NLayerAPI.Service.Services;
using NLayerAPI.Core.Services;
using NLayerAPI.Repository.UnitOfWorks;
using NLayerAPI.Core.UnitOfWorks;
using NLayerAPI.Caching;

namespace NLayerAPI.API.Modules
{
    public class RepoServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();  

            var repoAssembly = Assembly.GetAssembly(typeof(AppDBContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
            builder.RegisterType<CategoryServiceWithCaching>().As<ICategoryService>();
            builder.RegisterType<ProductFeatureServiceWithCaching>().As<IProductFeatureService>();

        }

    }
}
