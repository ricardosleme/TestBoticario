using AutoMapper;
using Sales.Seller.CashBack.API.Entities;
using Sales.Seller.CashBack.API.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Seller.CashBack.API.Mapper
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SellerRequest, Vendor>()
               .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
               .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<SalePostRequest, Sale>()
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
               .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.DataCompra))
               .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));
            CreateMap<Sale, SaleListReponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id.ToString()))
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
               .ForMember(dest => dest.DataCompra, opt => opt.MapFrom(src => src.Data))
               .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
               .ForMember(dest => dest.CashBackPer, opt => opt.MapFrom(src => src.CashBackPer))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.CashBackValor, opt => opt.MapFrom(src => src.CashBackValor));

        }
    }
}
