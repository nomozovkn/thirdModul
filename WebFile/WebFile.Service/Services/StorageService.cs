using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFile.StorageBrokker.Services;

namespace WebFile.Service.Services;

public class StorageService : IStorageService
{
    private readonly IStorageBrokkerService _storageBrokkerService;
    public StorageService(IStorageBrokkerService storageBrokkerService)
    {
        _storageBrokkerService = storageBrokkerService;
    }

    public void CreateDirectory(string directoryPath)
    {

        _storageBrokkerService.CreateDirectory(directoryPath);

    }

    public void DeleteFile(string filePath)
    {
        _storageBrokkerService.DeleteFile(filePath);
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {

        var filesAndDirectories = _storageBrokkerService.GetAllFilesAndDirectories(directoryPath);
        return filesAndDirectories;
    }

    public void UploadFile(string filePath, Stream stream)
    {
        var parentPath = Directory.GetParent(filePath);

        _storageBrokkerService.UploadFile(filePath, stream);
    }
}
 