using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessage _message;

        public MessageController(IMapper mapper, IMessage message)
        {
            _message = message;
            _mapper = mapper;   
        }

        [Authorize]
        [HttpPost("/api/Add")]
        public async Task<List<Notifies>> Add(MessageViewModel message)
        {
            message.UserId = await ReturnIdUserLog();
            var messageMap = _mapper.Map<Message>(message);
            await _message.Add(messageMap);
            return messageMap.Notitycoes;
        }
        [Authorize]
        [HttpPost("/api/Update")]
        public async Task<List<Notifies>> Update(MessageViewModel message)
        {
            var messageMap = _mapper.Map<Message>(message);
            await _message.Update(messageMap);
            return messageMap.Notitycoes;
        }
        [Authorize]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifies>> Delete(MessageViewModel message)
        {
            var messageMap = _mapper.Map<Message>(message);
            await _message.Delete(messageMap);
            return messageMap.Notitycoes;
        }

        [Authorize]
        [HttpPost("/api/GetEntityById")]
        public async Task<MessageViewModel> GetEntityById(Message message)
        {
            message = await _message.GetEntityById(message.Id);
            var messageMap = _mapper.Map<MessageViewModel>(message);
            return messageMap;
        }
        [Authorize]
        [HttpPost("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var mensagens = await _message.List();
            var messageMap = _mapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }

        private async Task<string> ReturnIdUserLog()
        {
            if(User !=null)
            {
                var idUser = User.FindFirst("IdUser");
                return idUser.Value;
            }
            return string.Empty;
        }
    }
}
