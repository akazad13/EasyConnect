using AutoMapper;
using EasyConnect.API.Models;
using EasyConnect.API.DTOs;
using System.Linq;

namespace EasyConnect.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
                );
            CreateMap<User, UserForDetailedDTO>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge())
                );
            CreateMap<Photo, PhotosForDetailedDTO>();
            CreateMap<UserForUpdateDTO, User>();
            CreateMap<PhotoForCreationDTO, Photo>();
            CreateMap<Photo, PhotoForReturnDTO>(); // from, to
            CreateMap<User, UserForNavbarDTO>()
                .ForMember(
                    dest => dest.PhotoUrl,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url)
                );
            CreateMap<UserForRegisterDTO, User>();
            CreateMap<MessageForCreationDTO, Message>();
            CreateMap<Message, MessageToReturnDTO>()
                .ForMember(
                    m => m.SenderPhotoUrl,
                    opt => opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url)
                )
                .ForMember(
                    m => m.RecipientPhotoUrl,
                    opt =>
                        opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url)
                );
        }
    }
}
