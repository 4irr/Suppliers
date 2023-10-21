using AutoMapper;
using Suppliers.Application.Common.Mappings;
using Suppliers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suppliers.Application.Products.Queries.GetProductDetails
{
    public class ProductDetailsVm : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailsVm>()
                .ForMember(productVm => productVm.Id,
                    opt => opt.MapFrom(product => product.Id))
                .ForMember(productVm => productVm.Name,
                    opt => opt.MapFrom(product => product.Name))
                .ForMember(productVm => productVm.Price,
                    opt => opt.MapFrom(product => product.Price))
                .ForMember(productVm => productVm.Quantity,
                    opt => opt.MapFrom(product => product.Quantity))
                .ForMember(productVm => productVm.ExpirationDate,
                    opt => opt.MapFrom(product => product.ExpirationDate));
        }
    }
}
