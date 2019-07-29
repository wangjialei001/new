using Autofac;
using New.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internel.Api.Injection
{
    /// <summary>
    /// 依赖注入的模块
    /// </summary>
    public class Evaluation:Module
    {
        /// <summary>
        /// 重写Load方法，进行依赖的注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UserManagerCore).Assembly).Where(t => t.Name.EndsWith("Core")).AsImplementedInterfaces();
        }
    }
}
