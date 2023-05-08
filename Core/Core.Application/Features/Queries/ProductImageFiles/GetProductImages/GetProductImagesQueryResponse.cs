namespace Core.Application.Features.Queries.ProductImageFiles.GetProductImages;

public class GetProductImagesQueryResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }

    public GetProductImagesQueryResponse(Guid id, string fileName, string path)
    {
        Id = id;
        FileName = fileName;
        Path = path;
    }
}