using AutoMapper;
using Invoice.Domain.DTO;
using Invoice.Domain.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Invoice.Services.API
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<InvoiceDTO, InvoiceInfo>();
            CreateMap<InvoiceDetailsDTO, InvoiceDetails>()
                .ForMember(dest => dest.Description , opt => opt.MapFrom(src => src.Desc));
        }
    }
}
