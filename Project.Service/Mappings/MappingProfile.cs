using Project.Models.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Models;
using Project.Service.Interfaces;

namespace Project.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, IMake>();
            CreateMap<IMake, Make>();
            CreateMap<IMake, VehicleMake>();
            CreateMap<VehicleMake, IMake>();
            CreateMap<VehicleMake, Make>();
            CreateMap<Make, VehicleMake>();

            CreateMap<Model, IModel>();
            CreateMap<IModel, Model>();
            CreateMap<IModel, VehicleModel>();
            CreateMap<VehicleModel, IModel>();
            CreateMap<VehicleModel, Model>();
            CreateMap<Make, VehicleModel>();


            CreateMap<IModel, IVehicleModel>();
            CreateMap<IVehicleModel, IModel>();
            CreateMap<IVehicleModel, Model>();
            CreateMap<Make, IVehicleModel>();
        }
    }
}
