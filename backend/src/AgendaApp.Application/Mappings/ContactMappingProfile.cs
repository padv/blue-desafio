using AutoMapper;
using AgendaApp.Application.DTOs;
using AgendaApp.Domain.Entities;


namespace AgendaApp.Application.Mappings
{
    public class ContactMappingProfile : Profile
        {
        public ContactMappingProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
        }
    }
}