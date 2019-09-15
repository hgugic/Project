using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Models.Interfaces;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.MVC.Extensions
{
    public static class EnumerableExtension
    {

        public static IEnumerable<IModel> IncludeMake(this IEnumerable<IModel> models, IEnumerable<IMake> makers)
        {
            foreach (var model in models)
            {
                model.Make = makers.FirstOrDefault(x => x.Id == model.MakeId);
            }

            return models;
        }
    }
}
