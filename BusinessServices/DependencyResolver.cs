using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resolver;
using AutoMapper;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
       public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductServices, ProductServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterInstance<IMapper>(ModelMapper.Mapper);
        }
    }
}
