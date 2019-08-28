using Microsoft.EntityFrameworkCore;
using Project.Service.Enums;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Project.Service
{
    // Klasa je definirana projektom
    // Da li krši SOLID?

    public class VehicleService : IModelRepository, IMakeRepository
    {
        private readonly VehicleDbContext context;

        public VehicleService(VehicleDbContext context)
        {
            this.context = context;
        }

        public IQueryable<VehicleModel> VehicleModels => context.VehicleModels.Include(x => x.Make);

        public IQueryable<VehicleMake> VehicleMakers => context.VehicleMakers.Include(x => x.VehicleModels);

        public VehicleMake DeleteVehicleMake(int makeId)
        {
            VehicleMake make = context.VehicleMakers.Find(makeId);
            if (make != null)
            {
                context.Remove(make);
                context.SaveChanges();
            }
            return make;
        }

        public VehicleModel DeleteVehicleModel(int modelId)
        {
            VehicleModel model = context.VehicleModels.Find(modelId);
            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }
            return model;
        }

        public void SaveVehicleModel(VehicleModel vehicleModel)
        {
            if(vehicleModel.Id == 0)
            {
                context.Add(vehicleModel);
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleModels.Attach(vehicleModel);
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void SaveVehicleMake(VehicleMake vehicleMake)
        {
            if (vehicleMake.Id == 0)
            {
                context.Add(vehicleMake);
                context.SaveChanges();
            }
            else
            {
                var vehicle = context.VehicleMakers.Attach(vehicleMake);
                vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<VehicleMake> SortBy(VehicleData sortBy, bool sortByDescending, IEnumerable<VehicleMake> vehicleMakers)
        {
            if (vehicleMakers != null)
            {
                if (vehicleMakers.Any())
                {
                    switch (sortBy)
                    {

                        case Enums.VehicleData.Id:
                            if (sortByDescending)
                            {
                                return vehicleMakers.OrderBy(x => x.Id).Reverse();
                            }
                            return vehicleMakers.OrderBy(x => x.Id);

                        case Enums.VehicleData.Name:
                            if (sortByDescending)
                            {
                                return vehicleMakers.OrderBy(x => x.Name).Reverse();
                            }
                            return vehicleMakers.OrderBy(x => x.Name);

                        case Enums.VehicleData.Abrv:
                            if (sortByDescending)
                            {
                                return vehicleMakers.OrderBy(x => x.Abrv).Reverse();
                            }
                            return vehicleMakers.OrderBy(x => x.Abrv);

                        case Enums.VehicleData.Make:
                            throw new ArgumentException("Nije moguće sortiranje");

                        default:
                            return vehicleMakers;
                    }
                }
            }
            return vehicleMakers;
        }

        public IEnumerable<VehicleMake> Paging(int page, int pageSize, IEnumerable<VehicleMake> vehicleMakers)
        {
            if (vehicleMakers != null)
            {
                if (vehicleMakers.Any())
                {
                    return vehicleMakers.Skip((page - 1) * pageSize).Take(pageSize);
                }
            }
            return vehicleMakers;
        }

        public IEnumerable<VehicleMake> Search(string searchString, IEnumerable<VehicleMake> vehicleMakers)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return vehicleMakers;
            }
            else
            {
                return vehicleMakers.Where(x => x?.Name?.ToLower().Contains(searchString.ToLower()) == true
                || x?.Abrv?.ToLower().Contains(searchString.ToLower()) == true);
                
            }
        }

        public IEnumerable<VehicleModel> SortBy(VehicleData sortBy, bool sortByDescending, IEnumerable<VehicleModel> vehicleModels)
        {
            if (vehicleModels != null)
            {
                if (vehicleModels.Any())
                {
                    switch (sortBy)
                    {

                        case Enums.VehicleData.Id:
                            if (sortByDescending)
                            {
                                return vehicleModels.OrderBy(x => x.Id).Reverse();
                            }
                            return vehicleModels.OrderBy(x => x.Id);

                        case Enums.VehicleData.Name:
                            if (sortByDescending)
                            {
                                return vehicleModels.OrderBy(x => x.Name).Reverse();
                            }
                            return vehicleModels.OrderBy(x => x.Name);

                        case Enums.VehicleData.Abrv:
                            if (sortByDescending)
                            {
                                return vehicleModels.OrderBy(x => x.Abrv).Reverse();
                            }
                            return vehicleModels.OrderBy(x => x.Abrv);

                        case Enums.VehicleData.Make:
                            if (sortByDescending)
                            {
                                return vehicleModels.OrderBy(x => x.Make.Name).Reverse();
                            }
                            return vehicleModels.OrderBy(x => x.Make.Name);
                        default:
                            return vehicleModels;
                    }
                }
            }
            return vehicleModels;
        }

        public IEnumerable<VehicleModel> Paging(int page, int pageSize, int? makeId, IEnumerable<VehicleModel> vehicleModels)
        {
            if (vehicleModels != null)
            {
                if (vehicleModels.Any())
                {
                    if (makeId == null)
                    {
                        return vehicleModels.Skip((page - 1) * pageSize).Take(pageSize);
                    }
                    else
                    {
                        return vehicleModels.Where(x => makeId == null || x.MakeId == makeId)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize);
                    }
                }
            }
            return vehicleModels;
        }

        public IEnumerable<VehicleModel> Search(string searchString, IEnumerable<VehicleModel> vehicleModels, VehicleData filter)
        {

            if (vehicleModels != null)
            {
                if (vehicleModels.Any())
                {
                    if (string.IsNullOrEmpty(searchString))
                    {
                        return vehicleModels;
                    }
                    else
                    {
                        switch (filter)
                        {

                            case Enums.VehicleData.Id:
                                return vehicleModels.Where(x => x?.Id.ToString()?.ToLower().Contains(searchString.ToLower()) == true);

                            case Enums.VehicleData.Name:
                                return vehicleModels.Where(x => x?.Name?.ToLower().Contains(searchString.ToLower()) == true);

                            case Enums.VehicleData.Abrv:
                                return vehicleModels.Where(x => x?.Abrv?.ToLower().Contains(searchString.ToLower()) == true);

                            case Enums.VehicleData.Make:
                                return vehicleModels.Where(x => x?.Make?.Name?.ToLower().Contains(searchString.ToLower()) == true);
                            default:
                                return vehicleModels;
                        }
                    }
                }
            }
            return vehicleModels;
        }

        public IEnumerable<VehicleMake> GetAllMakers()
        {
            return VehicleMakers;
        }
    }
}
