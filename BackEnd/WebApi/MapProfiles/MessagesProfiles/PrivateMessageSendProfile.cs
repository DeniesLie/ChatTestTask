using AutoMapper;
using Core.Entities;
using Core.VIewModels.Messages;

namespace WebApi.MapProfiles.MessagesProfiles;

public class PrivateMessageSendProfile : Profile
{
    public PrivateMessageSendProfile()
    {
        CreateMap<PrivateMessageSendViewModel, Message>()
            .ForMember(m => m.SentAt, opt =>
                opt.MapFrom(mVm => DateTimeOffset.UtcNow));
    }
}