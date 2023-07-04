using AutoMapper;
using Test.Data.Entities;
using Test.Dtos;

namespace Test.Profiles
{
    public class InvoicesProfile : Profile
    {
        public InvoicesProfile()
        {
            //CreateMap<InvoiceCreateDto, Invoice>();
            CreateMap<Invoice, InvoiceReadDto>();
            CreateMap<InvoiceUpdateDto, Invoice>();
        }
    }
}
