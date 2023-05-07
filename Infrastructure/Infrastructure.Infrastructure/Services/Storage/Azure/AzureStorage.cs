using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Infrastructure.Services.Storage.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new(configuration["Storage:Azure"]);
    }

    public async Task DeleteAsync(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient= _blobContainerClient.GetBlobClient(fileName);
        
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        
        return _blobContainerClient.GetBlobs().Select(blob => blob.Name).ToList();
    }

    public bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        return _blobContainerClient.GetBlobs().Any(blob => blob.Name.Equals(fileName));
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection formFiles)
    {
        _blobContainerClient =  _blobServiceClient.GetBlobContainerClient(containerName);
        
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        var results = new List<(string fileName, string pathOrContainerName)>();

        foreach (var file in formFiles)
        {
            var replacedFileName = await base.FileRenameAsync(containerName, file.Name, this.HasFile);

            var blobClient = _blobContainerClient.GetBlobClient(replacedFileName);
            await blobClient.UploadAsync(file.OpenReadStream());

            results.Add((file.Name, $"{containerName}/{replacedFileName}"));
        }

        return results;
    }
}
