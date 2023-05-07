using Microsoft.AspNetCore.Http;

namespace Core.Application.Abstractions.Storage;

public interface IStorage
{
    Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles);
    Task DeleteAsync(string pathOrContainerName, string fileName);
    List<string> GetFiles(string pathOrContainerName);
    bool HasFile(string pathOrContainerName, string fileName);
}
