using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessServices
{
    public class ModelMapper
    {
        private static IMapper _mapper;

        private static void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            _mapper = config.CreateMapper();
        }

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    InitializeMapper();
                }
                return _mapper;
            }
        }
    }
}