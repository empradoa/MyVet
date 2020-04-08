using System.Threading.Tasks;

namespace MyVet.Common.Services
{
    public interface IGeolocatorService
    {
        //lo que nos alejamos y nos acercamos a la linea del ecuador
        double Latitude { get; set; }

        //Lo que nos movemos al oriente o al occidente del meridiano de grenwichs
        double Longitude { get; set; }

        Task GetLocationAsync();
    }
}
