using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFile.StorageBrokker.Services;

public interface IStorageBrokkerService
{
    void UploadFile(string filePath, Stream stream);
    void CreateDirectory(string directoryPath);
    List<string> GetAllFilesAndDirectories(string directoryPath);
    void DeleteFile(string filePath);
}
