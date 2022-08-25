using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class RepliedMessageGetProfile : Profile
{
    public RepliedMessageGetProfile()
    {
        CreateMap<Message, RepliedMessageGetViewModel>()
            .ForMember(m => m.SenderName, opt =>
                opt.MapFrom(m => m.UserChatroom.User.Username ?? ""));
    }
}