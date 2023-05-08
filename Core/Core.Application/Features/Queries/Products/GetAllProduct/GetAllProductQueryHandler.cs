using Core.Application.Repositories.Products;
using Core.Application.RequestParameters;

namespace Core.Application.Features.Queries.Products.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = _productReadRepository.GetAll(false).Count();

        var products = _productReadRepository.GetAll(false).OrderBy(_product => _product.CreatedDate).Select(_product => new
        {
            _product.Id,
            _product.Name,
            _product.Stock,
            _product.Price,
            _product.CreatedDate,
            _product.UpdatedDate
        }).Skip(request.Page * request.Size).Take(request.Size);

        return new(totalCount, products);
    }
}
