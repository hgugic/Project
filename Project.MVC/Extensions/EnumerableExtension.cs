using Microsoft.AspNetCore.Mvc.Rendering;
using Project.MVC.Models;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<Make> AsMake(this IEnumerable<IVehicleMake> makers)
        {
            var makeArray = new Make[makers.Count()];

            for (int i = 0; i < makers.Count(); i++)
            {
                makeArray[i] = new Make(makers.ElementAt(i));
            }

            return makeArray.AsEnumerable().OrderBy(x => x.Name);
        }

        public static IEnumerable<Model> AsModel(this IEnumerable<IVehicleModel> models)
        {
            var modelArray = new Model[models.Count()];

            for (int i = 0; i < models.Count(); i++)
            {
                modelArray[i] = new Model(models.ElementAt(i));
            }

            return modelArray.AsEnumerable();
        }

        public static IEnumerable<Model> IncludeMake(this IEnumerable<Model> models, IEnumerable<IVehicleMake> makers)
        {
            foreach (var model in models)
            {
                model.Make = new Make(makers.FirstOrDefault(x => x.Id == model.MakeId));
            }

            return models;
        }
    }
}
