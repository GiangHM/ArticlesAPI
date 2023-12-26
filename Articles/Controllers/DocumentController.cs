using Articles.Models;
using Articles.Services.Interfaces;
using AzureBlobStorage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;

        public DocumentController(IBlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;

        }

        [HttpGet("topics")]
        public async Task<string> GetSASTokenBlob(string fileName)
        {
            var uri = _blobStorageService.CreateServiceSASBlob("Document", fileName);
            return uri.ToString();
        }
    }
}
