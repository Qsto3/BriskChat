﻿using System;
using TrollChat.BusinessLogic.Actions.Base;
using TrollChat.BusinessLogic.Models;

namespace TrollChat.BusinessLogic.Actions.Message.Interfaces
{
    public interface IAddNewMessage : IAction
    {
        Guid Invoke(MessageModel message);
    }
}