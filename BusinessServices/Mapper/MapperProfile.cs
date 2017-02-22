using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DataModel;
using BusinessEntites;

namespace BusinessServices
{
    public class MapperProfile :Profile
    {
        protected override void Configure()
        {
            CreateMap<User, UserEntity>();
            CreateMap<Token, TokenEntity>();
            CreateMap<Product, ProductEntity>();
        }
    }
}