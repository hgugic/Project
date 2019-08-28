using Microsoft.EntityFrameworkCore;

namespace Project.Service.Extensions
{
    /// <summary>
    /// Proširenje Model builder u svrhu dodavanja podataka u bazu 
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            int i = 0;

            modelBuilder.Entity<VehicleMake>(b =>
            {
                b.HasData(new VehicleMake { Id = ++i, Name = "Audi", Abrv = "A" });
                b.HasData(new VehicleMake { Id = ++i, Name = "Dacia", Abrv = "D" });
                b.HasData(new VehicleMake { Id = ++i, Name = "BMW", Abrv = "B" });
                b.HasData(new VehicleMake { Id = ++i, Name = "Ford", Abrv = "F", });
                b.HasData(new VehicleMake { Id = ++i, Name = "Peugeot", Abrv = "P" });

            });

            modelBuilder.Entity<VehicleModel>(b =>
            {
                b.HasData(new VehicleModel { Id = ++i, Name = "S4", Abrv = "A", MakeId = 1 });
                b.HasData(new VehicleModel { Id = ++i, Name = "S6", Abrv = "A", MakeId = 1 });
                b.HasData(new VehicleModel { Id = ++i, Name = "A4", Abrv = "A", MakeId = 1 });
                b.HasData(new VehicleModel { Id = ++i, Name = "A6", Abrv = "A", MakeId = 1 });
                b.HasData(new VehicleModel { Id = ++i, Name = "A8", Abrv = "A", MakeId = 1 });

                b.HasData(new VehicleModel { Id = ++i, Name = "Sandero", Abrv = "S", MakeId = 2 });
                b.HasData(new VehicleModel { Id = ++i, Name = "Duster", Abrv = "D", MakeId = 2 });

                b.HasData(new VehicleModel { Id = ++i, Name = "X5", Abrv = "S", MakeId = 3 });
                b.HasData(new VehicleModel { Id = ++i, Name = "X6", Abrv = "D", MakeId = 3 });

                b.HasData(new VehicleModel { Id = ++i, Name = "Fiesta", Abrv = "F", MakeId = 4 });
                b.HasData(new VehicleModel { Id = ++i, Name = "Mondeo", Abrv = "M", MakeId = 4 });

                b.HasData(new VehicleModel { Id = ++i, Name = "106", Abrv = "1", MakeId = 5 });
                b.HasData(new VehicleModel { Id = ++i, Name = "207", Abrv = "2", MakeId = 5 });
                b.HasData(new VehicleModel { Id = ++i, Name = "306", Abrv = "3", MakeId = 5 });
                b.HasData(new VehicleModel { Id = ++i, Name = "407", Abrv = "4", MakeId = 5 });
            });

        }
    }
}
