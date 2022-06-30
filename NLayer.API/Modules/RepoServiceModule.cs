using Autofac;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWork;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace NLayer.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var apiAssembly = Assembly.GetExecutingAssembly();//üzerinde çalışmış olduğun assembly al(bu apidir)
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));//repository assemblysini(katmamını) al. Bunu orada bulunan herhangi bir sınıftan bulacak(bunu AppDbContex olarak verdik)
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));//aynı şekilde, MapProfile tipinden servis katmanını bulacak. Katmanlarımız birer assemblydir.

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            //bu assemblylere git. Sonu "Respository" ile bitenleri al. Interfacelerini implemente et. 

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //InstancePerLifetimeScope() => Scope a karşılık gelir. Request bitine kadar aynı instance ı kullanır.
            //InstanceDependency() => transiet e karşılık gelir. Constructor da gördüğünde her seferinde yeni instance oluşturur.

            //Generic olanları da bu şekilde.
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


           // builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

        }
    }
}
