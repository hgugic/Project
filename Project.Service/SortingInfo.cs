using Project.Service.Enums;

namespace Project.Service
{
    /// <summary>
    /// Klasa za formatiranje sortiranja
    /// </summary>
    public class SortingInfo
    {
        public VehicleData SortBy { get; set; }

        public bool SortByDescending { get; set; }
    }
}
