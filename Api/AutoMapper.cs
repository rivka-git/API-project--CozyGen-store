using AutoMapper;
using Dto;
using Model;
namespace Api
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, DtoProductIdNameCategoryPriceDescImage>();
            CreateMap<User, DtoUserEmailPassword>().ReverseMap();
            CreateMap<Style, DtoStyleIdName>().ReverseMap();
            CreateMap<User, DtoUserAll>().ReverseMap();
            CreateMap<User, DtoUserNameEmailRoleId>().ReverseMap();
            CreateMap<Category, DtoCategoryNameId>().ReverseMap();
            CreateMap<Category, DtoCategoryAll>().ReverseMap();

            CreateMap<Style, DtoStyleAll>().ReverseMap();
            CreateMap<DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds, Product>();
            CreateMap<Product, DtoProductIdNameCategoryPriceDescImage>()
                           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Order, DtoOrderIdUserIdDateSumOrderItems>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).ReverseMap();
            CreateMap<OrderItem, DtoOrderItemIdOrderIdProductIdQuantity>()
                 .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<DtoOrderItemIdOrderIdProductIdQuantity, OrderItem>()
                 .ForMember(dest => dest.Product, opt => opt.Ignore())
                 .ForMember(dest => dest.Order, opt => opt.Ignore());
        }
    }
}
