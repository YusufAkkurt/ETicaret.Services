using Core.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Infrastructure.Services.Storage.Local;

public class LocalStorage : Storage, ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private async Task<bool> CopyFileAsync(string fullPath, IFormFile file)
    {
        try
        {
            await using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            return true;
        }
        catch (Exception exception)
        {
            // todo log!
            throw exception;
        }
    }

    public async Task DeleteAsync(string path, string fileName) => await Task.Run(() => File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path, fileName)));

    public List<string> GetFiles(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        return directoryInfo.GetFiles().Select(_file => _file.DirectoryName).ToList();
    }

    public bool HasFile(string path, string fileName) => File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, path, fileName));

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection formFiles)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (Directory.Exists(uploadPath) is false)
            Directory.CreateDirectory(uploadPath);

        var results = new List<(string fileName, string path)>();

        foreach (var file in formFiles)
        {
            var replacedFileName = await base.FileRenameAsync(path, file.FileName, this.HasFile);

            await this.CopyFileAsync(Path.Combine(uploadPath, replacedFileName), file);

            results.Add(new(replacedFileName, Path.Combine(path, replacedFileName)));
        }

        return results;
    }
}
