//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FerreteriaGHome.Web.Helper
//{
//    public class ImageHelper : IImageHelper
//    {
//        public async Task<string> UploadImageAsync(IFormFile ImageFile, string nameFile, string folder)
//        {
//            var guid = Guid.NewGuid().ToString();
//            var file = $"{nameFile}{guid}.png";
//            var path = Path.Combine(Directory.GetCurrentDirectory(), 
//                $"wwwroot\\images\\{folder}", 
//                file);

//            using (var stream = new FileStream(path, FileMode.Create))
//            {
//                await ImageFile.CopyToAsync(stream);
//            }

//            return $"~/images/{folder}/{file}";
//        }
//    }
//}
