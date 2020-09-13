using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Framework.Common
{
    public static class GlobalHelpers
    {
        public static bool IsFileValid(this IFormFile file)
        {
            return file == null || file.Length == 0 || file.Length <= 0;
        }
        public static async Task<FileInfo> GetFileLocationAndSaveFile(this IFormFile uploadedFile, string rootFolder)
        {
            var fileName = uploadedFile.FileName;
            var filePath = Path.Combine(rootFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            return await Task.FromResult(new FileInfo(Path.Combine(rootFolder, fileName)));
        }
    }
}
