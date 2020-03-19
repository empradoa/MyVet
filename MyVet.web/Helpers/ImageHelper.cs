using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyVet.web.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<String> UploadImageAsync(IFormFile imageFile)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";

            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot\\images\\Pets",
            file);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"~/images/Pets/{file}";
        }
    }
}
