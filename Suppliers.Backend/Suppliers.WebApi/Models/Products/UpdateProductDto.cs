using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Products.Commands.UpdateProduct;

namespace Suppliers.WebApi.Models.Products
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
                .ForMember(productCommand => productCommand.Id,
                    opt => opt.MapFrom(productDto => productDto.Id))
                .ForMember(productCommand => productCommand.Name,
                    opt => opt.MapFrom(productDto => productDto.Name))
                .ForMember(productCommand => productCommand.Price,
                    opt => opt.MapFrom(productDto => productDto.Price))
                .ForMember(productCommand => productCommand.Quantity,
                    opt => opt.MapFrom(productDto => productDto.Quantity))
                .ForMember(productCommand => productCommand.ExpirationDate,
                    opt => opt.MapFrom(productDto => productDto.ExpirationDate));
        }
    }
}
