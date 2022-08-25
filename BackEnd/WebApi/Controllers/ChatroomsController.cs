using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.Services;
using Core.VIewModels.Chatrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/chatrooms")]
    [ApiController]
    [Authorize]
    public class ChatroomsController : ControllerBase
    {
        private readonly IChatroomService _chatroomService;
        private readonly IMapper _mapper;

        public ChatroomsController(IChatroomService chatroomService, IMapper mapper)
        {
            _chatroomService = chatroomService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ChatroomGetViewModel>> GetAsync()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var chatrooms = await _chatroomService.GetByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<ChatroomGetViewModel>>(chatrooms);
        }
    }
}
