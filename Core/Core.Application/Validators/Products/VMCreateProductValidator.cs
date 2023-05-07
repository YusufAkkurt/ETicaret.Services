using Core.Application.ViewModels.Products;
using FluentValidation;

namespace Core.Application.Validators.Products;

public class VMCreateProductValidator : AbstractValidator<VMCreateProduct>
{
    public VMCreateProductValidator()
    {
        RuleFor(prop => prop.Name)
            .NotNull().NotEmpty().WithMessage("Ürün adını boş geçmeyin.")
            .MaximumLength(150).MinimumLength(3).WithMessage("Ürün adı 3 ile 150 karakter arasında olmalı.");

        RuleFor(prop => prop.Stock)
            .NotNull().WithMessage("Stok adedini boş geçmeyin.")
            .Must(stock => stock >= 0).WithMessage("Stok bilgisi negatif değer olamaz.");

        RuleFor(prop => prop.Price)
            .NotNull().WithMessage("Fiyat bilgisini boş geçmeyin.")
            .Must(stock => stock >= 0).WithMessage("Fiyat bilgisi negatif değer olamaz.");
    }
}
