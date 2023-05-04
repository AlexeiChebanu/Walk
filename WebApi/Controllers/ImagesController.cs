using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Domain;
using WebApi.Models.DTO;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequestDTO)
        {
            ValidateFileUpload(imageUploadRequestDTO);
            
            if(ModelState.IsValid)
            {
                var imageDomaiModel = new Image
                {
                    File = imageUploadRequestDTO.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDTO.File.FileName),
                    FileSizeInBytes = imageUploadRequestDTO.File.Length,
                    FileName = imageUploadRequestDTO.FileName,
                    FileDescription = imageUploadRequestDTO.FileDescription
                };

                await imageRepository.Upload(imageDomaiModel);

                return Ok(imageDomaiModel);

            }

            return BadRequest(ModelState);


        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequestDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDTO.File.FileName))) 
            { 
                ModelState.AddModelError("file", "Unsupported file extension"); 
            }

            if(imageUploadRequestDTO.File.Length > 10485760) //10mb
            {
                ModelState.AddModelError("file", "File size more than 100mb, plz upload a smalelr size file.");
            }

        }

    }
}
