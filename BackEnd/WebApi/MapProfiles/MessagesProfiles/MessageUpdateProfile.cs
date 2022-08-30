using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class MessageUpdateProfile : Profile
{
    public MessageUpdateProfile()
    {
        CreateMap<MessageUpdateViewModel, Message>().ReverseMap();
    }
}