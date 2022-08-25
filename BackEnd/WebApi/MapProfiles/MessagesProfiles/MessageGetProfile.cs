using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class MessageGetProfile : Profile
{
    public MessageGetProfile()
    {
        CreateMap<Message, MessageGetViewModel>()
            .ForMember(m => m.SenderName, opt =>
                opt.MapFrom(m => m.UserChatroom.User.Username ?? ""));
    }
}