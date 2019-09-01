using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service
{
    public class EFMakeRepository : IMakeRepository
    {
        private readonly VehicleDbContext context;
        IEnumerable<VehicleMake> Makers;
        PagingInfo pagingInfo;

        public EFMakeRepository(VehicleDbContext context)
        {
            this.context = context;          
        }

        public VehicleMake GetById(int id)
        {
            return context.VehicleMakers.FirstOrDefault(x => x.Id == id);
        }

        public VehicleMake Delete(int id)
        {
            VehicleMake make = context.VehicleMakers.Find(id);
            if (make != null)
            {
                context.Remove(make);
                context.SaveChanges();
            }
            return make;
        }


        public IMakeRepository Find(string searchString, string filter = "")
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();

                switch (filter)
                {
                    case "Name":
                        Makers = Makers.Where(x => x.Name.ToUpper().Contains(searchString));
                        break;

                    case "Id":
                        Makers = Makers.Where(x => x.Id.ToString() == searchString);
                        break;

                    case "Abrv":
                        Makers = Makers.Where(x => x.Abrv.ToUpper().Contains(searchString));
                        break;

                    default:
                        Makers = Makers.Where(x => x.Name.ToUpper().Contains(searchString) ||
                                                   x.Id.ToString().Contains(searchString) ||
                                                   x.Abrv.ToUpper().Contains(searchString));
                        break;
                }
            }

            return this;
        }

        public IMakeRepository Pagination(int itemsPerPage, int page = 1)
        {
            if (itemsPerPage != 0)
            {
                pagingInfo = new PagingInfo() { TotalItems = Makers.Count(), ItemsPerPage = itemsPerPage, CurrentPage = page };
                Makers = Makers.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            return this;
        }

        public void SaveChanges(VehicleMake vehicleMake)
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

        public IMakeRepository SortBy(string sortBy)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "Name_desc":
                        Makers = Makers.OrderBy(x => x.Name).Reverse();
                        break;
                    case "Id":
                        Makers = Makers.OrderBy(x => x.Id);
                        break;
                    case "Id_desc":
                        Makers = Makers.OrderBy(x => x.Id).Reverse();
                        break;
                    case "Abrv":
                        Makers = Makers.OrderBy(x => x.Abrv);
                        break;
                    case "Abrv_desc":
                        Makers = Makers.OrderBy(x => x.Abrv).Reverse();
                        break;
                    default:
                        Makers = Makers.OrderBy(x => x.Name);
                        break;
                }
            }

            return this;
        }

        public IEnumerable<VehicleMake> ToCollection()
        {
            return Makers;
        }

        public PagingInfo PagingInfo()
        {
            return pagingInfo;
        }

        public IMakeRepository VehicleMakers()
        {
            Makers = context.VehicleMakers;
            return this;
        }
    }
}
