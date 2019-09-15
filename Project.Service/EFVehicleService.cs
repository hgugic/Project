using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.Interfaces;
using Project.Service.Interfaces;
using Project.Service.Mappings;
using Project.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service
{
    public class EFVehicleService : IVehicleService  
    {
        private readonly VehicleDbContext context;
        private IMapper mapper;

        public EFVehicleService(VehicleDbContext context)
        {
            this.context = context;
            AutoMapperMap.ConfigureMapping();
            mapper = AutoMapperMap.GetMapper();
        }

        #region Model

        public IModel GetModelById(int id)
        {
            return mapper.Map<Model>(context.VehicleModels.FirstOrDefault(x => x.Id == id));
        }

        public IModel DeleteModel(int id)
        {
            VehicleModel model = context.VehicleModels.Find(id);
            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }
            return mapper.Map<Model>(model);
        }

        public void SaveChanges(IModel vehicleModel)
        {

            if (vehicleModel.Id == 0)
            {
                context.Add(mapper.Map<VehicleModel>(vehicleModel));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleModels.Attach(mapper.Map<VehicleModel>(vehicleModel));
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<IModel> FindModel(IFilter filter, ISort sort, IPaging paging)
        {
            IEnumerable<IVehicleModel> models = context.VehicleModels;
            models = ModelListFind(models, filter);
            models = ModelListSortBy(models, sort);
            models = ModelListPaging(models, paging);
            return mapper.Map<IEnumerable<Model>>(models);
        }

        private IEnumerable<IVehicleModel> ModelListFind(IEnumerable<IVehicleModel> models, IFilter filter)
        {

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                filter.SearchString = filter.SearchString.ToUpper();

                switch (filter.Filter)
                {
                    case "Name":
                        models = models.Where(x => x.Name.ToUpper().Contains(filter.SearchString));
                        break;

                    case "Id":
                        models = models.Where(x => x.Id.ToString() == filter.SearchString);
                        break;

                    case "Abrv":
                        models = models.Where(x => x.Abrv.ToUpper().Contains(filter.SearchString));
                        break;

                    case "Make":
                        IEnumerable<VehicleModel> ModelsWithMake = context.VehicleModels.Include(x => x.Make)
                                                                            .Where(x => x.Make.Name.ToUpper()
                                                                                                   .Contains(filter.SearchString));

                        models = models.Intersect(ModelsWithMake, new VehicleModelIdComparer());
                        break;

                    case "MakeId":
                        models = models.Where(x => x.MakeId.ToString() == filter.SearchString);
                        break;

                    default:
                        models = models.Where(x => x.Name.ToUpper().Contains(filter.SearchString) ||
                                                   x.Id.ToString().Contains(filter.SearchString) ||
                                                   x.Abrv.ToUpper().Contains(filter.SearchString));

                        break;
                }
            }
            return models;
        }

        private IEnumerable<IVehicleModel> ModelListPaging(IEnumerable<IVehicleModel> models, IPaging paging)
        {
            paging.TotalPages = 0;

            if (paging.ItemsPerPage != 0)
            {
                paging.TotalPages = (int)Math.Ceiling((decimal)models.Count() / paging.ItemsPerPage);
                return models.Skip((paging.CurrentPage - 1) * paging.ItemsPerPage).Take(paging.ItemsPerPage);
            }

            return models;
        }

        private IEnumerable<IVehicleModel> ModelListSortBy(IEnumerable<IVehicleModel> models, ISort sort)
        {
            if (!string.IsNullOrEmpty(sort.SortBy))
            {
                switch (sort.SortBy)
                {
                    case "Id":
                        models = models.OrderBy(x => x.Id);
                        break;

                    case "Abrv":
                        models = models.OrderBy(x => x.Abrv);
                        break;

                    case "Make":
                        IEnumerable<VehicleModel> modelsWithMakeAsc = context.VehicleModels.Include(x => x.Make).OrderBy(x => x.Make.Name);                    
                        models = modelsWithMakeAsc.Intersect(models, new VehicleModelIdComparer());
                        break;

                    default:
                        models = models.OrderBy(x => x.Name);
                        break;
                }

                if(sort.SortDirection == "desc")
                {
                    models = models.Reverse();
                }

            }
            return models;
        }

        #endregion Model

        #region Make

        public IMake GetMakeById(int id)
        {
            return mapper.Map<Make>(context.VehicleMakers.FirstOrDefault(x => x.Id == id));
        }

        public IMake DeleteMake(int id)
        {
            VehicleMake make = context.VehicleMakers.Find(id);
            if (make != null)
            {
                context.Remove(make);
                context.SaveChanges();
            }
            return mapper.Map<Make>(make);
        }

        public void SaveChanges(IMake vehicleMake)
        {
            if (vehicleMake.Id == 0)
            {
                context.Add(mapper.Map<VehicleMake>(vehicleMake));
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleMakers.Attach(mapper.Map<VehicleMake>(vehicleMake));
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<IMake> FindMake(IFilter filter, ISort sort, IPaging paging)
        {
            IEnumerable<IVehicleMake> makers = context.VehicleMakers;
            makers = MakeListFind(makers, filter);
            makers = MakeListSortBy(makers, sort);
            makers = MakeListPaging(makers, paging);
            return mapper.Map<IEnumerable<Make>>(makers);
        }

        private IEnumerable<IVehicleMake> MakeListFind(IEnumerable<IVehicleMake> makers, IFilter filter)
        {
            if (filter !=null)
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                {
                    filter.SearchString = filter.SearchString.ToUpper();

                    switch (filter.Filter)
                    {
                        case "Name":
                            makers = makers.Where(x => x.Name.ToUpper().Contains(filter.SearchString));
                            break;

                        case "Id":
                            makers = makers.Where(x => x.Id.ToString() == filter.SearchString);
                            break;

                        case "Abrv":
                            makers = makers.Where(x => x.Abrv.ToUpper().Contains(filter.SearchString));
                            break;

                        default:
                            makers = makers.Where(x => x.Name.ToUpper().Contains(filter.SearchString) ||
                                                       x.Id.ToString().Contains(filter.SearchString) ||
                                                       x.Abrv.ToUpper().Contains(filter.SearchString));
                            break;
                    }
                }
            }

            return makers;

        }

        private IEnumerable<IVehicleMake> MakeListPaging(IEnumerable<IVehicleMake> makers, IPaging paging)
        {
            if (paging != null)
            {
                if (paging.ItemsPerPage != 0)
                {
                    paging.TotalPages = (int)Math.Ceiling((decimal)makers.Count() / paging.ItemsPerPage);
                    return makers.Skip((paging.CurrentPage - 1) * paging.ItemsPerPage).Take(paging.ItemsPerPage);
                }
            }
            return makers;
        }

        private IEnumerable<IVehicleMake> MakeListSortBy(IEnumerable<IVehicleMake> makers, ISort sort)
        {
            if (sort != null)
            {
                if (!string.IsNullOrEmpty(sort.SortBy))
                {
                    switch (sort.SortBy)
                    {
                        case "Id":
                            makers = makers.OrderBy(x => x.Id);
                            break;
                        case "Abrv":
                            makers = makers.OrderBy(x => x.Abrv);
                            break;
                        default:
                            makers = makers.OrderBy(x => x.Name);
                            break;
                    }

                    if (sort.SortDirection == "desc")
                    {
                        makers = makers.Reverse();
                    }
                }
            }
            return makers;
        }

        #endregion Make
    }
}
