using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFileManagement.Service.Services;

namespace WebFileManagement.Server.Controllers;

[Route("api/[storage")]
[ApiController]
public class StorageController : ControllerBase
{
    private readonly IStorageService _storageService;
    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }
    [HttpPost("uploadFile")]
    public void UploadFile(IFormFile file, string directoryPath)
    {
        directoryPath += file.FileName;
        using(Stream stream = file.OpenReadStream())
        {
            _storageService.UploadFile(directoryPath, stream);
        }
    }
    [HttpPost("createFolder")]
    public void CreateFolder(string folderPath)
    {
        _storageService.CreateDirectory(folderPath);
    }
    [HttpGet("getAll")]
    public List<string> GetAllInFolder(string folderPath)
    {
        var all= _storageService.GetAllFilesAndDirectories(folderPath);
        return all;
    }
}
