﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFileManagement.StorageBrokker.Services;

public class DropBoxStorageService : IStorageBrokkerService
{
    public void CreateDirectory(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public void UploadFile(string filePath, Stream stream)
    {
        throw new NotImplementedException();
    }
}
