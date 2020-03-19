using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyVet.web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}