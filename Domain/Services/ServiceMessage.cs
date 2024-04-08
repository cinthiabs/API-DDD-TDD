using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage: IServiceMessage
    {
        private readonly IMessage _message;

        public ServiceMessage(IMessage message)
        {
            _message = message;
        }
        public async Task Add(Message Obj)
        {
            var validTitle = Obj.ValidaPropriedadeString(Obj.Titulo, "Titulo");
            if (validTitle)
            {
                Obj.DataCadastro = DateTime.Now;
                Obj.DataAlteracao = DateTime.Now;
                Obj.Ativo = true;
                await _message.Add(Obj);
            }
        }
        public async Task Update(Message Obj)
        {
            var validTitle = Obj.ValidaPropriedadeString(Obj.Titulo, "Titulo");
            if (validTitle)
            {
                Obj.DataAlteracao = DateTime.Now;
                Obj.Ativo = true;
                await _message.Update(Obj);
            }
        }
        public async Task<List<Message>> ListMessageActive()
        {
            return await _message.ListMessage(n => n.Ativo);
        }
    }
}
