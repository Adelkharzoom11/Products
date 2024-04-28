using Microsoft.AspNetCore.Http;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.Interfaces
{
    public interface IFileRepository
    {
        Task<List<FileModel>> GetAllFiles();
        Task<FileModel> GetFileById(int id);
        Task<int> UploadFile(FileModel file, IFormFile fileData);
        Task<FileModel> DownloadFile(int id);
    }
}
