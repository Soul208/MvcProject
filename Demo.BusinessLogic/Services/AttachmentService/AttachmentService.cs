using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions = [ ".png" , ".jpg" , ".jpeg"];

        const int maxSize = 2_097_152;
        public string? Upload(IFormFile file, string FolderName)
        {
            var extension = Path.GetExtension(file.FileName);
            if(!allowedExtensions.Contains(extension))return null;

            //2

            if(file.Length == 0 || file.Length > maxSize) 
                return null;

            //3
            //var floderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\";
            var floderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //4
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            //5
            var filePath = Path.Combine(floderPath, fileName); //File Loc

            //6

           using FileStream Fs = new FileStream(filePath , FileMode.Create);

            //7 
            file.CopyTo(Fs);
            
            //8
            return fileName;

        }
        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }

        }

    }
}
