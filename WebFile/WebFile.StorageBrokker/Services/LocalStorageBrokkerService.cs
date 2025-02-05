using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFile.StorageBrokker.Services;

public class LocalStorageBrokkerService : IStorageBrokkerService
{
    private string _dataPath;
    public LocalStorageBrokkerService()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }

    }
    public void CreateDirectory(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        ValidateDirectoryPath(directoryPath);
        Directory.CreateDirectory(directoryPath);
    }

    public void DeleteFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);

        if (!File.Exists(filePath))
        {
            throw new Exception("File not fount to delete");
        }

        File.Delete(filePath);
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        var parentPath = Directory.GetParent(directoryPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
        var allFilesAndDirectory = Directory.GetFileSystemEntries(directoryPath).ToList();
        allFilesAndDirectory = allFilesAndDirectory.Select(p => p.Remove(0, directoryPath.Length + 1)).ToList();
        return allFilesAndDirectory;
    }

    public void UploadFile(string filePath, Stream stream)
    {
        filePath = Path.Combine(_dataPath, filePath);
        var parentPath = Directory.GetParent(filePath);
        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream);
        }
    }  
    private void ValidateDirectoryPath(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            throw new Exception("Directory already exists");
        }
        var parentPath = Directory.GetParent(directoryPath);

        if (!Directory.Exists(parentPath.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
    }
}
  
