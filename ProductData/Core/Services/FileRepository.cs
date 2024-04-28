using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductData.Core.Interfaces;
using ProductsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.Classes
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        private readonly string _uploadsDirectory;

        public FileRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _uploadsDirectory = config["UploadsDirectory"]; // Get the uploads directory from configuration
        }

        public async Task<List<FileModel>> GetAllFiles()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task<FileModel> GetFileById(int id)
        {
            return await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int> UploadFile(FileModel file, IFormFile fileData)
        {
            if (fileData == null || fileData.Length == 0)
            {
                throw new Exception("File is empty");
            }

            // Save the file to the uploads directory
            string filePath = Path.Combine(_uploadsDirectory, fileData.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileData.CopyToAsync(stream);
            }

            // Save the file information to the database
            file.FilePath = filePath;
            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            return file.Id;
        }

        public async Task<FileModel> DownloadFile(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
            if (file == null)
            {
                throw new Exception("File not found");
            }

            return file;
        }
    }
}
