﻿using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMessage
    {
        Task Add(Message Obj);
        Task Update(Message Obj);
        Task<List<Message>> ListMessageActive();
    }
}
