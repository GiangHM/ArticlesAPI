using API.Models;
using API.Services.Interfaces;
using AzureBlobStorage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IBlobStorageService blobStorageService
            , ILogger<DocumentController> logger)
        {
            _blobStorageService = blobStorageService;
            _logger = logger;
        }

        [HttpGet("SasGeneration")]
        public Uri GenereateSasBlob(string fileName)
        {
            _logger.LogInformation("If you're seeing this, we were generating the sas blob");
            return _blobStorageService.CreateServiceSASBlob("Put container name", fileName: fileName);
        }
    }
}
