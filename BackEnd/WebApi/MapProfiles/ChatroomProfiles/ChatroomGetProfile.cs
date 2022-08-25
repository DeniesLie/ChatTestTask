using AutoMapper;
using Core.Entities;
using Core.VIewModels.Chatrooms;

namespace WebApi.MapProfiles.ChatroomProfiles;

public class ChatroomGetProfile : Profile
{
    public ChatroomGetProfile()
    {
        CreateMap<Chatroom, ChatroomGetViewModel>()
            .ForMember(vm => vm.ChatType,
                opt =>
                    opt.MapFrom(cr => cr.Type));
    }
}