using Microsoft.EntityFrameworkCore;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service
{
    public class EFModelRepository : IModelRepository
    {
        private readonly VehicleDbContext context;
        IEnumerable<VehicleModel> Models;
        PagingInfo pagingInfo;
        public EFModelRepository(VehicleDbContext context)
        {
            this.context = context;
        }

        public VehicleModel GetById(int id)
        {
            return context.VehicleModels.FirstOrDefault(x => x.Id == id);
        }

        public VehicleModel Delete(int id)
        {
            VehicleModel model = context.VehicleModels.Find(id);
            if (model != null)
            {
                context.Remove(model);
                context.SaveChanges();
            }
            return model;
        }

        public IModelRepository Find(string searchString, string filter = "")
        {            

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();

                switch (filter)
                {
                    case "Name":
                        Models = Models.Where(x => x.Name.ToUpper().Contains(searchString));
                        break;

                    case "Id":
                        Models = Models.Where(x => x.Id.ToString() == searchString);
                        break;

                    case "Abrv":
                        Models = Models.Where(x => x.Abrv.ToUpper().Contains(searchString));
                        break;

                    case "Make":
                        Models = Models.Where(x => x.Make.Name.ToUpper().Contains(searchString));
                        break;

                    case "MakeId":
                        Models = Models.Where(x => x.Make.Id.ToString() == searchString);
                        break;

                    default:
                        Models = Models.Where(x => x.Name.ToUpper().Contains(searchString) ||
                                                   x.Id.ToString().Contains(searchString) ||
                                                   x.Abrv.ToUpper().Contains(searchString) ||
                                                   x.Make.Name.ToUpper().Contains(searchString));
                        break;
                }
            }
            return this;
        }

        public IModelRepository Pagination(int itemsPerPage, int page = 1)
        {
            if (itemsPerPage != 0)
            {
                pagingInfo = new PagingInfo() { TotalItems = Models.Count(), ItemsPerPage = itemsPerPage, CurrentPage = page };
                Models = Models.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return this;
        }

        public void SaveChanges(VehicleModel vehicleModel)
        {
            if (vehicleModel.Id == 0)
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

        public IModelRepository SortBy(string sortBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_desc":
                        Models = Models.OrderBy(x => x.Name).Reverse();
                        break;
                    case "Id":
                        Models = Models.OrderBy(x => x.Id);
                        break;
                    case "Id_desc":
                        Models = Models.OrderBy(x => x.Id).Reverse();
                        break;
                    case "Abrv":
                        Models = Models.OrderBy(x => x.Abrv);
                        break;
                    case "Abrv_desc":
                        Models = Models.OrderBy(x => x.Abrv).Reverse();
                        break;
                    case "Make":
                        Models = Models.OrderBy(x => x.Make.Name);
                        break;
                    case "Make_desc":
                        Models = Models.OrderBy(x => x.Make.Name).Reverse();
                        break;
                    default:
                        Models = Models.OrderBy(x => x.Name);
                        break;
                }
            }
            return this;
        }

        public IEnumerable<VehicleModel> ToCollection()
        {
            return Models;
        }

        public PagingInfo PagingInfo()
        {
            return pagingInfo;
        }

        public IModelRepository VehicleModels()
        {
            Models = context.VehicleModels.Include(x => x.Make);
            return this;
        }

        public IEnumerable<VehicleMake> VehicleMakers()
        {
            return context.VehicleMakers.OrderBy(x => x.Name);
        }


    }
}
