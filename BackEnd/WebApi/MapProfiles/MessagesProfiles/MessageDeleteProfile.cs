using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class MessageDeleteProfile : Profile
{
    public MessageDeleteProfile()
    {
        CreateMap<Message, MessageDeleteViewModel>().ReverseMap();
    }
}