using Domain.Interfaces;
using Domain.Interfaces.Generics;
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
    }
}
