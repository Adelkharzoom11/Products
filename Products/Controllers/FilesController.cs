using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductData.Core.Interfaces;
using ProductsDomain;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetAllFiles()
        {
            var files = await _fileRepository.GetAllFiles();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileModel>> DownloadFile(int id)
        {
            var file = await _fileRepository.DownloadFile(id);
            if (file == null)
            {
                return NotFound();
            }

            return File(file.FilePath, "application/octet-stream", file.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<int>> UploadFile([FromForm] FileModel file, IFormFile fileData)
        {
            if (fileData == null || fileData.Length == 0)
            {
                return BadRequest("File is empty");
            }

            try
            {
                int fileId = await _fileRepository.UploadFile(file, fileData);
                return Ok(fileId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
