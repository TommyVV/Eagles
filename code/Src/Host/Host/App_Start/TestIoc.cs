using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Autofac;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Implement;

namespace Eagles.Application.Host
{
    public class TestIoc: AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build(); ;
            using (var scope = container.BeginLifetimeScope())
            {
                var component = scope.Resolve<IDbManager>();
            }

            return true;
        }

        public static IContainer Container { get; set; }

        public static object GetResolver<TService>()
        {
            var iTypes = typeof(TService).GetInterfaces();
            if (iTypes.Count() == 0)
            {
                throw new NotImplementedException("依赖注入的类型没有实现接口");
            }
            Type iType = iTypes[0];
            if (Container.IsRegistered(iType))
            {
                return Container.Resolve(iType);
            }
            var builder = new ContainerBuilder();
            RegisterServices<TService>(builder, iType);
            builder.Update(Container);
            return Container.Resolve(iType);
        }

        private static void RegisterServices<TService>(ContainerBuilder builder, Type iType)
        {
            builder.RegisterType<TService>().As(new Type[] { iType });
        }
    }
}