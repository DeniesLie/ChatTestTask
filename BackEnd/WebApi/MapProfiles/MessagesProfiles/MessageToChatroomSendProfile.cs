using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class MessageToChatroomSendProfile : Profile
{
    public MessageToChatroomSendProfile()
    {
        CreateMap<MessageToChatroomSendViewModel, Message>()
            .ForMember(m => m.SentAt, opt =>
                opt.MapFrom(mVm => DateTimeOffset.UtcNow));
    }
}