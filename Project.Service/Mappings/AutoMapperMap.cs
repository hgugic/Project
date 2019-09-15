using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Mappings
{
    public static class AutoMapperMap
    {
        private static IMapper mapper;

        public static IMapper GetMapper()
        {
            return mapper;
        }

        public static void ConfigureMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            mapper = config.CreateMapper();
        }

    }
}
