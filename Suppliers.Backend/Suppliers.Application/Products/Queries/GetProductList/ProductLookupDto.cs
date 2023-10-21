using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;

namespace Suppliers.Application.Products.Queries.GetProductList
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>()
                .ForMember(productDto => productDto.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(productDto => productDto.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(productDto => productDto.Price,
                    opt => opt.MapFrom(product => product.Price))
                .ForMember(productDto => productDto.Quantity,
                    opt => opt.MapFrom(product => product.Quantity))
                .ForMember(productDto => productDto.ExpirationDate,
                    opt => opt.MapFrom(product => product.ExpirationDate));
        }
    }
}
