using System.Security.Claims;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Services;
using Core.VIewModels.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public MessagesController(
            IMessageService messageService, 
            IUserService userService,
            IMapper mapper)
        {
            _messageService = messageService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{chatRoomId:int:min(1)}/{page:int:min(1)}", Name = "GetMessages")]
        public async Task<IEnumerable<MessageGetViewModel>> GetMessagesInChatroomAsync(
            int chatroomId, int page)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var messages = await _messageService.GetAsync(chatroomId, userId, page);
            
            return _mapper.Map<IEnumerable<MessageGetViewModel>>(messages);
        }

        [HttpPost("sendPrivate")]
        public async Task<ActionResult<MessageGetViewModel>> SendPrivateMessageAsync(
            [FromBody] PrivateMessageSendViewModel messageModel)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var message = _mapper.Map<Message>(messageModel);
            message.SenderId = userId;
            
            await _messageService.CreatePrivateMessageAsync(message, messageModel.ReceiverId);
        
            var messageGetVm = _mapper.Map<MessageGetViewModel>(message);

            return CreatedAtRoute("GetMessages",
                new {chatRoomId = message.ChatroomId, page = 1}, 
                messageGetVm);
        }

        [HttpPost("sendToChatroom")]
        public async Task<ActionResult<MessageGetViewModel>> SendToChatroomAsync(
            [FromBody] MessageToChatroomSendViewModel messageModel)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var message = _mapper.Map<Message>(messageModel);
            message.SenderId = userId;
            
            await _messageService.CreateMessageToChatroomAsync(message);
            
            var messageGetVm = _mapper.Map<MessageGetViewModel>(message);

            return CreatedAtRoute("GetMessages",
                new {chatRoomId = message.ChatroomId, page = 1}, 
                messageGetVm);
        }

        [HttpPut()]
        public async Task<MessageGetViewModel> EditAsync(
            [FromBody] MessageUpdateViewModel editMessageModel)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var message = await _messageService.EditAsync(editMessageModel.Id, editMessageModel.Text, userId);

            return _mapper.Map<MessageGetViewModel>(message);
        }

        [HttpDelete("deleteForSelf/{messageId:int:min(1)}")]
        public async Task<IActionResult> DeleteForSenderOnlyAsync(int messageId)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            await _messageService.DeleteForSenderOnlyAsync(messageId, userId);

            return NoContent();
        }
        
        [HttpDelete("deleteForEveryone/{messageId:int:min(1)}")]
        public async Task<IActionResult> DeleteForEveryoneInGroupAsync(int messageId)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            await _messageService.DeleteForEveryoneAsync(messageId, userId);

            return NoContent();
        }
    }
}
